using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;
using server.Request;
using server.Services;

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
            "name",
            "preferred_username"
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
    /// <param name="booking">The UserBookingRequest containing information about the user booking.</param>
    /// <returns>
    /// If successful, returns a 200 OK response with the updated UserDto.
    /// If the input is invalid, returns a 400 Bad Request response with validation errors.
    /// respons object contains the corrent booking information, not the booking histories
    /// </returns>
    [HttpPost("UpsertUserBooking")]
    public async Task<ActionResult<UserDto>> UpsertUserBooking(UserBookingRequest booking)
    {
        // Here we need to fetch the name and email of the user like in the method DeleteBooking
        var email = "Email@gmail.com";
        var response = await _userService.InsertOrUpdateUsersBooking(booking, email);
        return new OkObjectResult(response);
    }

}

public record UserInfo(
    bool IsAuthenticated,
    List<KeyValuePair<string, string>> Claims);