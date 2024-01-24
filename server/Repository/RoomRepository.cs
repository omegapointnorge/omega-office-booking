
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;

namespace server.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly OfficeDbContext _dbContext;
        public RoomRepository(OfficeDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public Task<List<Room>> GetRoomsAsync()
        {
            return _dbContext.Rooms
                .Include(Room => Room.Seats)
                .ToListAsync();
        }
    }
}