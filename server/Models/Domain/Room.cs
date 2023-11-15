using server.Services;

namespace server.Models.Domain
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int OfficeId { get; set; }
        public Office Office { get; set; } = null!;
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
        
        public Room(int id, string name, int officeId)
        {
            Id = id;
            Name = name;
            OfficeId = officeId;
        }
    }
}