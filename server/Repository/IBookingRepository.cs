using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs;

namespace server.Repository
{
    public interface IBookingRepository
    {
        Task<ActionResult> DeleteBooking(int id);
        Task<List<BookingDto>> GetAllFutureBookings();
        Task<List<BookingDto>> GetAllBookingsForUser(string userId);
        Task<Booking> CreateBookingAsync(Booking booking);

    }
}