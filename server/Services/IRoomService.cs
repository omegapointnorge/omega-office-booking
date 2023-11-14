using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Models.Domain;

namespace server.Services
{
    public interface IRoomService
    {
        Task<ActionResult<List<RoomDto>>> GetAllRooms();
    }
}