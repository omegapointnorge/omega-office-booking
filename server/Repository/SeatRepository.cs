using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;
using server.Models.DTOs;

namespace server.Repository
{
    public class SeatRepository : ISeatRepository
    {
        private readonly OfficeDbContext dbContext;


        public SeatRepository(OfficeDbContext officeDbContext)
        {
            dbContext = officeDbContext;
        }
        
        public Task<List<SeatDto>> GetAllSeats()
        {
            return dbContext.Seats
                .Select(seat => new SeatDto(
                        seat.Id,
                        seat.RoomId)
                ).ToListAsync();
        }
    }
}

