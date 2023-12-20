
using server.Models.Domain;
using server.Models.DTOs;
using server.Request;

namespace server.Repository
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllUsers();
        Booking? GetBookingByEmailAndBookingid(int id, string email);
        Task<UserDto> InsertOrUpdateUsersBooking(UserBookingRequest booking);
    }
}