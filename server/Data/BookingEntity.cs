namespace server.Data; 
public class BookingEntity
{
    public int Id { get; set; }
    public int SeatId { get; set; }
    public SeatEntity? Seat { get; set; }
    public string Bookingder { get; set; } = string.Empty;
    public int Amount { get; set; }
}