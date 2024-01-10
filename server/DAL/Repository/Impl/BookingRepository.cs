using Microsoft.EntityFrameworkCore;
using server.Context;
using Microsoft.AspNetCore.Mvc;
using server.DAL.Models;
using server.DAL.Dto;
using server.DAL.Repository.Interface;

namespace server.DAL.Repository.Impl
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly OfficeDbContext _dbContext;

        public BookingRepository(OfficeDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();
            return booking;
        }

        //public Task<List<BookingDto>> GetAllFutureBookings()
        //{
        //    return _dbContext.Bookings.Select(booking =>
        //        new BookingDto(booking.Id, booking.UserId, booking.SeatId, booking.BookingDateTime)
        //    ).ToListAsync();
        //}


        //public Task<List<BookingDto>> GetAllBookingsForUser(string userId)
        //{
        //    return _dbContext.Bookings
        //    .Where(booking => booking.UserId == userId)
        //    .OrderByDescending(booking => booking.BookingDateTime)
        //    .Select(booking =>
        //            new BookingDto(booking.Id, booking.UserId, booking.SeatId, booking.BookingDateTime)
        //        )
        //    .ToListAsync();
        //}

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