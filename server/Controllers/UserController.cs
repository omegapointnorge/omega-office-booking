using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;
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
            "name"
        };

        var user = new UserInfo(
            User.Identity?.IsAuthenticated ?? false,
            User.Claims
                .Select(c => new KeyValuePair<string, string>(c.Type, c.Value))
                .Where(c => claimsToExpose.Contains(c.Key))
                .ToList());

        return Ok(user);
    }

    [HttpGet("users")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var response = await _userService.GetAllUsers();
        return new OkObjectResult(response);
    }

    [HttpGet("InsertOrUpdateUsers")]
    public async Task<ActionResult<UserDto>> InsertOrUpdateUsers(UserDto user)
    {
        var response = await _userService.InsertOrUpdateUsers(user);
        return new OkObjectResult(response);
    }

}

public record UserInfo(
    bool IsAuthenticated,
    List<KeyValuePair<string, string>> Claims);