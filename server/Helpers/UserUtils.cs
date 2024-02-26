using Microsoft.Identity.Web;
using server.Models.DTOs.Internal;
using System.Security.Claims;

namespace server.Helpers;

public static class UserUtils
{
    public static UserClaims GetCurrentUserClaims(ClaimsPrincipal User)
    {
        string id = User.FindFirst(ClaimConstants.ObjectId).Value;
        string name = User.FindFirst(ClaimConstants.Name).Value;
        string email = User.FindFirst(ClaimConstants.PreferredUserName).Value;
        var role = User.FindFirst(ClaimConstants.Role)?.Value ?? null;

        if (id == null || name == null || email == null)
        {
            return null; // or throw an exception as per your requirement
        }

        return new UserClaims(name, id, email, role);
    }

}
