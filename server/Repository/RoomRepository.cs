
using server.Context;
using server.Models.Domain;

namespace server.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(OfficeDbContext context) : base(context) { }
    }
}