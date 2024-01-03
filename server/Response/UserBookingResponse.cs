using server.Models.Domain;

namespace server.Response;

public class UserBookingResponse
{
    public User? UserResponse { get; set; }
    
    public String? Error { get; set; }
}