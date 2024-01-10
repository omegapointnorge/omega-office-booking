using Microsoft.AspNetCore.Mvc;
using server.DAL;

namespace server.Services
{
    public interface IRoomService
    {
        Task<ActionResult<List<RoomDto>>> GetAllRooms();

        Task<ActionResult<List<SeatDto>>> GetAllSeatsForRoom(int roomId);

    }
}