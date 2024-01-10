using server.DAL.Dto;
using server.DAL.Models;

namespace server.DAL.Repository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<UserDto>> GetAllUsers();
        Booking? GetBookingByUserIdAndBookingId(int id, string userId);
    }
}