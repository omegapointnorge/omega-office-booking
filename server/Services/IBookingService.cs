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

        Task<ActionResult<(bool IsSuccess, IEnumerable<BookingDto> BookingDto, string ErrorMessage)>> GetAllFutureBookings();

        Task<ActionResult<(bool IsSuccess, IEnumerable<BookingDto> BookingDto, string ErrorMessage)>> GetAllBookingsForUser(string userid);

        Task<ActionResult<CreateBookingResponse>> CreateBookingAsync(CreateBookingRequest bookingRequest, User userId);
    }
}

