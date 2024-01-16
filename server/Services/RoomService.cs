using Microsoft.AspNetCore.Mvc;
using server.Helpers;
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
            var roomListResult = await _roomRepository.GetAsync();
            var roomDtos = Mappers.MapRoomDtos(roomListResult);
            return roomDtos;
        }

        public async Task<ActionResult<List<SeatDto>>> GetAllSeatsForRoom(int roomId)
        {
            var seats = new List<SeatDto>();
            var roomListResult = await _roomRepository.GetAsync();
            var roomDtos = Mappers.MapRoomDtos(roomListResult);
            seats = roomDtos.First().Seats;
            return seats;
        }

    }
}