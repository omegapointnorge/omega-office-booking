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
    public async Task Logout()
    {
        
        await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties
        {
            RedirectUri = "/",
        });

        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    [AllowAnonymous]
    [HttpGet("IsAuthenticated")]
    public ActionResult IsAuthenticated()
    {
        Console.WriteLine(User?.Identity?.AuthenticationType);
        if (User?.Identity?.IsAuthenticated is null || !User.Identity.IsAuthenticated) return Unauthorized();

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