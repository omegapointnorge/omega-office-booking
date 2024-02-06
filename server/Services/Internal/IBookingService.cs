using server.Models.DTOs;
using server.Models.DTOs.Internal;
using server.Models.DTOs.Request;

namespace server.Services.Internal
{
    public interface IBookingService
    {
        Task DeleteBookingAsync(int id, User user);

        Task<IEnumerable<BookingDto>> GetAllActiveBookings();

        Task<IEnumerable<BookingDto>> GetAllBookingsForUser(string userid);

        Task<BookingDto> CreateBookingAsync(CreateBookingRequest bookingRequest, User user);

        Task<IEnumerable<BookingDto>> CreateEventBookingsForSeatsAsync(CreateBookingRequest bookingRequest, User user);


    }
}