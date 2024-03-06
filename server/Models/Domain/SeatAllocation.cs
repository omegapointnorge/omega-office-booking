namespace server.Models.Domain
{
    public class SeatAllocation
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public int SeatId { get; set; }

        public SeatAllocation(int id, string userId, int seatId, string displayName)
        {
            Id = id;
            SeatId = seatId;
        }
    }
}