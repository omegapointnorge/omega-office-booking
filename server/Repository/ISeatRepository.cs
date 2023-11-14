
using server.Models.DTOs;

namespace server.Repository
{
    public interface ISeatRepository
    {
        public Task<List<SeatDto>> GetAllSeats();
    }
}