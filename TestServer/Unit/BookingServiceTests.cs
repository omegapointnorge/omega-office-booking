using Moq;
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
            var bookingService = new BookingService(_bookingRepositoryMock.Object);
            _bookingRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Booking> { new Booking { SeatId = bookingRequest.SeatId, BookingDateTime = bookingRequest.BookingDateTime } });

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => bookingService.CreateBookingAsync(bookingRequest, userClaims));
        }

        [Fact]
        public async Task CreateBookingAsync_UserAlreadyBookedForDay_ThrowsException()
        {
            // Arrange
            var bookingRequest = GetBookingRequest();
            var userClaims = GetUserClaims();
            var bookingService = new BookingService(_bookingRepositoryMock.Object);
            _bookingRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Booking> { new Booking { UserId = userClaims.Objectidentifier, BookingDateTime = bookingRequest.BookingDateTime } });

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => bookingService.CreateBookingAsync(bookingRequest, userClaims));
        }

        [Fact]
        public async Task CreateBookingAsync_EventAdminMultipleBookingsSameDay_ReturnsMultipleBookingDtos()
        {
            // Arrange
            var bookingRequest1 = GetBookingRequest();
            var bookingRequest2 = GetBookingRequest();
            var userClaims = GetEventAdminClaims();
            var bookingService = new BookingService(_bookingRepositoryMock.Object);
            _bookingRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Booking>());

            // Act
            var result1 = await bookingService.CreateBookingAsync(bookingRequest1, userClaims);
            bookingRequest2.SeatId = 2; // Change the seat ID for the second booking request
            var result2 = await bookingService.CreateBookingAsync(bookingRequest2, userClaims);

            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal(userClaims.Objectidentifier, result1.UserId);
            Assert.Equal(userClaims.Objectidentifier, result2.UserId);
            Assert.Equal(userClaims.UserName, result1.UserName);
            Assert.Equal(userClaims.UserName, result2.UserName);
            Assert.Equal(bookingRequest1.SeatId, result1.SeatId);
            Assert.Equal(bookingRequest2.SeatId, result2.SeatId);
            Assert.Equal(bookingRequest1.BookingDateTime.ToUniversalTime().ToString("o"), result1.BookingDateTime);
            Assert.Equal(bookingRequest2.BookingDateTime.ToUniversalTime().ToString("o"), result2.BookingDateTime);
        }

    }
}
