
using server.Repository;

namespace server.Services.Internal;


public class EventService : IEventService
{
    IEventRepository _eventRepository;
    IBookingRepository _bookingRepository;
    public EventService(IEventRepository eventRepository, IBookingRepository bookingRepository)
    {
        _eventRepository = eventRepository;
        _bookingRepository = bookingRepository;
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
            await _bookingRepository.DeleteBookingsWithEventId(id);
            await _eventRepository.Delete(eventToDelete);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the booking.", ex);
        }
    }
}

