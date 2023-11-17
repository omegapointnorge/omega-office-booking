namespace server.Models.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SeatDto>? Seats { get; set; }

        public RoomDto(int id, string name, ICollection<SeatDto>? seats)
        {
            Id = id;
            Name = name;
            Seats = seats;
        }
    }
}