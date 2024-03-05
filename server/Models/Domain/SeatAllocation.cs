using System.Transactions;

namespace server.Models.Domain
{
    public class SeatAllocation
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SeatId { get; set; }

CommittableTransaction 
        public SeatAllocation(int id, string name, int seatId)
        {
            Id = id;
            UserId = name;
            SeatId = seatId;
        }
    }
}