using server.DAL;
using server.Models;
using server.Response;

namespace server.Repository.Interface
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