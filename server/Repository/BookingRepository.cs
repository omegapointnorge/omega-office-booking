using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
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
        public Task<List<BookingDto>> GetAllBookings()
        {
            return _dbContext.Bookings.Select(booking =>
                new BookingDto(booking.Id, booking.UserId, booking.SeatId, booking.BookingDateTime)
            ).ToListAsync();
        }
    }
}