using server.Models.Domain;

namespace server.Repository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<List<Booking>> GetAllActiveBookings();
        Task<List<Booking>> GetBookingsWithSeatForUserAsync(string userId);
    }
}