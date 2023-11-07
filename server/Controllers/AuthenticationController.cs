using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace server.Controllers;

[Route("api/Account")]
[ApiController]
public class AuthenticationController : Controller
{
    [AllowAnonymous]
    [HttpGet("Login")]
    public ActionResult Login(string? returnUrl)
    {
        var redirectUri = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "/";
        var properties = new AuthenticationProperties { RedirectUri = redirectUri };
        
        return Challenge(properties);
    }

    [Authorize]
    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        return SignOut(
            new AuthenticationProperties { RedirectUri = "/" },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme);
    }

    [AllowAnonymous]
    [HttpGet("IsAuthenticated")]
    public ActionResult IsAuthenticated()
    {
        //if (User?.Identity?.IsAuthenticated is null || !User.Identity.IsAuthenticated) return Unauthorized();

        return Ok();
    }
    
    private bool IsUserFoundOrCreated()
    {
        var userId = User?.FindFirst(ClaimConstants.ObjectId)?.Value;
        var userEmail = User?.FindFirst("email")?.Value;

        if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException("Could not find userID");
        //if (string.IsNullOrWhiteSpace(userEmail)) throw new ArgumentNullException("Could not find user email");

        return true;             
    }
}