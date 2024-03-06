namespace server.Models.DTOs.Internal;

public class RequiringBookingDetails
{
    public int SeatId { get; }
    public string DisplayName { get; }
    public string UserId { get; }

    public RequiringBookingDetails(int seatId, string displayName, string userId)
    {
        SeatId = seatId;
        DisplayName = displayName;
        UserId = userId;
    }
}
