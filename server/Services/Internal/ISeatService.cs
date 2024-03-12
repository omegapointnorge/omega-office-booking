
using server.Models.DTOs;

namespace server.Services.Internal;

public interface ISeatService
{
    public Task<IEnumerable<int>> GetUnavailableSeatIds();
    public Task<IEnumerable<SeatDto>> GetAllSeats();
}
