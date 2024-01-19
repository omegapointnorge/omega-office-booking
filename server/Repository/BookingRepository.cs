using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;

namespace server.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly OfficeDbContext _dbContext;

        public BookingRepository(OfficeDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public Task<List<Booking>> GetAllActiveBookings()
        {
            return _dbContext.Bookings
                .Where(booking => booking.BookingDateTime.Date >= DateTime.Today)
                .ToListAsync();
        }

        public Task<List<Booking>> GetBookingsWithSeatForUserAsync(String userId)
        {
            return _dbContext.Bookings
                .Where(booking => booking.UserId == userId)
                .ToListAsync();
        }

    }
}