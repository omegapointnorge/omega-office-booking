using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Helpers;
using server.Models.Domain;
using server.Models.DTOs;
using server.Request;
using server.Response;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using User = server.Models.Domain.User;

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

        public async Task<UserBookingResponse> InsertOrUpdateUsersBooking(UserBookingRequest bookingReq, String userId, String email, String name)
        {
            var response = new UserBookingResponse();
            try
            {
                // existingUser as it currently exists in the db
                var existingUser = GetUserByUserId(userId);
                // User doesn't exist, so add a new one, timeForBooking is the time for the reservation, now set it to tomorrow
                var bookingDateTime = DateTime.Now.AddDays(1);
                existingUser ??= CreateUser(userId, email, name);
                if (CanBookSeatAndUser(bookingDateTime, bookingReq.SeatId, userId))
                {
                    CreateBooking(bookingDateTime, existingUser, bookingReq.SeatId);
                    await _dbContext.SaveChangesAsync();
                    response.UserResponse = EntityToDto(existingUser);
                }
                else
                {
                    response.Error = "Seat Can not be Booked, or the user has already a reservation";
                }

            }
            catch (DbUpdateException ex)
            {
                // Handle specific database-related exceptions
                ExceptionHandler.HandleDbUpdateException(response, ex);
            }
            return response;
        }



        private User CreateUser(String userId, String email, String name)
        {
            var user = new User
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

        private User CreateBooking(DateTime bookingDateTime, Models.Domain.User user, int seatId)
        {
            var booking = new Booking
            {
                User = user,// Reference the related, now tracked entity, not the PK
                SeatId = seatId,
                BookingDateTime = bookingDateTime
            };
            user.Bookings.Add(booking);
            return user;
        }

        private bool CanBookSeatAndUser(DateTime bookingDateTime, int seatId, String userId)
        {
            // Check if the seat already has a booking on the same day,as well as user,
            return !_dbContext.Bookings.Any(booking => booking.BookingDateTime.Date == bookingDateTime.Date && (booking.SeatId == seatId || booking.UserId == userId));
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