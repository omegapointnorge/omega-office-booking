using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs;
using server.Request;

namespace server.Services
{
    public interface IUserService
    {
        Task<ActionResult<List<UserDto>>> GetAllUsers();

        Task<ActionResult<UserDto>> InsertOrUpdateUsersBooking(UserBookingRequest booking);
    }
}