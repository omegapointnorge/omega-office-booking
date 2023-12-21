using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;

namespace server.Repository
{
    public interface IBookingRepository
    {
        Task<ActionResult> DeleteBooking(int id);
        Task<List<BookingDto>> GetAllBookings();
        Task<List<BookingDto>> GetAllBookingsForUser(int? userid);
    }
}