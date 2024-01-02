using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;

namespace server.Services
{
    public interface IBookingService
    {
        Task<ActionResult> DeleteBooking(int id, Guid userId);

        Task<ActionResult<List<BookingDto>>> GetAllBookings();

        Task<ActionResult<List<BookingDto>>> GetAllBookingsForUser(Guid userid);

        Task<ActionResult<List<BookingDto>>> GetAllBookingsForCurrentUser(Guid userId);
    }
}

