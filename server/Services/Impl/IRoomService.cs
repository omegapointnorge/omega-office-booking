using Microsoft.AspNetCore.Mvc;
using server.DAL.Dto;

namespace server.Services.Impl
{
    public interface IRoomService
    {
        Task<ActionResult<List<RoomDto>>> GetAllRooms();

        Task<ActionResult<List<SeatDto>>> GetAllSeatsForRoom(int roomId);

    }
}