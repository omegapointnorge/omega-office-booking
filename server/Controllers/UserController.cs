using Microsoft.AspNetCore.Mvc;
using server.DAL.Dto;
using server.Services.Impl;

namespace server.Controllers;

[ApiController]
[Route("client/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
        
    [HttpGet]
    [ProducesResponseType(typeof(UserInfo), StatusCodes.Status200OK)]
    public IActionResult GetCurrentUser()
    {
        var claimsToExpose = new List<string>()
        {
            "http://schemas.microsoft.com/identity/claims/objectidentifier",
            "name",
            "preferred_username",
        };

        var user = new UserInfo(
            User.Identity?.IsAuthenticated ?? false,
            User.Claims
                .Select(c => new KeyValuePair<string, string>(c.Type, c.Value))
                .Where(c => claimsToExpose.Contains(c.Key))
                .ToList());

        return Ok(user);
    }

    [HttpGet("Users")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var response = await _userService.GetAllUsers();
        return new OkObjectResult(response);
    }

    /// <summary>
    /// Handles HTTP POST requests to upsert a user booking.
    /// </summary>
    /// <param name="booking">The CreateBookingRequest containing information about the user booking.</param>
    /// <returns>
    /// If successful, returns a 200 OK response with the updated UserDto.
    /// If the input is invalid, returns a 400 Bad Request response with validation errors.
    /// respons object contains the corrent booking information, not the booking histories
    /// </returns>
    [HttpPost("UpsertUserBooking")]
    public async Task<ActionResult<UserBookingResponse>> UpsertUserBooking(CreateBookingRequest booking)
    {
        var userId = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
        var email = User.FindFirst("preferred_username")?.Value?? String.Empty;
        var name = User.FindFirst("name")?.Value?? String.Empty;

        var response = await _userService.InsertOrUpdateUsersBooking(booking, userId, email, name);
        if ( response?.Value?.Error != null ) {
            return new BadRequestObjectResult(response?.Value?.Error);
        }
        else
        return new OkObjectResult(response);
    }

}

public record UserInfo(
    bool IsAuthenticated,
    List<KeyValuePair<string, string>> Claims);