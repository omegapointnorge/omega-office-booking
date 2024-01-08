namespace server.Models.DTOs
{
    public class BookingDetailsDto
    {
        public BookingDetailsDto(BookingDto bookingDto, string roomName)
        {
            BookingDto = bookingDto;
            RoomName = roomName;
        }

        public BookingDto BookingDto { get; set; }

        public string RoomName { get; set; }

    }
}
