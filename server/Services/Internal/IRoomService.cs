using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;

namespace server.Services.Internal
{
    public interface IRoomService
    {
        Task<ActionResult<List<RoomDto>>> GetAllRooms();

        Task<ActionResult<List<SeatDto>>> GetAllSeatsForRoom(int roomId);

    }
}