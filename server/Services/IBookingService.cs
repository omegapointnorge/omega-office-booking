using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs;
using server.Models.DTOs.Request;
using server.Models.DTOs.Response;

namespace server.Services
{
    public interface IBookingService
    {
        Task<ActionResult> DeleteBookingAsync(int id, string userId);

        Task<ActionResult<List<BookingDto>>> GetAllFutureBookings();

        Task<ActionResult<CreateBookingResponse>> CreateBookingAsync(CreateBookingRequest bookingRequest, User userId);
        Task<ActionResult<List<BookingDto>>> GetAllBookingsForUser(string userId);



    }
}

