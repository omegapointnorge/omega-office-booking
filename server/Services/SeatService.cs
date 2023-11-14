using Microsoft.AspNetCore.Mvc;
using server.Context;
using server.DTOs;
using server.Models.DTOs;

namespace server.Services
{
    public class SeatService : ISeatService
    {

        private readonly ISeatService _iSeatService;

        public SeatService(ISeatService iSeatService)
        {
            _iSeatService = iSeatService;
        }
            
        public async Task<ActionResult<List<SeatDto>>> GetAllSeats()
        {
            return await _iSeatService.GetAllSeats();
        }
    }
}

