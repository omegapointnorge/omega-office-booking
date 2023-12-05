using Microsoft.AspNetCore.Mvc;
using server.Context;
using server.Models.DTOs;
using server.Repository;

namespace server.Services
{
    public class SeatService : ISeatService
    {

        private readonly ISeatRepository _seatRepository;

        public SeatService(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task<ActionResult<List<SeatDto>>> GetAllSeats()
        {
            return await _seatRepository.GetAllSeats();
        }

        public async Task<ActionResult<List<SeatDto>>> GetAllSeatsForRoom(int roomId)
        {
            return await _seatRepository.GetAllSeatsForRoom(roomId);
        }
    }
}

