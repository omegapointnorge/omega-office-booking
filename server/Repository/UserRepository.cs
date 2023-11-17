

using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.DTOs;

namespace server.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OfficeDbContext dbContext;

        public UserRepository(OfficeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<UserDto>> GetAllUsers()
        {
            return dbContext.Users.Select(user =>
                new UserDto(user.Id, user.Name, user.Email, null)
            ).ToListAsync();
        }
    }
}