using server.DAL.Dto;
using server.DAL.Models;

namespace server.DAL.Repository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<UserDto>> GetAllUsers();
        Booking? GetBookingByUserIdAndBookingId(int id, string userId);
        Task<UserBookingResponse> InsertOrUpdateUsersBooking(CreateBookingRequest booking, string userId, string email, string name);

        User? GetUserByUserId(string userId);

        Task UpsertUserAsync(User user);
    }
}