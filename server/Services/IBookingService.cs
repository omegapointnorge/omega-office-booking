using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs;
using server.Models.DTOs.Request;
using server.Models.DTOs.Response;

namespace server.Services
{
    public interface IBookingService
    {
        Task<ActionResult> DeleteBooking(int id, string userId);

        Task<ActionResult<List<BookingDto>>> GetAllFutureBookings();

        Task<ActionResult<List<BookingDto>>> GetAllBookingsForUser(string userid);

        Task<ActionResult<List<BookingDto>>> GetAllBookingsForCurrentUser(string userId);

        Task<ActionResult<CreateBookingResponse>> CreateBookingAsync(CreateBookingRequest bookingRequest, User userId);
        Task<ActionResult<List<BookingDto>>> GetPreviousBookingsForUser(string userId, int itemCount, int pageNumber);
        Task<int> GetPreviousBookingCountForUser(string userId);


    }
}

