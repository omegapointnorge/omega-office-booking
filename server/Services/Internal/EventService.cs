using Microsoft.IdentityModel.Tokens;
using server.Models.Domain;
using server.Models.DTOs;
using server.Models.DTOs.Internal;
using server.Models.DTOs.Request;
using server.Repository;
using System.ComponentModel.DataAnnotations;

namespace server.Services.Internal
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IBookingRepository _bookingRepository;

        public EventService(IEventRepository eventRepository, IBookingRepository bookingRepository)
        {
            _eventRepository = eventRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<EventDto> CreateEventAsync(CreateEventRequest eventRequest, UserClaims user)
        {
            try
            {
                await ValidateEventRequestAsync(eventRequest);

                var eventData = new Event { Name = eventRequest.EventName };
                eventData.Bookings = new List<Booking>();

                foreach (var seatId in eventRequest.SeatList)
                {
                    var booking = CreateBookingFromEventRequest(eventRequest, user, seatId, eventData);
                    await _bookingRepository.AddAsync(booking);
                    eventData.Bookings.Add(booking);
                }

                await _eventRepository.AddAsync(eventData);
                await _eventRepository.SaveAsync();
                await _bookingRepository.SaveAsync();

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
                var eventToDelete = await _eventRepository.GetAsync(e => e.Id == id);

                if (eventToDelete == null)
                {
                    throw new Exception("Event not found");
                }

                await _eventRepository.Delete(eventToDelete);
                await _eventRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the event.", ex);
            }
        }

        private async Task ValidateEventRequestAsync(CreateEventRequest eventRequest)
        {
            if (eventRequest.SeatList.IsNullOrEmpty())
            {
                throw new Exception("Seat list is null or empty");
            }

            IEnumerable<Booking> existedBookings = await _bookingRepository.GetAsync();

            foreach (var seatId in eventRequest.SeatList)
            {
                if (BookingService.IsSeatAlreadyBooked(existedBookings, eventRequest.BookingDateTime, seatId))
                {
                    throw new Exception("Seat is already booked. Delete booking before creating event.");
                }
            }

            var validationContext = new ValidationContext(eventRequest, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(eventRequest, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                var errorMessages = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                throw new Exception(errorMessages);
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
