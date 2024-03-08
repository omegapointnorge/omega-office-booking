using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctions.Models
{
    public class SeatAllocation
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SeatId { get; set; }

        public SeatAllocation()
        {
        }
        public SeatAllocation(int id, string userId, int seatId)
        {
            Id = id;
            UserId = userId;
            SeatId = seatId;
        }
    }
}
