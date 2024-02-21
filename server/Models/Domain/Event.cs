namespace server.Models.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();

        public Event()
        {
        }
        public Event(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}