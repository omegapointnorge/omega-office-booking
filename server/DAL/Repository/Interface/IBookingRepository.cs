using Microsoft.AspNetCore.Mvc;
using server.DAL.Dto;
using server.DAL.Models;

namespace server.DAL.Repository.Interface
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<ActionResult> DeleteBooking(int id);
        Task<List<BookingDto>> GetAllFutureBookings();
        Task<List<BookingDto>> GetAllBookingsForUser(string userId);
        Task<Booking> CreateBookingAsync(Booking booking);

    }
}