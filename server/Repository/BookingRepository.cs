using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;
using server.Models.DTOs;

namespace server.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly OfficeDbContext _dbContext;

        public BookingRepository(OfficeDbContext officeDbContext)
        {
            _dbContext = officeDbContext;
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();
            return booking;
        }

        public Task<List<BookingDto>> GetAllFutureBookings()
        {
            return _dbContext.Bookings.Select(booking =>
                new BookingDto(booking.Id, booking.UserId, booking.SeatId, booking.BookingDateTime)
            ).ToListAsync();
        }

        public Task<List<Booking>> GetAllBookingsForUser(String userId)
        {
            return _dbContext.Bookings
                .Include(booking => booking.Seat)
                .Where(booking => booking.UserId == userId)
                .ToListAsync();
        }

        public async Task<ActionResult> DeleteBooking(int id)
        {
            try
            {
                var booking = await _dbContext.Bookings.FindAsync(id);
                if (booking == null) return new StatusCodeResult(StatusCodes.Status404NotFound);
                _dbContext.Bookings.Remove(booking);
                await _dbContext.SaveChangesAsync();
                return new StatusCodeResult(StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}