using Microsoft.AspNetCore.Mvc;
using server.DAL;
using server.DAL.Dto;
using server.DAL.Models;

namespace server.Services.Interface
{
    public interface IBookingService
    {
        Task<ActionResult> DeleteBookingAsync(int id, string userId);

        Task<ActionResult<(bool IsSuccess, IEnumerable<BookingDto> BookingDto, string ErrorMessage)>> GetAllFutureBookings();

        Task<ActionResult<(bool IsSuccess, IEnumerable<BookingDto> BookingDto, string ErrorMessage)>> GetAllBookingsForUser(string userid);

        Task<ActionResult<CreateBookingResponse>> CreateBookingAsync(CreateBookingRequest bookingRequest, User userId);

    }
}

