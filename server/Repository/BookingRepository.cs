using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;
using server.Models.DTOs;
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

        public BookingDto Add(BookingDto dto)
        {
            var entity = new Booking(dto.SeatId,dto.UserId);
            _dbContext.Bookings.Add(entity);
            _dbContext.SaveChanges();
            return new BookingDto(entity.SeatId,
                entity.UserId);
        }
    }
}