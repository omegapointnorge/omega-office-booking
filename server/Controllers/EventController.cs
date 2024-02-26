using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Helpers;
using server.Models.DTOs;
using server.Models.DTOs.Request;
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

    [HttpPost]
    public async Task<ActionResult<IEnumerable<EventDto>>> CreateEventAsync(CreateEventRequest eventRequest)
    {
        try
        {
            var userClaim = UserUtils.GetCurrentUserClaims(User);
            var eventDto = await _eventService.CreateEventAsync(eventRequest, userClaim);
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
}