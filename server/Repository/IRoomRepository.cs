using server.Models.Domain;

namespace server.Repository;

public interface IRoomRepository : IRepository<Room>
{
    Task<List<Room>> GetRoomsAsync();
}