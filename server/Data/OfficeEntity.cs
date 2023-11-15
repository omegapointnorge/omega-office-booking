
using server.Models.Domain;

namespace server.Data
{
    public class OfficeEntity

    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}