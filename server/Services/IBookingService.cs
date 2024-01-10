using Microsoft.AspNetCore.Mvc;
using server.DAL;
using server.Models;

namespace server.Services
{
    public interface IBookingService
    {
        Task<ActionResult> DeleteBookingAsync(int id, string userId);

        Task<ActionResult<List<BookingDto>>> GetAllFutureBookings();

        Task<ActionResult<List<BookingDto>>> GetAllBookingsForUser(string userid);

        Task<ActionResult<List<BookingDto>>> GetAllBookingsForCurrentUser(string userId);

        Task<ActionResult<CreateBookingResponse>> CreateBookingAsync(CreateBookingRequest bookingRequest, User userId);

    }
}

