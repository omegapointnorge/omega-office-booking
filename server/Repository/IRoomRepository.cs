using server.Models.DTOs;

namespace server.Repository;

public interface IRoomRepository
{
    Task<List<RoomDto>> GetAllRooms();
}