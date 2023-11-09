namespace server.Models.Domain
{
    public class Office
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}