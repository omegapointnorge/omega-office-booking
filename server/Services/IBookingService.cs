using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;

namespace server.Services
{
    public interface IBookingService
    {
        Task<ActionResult> DeleteBooking(int id, String email);

        Task<ActionResult<List<BookingDto>>> GetAllBookings();

        Task<ActionResult<List<BookingDto>>> GetAllBookingsForUser(int userid);
    }
}

