using Microsoft.AspNetCore.Mvc;
using server.DAL;
using server.Response;

namespace server.Services
{
    public interface IUserService
    {
        Task<ActionResult<List<UserDto>>> GetAllUsers();

        Task<ActionResult<UserBookingResponse>> InsertOrUpdateUsersBooking(CreateBookingRequest booking, String userId,String email, String name);
    }
}