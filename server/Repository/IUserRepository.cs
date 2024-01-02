
using server.Models.Domain;
using server.Models.DTOs;
using server.Request;

namespace server.Repository
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllUsers();
        Booking? GetBookingByGuidAndBookingId(int id, Guid userId);
        Task<UserDto> InsertOrUpdateUsersBooking(UserBookingRequest booking, Guid userId, String email, String name);

        User? GetUserByUserId(Guid userId);
    }
}