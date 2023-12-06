using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;
using server.Models.DTOs;

namespace server.Repository
{
    public class SeatRepository : ISeatRepository
    {
        private readonly OfficeDbContext _dbContext;

        public SeatRepository(OfficeDbContext officeDbContext)
        {
            _dbContext = officeDbContext;
        }

        public Task<List<SeatDto>> GetAllSeats()
        {
            return _dbContext.Seats.Select(seat =>
                    new SeatDto(seat.Id, seat.RoomId, seat.Bookings)
                ).ToListAsync();
        }


    }
}

