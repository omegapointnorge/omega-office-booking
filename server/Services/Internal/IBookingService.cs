using server.Models.DTOs;
using server.Models.DTOs.Internal;
using server.Models.DTOs.Request;

namespace server.Services.Internal
{
    public interface IBookingService
    {
        Task DeleteBookingAsync(int id, UserClaims user);

        Task<IEnumerable<BookingDto>> GetAllActiveBookings();

        Task<IEnumerable<HistoryBookingDto>> GetAllBookingsForUserAsync(string userid);

        Task<BookingDto> CreateBookingAsync(CreateBookingRequest bookingRequest, UserClaims user);
    }
}