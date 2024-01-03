
using server.Models.Domain;
using server.Models.DTOs;
using server.Request;

namespace server.Repository
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllUsers();
        Booking? GetBookingByUserIdAndBookingId(int id, String userId);
        Task<UserDto> InsertOrUpdateUsersBooking(UserBookingRequest booking, String userId, String email, String name);

        User? GetUserByUserId(String userId);
    }
}