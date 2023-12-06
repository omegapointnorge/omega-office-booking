using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;

namespace server.Services
{
    public interface IBookingService
    {
        Task<ActionResult> DeleteBooking(int id);

        Task<ActionResult<List<BookingDto>>> GetAllBookings();

        Task<ActionResult<List<BookingDto>>> GetAllBookingsForPerson(int userid);
    }
}

