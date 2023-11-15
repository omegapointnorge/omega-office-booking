using server.Models.Domain;
using server.Models.DTOs;

namespace server.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Office? Office { get; set; }
        public ICollection<SeatDto>? Seats { get; set; }

        public RoomDto(int id, string name, Office? office, ICollection<SeatDto>? seats)
        {
            Id = id;
            Name = name;
            Office = office;
            Seats = seats;
        }
    }
}