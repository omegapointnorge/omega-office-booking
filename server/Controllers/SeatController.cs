using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;
using server.Services;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatController : ControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        [HttpGet("seats")]
        public async Task<ActionResult<IEnumerable<SeatDto>>> GetAllSeats()
        {
            var response = await _seatService.GetAllSeats();
            return new OkObjectResult(response);
        }
    }
}

