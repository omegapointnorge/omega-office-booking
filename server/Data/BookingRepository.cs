using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Data;
using server.Models.Domain;
using System.Globalization;
using System.Linq;

public interface IBookingRepository
{
    Task<List<BookingDetailDto>> GetBookingfromUserId(int Userid);

    Task<List<BookingDetailDto>> GetAll();
    Task<BookingDto> Add(BookingDto Booking);
}

public class BookingRepository: IBookingRepository
{
    private readonly OfficeDbContextLokal context;

    public BookingRepository(OfficeDbContextLokal context)
    {
        this.context = context;
    }

    public async Task<List<BookingDetailDto>> GetBookingfromUserId(int Userid)
    {
        return await context.Bookings.Where(be => be.UserId == Userid)
            .Select(be => new BookingDetailDto(be.Id, be.SeatId, be.User.Name, be.Seat.Room.Name, DateTime.Today.AddDays(1).ToLongDateString()))
            .ToListAsync();
    }

    public async Task<List<BookingDetailDto>> GetAll()
    {
        return await context.Bookings.Select(b => new BookingDetailDto(b.Id, b.SeatId, b.User.Name, b.Seat.Room.Name, DateTime.Now.Day.ToString()))
            .ToListAsync();
    }

    public async Task<BookingDto> Add(BookingDto dto)
    {
        var entity = new Booking
        {
            SeatId = dto.SeatId,
            UserId = dto.UserId
        };
        context.Bookings.Add(entity);
        await context.SaveChangesAsync();
        return new BookingDto(entity.Id, entity.SeatId, 
            entity.UserId);
    }
}