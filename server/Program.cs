using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

using Microsoft.EntityFrameworkCore;

using server.Context;
using server.Data;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
// Set our ClientId and ClientSecret in appsettings to user-secrets values
builder.Configuration["AzureAD:ClientId"] = builder.Configuration["AzureAd__ClientId"];
builder.Configuration["AzureAD:ClientSecret"] = builder.Configuration["AzureAd__ClientSecret"];
builder.Configuration["AzureAD:TenantId"] = builder.Configuration["AzureAd__TenantId"];


// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();
//builder.Services.AddCors();

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

// TODO
//builder.Services.AddDbContext<OfficeDbContext>(options => {
//    SqlAuthenticationProvider.SetProvider(
//        SqlAuthenticationMethod.ActiveDirectoryManagedIdentity,
//        new server.Helpers.AzureSqlAuthProvider());

//    var connectionString = "Server=tcp:" + builder.Configuration["SqlServerName"] +
//                           ".database.windows.net;Database=" + builder.Configuration["SqlDatabaseName"] +
//                           ";TrustServerCertificate=True;Authentication=Active Directory Default";
//    var sqlConnection = new SqlConnection(connectionString);
//    options.UseSqlServer(sqlConnection);
//});

builder.Services.AddDbContext<OfficeDbContext>(options => options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.UseAuthentication();

//app.UseCors(p => p.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());

app.MapSeatEndpoints();
app.MapUserEndpoints();
app.MapBookingEndpoints();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(e => e.MapDefaultControllerRoute());
app.MapFallbackToFile("index.html");

app.Run();
