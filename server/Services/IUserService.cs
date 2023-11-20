using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;

namespace server.Services
{
    public interface IUserService
    {
        Task<ActionResult<List<UserDto>>> GetAllUsers();
    }
}