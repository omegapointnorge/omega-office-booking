using Microsoft.AspNetCore.Mvc;
using server.DAL;
using server.Repository.Interface;
using server.Response;

namespace server.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<ActionResult<UserBookingResponse>> InsertOrUpdateUsersBooking(CreateBookingRequest booking, String userId, String email, String name)
        {
            return await _userRepository.InsertOrUpdateUsersBooking(booking, userId, email, name);
        }
    }
}