
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.DTOs;

namespace server.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly OfficeDbContext dbContext;

        public RoomRepository(OfficeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<RoomDto>> GetAllRooms()
        {
            return dbContext.Rooms.Select(room =>
                new RoomDto(room.Id, room.Name, null))
                .ToListAsync();
        }
    }
}