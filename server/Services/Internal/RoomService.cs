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

        //TODO check if frontend need this method. Delete it if not in use
        public async Task<List<SeatDto>> GetAllSeatsForRoom(int roomId)
        {
            try
            {
                var seats = new List<SeatDto>();
                var roomListResult = await _roomRepository.GetAsync();
                var roomDtos = Mappers.MapRoomDtos(roomListResult);
                // Check if roomDtos is not null and contains at least one room
                if (roomDtos?.Any() == true)
                {
                    seats = roomDtos.First().Seats ?? new List<SeatDto>();
                }
                return seats;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}