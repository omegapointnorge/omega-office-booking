using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddMicrosoftIdentityWebApp(options =>
{
    builder.Configuration.Bind("AzureAd", options);
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.ResponseType = OpenIdConnectResponseType.Code;
}, cookieOptions =>
{
    cookieOptions.ExpireTimeSpan = TimeSpan.FromHours(1);
    cookieOptions.SlidingExpiration = true;
    cookieOptions.Cookie.HttpOnly = true;
    cookieOptions.Cookie.IsEssential = true;
    cookieOptions.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    cookieOptions.Cookie.SameSite = SameSiteMode.Strict;

    cookieOptions.Events.OnRedirectToAccessDenied = c =>
    {
        c.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
    cookieOptions.Events.OnRedirectToLogin = c =>
    {
        c.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});
builder.Services.AddAuthorization(options =>
{
    var defaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.AddPolicy("AuthenticatedUser", defaultPolicy);
    options.DefaultPolicy = defaultPolicy;
    options.FallbackPolicy = defaultPolicy;
});


builder.Services.AddCors(options =>
{

    options.AddPolicy(name: "Client Origin",
        builder => builder
            .AllowAnyOrigin()
            //.WithOrigins("https://app-prod-itv-officebooking.azurewebsites.net", "http://localhost:5002")
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseCors("Client Origin");
app.UseRouting();

// Authentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.Run();
