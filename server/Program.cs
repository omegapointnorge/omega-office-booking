using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.ApplicationInsights;
using Microsoft.Data.SqlClient;

using Microsoft.EntityFrameworkCore;
using server.Data;
using Azure.Identity;
using server.Context;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    var keyVaultName = builder.Configuration.GetValue<string>("KeyVaultName");
    builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{keyVaultName}.vault.azure.net/"),
        new DefaultAzureCredential());

    var clientId = builder.Configuration.GetValue<string>("AzureAd-ClientId");
    var clientSecret = builder.Configuration.GetValue<string>("AzureAd-ClientSecret");
    var tenantId = builder.Configuration.GetValue<string>("AzureAd-TenantId");
builder.Configuration["AzureAD:TenantId"] = builder.Configuration["AzureAd__TenantId"];

    builder.Configuration["AzureAd:TenantId"] = tenantId;
    builder.Configuration["AzureAd:ClientId"] = clientId;
    builder.Configuration["AzureAd:ClientSecret"] = clientSecret;
}

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
 .AddMicrosoftIdentityWebApp(options => {
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
builder.Services.AddControllersWithViews();

if (builder.Environment.IsDevelopment())
{
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
}






    if (builder.Environment.IsProduction())
    {
    builder.Services.AddDbContext<OfficeDbContext>(options =>
    {
        SqlAuthenticationProvider.SetProvider(
            SqlAuthenticationMethod.ActiveDirectoryManagedIdentity,
            new server.Helpers.AzureSqlAuthProvider());

        options.UseSqlServer("name=ConnectionStrings:DefaultConnection");
    });
}
    else
{
    builder.Services.AddDbContext<OfficeDbContextLokal>(options =>
    {
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        options.UseSqlite($"Data Source={Path.Join(path, "Seats2.db")}");
});
       
}
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


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

// Temporary fix to apply migrations on startup
// since we dont apply them in our pipeline
// (inb4 this is a permanent fix)
if (builder.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<server.Context.OfficeDbContext>();
    dbContext.Database.Migrate();
}
}


app.Run();
