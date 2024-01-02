using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<ActionResult<List<BookingDto>>> GetAllBookingsForUser(Guid userid)
        {
            return await _bookingRepository.GetAllBookingsForUser(userid);
        }

        public async Task<ActionResult> DeleteBooking(int bookingId, String email)
        {
            bool isThisBookingBelongToCurrentUser = _userRepository.GetBookingByEmailAndBookingid(bookingId, email) != null;
            if (isThisBookingBelongToCurrentUser) return await _bookingRepository.DeleteBooking(bookingId);
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<ActionResult<List<BookingDto>>> GetAllBookingsForCurrentUser(string email)
        {
            //TODO add user service
            // existingUser as it currently exists in the db
            var user = _userRepository.GetUserByEmail(email);

            return await _bookingRepository.GetAllBookingsForUser(user?.Id);
        }
    }
}
