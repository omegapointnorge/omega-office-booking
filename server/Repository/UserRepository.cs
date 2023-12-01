using System.Security.Cryptography;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;
using server.Models.DTOs;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace server.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OfficeDbContext _dbContext;

        public UserRepository(OfficeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private static void DtoToEntity(UserDto dto, Models.Domain.User e)
        {
            e.Id = dto.Id;
            e.Name = dto.Name;
            e.Email = dto.Email;
            e.Bookings = dto.Bookings != null && dto.Bookings.Count != 0 ? dto.Bookings.Select(booking =>
                    new Booking(booking.Id, booking.UserId, booking.SeatId, booking.DateTime)
                ).ToList() : new List<Booking>();
        }
        private static UserDto EntityToDto(Models.Domain.User e)
        {
            return new UserDto(e.Id, e.Name, e.Email, e.Bookings);
        }

        public Task<List<UserDto>> GetAllUsers()
        {
            return _dbContext.Users.Select(user =>
                new UserDto(user.Id, user.Name, user.Email, user.Bookings)
            ).ToListAsync();
        }

        public async Task<UserDto> InsertOrUpdateUsers(UserDto userDto)
        {
            var existingUser = _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);
            var entity = new Models.Domain.User();
            if (existingUser == null)
            {
                // User doesn't exist, so add a new one
            
                DtoToEntity(userDto, entity);
                _dbContext.Users.Add(entity);
                await _dbContext.SaveChangesAsync();
                return EntityToDto(entity);
            }
            else
            {
                DtoToEntity(userDto, entity);
                // User exists, so give a entity State, and then update the existing one
                _dbContext.Entry(existingUser).State = EntityState.Modified;
                _dbContext.Entry(existingUser).CurrentValues.SetValues(entity);
            }


            _dbContext.SaveChanges();
            return EntityToDto(entity);
        }
    }
}