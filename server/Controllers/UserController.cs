using Microsoft.AspNetCore.Mvc;

namespace server.Controllers;

[ApiController]
[Route("client/[controller]")]
public class UserController : ControllerBase
{
        
    [HttpGet]
    [ProducesResponseType(typeof(UserInfo), StatusCodes.Status200OK)]
    public IActionResult GetCurrentUser()
    {
        var claimsToExpose = new List<string>()
        {
            "http://schemas.microsoft.com/identity/claims/objectidentifier",
            "name",
            "preferred_username",
        };

        var user = new UserInfo(
            User.Identity?.IsAuthenticated ?? false,
            User.Claims
                .Select(c => new KeyValuePair<string, string>(c.Type, c.Value))
                .Where(c => claimsToExpose.Contains(c.Key))
                .ToList());

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserNameByObjectId(string objectIdentifier)
    {
        try
        {
            var graphServiceClient = GetGraphServiceClient(); // Implement this method to get a Microsoft Graph Service Client

            // Retrieve user information based on object identifier
            var user = await graphServiceClient.Users[objectIdentifier]
                .Request()
                .GetAsync();

            if (user != null)
            {
                var userName = user.DisplayName; // DisplayName is just an example; you may need to adjust this based on your requirements
                return Ok(new { ObjectIdentifier = objectIdentifier, UserName = userName });
            }
            else
            {
                return NotFound($"User with ObjectIdentifier '{objectIdentifier}' not found.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving user information: {ex.Message}");
        }
    }
    private GraphServiceClient<IGraphServiceUsersCollectionRequest> GetGraphServiceClient()
    {
        // Implement this method to create and configure a Microsoft Graph Service Client
        // You need to provide the necessary authentication and permissions
        // Example: return a GraphServiceClient using an access token or client credentials
    }

}

public record UserInfo(
    bool IsAuthenticated,
    List<KeyValuePair<string, string>> Claims);