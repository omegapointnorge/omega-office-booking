namespace server.Models.Domain
{
    public class SeatAllocation
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int SeatId { get; set; }

        public SeatAllocation()
        {
        }
        public SeatAllocation(int id, int seatId, string email)
        {
            Id = id;
            SeatId = seatId;
            Email = email;
        }
    }
}