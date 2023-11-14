using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.DTOs;
using server.Models.Domain;
using server.Services;

namespace server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class RoomController : ControllerBase
    {
        readonly ILogger<RoomController> _logger;
        readonly RoomService _roomService;

        public RoomController(ILogger<RoomController> logger, RoomService roomService)
        {
            _logger = logger;
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