using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;

namespace server.Services
{
    public interface ISeatService
    {
        Task<ActionResult<List<SeatDto>>> GetAllSeats();
        Task<ActionResult<List<SeatDto>>> GetAllSeatsForRoom(int roomId);
    }
}

