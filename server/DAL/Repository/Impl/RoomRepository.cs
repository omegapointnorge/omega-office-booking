
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.DAL.Dto;
using server.DAL.Repository.Interface;

namespace server.DAL.Repository.Impl
{
    public class RoomRepository : Repository<RoomDto>, IRoomRepository
    {
        private readonly OfficeDbContext _dbContext;

        public RoomRepository(OfficeDbContext context) : base(context)
        {
            _dbContext = context;
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