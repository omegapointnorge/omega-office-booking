using server.Models.Domain;

namespace server.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Office Office { get; set; }
        public ICollection<Seat> Seats { get; set; }

        public RoomDto(int id, string? name, Office office, ICollection<Seat> seats)
        {
            Id = id;
            Name = name;
            Office = office;
            Seats = seats;
        }
    }
}