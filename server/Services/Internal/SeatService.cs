
using server.Models.Domain;
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

        public async Task<IEnumerable<int>> GetUnavailableSeatIds()
        {
            IEnumerable<Seat> unavailableSeats = await _seatRepository.GetAllAsync(seat => seat.IsAvailable == false);
            return unavailableSeats.Select(seat => seat.Id);
        }
    }
}
