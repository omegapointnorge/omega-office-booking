namespace server.Models.Domain
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Seat> Seats { get; set; } = new List<Seat>();

        public Room(int id, string name)
        {
            Id = id;
            Name = name;
            Seats = new List<Seat>();
        }
    }
}