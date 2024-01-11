
using server.Models.Domain;
using server.Models.DTOs;
using server.Request;
using server.Response;

namespace server.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Booking? GetBookingByUserIdAndBookingId(int id, String userId);
    }
}