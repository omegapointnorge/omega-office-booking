using server.Models.Domain;

namespace server.Models.DTOs
{
    public class EventDto
    {
        public string EventName { get; set; }
        public List<BookingDto> Bookings { get; set; }

        public EventDto(Event eventItem)
        {
            EventName = eventItem.Name;
            Bookings = eventItem.Bookings.Select(booking => new BookingDto(booking)).ToList();
        }

    }
}