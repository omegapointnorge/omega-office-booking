using server.Models.Domain;
using server.Models.DTOs.Internal;
using server.Models.DTOs.Request;
using server.Services.Internal;

public class BookingServiceValidationTests
{
    private CreateBookingRequest GetBookingRequest()
    {
        return new CreateBookingRequest { BookingDateTime = DateTime.Now, SeatId = 1 };
    }

    private Booking GetBooking()
    {
        return new Booking { BookingDateTime = DateTime.Now, UserId = "testUser" };
    }

    [Fact]
    public void ValidateBookingRequest_ValidBooking_ReturnsNull()
    {
        // Arrange
        var bookingRequest = GetBookingRequest();
        var bookingList = new List<Booking>();
        UserClaims userClaims = new UserClaims("name", "testUser", "noRole");

        // Act
        var result = BookingService.ValidateUserBookingRequest(bookingRequest, bookingList, userClaims);

        // Assert
        Assert.True(result.Count() == 0);
    }

    [Fact]
    public void ValidateBookingRequest_DateExceedsAllowedBookingDate_ReturnsErrorMessage()
    {
        // Arrange
        var bookingRequest = GetBookingRequest();
        bookingRequest.BookingDateTime = DateTime.Now.AddDays(10);
        var bookingList = new List<Booking>();
        UserClaims userClaims = new UserClaims("name", "testUser", "noRole");

        // Act
        var result = BookingService.ValidateUserBookingRequest(bookingRequest, bookingList, userClaims);

        // Assert
        Assert.Equal("Booking date exceeds the latest allowed booking date.", result.First());
    }

    [Fact]
    public void ValidateBookingRequest_SeatAlreadyBooked_ReturnsErrorMessage()
    {
        // Arrange
        var bookingRequest = GetBookingRequest();
        var bookedSeatId = 1;
        var bookingList = new List<Booking> { new Booking { BookingDateTime = DateTime.Now, SeatId = bookedSeatId } };
        UserClaims userClaims = new UserClaims("name", "testUser", "noRole");

        // Act
        var result = BookingService.ValidateUserBookingRequest(bookingRequest, bookingList, userClaims);

        // Assert
        Assert.Equal($"Seat {bookedSeatId} is already booked for the specified time.", result.First());
    }

    [Fact]
    public void ValidateBookingRequest_UserAlreadyBookedForDay_ReturnsErrorMessage()
    {
        // Arrange
        UserClaims userClaims = new UserClaims("name", "testUser", "noRole");
        var bookingRequest = GetBookingRequest();
        var bookingList = new List<Booking> { new Booking { BookingDateTime = DateTime.Now, UserId = userClaims.Objectidentifier } };

        // Act
        var result = BookingService.ValidateUserBookingRequest(bookingRequest, bookingList, userClaims);

        // Assert
        Assert.Equal("User has already booked for the specified day.", result.First());
    }

    [Fact]
    public void ValidateBookingRequest_InvalidTimeAndSeatAlreadyBooked_ReturnsErrorMessages()
    {
        // Arrange
        var bookingRequest = GetBookingRequest();
        bookingRequest.BookingDateTime = DateTime.Now.AddDays(2); // Choose a date outside the allowed booking window

        var bookingList = new List<Booking> { new Booking { BookingDateTime = bookingRequest.BookingDateTime, SeatId = bookingRequest.SeatId } };
        UserClaims userClaims = new UserClaims("name", "testUser", "noRole");

        // Act
        var result = BookingService.ValidateUserBookingRequest(bookingRequest, bookingList, userClaims);

        // Assert
        Assert.Contains("Booking date exceeds the latest allowed booking date.", result);
        Assert.Contains($"Seat {bookingRequest.SeatId} is already booked for the specified time.", result);
    }

    [Fact]
    public void ValidateUserDeleteBookingRequest_ValidBooking_ReturnsEmptyList()
    {
        // Arrange
        var booking = GetBooking();
        var userClaims = new UserClaims("name", booking.UserId, "noRole");

        // Act
        var result = BookingService.ValidateUserDeleteBookingRequest(booking, userClaims);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void ValidateUserDeleteBookingRequest_UserNotAuthorized_ReturnsErrorMessage()
    {
        // Arrange
        var booking = GetBooking();
        var userClaims = new UserClaims("name", "someOtherUserId", "noRole");

        // Act
        var result = BookingService.ValidateUserDeleteBookingRequest(booking, userClaims);

        // Assert
        Assert.Single(result);
        Assert.Contains("User is not authorized to delete the booking.", result[0]);
    }

    [Fact]
    public void ValidateUserDeleteBookingRequest_UserIsEventAdmin_ReturnsEmptyList()
    {
        // Arrange
        var booking = GetBooking();
        var userClaims = new UserClaims("name", "userId123", "EventAdmin");

        // Act
        var result = BookingService.ValidateUserDeleteBookingRequest(booking, userClaims);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void ValidateUserDeleteBookingRequest_BookingNotFound_ReturnsErrorMessage()
    {
        // Arrange
        Booking booking = null; // Simulating a scenario where booking is not found
        UserClaims userClaims = new UserClaims("name", "testUser", "noRole");

        // Act
        var result = BookingService.ValidateUserDeleteBookingRequest(booking, userClaims);

        // Assert
        Assert.Single(result);
        Assert.Contains("Booking not found", result);
    }

}