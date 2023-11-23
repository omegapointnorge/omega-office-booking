using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;

namespace server.Services
{
    public interface IBookingService
    {
        Task<ActionResult<List<BookingDto>>> GetAllBookings();
    }
}

