using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
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
        var id = User.FindFirst(ClaimConstants.ObjectId)?.Value ?? String.Empty;
        var name = User.FindFirst(ClaimConstants.Name)?.Value ?? String.Empty;
        var email = User.FindFirst(ClaimConstants.PreferredUserName)?.Value ?? String.Empty;
        var role = User.FindFirst(ClaimConstants.Role)?.Value ?? String.Empty;
        var userClaims = new UserClaims(name, id, email,role);
        var user = new UserInfo(
            User.Identity?.IsAuthenticated ?? false,
            userClaims);

        return Ok(user);
    }
}

public record UserInfo(
    bool IsAuthenticated,
    UserClaims? User);