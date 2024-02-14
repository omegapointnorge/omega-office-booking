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

        static BookingServiceTests()
        {
            BookingTimeUtils.SetOpeningTime(new TimeOnly(15, 00));
        }

        private readonly Mock<IBookingRepository> _bookingRepositoryMock;

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


        private CreateBookingRequest GetBookingRequest()
        {
            return new CreateBookingRequest { BookingDateTime = DateTime.Now, SeatId = 1 };
        }

        [Fact]
        public async Task CreateBookingAsync_ValidBooking_ReturnsBookingDto()
        {
            // Arrange
            var bookingRequest = GetBookingRequest();
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
            var bookingRequest = GetBookingRequest();
            var userClaims = GetUserClaims();
            _bookingRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Booking> { new Booking { SeatId = bookingRequest.SeatId, BookingDateTime = bookingRequest.BookingDateTime } });

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => Sut.CreateBookingAsync(bookingRequest, userClaims));
        }

        [Fact]
        public async Task CreateBookingAsync_UserAlreadyBookedForDay_ThrowsException()
        {
            // Arrange
            var bookingRequest = GetBookingRequest();
            var userClaims = GetUserClaims();
            _bookingRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Booking> { new Booking { UserId = userClaims.Objectidentifier, BookingDateTime = bookingRequest.BookingDateTime } });

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
            _bookingRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Booking> { new Booking { UserId = adminClaims.Objectidentifier, BookingDateTime = existingBookingRequest.BookingDateTime, SeatId = existingBookingRequest.SeatId } });

            // Act
            var result = await Sut.CreateBookingAsync(bookingRequest, adminClaims);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(adminClaims.Objectidentifier, result.UserId);
            Assert.Equal(adminClaims.UserName, result.UserName);
            Assert.Equal(bookingRequest.SeatId, result.SeatId);
            Assert.Equal(bookingRequest.BookingDateTime.ToUniversalTime().ToString("o"), result.BookingDateTime);
        }
    }
}
