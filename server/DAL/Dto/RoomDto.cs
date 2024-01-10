using server.DAL.Models;
using server.Helpers;

namespace server.DAL.Dto
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