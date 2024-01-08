using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;

namespace server.Repository
{
    public interface IBookingRepository
    {
        Task<ActionResult> DeleteBooking(int id);
        Task<List<BookingDto>> GetAllBookings();
        Task<List<BookingDto>> GetAllBookingsForUser(string userId);
        Task<List<BookingDto>> GetUpcomingBookingsForUser(string userId);
        Task<List<BookingDto>> GetPreviousBookingsForUser(string userId, int itemCount, int pageNumber);
    }
}