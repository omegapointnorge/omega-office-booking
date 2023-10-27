using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AzureAD.Example.Controllers;

[Route("client/account/[controller]")]
public class AuthenticationController : ControllerBase
{

    // Challenge the request from the user
    /// <summary>
    /// Challenge the authentication request from the User.
    /// </summary>
    /// <param name="returnUri"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("login")]
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