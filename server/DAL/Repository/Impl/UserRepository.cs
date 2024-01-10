using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.DAL.Dto;
using server.DAL.Models;
using server.DAL.Repository.Interface;

namespace server.DAL.Repository.Impl
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly OfficeDbContext _dbContext;

        public UserRepository(OfficeDbContext context) : base(context)
        {
            _dbContext = context;
        }

        private static UserDto EntityToDto(User e)
        {
            return new UserDto(e.Id, e.Name, e.Email, e.Bookings);
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