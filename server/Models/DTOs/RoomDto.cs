using server.Helpers;
using server.Models.Domain;

namespace server.Models.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SeatDto>? Seats { get; set; }

        public RoomDto(int id, string name, List<Seat>? seats)
        {
            Id = id;
            Name = name;
            if (seats != null) Seats = Mappers.MapSeatDtos(seats);
        }
    }
}