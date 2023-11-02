using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
}