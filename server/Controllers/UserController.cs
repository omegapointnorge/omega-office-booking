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
        string id = User.FindFirst(ClaimConstants.ObjectId).Value;
        string name = User.FindFirst(ClaimConstants.Name).Value;
        string email = User.FindFirst(ClaimConstants.PreferredUserName).Value;
        var role = User.FindFirst(ClaimConstants.Role)?.Value ?? null;


        if(id == null|| name == null || email == null)
        {
            return BadRequest("Some of the user claims are null");
        }

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