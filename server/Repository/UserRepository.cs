using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;
using server.Models.DTOs;
using server.Request;
using server.Response;

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
                    UpdateUser(user);
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

            private void UpdateUser(User user)
            {
                _dbContext.Entry(user).State = EntityState.Modified;
            }

        public async Task<UserBookingResponse> InsertOrUpdateUsersBooking(UserBookingRequest bookingReq, String userId, String email, String name)
        {
            var response = new UserBookingResponse();
            try
            {
                // existingUser as it currently exists in the db
                var existingUser = GetUserByUserId(userId);
            // User doesn't exist, so add a new one
            existingUser ??= CreateUser(userId, email, name);
            CreateBooking(existingUser, bookingReq.SeatId);
            await _dbContext.SaveChangesAsync();
            response.UserResponse = EntityToDto(existingUser);

            }
            catch (DbUpdateException ex)
            {
                // Handle specific database-related exceptions
                if (ex.InnerException is SqlException sqlException)
                {
                    // Check for specific SQL Server error codes and handle accordingly
                    if (sqlException.Number == 2601 || sqlException.Number == 2627)
                    {
                        // Unique key violation (duplicate entry)
                        response.Error = "User booking already exists.";
                    }
                    else
                    {
                        // Handle other SQL Server error codes or provide a generic error message
                        response.Error = "Error while saving changes to the database.";
                    }
                }
                else
                {
                    // Handle other database-related exceptions or provide a generic error message
                    response.Error = "Error while saving changes to the database.";
                }
            }

            return response;
        }

        private Models.Domain.User CreateUser(String userId, String email, String name)
        {
            var user = new Models.Domain.User(userId, name, email);
            _dbContext.Users.Add(user);
            return user;
        }



        public User? GetUserByUserId(String userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            return user;
        }

        private User CreateBooking(Models.Domain.User user, int seatId)
        {
            var booking = new Booking
            {
                User = user,// Reference the related, now tracked entity, not the PK
                SeatId = seatId,
                BookingDateTime = DateTime.Now.AddDays(1)
            };
            user.Bookings.Add(booking);
            return user;
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