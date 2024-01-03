using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;
using server.Repository;

namespace server.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;

        public BookingService(IBookingRepository bookingRepository, IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
        }
        public async Task<ActionResult<List<BookingDto>>> GetAllBookings()
        {
            return await _bookingRepository.GetAllBookings();
        }

        public async Task<ActionResult<List<BookingDto>>> GetAllBookingsForUser(String userId)
        {
            return await _bookingRepository.GetAllBookingsForUser(userId);
        }

        public async Task<ActionResult> DeleteBooking(int bookingId, String userId)
        {
            bool isThisBookingBelongToCurrentUser = _userRepository.GetBookingByUserIdAndBookingId(bookingId, userId) != null;
            if (isThisBookingBelongToCurrentUser) return await _bookingRepository.DeleteBooking(bookingId);
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<ActionResult<List<BookingDto>>> GetAllBookingsForCurrentUser(String userId)
        {
            //TODO add user service
            // existingUser as it currently exists in the db
            var user = _userRepository.GetUserByUserId(userId);

            return await _bookingRepository.GetAllBookingsForUser(user.Id);
        }
    }
}
