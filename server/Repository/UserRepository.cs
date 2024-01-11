using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;
using server.Models.DTOs;

namespace server.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly OfficeDbContext _dbContext;

        public UserRepository(OfficeDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public Task<List<UserDto>> GetAllUsers()
        {
            return _dbContext.Users.Select(user =>
                new UserDto(user.Id, user.Name, user.Email, user.Bookings)
            ).ToListAsync();
        }

        public Booking? GetBookingByUserIdAndBookingId(int bookingId, String userId)
        {
            // existingUser as it currently exists in the db
            var existingUser = _dbContext.Users.Include(u => u.Bookings)
                .FirstOrDefault(u => u.Id == userId);
            var existingbooking = existingUser?.Bookings.FirstOrDefault(booking =>
                    booking.Id == bookingId);
            return existingbooking;
        }
    }
}