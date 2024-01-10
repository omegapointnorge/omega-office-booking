using Microsoft.AspNetCore.Mvc;
using server.DAL.Dto;
using server.Services.Interface;

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

   
}

public record UserInfo(
    bool IsAuthenticated,
    List<KeyValuePair<string, string>> Claims);