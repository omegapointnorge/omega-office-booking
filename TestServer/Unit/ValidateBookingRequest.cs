using server.Models.Domain;
using server.Models.DTOs.Request;
using server.Services;

public class BookingServiceTests
{
    private CreateBookingRequest GetValidBookingRequest() =>
        new CreateBookingRequest { SeatId = 1, BookingDateTime = DateTime.Now };

    private List<Booking> GetEmptyBookingList() => new List<Booking>();

    [Fact]
    public void ValidateBookingRequest_ValidBooking_ReturnsNull()
    {
        var existingBookings = GetEmptyBookingList();
        var bookingRequest = GetValidBookingRequest();
        var userId = "testUser";

        var result = BookingService.ValidateBookingRequest(bookingRequest, existingBookings, userId);

        Assert.Null(result);
    }

    [Fact]
    public void ValidateBookingRequest_DateExceedsAllowedBookingDate_ReturnsErrorMessage()
    {
        var bookingRequest = new CreateBookingRequest { BookingDateTime = DateTime.Now.AddDays(2), SeatId = 1 };
        var userId = "testUser";

        var result = BookingService.ValidateBookingRequest(bookingRequest, GetEmptyBookingList(), userId);

        Assert.Equal("Booking date exceeds the latest allowed booking date.", result);
    }

    [Fact]
    public void ValidateBookingRequest_SeatAlreadyBooked_ReturnsErrorMessage()
    {
        var bookingRequest = GetValidBookingRequest();
        var bookingList = new List<Booking> { new Booking { BookingDateTime = DateTime.Now, SeatId = 1 } };
        var userId = "testUser";

        var result = BookingService.ValidateBookingRequest(bookingRequest, bookingList, userId);

        Assert.Equal("Seat is already booked for the specified time.", result);
    }

    [Fact]
    public void ValidateBookingRequest_UserAlreadyBookedForDay_ReturnsErrorMessage()
    {
        var bookingRequest = GetValidBookingRequest();
        var bookingList = new List<Booking> { new Booking { BookingDateTime = DateTime.Now, UserId = "testUser" } };
        var userId = "testUser";

        var result = BookingService.ValidateBookingRequest(bookingRequest, bookingList, userId);

        Assert.Equal("User has already booked for the specified day.", result);
    }
}
