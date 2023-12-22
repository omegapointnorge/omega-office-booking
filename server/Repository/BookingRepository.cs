using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using server.Models.Domain;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

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


        public Task<List<BookingDto>> GetAllBookingsForUser(int? userid)
        {
            return _dbContext.Bookings
            .Where(booking => booking.UserId == userid)
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