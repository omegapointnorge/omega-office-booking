using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;

namespace server.Services
{
    public interface IBookingService
    {
        Task<ActionResult> DeleteBooking(int id, string userId);

        Task<ActionResult<List<BookingDetailsDto>>> GetAllBookings();

        Task<ActionResult<List<BookingDetailsDto>>> GetAllBookingsForUser(string userid);
    }
}

