using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Services.Internal;

namespace server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "EventAdmin")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }


    [HttpDelete("{eventId}")]
    public async Task<ActionResult> DeleteEventAsync(int eventId)
    {
        try
        {
            await _eventService.DeleteEventAsync(eventId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred processing your request." + ex.Message);
        }
    }
}