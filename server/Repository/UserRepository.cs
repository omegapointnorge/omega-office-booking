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

        public async Task<UserDto?> InsertOrUpdateUsersBooking(UserBookingRequest bookingReq, String userId, String email, String name)
        {
            // existingUser as it currently exists in the db
            var existingUser = GetUserByUserId(userId);
            // User doesn't exist, so add a new one
            var timeForBooking = DateTime.Now.AddDays(1);
            existingUser ??= CreateUser(userId, email, name);
            var userDto = EntityToDto(existingUser);
            if (SeatCanBeBookedOnSameDay(timeForBooking, bookingReq.SeatId) && UserCanBeBookedOnSameDay(timeForBooking, userId))
            {
                CreateBooking(timeForBooking, existingUser, bookingReq.SeatId);
                await _dbContext.SaveChangesAsync();

                //userDto = EntityToDto(existingUser);
                return userDto;
            }
            else
            {
                return null;
               // here implemented error handling
            }
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

        private Booking CreateBooking(DateTime bookingDateTime, Models.Domain.User user, int seatId)
        {
            var booking = new Booking
            {
                User = user,// Reference the related, now tracked entity, not the PK
                SeatId = seatId,
                BookingDateTime = bookingDateTime
            };
            _dbContext.Bookings.Add(booking);
            return booking;
        }

        private bool SeatCanBeBookedOnSameDay(DateTime bookingDateTime, int seatId)
        {
            // Check if the seat already has a booking on the same day
            return !_dbContext.Bookings.Any(booking => booking.BookingDateTime.Date == bookingDateTime.Date && booking.SeatId == seatId);
        }

        //Todo This can be implemented when Guid is ready for user, always return true for now
        private bool UserCanBeBookedOnSameDay(DateTime bookingDateTime, string userId)
        {
            // Check if the user already has a booking on the same day
            return true;
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