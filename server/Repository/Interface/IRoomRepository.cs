using server.DAL;

namespace server.Repository.Interface;

public interface IRoomRepository : IRepository<RoomDto>
{
    Task<List<RoomDto>> GetAllRooms();
    Task<List<SeatDto>> GetAllSeatsForRoom(int roomId);

}