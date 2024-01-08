using server.Models.Domain;
using server.Models.DTOs;

namespace server.Response;

public class UserBookingResponse
{
    public UserDto? UserResponse { get; set; }
    
    public String? Error { get; set; }
}