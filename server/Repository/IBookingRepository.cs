using server.Models.Domain;

namespace server.Repository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task DeleteBookingsWithEventId(int eventId);
        Task<List<Booking>> GetAllActiveBookings();
        Task<Booking> GetBookingDetailsBySeatIdAndDate(int seatId, DateTime date);
        Task<List<Booking>> GetBookingsWithSeatForUserAsync(string userId);
    }
}