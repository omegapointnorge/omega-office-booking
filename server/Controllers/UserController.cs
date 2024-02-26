using Microsoft.AspNetCore.Mvc;
using server.Helpers;
using server.Models.DTOs.Internal;

namespace server.Controllers;

[ApiController]
[Route("client/[controller]")]
public class UserController : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(typeof(UserInfo), StatusCodes.Status200OK)]
    public IActionResult GetCurrentUser()
    {
        var claims = UserUtils.GetCurrentUserClaims(User);
        if (claims == null)
        {
            return BadRequest("Some of the user claims are null");
        }

        var user = new UserInfo(
            User.Identity?.IsAuthenticated ?? false,
            claims);

        return Ok(user);
    }
}

public record UserInfo(
    bool IsAuthenticated,
    UserClaims? Claims);