using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;
using server.Models.DTOs.Internal;
using server.Models.DTOs.Request;

namespace server.Services.Internal
{
    public interface IBookingService
    {
        Task<ActionResult> DeleteBookingAsync(int id);

        Task<ActionResult<IEnumerable<BookingDto>>> GetAllActiveBookings();

        Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookingsForUser(string userid);

        Task<ActionResult<BookingDto>> CreateBookingAsync(CreateBookingRequest bookingRequest, User user);

        Task<ActionResult<IEnumerable<BookingDto>>> CreateEventBookingsForSeatsAsync(CreateBookingRequest bookingRequest, User user);


    }
}