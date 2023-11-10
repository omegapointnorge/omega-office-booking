using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Data;
using System.Globalization;
using System.Linq;

public interface IBookingRepository
{
    Task<List<BookingDetailDto>> Get(int SeatId);

    Task<List<BookingDetailDto>> GetAll();
    Task<BookingDto> Add(BookingDto Booking);
}

public class BookingRepository: IBookingRepository
{
    private readonly OfficeDbContext context;

    public BookingRepository(OfficeDbContext context)
    {
        this.context = context;
    }

    public async Task<List<BookingDetailDto>> Get(int SeatId)
    {
        return await context.Bookings.Where(b => b.SeatId == SeatId)
            .Select(b => new BookingDetailDto(b.Id, b.SeatId, b.Bookingder, b.Seat.Room, DateTime.Today.AddDays(1).ToLongDateString()))
            .ToListAsync();
    }

    public async Task<List<BookingDetailDto>> GetAll()
    {
        return await context.Bookings.Select(b => new BookingDetailDto(b.Id, b.SeatId, b.Bookingder, b.Seat.Room, DateTime.Now.Day.ToString()))
            .ToListAsync();
    }

    public async Task<BookingDto> Add(BookingDto dto)
    {
        var entity = new BookingEntity();
        entity.SeatId = dto.SeatId;
        entity.Bookingder = dto.Name;
        context.Bookings.Add(entity);
        await context.SaveChangesAsync();
        return new BookingDto(entity.Id, entity.SeatId, 
            entity.Bookingder);
    }
}