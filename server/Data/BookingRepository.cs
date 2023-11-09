using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Data;

public interface IBookingRepository
{
    Task<List<BookingDto>> Get(int SeatId);
    Task<BookingDto> Add(BookingDto Booking);
}

public class BookingRepository: IBookingRepository
{
    private readonly OfficeDbContext context;

    public BookingRepository(OfficeDbContext context)
    {
        this.context = context;
    }

    public async Task<List<BookingDto>> Get(int SeatId)
    {
        return await context.Bookings.Where(b => b.SeatId == SeatId)
            .Select(b => new BookingDto(b.Id, b.SeatId, b.Bookingder, b.Amount))
            .ToListAsync();
    }

    public async Task<BookingDto> Add(BookingDto dto)
    {
        var entity = new BookingEntity();
        entity.SeatId = dto.SeatId;
        entity.Bookingder = dto.Bookingder;
        entity.Amount = dto.Amount;
        context.Bookings.Add(entity);
        await context.SaveChangesAsync();
        return new BookingDto(entity.Id, entity.SeatId, 
            entity.Bookingder, entity.Amount);
    }
}