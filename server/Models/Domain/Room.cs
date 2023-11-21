namespace server.Models.Domain
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Seat> Seats { get; set; }

        public Room(int id, string name, List<Seat> seats)
        {
            Id = id;
            Name = name;
            Seats = seats;
        }
    }
}