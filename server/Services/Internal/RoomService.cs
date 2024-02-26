using server.Helpers;
using server.Models.DTOs;
using server.Repository;


namespace server.Services.Internal
{
    public class RoomService : IRoomService
    {

        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<RoomDto>> GetAllRooms()
        {
            try
            {
                var roomListResult = await _roomRepository.GetRoomsAsync();
                var roomDtos = Mappers.MapRoomDtos(roomListResult);
                return roomDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}