using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;
using server.Request;

namespace server.Services
{
    public interface IUserService
    {
        Task<ActionResult<List<UserDto>>> GetAllUsers();

        Task<ActionResult<UserDto>> InsertOrUpdateUsersBooking(UserBookingRequest booking, Guid userId,String email, String name);
    }
}