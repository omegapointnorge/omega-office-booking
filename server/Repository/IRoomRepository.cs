

using server.DTOs;

public interface IRoomRepository
{
    Task<List<RoomDto>> GetAllRooms();
}