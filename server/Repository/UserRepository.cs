using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;
using server.Models.DTOs;
using server.Request;

namespace server.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OfficeDbContext _dbContext;

        public UserRepository(OfficeDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public async Task UpsertUserAsync(User user)
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                if (await UserExistsAsync(user.Id))
                {
                    await UpdateUserAsync(user);
                }
                else
                {
                    await AddUserAsync(user);
                }

                await _dbContext.SaveChangesAsync();
            }

            private async Task<bool> UserExistsAsync(string userId)
            {
                return await _dbContext.Users.AsNoTracking().AnyAsync(u => u.Id == userId);
            }

            private async Task AddUserAsync(User user)
            {
                await _dbContext.Users.AddAsync(user);
            }

            private async Task UpdateUserAsync(User user)
            {
                _dbContext.Entry(user).State = EntityState.Modified;
            }

        public async Task<UserDto> InsertOrUpdateUsersBooking(UserBookingRequest bookingReq, String userId, String email, String name)
        {
            // existingUser as it currently exists in the db
            var existingUser = GetUserByUserId(userId);
            // User doesn't exist, so add a new one
            existingUser ??= CreateUser(userId, email, name);
            CreateBooking(existingUser, bookingReq.SeatId);
            await _dbContext.SaveChangesAsync();
            return EntityToDto(existingUser);
        }

        private Models.Domain.User CreateUser(String userId, String email, String name)
        {
            var user = new Models.Domain.User
            {
                Id = userId,
                Email = email,
                Name = name

            };
            _dbContext.Users.Add(user);
            return user;
        }



        public User? GetUserByUserId(String userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            return user;
        }

        private Booking CreateBooking(Models.Domain.User user, int seatId)
        {
            var booking = new Booking
            {
                User = user,// Reference the related, now tracked entity, not the PK
                SeatId = seatId,
                BookingDateTime = DateTime.Now.AddDays(1)
            };
            _dbContext.Bookings.Add(booking);
            return booking;
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