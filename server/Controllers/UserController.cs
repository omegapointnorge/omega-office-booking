using Microsoft.AspNetCore.Mvc;

namespace server.Controllers;

[ApiController]
[Route("client/[controller]")]
public class UserController : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(typeof(UserInfo), StatusCodes.Status200OK)]
    public IActionResult GetCurrentUser()
    {
        var claimsToExpose = new List<string>()
        {
            "http://schemas.microsoft.com/identity/claims/objectidentifier",
            "name",
            "preferred_username",
            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
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