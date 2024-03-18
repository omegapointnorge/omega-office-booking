using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;
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

    [HttpGet("GetAllSeats")]
    public async Task<ActionResult<IEnumerable<SeatDto>>> GetAllSeats()
    {
        try
        {
            var result = await _seatService.GetAllSeats();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred processing your request." });
        }
    }
}