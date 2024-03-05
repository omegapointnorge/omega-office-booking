using server.Models.Domain;
using server.Models.DTOs;
using server.Models.DTOs.Internal;
using server.Models.DTOs.Request;
using server.Repository;

namespace server.Services.Internal
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<EventDto> CreateEventAsync(CreateEventRequest eventRequest, UserClaims user)
        {
            try
            {
                var eventData = new Event { Name = eventRequest.EventName };

                foreach (var seatId in eventRequest.SeatList)
                {
                    var booking = CreateBookingFromEventRequest(eventRequest, user, seatId, eventData);
                    eventData.Bookings.Add(booking);
                }

                await _eventRepository.AddAsync(eventData);
                await _eventRepository.SaveAsync();

                return new EventDto(eventData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteEventAsync(int id)
        {
            try
            {
                var eventToDelete = await _eventRepository.GetAsync(e => e.Id == id) ?? throw new Exception("Event not found");
                await _eventRepository.Delete(eventToDelete);
                await _eventRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the event.", ex);
            }
        }


        private Booking CreateBookingFromEventRequest(CreateEventRequest eventRequest, UserClaims user, int seatId, Event eventData)
        {
            return new Booking(
                userId: user.Objectidentifier,
                userName: user.UserName,
                seatId: seatId,
                bookingDateTime: eventRequest.BookingDateTime,
                bookingDateTimeDayOnly: eventRequest.BookingDateTime.Date,
                eventID: eventData.Id);
        }
    }
}
