
using server.Models.DTOs;

namespace server.Services.Internal;

public interface ISeatService
{
    public Task<IEnumerable<SeatDto>> GetAllSeats();
}
