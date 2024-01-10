using Microsoft.AspNetCore.Mvc;
using server.DAL;
using server.Services;

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
            var response = await _roomService.GetAllRooms();
            return new OkObjectResult(response);
        }

        [HttpGet("{roomId}/Seats")]
        public async Task<ActionResult<IEnumerable<SeatDto>>> GetAllSeatsForRoom(int roomId)
        {
            var response = await _roomService.GetAllSeatsForRoom(roomId);
            if (response == null)
            {
                return NotFound();
            }
            return new OkObjectResult(response);
        }

    }
}