using Moq;
using server.Helpers;
using server.Models.Domain;
using server.Models.DTOs.Internal;
using server.Models.DTOs.Request;
using server.Repository;
using TestServer.Unit;

namespace server.Services.Internal.Tests;

public class BookingServiceTests : TestServiceBase<BookingService>
{
    private readonly Mock<IBookingRepository> _bookingRepositoryMock;

    static BookingServiceTests()
    {
        BookingTimeUtils.SetOpeningTime(new TimeOnly(15, 00));
    }

    public BookingServiceTests()
    {
        _bookingRepositoryMock = Mocker.GetMock<IBookingRepository>();
    }
    private UserClaims GetUserClaims()
    {
        return new UserClaims("name", "testUser", "noRole");
    }

    private UserClaims GetEventAdminClaims()
    {
        return new UserClaims("name", "testUser", "EventAdmin");
    }

    [Theory]
    [InlineData("2024-02-13T15:00:00", "2024-02-14")] // Tuesday, Wednesday
    [InlineData("2024-02-16T15:00:00", "2024-02-19")] // Friday, Monday
    public async Task CreateBookingAsync_ValidBooking_ReturnsBookingDto(string testDateTimeString, string bookingDateTimeString)
    {
        // Arrange
        var testDateTime = DateTime.Parse(testDateTimeString);
        var bookingDateTime = DateTime.Parse(bookingDateTimeString);
        var bookingRequest = new CreateBookingRequest { BookingDateTime = bookingDateTime, SeatId = 1 };
        var userClaims = GetUserClaims();

        var dateTimeProvider = new TestDateTimeProvider(testDateTime);
        BookingTimeUtils.SetDateTimeProvider(dateTimeProvider);

        _bookingRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Booking>());

        // Act
        var result = await Sut.CreateBookingAsync(bookingRequest, userClaims);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userClaims.Objectidentifier, result.UserId);
        Assert.Equal(userClaims.UserName, result.UserName);
        Assert.Equal(bookingRequest.SeatId, result.SeatId);
        Assert.Equal(bookingRequest.BookingDateTime.ToUniversalTime().ToString("o"), result.BookingDateTime);
    }

    [Theory]
    [InlineData("2024-02-13T15:00", "2024-02-14")] // Tuesday, Wednesday
    [InlineData("2024-02-16T15:00", "2024-02-19")] // Friday, Monday
    public async Task CreateBookingAsync_SeatAlreadyBooked_ThrowsException(string testDateTimeString, string bookingDateTimeString)
    {
        // Arrange
        var testDateTime = DateTime.Parse(testDateTimeString);
        var bookingDateTime = DateTime.Parse(bookingDateTimeString);

        var dateTimeProvider = new TestDateTimeProvider(testDateTime);
        BookingTimeUtils.SetDateTimeProvider(dateTimeProvider);

        var bookingRequest = new CreateBookingRequest { BookingDateTime = bookingDateTime, SeatId = 1 };
        var userClaims = GetUserClaims();
        _bookingRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Booking> { new Booking(userClaims.Objectidentifier, userClaims.UserName, bookingRequest.SeatId, bookingRequest.BookingDateTime) });
        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => Sut.CreateBookingAsync(bookingRequest, userClaims));
    }

    [Theory]
    [InlineData("2024-02-13T15:00:00", "2024-02-14")] // Tuesday, Wednesday
    [InlineData("2024-02-16T15:00:00", "2024-02-19")] // Friday, Monday
    public async Task CreateBookingAsync_UserAlreadyBookedForDay_ThrowsException(string testDateTimeString, string bookingDateTimeString)
    {
        // Arrange
        var testDateTime = DateTime.Parse(testDateTimeString);
        var bookingDateTime = DateTime.Parse(bookingDateTimeString);

        var dateTimeProvider = new TestDateTimeProvider(testDateTime);
        BookingTimeUtils.SetDateTimeProvider(dateTimeProvider);

        var bookingRequest = new CreateBookingRequest { BookingDateTime = bookingDateTime, SeatId = 1 };
        var userClaims = GetUserClaims();
        _bookingRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Booking> { new Booking(userClaims.Objectidentifier, userClaims.UserName, bookingRequest.SeatId, bookingRequest.BookingDateTime) });

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => Sut.CreateBookingAsync(bookingRequest, userClaims));
    }

    [Theory]
    [InlineData("2024-02-13T12:00", "2024-02-14")] // Tuesday, Wednesday
    [InlineData("2024-02-16T12:00", "2024-02-19")] // Friday, Monday
    public async Task CreateBookingAsync_AttemptToBookBefore15_ThrowsException(string testDateTimeString, string bookingDateTimeString)
    {
        // Arrange
        var testDateTime = DateTime.Parse(testDateTimeString);
        var bookingDateTime = DateTime.Parse(bookingDateTimeString);

        var dateTimeProvider = new TestDateTimeProvider(testDateTime);
        BookingTimeUtils.SetDateTimeProvider(dateTimeProvider);

        var bookingRequest = new CreateBookingRequest { BookingDateTime = bookingDateTime, SeatId = 2 };
        var userClaims = GetUserClaims();


        _bookingRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Booking>());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => Sut.CreateBookingAsync(bookingRequest, userClaims));
    }

    [Theory]
    [InlineData("2024-02-13T15:00:00", "2024-02-14")] // Tuesday, Wednesday
    public async Task CreateBookingAsync_AdminAlreadyBookedForDay_ReturnsBookingDto(string testDateTimeString, string bookingDateTimeString)
    {
        // Arrange
        var testDateTime = DateTime.Parse(testDateTimeString);
        var bookingDateTime = DateTime.Parse(bookingDateTimeString);
        var bookingRequest = new CreateBookingRequest { BookingDateTime = bookingDateTime, SeatId = 1 };
        var existingBookingRequest = new CreateBookingRequest { BookingDateTime = DateTime.Now, SeatId = 2 }; // Using DateTime.Now as placeholder for existing booking time
        var adminClaims = GetEventAdminClaims();
        _bookingRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Booking> { new Booking(adminClaims.Objectidentifier, adminClaims.UserName, existingBookingRequest.SeatId, existingBookingRequest.BookingDateTime) });

        var dateTimeProvider = new TestDateTimeProvider(testDateTime);
        BookingTimeUtils.SetDateTimeProvider(dateTimeProvider);

        // Act
        var result = await Sut.CreateBookingAsync(bookingRequest, adminClaims);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(adminClaims.Objectidentifier, result.UserId);
        Assert.Equal(adminClaims.UserName, result.UserName);
        Assert.Equal(bookingRequest.SeatId, result.SeatId);
        Assert.Equal(bookingRequest.BookingDateTime.ToUniversalTime().ToString("o"), result.BookingDateTime);
    }

    [Theory]
    [InlineData("2024-02-13T12:00", "2024-02-14")] // Tuesday, Wednesday
    public async Task CreateBookingAsync_AdminBookingBefore15_ReturnsBookingDto(string testDateTimeString, string bookingDateTimeString)
    {
        // Arrange
        var testDateTime = DateTime.Parse(testDateTimeString);
        var bookingDateTime = DateTime.Parse(bookingDateTimeString);
        var bookingRequest = new CreateBookingRequest { BookingDateTime = bookingDateTime, SeatId = 2 };
        var userClaims = GetEventAdminClaims();

        var dateTimeProvider = new TestDateTimeProvider(testDateTime);
        BookingTimeUtils.SetDateTimeProvider(dateTimeProvider);

        _bookingRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Booking>());

        // Act
        var result = await Sut.CreateBookingAsync(bookingRequest, userClaims);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userClaims.Objectidentifier, result.UserId);
        Assert.Equal(userClaims.UserName, result.UserName);
        Assert.Equal(bookingRequest.SeatId, result.SeatId);
        Assert.Equal(bookingRequest.BookingDateTime.ToUniversalTime().ToString("o"), result.BookingDateTime);
    }

    [Theory]
    [InlineData("2024-02-13T12:00")]
    public async Task GetAllBookingsForUserAsync_NoEvent(string testDateTimeString)
    {
        // Arrange
        DateTime testDateTime = DateTime.Parse(testDateTimeString);
        var userClaims = GetUserClaims();
        int[] seatIds = { 1, 2 };
        var bookings = GenerateBookings(userClaims, testDateTime, seatIds);

        _bookingRepositoryMock.Setup(repo => repo.GetBookingsWithSeatForUserAsync(It.IsAny<string>())).ReturnsAsync(bookings);

        // Act
        var result = await Sut.GetAllBookingsForUserAsync(userClaims.Objectidentifier);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(seatIds.Length, result.Count());
    }

    [Theory]
    [InlineData("2024-02-13T12:00")]
    public async Task GetAllBookingsForUserAsync_SingleEvent(string testDateTimeString)
    {
        // Arrange
        DateTime testDateTime = DateTime.Parse(testDateTimeString);
        var userClaims = GetUserClaims();
        const int EventId = 1;
        int[] seatIds = { 1, 2 };
        var bookings = GenerateBookings(userClaims, testDateTime, seatIds, EventId);

        _bookingRepositoryMock.Setup(repo => repo.GetBookingsWithSeatForUserAsync(It.IsAny<string>())).ReturnsAsync(bookings);

        // Act
        var result = await Sut.GetAllBookingsForUserAsync(userClaims.Objectidentifier);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(EventId, result.First().Id);
    }

    [Theory]
    [InlineData("2024-02-13T12:00")]
    public async Task RetrieveAllBookingsForUserAsync_DualEvent(string testDateTimeString)
    {
        // Arrange
        DateTime testDateTime = DateTime.Parse(testDateTimeString);
        var userClaims = GetUserClaims();
        const int FirstEventId = 1;
        const int SecondEventId = 2;
        int[] firstEventSeatIds = { 1, 2 };
        int[] secondEventSeatIds = { 3, 4 };

        var bookings = GenerateBookings(userClaims, testDateTime, firstEventSeatIds, FirstEventId);
        bookings.AddRange(GenerateBookings(userClaims, testDateTime, secondEventSeatIds, SecondEventId));

        _bookingRepositoryMock.Setup(repo => repo.GetBookingsWithSeatForUserAsync(It.IsAny<string>())).ReturnsAsync(bookings);

        // Act
        var result = await Sut.GetAllBookingsForUserAsync(userClaims.Objectidentifier);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }


    private List<Booking> GenerateBookings(UserClaims userClaims, DateTime testDateTime, int[] seatIds, int? eventId = null)
    {
        const int RoomId = 1;
        var bookings = new List<Booking>();

        foreach (var seatId in seatIds)
        {
            var booking = new Booking(userClaims.Objectidentifier, userClaims.UserName, seatId, testDateTime, testDateTime.Date, eventId);
            booking.Seat = new Seat(seatId, RoomId, null);
            bookings.Add(booking);
        }

        return bookings;
    }





    public class TestDateTimeProvider : IDateTimeProvider
    {
        private readonly DateTime _currentDateTime;

        public TestDateTimeProvider(DateTime currentDateTime)
        {
            _currentDateTime = currentDateTime;
        }
        public DateTime GetCurrentDateTime() => _currentDateTime;
    }

}

