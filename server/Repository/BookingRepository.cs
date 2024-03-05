using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Helpers;
using server.Models.Domain;

namespace server.Repository;

public class BookingRepository(OfficeDbContext context) : Repository<Booking>(context), IBookingRepository
{
    public Task<List<Booking>> GetAllActiveBookings()
    {
        return context.Booking
            .Where(booking => DateOnly.FromDateTime(booking.BookingDateTime) >= BookingTimeUtils.GetCurrentDate())
            .Include(booking => booking.Event)
            .ToListAsync();
    }

    public Task<List<Booking>> GetBookingsWithSeatForUserAsync(String userId)
    {
        return context.Booking
            .Include(booking => booking.Seat)
            .Include(booking => booking.Event)
            .Where(booking => booking.UserId == userId)
            .ToListAsync();
    }

    public Task DeleteBookingsWithEventId(int eventId)
    {
        var bookings = context.Booking
            .Where(booking => booking.EventId == eventId);
        context.Booking.RemoveRange(bookings);
        return context.SaveChangesAsync();
    }
    public Task<Booking?> GetBookingDetailsBySeatIdAndDate(int seatId, DateTime date)
    {
        var booking = context.Booking.FirstOrDefault(booking =>
            booking.SeatId == seatId &&
            DateOnly.FromDateTime(booking.BookingDateTime) == DateOnly.FromDateTime(date));

        return Task.FromResult(booking);
    }


}