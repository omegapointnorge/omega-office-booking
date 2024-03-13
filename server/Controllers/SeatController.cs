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
}