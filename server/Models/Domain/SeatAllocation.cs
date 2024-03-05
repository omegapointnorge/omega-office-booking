namespace server.Models.Domain
{
    public class SeatAllocation
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SeatId { get; set; }
        public string DisplayName { get; set; }

        public SeatAllocation()
        {
        }
        public SeatAllocation(int id, string userId, int seatId, string displayName)
        {
            Id = id;
            UserId = userId;
            SeatId = seatId;
            DisplayName = displayName;
        }
    }
}