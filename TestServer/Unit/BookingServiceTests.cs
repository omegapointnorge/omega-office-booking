using Moq;
using server.Helpers;
using server.Models.Domain;
using server.Models.DTOs.Internal;
using server.Models.DTOs.Request;
using server.Repository;
using TestServer.Unit;

namespace server.Services.Internal.Tests
{
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

        [Fact]
        public async Task CreateBookingAsync_ValidBooking_ReturnsBookingDto()
        {
            // Arrange
            var bookingRequest = new CreateBookingRequest { BookingDateTime = DateTime.Now, SeatId = 1 };
            var userClaims = GetUserClaims();

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


        [Fact]
        public async Task CreateBookingAsync_SeatAlreadyBooked_ThrowsException()
        {
            // Arrange
            var bookingRequest = new CreateBookingRequest { BookingDateTime = DateTime.Now, SeatId = 1 };
            var userClaims = GetUserClaims();
            _bookingRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Booking> { new Booking(userClaims.Objectidentifier, userClaims.UserName, bookingRequest.SeatId, bookingRequest.BookingDateTime) });

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => Sut.CreateBookingAsync(bookingRequest, userClaims));
        }

        [Fact]
        public async Task CreateBookingAsync_UserAlreadyBookedForDay_ThrowsException()
        {
            // Arrange
            var bookingRequest = new CreateBookingRequest { BookingDateTime = DateTime.Now, SeatId = 1 };
            var userClaims = GetUserClaims();
            _bookingRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Booking> { new Booking(userClaims.Objectidentifier, userClaims.UserName, bookingRequest.SeatId, bookingRequest.BookingDateTime) });

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => Sut.CreateBookingAsync(bookingRequest, userClaims));
        }


        [Fact]
        public async Task CreateBookingAsync_AdminAlreadyBookedForDay_ReturnsBookingDto()
        {
            // Arrange
            var bookingRequest = new CreateBookingRequest { BookingDateTime = DateTime.Now, SeatId = 1 };
            var existingBookingRequest = new CreateBookingRequest { BookingDateTime = DateTime.Now, SeatId = 2 };
            var adminClaims = GetEventAdminClaims();
            _bookingRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Booking> {  new Booking(adminClaims.Objectidentifier, adminClaims.UserName, existingBookingRequest.SeatId, existingBookingRequest.BookingDateTime) });

            // Act
            var result = await Sut.CreateBookingAsync(bookingRequest, adminClaims);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(adminClaims.Objectidentifier, result.UserId);
            Assert.Equal(adminClaims.UserName, result.UserName);
            Assert.Equal(bookingRequest.SeatId, result.SeatId);
            Assert.Equal(bookingRequest.BookingDateTime.ToUniversalTime().ToString("o"), result.BookingDateTime);
        }

        [Fact]
        public async Task CreateBookingAsync_AttemptToBookBefore15_ThrowsException()
        {
            // Arrange
            var bookingDateTime = DateTime.Today.AddDays(1).AddHours(14); // Next day after 14:00
            var bookingRequest = new CreateBookingRequest { BookingDateTime = bookingDateTime, SeatId = 2 };
            var userClaims = GetUserClaims();

            _bookingRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Booking>());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => Sut.CreateBookingAsync(bookingRequest, userClaims));
        }

        [Fact]
        public async Task CreateBookingAsync_AdminBookingBefore15_ReturnsBookingDto()
        {
            // Arrange
            var bookingDateTime = DateTime.Today.AddDays(1).AddHours(14); // Next day after 14:00
            var bookingRequest = new CreateBookingRequest { BookingDateTime = DateTime.Now, SeatId = 2 };
            var userClaims = GetEventAdminClaims();

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
    }
}
