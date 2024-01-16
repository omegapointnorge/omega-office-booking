using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;
using server.Models.DTOs.Request;
using server.Models.DTOs.Response;

namespace server.Services
{
    public interface IBookingService
    {
        Task<ActionResult> DeleteBookingAsync(int id);

        Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookings();

        Task<ActionResult<IEnumerable<BookingDto>>> GetActiveBookingsForUser(string userid);

        Task<ActionResult<IEnumerable<BookingDto>>> GetPreviousBookingsForUser(string userId);

        Task<ActionResult<CreateBookingResponse>> CreateBookingAsync(CreateBookingRequest bookingRequest, string userId);
       

    }
}

