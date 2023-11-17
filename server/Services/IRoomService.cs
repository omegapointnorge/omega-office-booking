using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs;

namespace server.Services
{
    public interface IRoomService
    {
        Task<ActionResult<List<RoomDto>>> GetAllRooms();
    }
}