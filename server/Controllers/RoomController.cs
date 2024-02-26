using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;
using server.Services.Internal;

namespace server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("rooms")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetAllRooms()
        {
            try
            {
                var response = await _roomService.GetAllRooms();
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                // Log the exception, handle the error appropriately
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}