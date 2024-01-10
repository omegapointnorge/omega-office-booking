using Microsoft.AspNetCore.Mvc;
using server.DAL;
using server.Repository.Interface;

namespace server.Services
{
    public class RoomService : IRoomService
    {

        readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<ActionResult<List<RoomDto>>> GetAllRooms()
        {
            return await _roomRepository.GetAllRooms();
        }

        public async Task<ActionResult<List<SeatDto>>> GetAllSeatsForRoom(int roomId)
        {
            return await _roomRepository.GetAllSeatsForRoom(roomId);
        }

    }
}