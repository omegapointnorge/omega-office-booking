using System.Security.Cryptography;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;
using server.Models.DTOs;
using server.Request;
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

        public async Task<UserDto> InsertOrUpdateUsersBooking(UserBookingRequest booking)
        {
            // existingUser as it currently exists in the db
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Email == booking.Email);

            if (existingUser == null)
            {
                // User doesn't exist, so add a new one
                var entity = new Models.Domain.User();
                entity.Email = booking.Email;
                entity.Name = booking.Name;            
                _dbContext.Users.Add(entity);
                _dbContext.SaveChanges();
                // Now, you can access the generated ID
                int entityId = entity.Id;
                var newBooking = new Booking(entityId,booking.SeatId);
                _dbContext.Bookings.Add(newBooking);
                await _dbContext.SaveChangesAsync();
                return EntityToDto(entity);
            }
            else
            {
                existingUser = _dbContext.Users.Include(t => t.Bookings)
                    .FirstOrDefault(u => u.Email == booking.Email);      
                existingUser?.Bookings.Add(new Booking(existingUser.Id, booking.SeatId));
                _dbContext.Entry(existingUser).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return EntityToDto(existingUser);
            }
        }
    }
}