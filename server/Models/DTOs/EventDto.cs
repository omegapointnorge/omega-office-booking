using server.Models.Domain;

namespace server.Models.DTOs
{
    public class EventDto
    {
        public EventDto(Event eventItem)
        {
            EventName = eventItem.Name;
            Bookings = new List<BookingDto>();

            foreach (var booking in eventItem.Bookings)
            {
                Bookings.Add(new BookingDto(booking));
            }
        }

        public string EventName { get; set; }
        public List<BookingDto> Bookings { get; set; }
    }
}
