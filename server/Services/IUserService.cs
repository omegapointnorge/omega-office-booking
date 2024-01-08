using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;
using server.Request;
using server.Response;

namespace server.Services
{
    public interface IUserService
    {
        Task<ActionResult<List<UserDto>>> GetAllUsers();

        Task<ActionResult<UserBookingResponse>> InsertOrUpdateUsersBooking(UserBookingRequest booking, String userId,String email, String name);
    }
}