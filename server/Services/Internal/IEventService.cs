using server.Models.DTOs;
using server.Models.DTOs.Internal;
using server.Models.DTOs.Request;

namespace server.Services.Internal
{
    public interface IEventService
    {
        Task<EventDto> CreateEventAsync(CreateEventRequest eventRequest, UserClaims user);
        Task DeleteEventAsync(int id);
    }
}
