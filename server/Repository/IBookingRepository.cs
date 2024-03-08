using server.Models.Domain;

namespace server.Repository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task DeleteBookingsWithEventId(int eventId);
        Task<List<Booking>> GetAllActiveBookings();
        Task<List<Booking>> GetBookingsWithSeatForUserAsync(string userId);
        Task<Booking?> GetBookingBySeatIdAndDateTime(int SeatId, DateTime bookingDateTime);
    }
}