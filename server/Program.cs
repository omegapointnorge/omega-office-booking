using Azure.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using server.Context;
using server.Helpers;
using server.Repository;
using server.Services.Internal;

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


    builder.Configuration["AzureAd:TenantId"] = tenantId;
    builder.Configuration["AzureAd:ClientId"] = clientId;
    builder.Configuration["AzureAd:ClientSecret"] = clientSecret;
}

// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
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
    cookieOptions.Cookie.SameSite = SameSiteMode.None;
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

if (!builder.Environment.IsProduction())
{
    builder.Services.AddSwaggerGen();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "Client Origin",
            builder => builder
                .WithOrigins(
                "https://app-officebooking.azurewebsites.net/",
                "https://app-dev-officebooking.azurewebsites.net/",
                "http://localhost:5001")
                .AllowAnyHeader()
                .AllowAnyMethod()
        );
    });
}
builder.Services.AddDbContext<OfficeDbContext>(options =>
{
    SqlAuthenticationProvider.SetProvider(
        SqlAuthenticationMethod.ActiveDirectoryManagedIdentity,
        new server.Helpers.AzureSqlAuthProvider());
    options.UseSqlServer("name=ConnectionStrings:DefaultConnection");
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddScoped<RecaptchaEnterprise>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//swagger

BookingTimeUtils.SetConfig(builder.Configuration);


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseCors("Client Origin");
app.UseStaticFiles();
app.UseRouting();

// Authentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapControllers();

app.MapFallbackToFile("index.html");

// Temporary fix to apply migrations on startup
// since we dont apply them in our pipeline
// (inb4 this is a permanent fix)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OfficeDbContext>();
    dbContext.Database.Migrate();
}

app.Run();