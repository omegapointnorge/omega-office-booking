using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;

namespace server.Services
{
    public interface IBookingService
    {
        Task<ActionResult> DeleteBooking(int id, string userId);

        Task<ActionResult<List<BookingDto>>> GetAllBookings();

        Task<ActionResult<List<BookingDto>>> GetAllBookingsForUser(string userId);
        Task<ActionResult<List<BookingDto>>> GetUpcomingBookingsForUser(string userId);
        Task<ActionResult<List<BookingDto>>> GetPreviousBookingsForUser(string userId, int itemCount, int pageNumber);
        Task<int> GetPreviousBookingCountForUser(string userId);
    }
}

