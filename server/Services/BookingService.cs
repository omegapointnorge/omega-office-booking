
using server.Models.DTOs;
using server.Repository;

namespace server.Services
{
    public class BookingService : IBookingRepository
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public Task<List<BookingDto>> GetAllBookings()
        {
            return _bookingRepository.GetAllBookings();
        }
    }
}
