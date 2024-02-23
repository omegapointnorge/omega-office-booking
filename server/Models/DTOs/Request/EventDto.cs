using server.Models.Domain;
using server.Models.DTOs;

namespace Server.Models.DTOs.Request
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
