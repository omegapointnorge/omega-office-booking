using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;
using server.Models.DTOs;
using System.Data;

namespace server.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly OfficeDbContext _dbContext;

        public BookingRepository(OfficeDbContext officeDbContext)
        {
            _dbContext = officeDbContext;
        }
        public Task<List<BookingDto>> GetAllBookings()
        {
            return _dbContext.Bookings.Select(booking =>
                new BookingDto(booking.Id, booking.UserId, booking.SeatId, booking.BookingDateTime)
            ).ToListAsync();
        }

        public Task<List<BookingDto>> GetAllBookingsForPerson(int userid)
        {
            return _dbContext.Bookings
            .Where(booking => booking.UserId == userid)
            .Select(booking =>
                    new BookingDto(booking.Id, booking.UserId, booking.SeatId, booking.BookingDateTime)
                )
            .ToListAsync();
        }

        public async Task<ActionResult> DeleteBooking(int id)
        {
            try
            {
                Booking booking = await _dbContext.Bookings.FindAsync(id);
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
        // Never call this add method directly, as always need to check whether the user exists in the database according to our logic.
       // public BookingDto Add(BookingDto dto)
       //{
       //   var entity = new Booking(dto.SeatId,dto.UserId);
       //   _dbContext.Bookings.Add(entity);
       //  _dbContext.SaveChanges();
       //   return new BookingDto(entity.SeatId,
       //       entity.UserId);

        
    }
}