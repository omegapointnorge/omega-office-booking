using Microsoft.AspNetCore.Mvc;
using server.DAL.Dto;
using server.DAL.Repository.Interface;
using server.Services.Interface;

namespace server.Services.Impl
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

        public async Task<ActionResult<UserBookingResponse>> InsertOrUpdateUsersBooking(CreateBookingRequest booking, string userId, string email, string name)
        {
            return await _userRepository.InsertOrUpdateUsersBooking(booking, userId, email, name);
        }
    }
}