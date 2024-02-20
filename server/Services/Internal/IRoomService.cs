using server.Models.DTOs;

namespace server.Services.Internal
{
    public interface IRoomService
    {
        Task<List<RoomDto>> GetAllRooms();

    }
}