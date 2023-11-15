using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Services;

namespace server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class RoomController : ControllerBase
    {
        //readonly ILogger<RoomController> _logger;
        readonly IRoomService _roomService;

        public RoomController(/*ILogger<RoomController> logger,*/ IRoomService roomService)
        {
            //_logger = logger;
            _roomService = roomService;
        }

        [HttpGet("rooms")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetAllRooms()
        {
            var response = await _roomService.GetAllRooms();
            return new OkObjectResult(response);
        }

    }
}