using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;

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

        public Task<List<Booking>> GetAllActiveBookings()
        {
            return _dbContext.Bookings
                .Where(booking => booking.BookingDateTime.Date >= DateTime.Today)
                .Select(booking => new Booking(booking.Id, booking.UserId, booking.SeatId, booking.BookingDateTime))
                .ToListAsync();
        }



        public Task<List<BookingDto>> GetAllBookingsForUser(String userId)
        {
            return _dbContext.Bookings
            .Where(booking => booking.UserId == userId)
            .OrderByDescending(booking => booking.BookingDateTime)
            .Select(booking =>
                    new BookingDto(booking.Id, booking.UserId, booking.SeatId, booking.BookingDateTime)
                )
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