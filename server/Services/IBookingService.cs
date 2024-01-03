using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;

namespace server.Services
{
    public interface IBookingService
    {
        Task<ActionResult> DeleteBooking(int id, string userId);

        Task<ActionResult<List<BookingDto>>> GetAllBookings();

        Task<ActionResult<List<BookingDto>>> GetAllBookingsForUser(string userid);

        Task<ActionResult<List<BookingDto>>> GetAllBookingsForCurrentUser(string userId);
    }
}

