using server.Models.DTOs;

namespace server.Repository
{
    public interface IBookingRepository
    {
        Task<List<BookingDto>> GetAllBookings();
    }
}