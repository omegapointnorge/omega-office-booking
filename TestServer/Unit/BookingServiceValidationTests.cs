using server.Models.Domain;
using server.Models.DTOs.Request;
using server.Services.Internal;

public class BookingServiceValidationTests
{
    private CreateBookingRequest GetBookingRequest()
    {
        return new CreateBookingRequest { BookingDateTime = DateTime.Now, SeatId = 1 };
    }

    [Fact]
    public void ValidateBookingRequest_ValidBooking_ReturnsNull()
    {
        // Arrange
        var bookingRequest = GetBookingRequest();
        var bookingList = new List<Booking>();
        var userId = "testUser";

        // Act
        var result = BookingService.ValidateUserBookingRequest(bookingRequest, bookingList, userId);

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
        var userId = "testUser";

        // Act
        var result = BookingService.ValidateUserBookingRequest(bookingRequest, bookingList, userId);

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
        var userId = "testUser";

        // Act
        var result = BookingService.ValidateUserBookingRequest(bookingRequest, bookingList, userId);

        // Assert
        Assert.Equal($"Seat {bookedSeatId} is already booked for the specified time.", result.First());
    }

    [Fact]
    public void ValidateBookingRequest_InvalidTimeAndSeatAlreadyBooked_ReturnsErrorMessages()
    {
        // Arrange
        var bookingRequest = GetBookingRequest();
        bookingRequest.BookingDateTime = DateTime.Now.AddDays(2); // Choose a date outside the allowed booking window

        var bookingList = new List<Booking> { new Booking { BookingDateTime = bookingRequest.BookingDateTime, SeatId = bookingRequest.SeatId } };
        var userId = "testUser";

        // Act
        var result = BookingService.ValidateUserBookingRequest(bookingRequest, bookingList, userId);

        // Assert
        Assert.Contains("Booking date exceeds the latest allowed booking date.", result);
        Assert.Contains($"Seat {bookingRequest.SeatId} is already booked for the specified time.", result);
    }
}