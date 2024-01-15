using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs;
using server.Models.DTOs.Request;
using server.Models.DTOs.Response;

namespace server.Services
{
    public interface IBookingService
    {
        Task<ActionResult> DeleteBookingAsync(int id);

        Task<ActionResult<(bool IsSuccess, IEnumerable<BookingDto> BookingDto, string ErrorMessage)>> GetAllFutureBookings();

        Task<ActionResult<(bool IsSuccess, IEnumerable<BookingDto> BookingDto, string ErrorMessage)>> GetActiveBookingsForUser(string userid);

        Task<ActionResult<(bool IsSuccess, IEnumerable<BookingDto> BookingDto, string ErrorMessage)>> GetPreviousBookingsForUser(string userId);

        Task<ActionResult<CreateBookingResponse>> CreateBookingAsync(CreateBookingRequest bookingRequest, User userId);
       

    }
}

