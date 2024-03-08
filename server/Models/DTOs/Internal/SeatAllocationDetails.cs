using server.Models.DTOs.Internal;

namespace server.Models.DTOs.Internala;

public class SeatAllocationDetails
{

    public UserClaims User { get; }
    public int SeatId { get; }

    public SeatAllocationDetails(string userId, string displayName, string email, int seatId)
    {
        User = new UserClaims(displayName, userId, email, null);
        SeatId = seatId;
    }
}