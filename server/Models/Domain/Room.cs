using server.Services;

namespace server.Models.Domain
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();

        public Room(string name)
        {
            Name = name;
        }
    }
}