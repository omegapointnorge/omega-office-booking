namespace server.Models.Domain;

public class SeatAllocation
{
    public int Id { get; set; }
    public string Email { get; set; }
    public int SeatId { get; set; }

    public SeatAllocation(int id, string email, int seatId)
    {
        Id = id;
        Email = email;
        SeatId = seatId;
    }
}