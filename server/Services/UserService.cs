using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs;
using server.Repository;
using server.Request;
using server.Response;

namespace server.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<ActionResult<UserBookingResponse>> InsertOrUpdateUsersBooking(UserBookingRequest booking, String userId, String email, String name)
        {
            return await _userRepository.InsertOrUpdateUsersBooking(booking, userId, email, name);
        }
    }
}