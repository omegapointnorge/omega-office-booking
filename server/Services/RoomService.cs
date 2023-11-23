using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;
using server.Repository;


namespace server.Services
{
    public class RoomService : IRoomService
    {

        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<ActionResult<List<RoomDto>>> GetAllRooms()
        {
            return await _roomRepository.GetAllRooms();
        }

    }
}