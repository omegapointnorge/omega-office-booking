using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs;

namespace server.Repository
{
    public interface IBookingRepository
    {
        Task<ActionResult> DeleteBooking(int id);
        Task<List<BookingDto>> GetAllFutureBookings();
        Task<List<Booking>> GetActiveBookingsForUser(string userId);
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<List<Booking>> GetPreviousBookingsForUser(string userId, int itemCount, int pageNumber);
        Task<int> GetPreviousBookingCountForUser(string userId);

    }
}