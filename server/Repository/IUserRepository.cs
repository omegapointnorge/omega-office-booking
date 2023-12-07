
using server.Models.DTOs;
using server.Request;

namespace server.Repository
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllUsers();

        Task<UserDto> InsertOrUpdateUsersBooking(UserBookingRequest booking);
    }
}