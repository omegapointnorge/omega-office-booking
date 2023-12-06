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

        public async Task<UserDto> InsertOrUpdateUsersBooking(UserBookingRequest bookingReq)
        {
            // existingUser as it currently exists in the db
            var existingUser =_dbContext.Users.FirstOrDefault(u => u.Email == bookingReq.Email);
            // User doesn't exist, so add a new one
            existingUser ??= CreateUser(bookingReq);
            CreateBooking(existingUser, bookingReq.SeatId);
            await _dbContext.SaveChangesAsync();
            return EntityToDto(existingUser);
        }

        private Models.Domain.User CreateUser(UserBookingRequest bookingReq)
        {
            var user = new Models.Domain.User();
            user.Email = bookingReq.Email;
            user.Name = bookingReq.Name;
            _dbContext.Users.Add(user);
            return user;
        }

        private Booking CreateBooking(Models.Domain.User user, int seatId)
        {
            var booking = new Booking
            {
                User = user,// Reference the related, now tracked entity, not the PK
                SeatId = seatId
            };
            _dbContext.Bookings.Add(booking);
            return booking;
        }
    }
}