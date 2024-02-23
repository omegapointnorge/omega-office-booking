using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs.Internal;
using server.Models.DTOs.Request;
using server.Services.Internal;
using Server.Models.DTOs.Request;

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

    [HttpPost]
    public async Task<ActionResult<IEnumerable<EventDto>>> CreateEventAsync(CreateEventRequest eventRequest)
    {
        try
        {
            var user = GetUser();
            var eventDto = await _eventService.CreateEventAsync(eventRequest, user);
            return CreatedAtRoute(null, eventDto);
        }
        catch (Exception ex)
        {
            // Log the exception, handle the error appropriately
            return StatusCode(500, ex.Message);
        }
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

    private UserClaims GetUser()
    {
        var id = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? String.Empty;
        var name = User.FindFirst("name")?.Value ?? String.Empty;
        var role = User.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value ?? String.Empty;

        UserClaims user = new(name, id, role);
        return user;

    }
}