
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.DTOs;

namespace server.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly OfficeDbContext _dbContext;

        public RoomRepository(OfficeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<RoomDto>> GetAllRooms()
        {
            return _dbContext.Rooms.Select(room =>
                new RoomDto(room.Id, room.Name, null))
                .ToListAsync();
        }

        public Task<List<SeatDto>> GetAllSeatsForRoom(int roomId)
        {
            return _dbContext.Seats
            .Where(seat => seat.RoomId == roomId)
            .Select(seat =>
                    new SeatDto(seat.Id, seat.RoomId, seat.Bookings)
                )
            .ToListAsync();
        }
    }
}