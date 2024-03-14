
using server.Models.Domain;
using server.Models.DTOs;
using server.Repository;

namespace server.Services.Internal
{
    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;

        public SeatService(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }
        public async Task<IEnumerable<SeatDto>> GetAllSeats()
        {
            IEnumerable<Seat> allSeats = await _seatRepository.GetAsync();
            return allSeats.Select(seat => new SeatDto(seat.Id, seat.RoomId));
        }
    }
}