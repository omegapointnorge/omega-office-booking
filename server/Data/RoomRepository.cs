using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;

namespace server.Data
{
    public interface IRoomRepository
    {
        Task<List<RoomDto>> GetAll();
        Task<RoomDto?> Get(int id);
    }

    public class RoomRepository : IRoomRepository
    {
        private readonly OfficeDbContextLokal context;

        private static RoomDto EntityToDetailDto(Room e)
        {
            return new RoomDto(e.Id, e.Name);
        }

        public RoomRepository(OfficeDbContextLokal context)
        {
            this.context = context;
        }

        public async Task<List<RoomDto>> GetAll()
        {
            return await context.Rooms.Select(e => new RoomDto(e.Id, e.Name)).ToListAsync();
        }

        public async Task<RoomDto?> Get(int id)
        {
            var entity = await context.Rooms.SingleOrDefaultAsync(h => h.Id == id);
            if (entity == null)
                return null;
            return EntityToDetailDto(entity);
        }

    }
}
