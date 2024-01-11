using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs;

namespace server.Repository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<ActionResult> DeleteBooking(int id);

    }
}