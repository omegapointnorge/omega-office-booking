using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Helpers;
using server.Models.Domain;

namespace server.Repository
{
    public class BookingRepository(OfficeDbContext context) : Repository<Booking>(context), IBookingRepository
    {
        public Task<List<Booking>> GetAllActiveBookings()
        {
            return context.Bookings
                .Where(booking => DateOnly.FromDateTime(booking.BookingDateTime) >= BookingTimeUtils.GetCurrentDate())
                .Include(booking => booking.Event)
                .ToListAsync();
        }

        public Task<List<Booking>> GetBookingsWithSeatForUserAsync(String userId)
        {
            return context.Bookings
                .Include(booking => booking.Seat)
                .Include(booking => booking.Event)
                .Where(booking => booking.UserId == userId)
                .ToListAsync();
        }

        public Task DeleteBookingsWithEventId(int eventId)
        {
            var bookings = context.Bookings
                .Where(booking => booking.EventId == eventId);
            context.Bookings.RemoveRange(bookings);
            return context.SaveChangesAsync();
        }

    }
}