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


        public Task<List<BookingDto>> GetAllFutureBookingsForUser(String userId)
        {
            DateTime currentDateTime = DateTime.Now;

            return _dbContext.Bookings
            .Where(booking => booking.UserId == userId && booking.BookingDateTime > currentDateTime)
            .OrderByDescending(booking => booking.BookingDateTime)
            .Select(booking =>
                    new BookingDto(booking.Id, booking.UserId, booking.SeatId, booking.BookingDateTime)
                )
            .ToListAsync();
        }

        public async Task<List<BookingDto>> GetPreviousBookingsForUser(string userId, int itemCount, int pageNumber)
        {
            DateTime currentDateTime = DateTime.Now;

            var query = _dbContext.Bookings
                .Where(booking => booking.UserId == userId && booking.BookingDateTime < currentDateTime)
                .OrderByDescending(booking => booking.BookingDateTime)
                .Skip((pageNumber - 1) * itemCount)  // Calculate the number of records to skip based on the page number and page size
                .Take(itemCount);  // Take only the specified number of records for the current page

            var bookings = await query
                .Select(booking => new BookingDto(booking.Id, booking.UserId, booking.SeatId, booking.BookingDateTime))
                .ToListAsync();

            return bookings;
        }

        public async Task<int> GetPreviousBookingCountForUser(string userId)
        {
            DateTime currentDateTime = DateTime.Now;

            var query = await _dbContext.Bookings
                .Where(booking => booking.UserId == userId && booking.BookingDateTime < currentDateTime)
                .CountAsync();

            return query;
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