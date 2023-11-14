using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Models.DTOs;

namespace server.Services
{
    public interface ISeatService
    {
        Task<ActionResult<List<SeatDto>>> GetAllSeats();
    }
}

