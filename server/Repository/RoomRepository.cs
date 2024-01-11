
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;
using server.Models.DTOs;

namespace server.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(OfficeDbContext context) : base(context) { }
    }
}