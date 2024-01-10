using Microsoft.AspNetCore.Mvc;
using server.DAL.Dto;

namespace server.Services.Interface
{
    public interface IRoomService
    {
        Task<ActionResult<List<RoomDto>>> GetAllRooms();

        Task<ActionResult<List<SeatDto>>> GetAllSeatsForRoom(int roomId);

    }
}