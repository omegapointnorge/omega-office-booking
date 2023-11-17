
using server.Models.DTOs;

namespace server.Repository
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllUsers();
    }
}