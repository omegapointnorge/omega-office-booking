using Microsoft.AspNetCore.Mvc;
using server.DAL.Dto;

namespace server.Services.Impl
{
    public interface IUserService
    {
        Task<ActionResult<List<UserDto>>> GetAllUsers();

        Task<ActionResult<UserBookingResponse>> InsertOrUpdateUsersBooking(CreateBookingRequest booking, string userId, string email, string name);
    }
}