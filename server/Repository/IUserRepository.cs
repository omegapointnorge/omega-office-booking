
using server.Models.Domain;
using server.Models.DTOs;
using server.Request;
using server.Response;

namespace server.Repository
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllUsers();
        Booking? GetBookingByUserIdAndBookingId(int id, String userId);
        Task<UserBookingResponse> InsertOrUpdateUsersBooking(UserBookingRequest booking, String userId, String email, String name);

        User? GetUserByUserId(String userId);

        Task UpsertUserAsync(User user);
    }
}