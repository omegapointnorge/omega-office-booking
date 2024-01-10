using Microsoft.AspNetCore.Mvc;
using server.DAL.Dto;
using server.DAL.Repository.Interface;
using server.Services.Interface;

namespace server.Services.Impl
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