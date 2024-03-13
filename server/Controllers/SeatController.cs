using Microsoft.AspNetCore.Mvc;
using server.Services.Internal;

namespace server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeatController : ControllerBase
{
    private readonly ISeatService _seatService;

    public SeatController(ISeatService seatService)
    {
        _seatService = seatService;
    }

    [HttpGet("GetUnavailableSeatIds")]
    public async Task<ActionResult<IEnumerable<int>>> GetUnavailableSeatIds()
    {
        try
        {
            var result = await _seatService.GetUnavailableSeatIds();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred processing your request." });
        }
    }
}