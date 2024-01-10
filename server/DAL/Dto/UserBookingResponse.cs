namespace server.DAL.Dto;

public class UserBookingResponse
{
    public UserDto? UserResponse { get; set; }

    public string? Error { get; set; }
}