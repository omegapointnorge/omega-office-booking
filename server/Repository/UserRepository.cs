using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;
using server.Models.DTOs;

namespace server.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OfficeDbContext _dbContext;

        public UserRepository(OfficeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<UserDto>> GetAllUsers()
        {
            return _dbContext.Users.Select(user =>
                new UserDto(user.Id, user.Name, user.Email, user.Bookings)
            ).ToListAsync();
        }
    }
}