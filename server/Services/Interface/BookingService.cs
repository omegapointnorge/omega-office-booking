using Microsoft.AspNetCore.Mvc;
using server.DAL;
using server.DAL.Dto;
using server.DAL.Models;
using server.DAL.Repository.Interface;

namespace server.Services.Interface
{
    public class BookingService : IBookingService
    {
        readonly IBookingRepository _bookingRepository;
        readonly IUserRepository _userRepository;

        public BookingService(IBookingRepository bookingRepository, IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
        }

        public async Task<ActionResult<CreateBookingResponse>> CreateBookingAsync(CreateBookingRequest bookingRequest, User user)
        {

            await _userRepository.UpsertUserAsync(user);

            var booking = new Booking
            {
                UserId = user.Id,
                SeatId = bookingRequest.SeatId,
                BookingDateTime = DateTime.Now
            };

            var createdBooking = await _bookingRepository.CreateBookingAsync(booking);
            var createBookingResponse = new CreateBookingResponse(createdBooking);

            return createBookingResponse;
        }
        public async Task<ActionResult<List<BookingDto>>> GetAllFutureBookings()
        {
            return await _bookingRepository.GetAllFutureBookings();
        }

        public async Task<ActionResult<List<BookingDto>>> GetAllBookingsForUser(string userId)
        {
            return await _bookingRepository.GetAllBookingsForUser(userId);
        }

        public async Task<ActionResult> DeleteBooking(int bookingId, string userId)
        {
            bool isThisBookingBelongToCurrentUser = _userRepository.GetBookingByUserIdAndBookingId(bookingId, userId) != null;
            if (isThisBookingBelongToCurrentUser) return await _bookingRepository.DeleteBooking(bookingId);
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<ActionResult<List<BookingDto>>> GetAllBookingsForCurrentUser(string userId)
        {
            //TODO add user service
            // existingUser as it currently exists in the db
            var user = _userRepository.GetUserByUserId(userId);

            return await _bookingRepository.GetAllBookingsForUser(user.Id);
        }

        private DateTime ConvertToTimeZone(DateTime originalDateTime, string timeZoneId)
        {
            // Get the time zone information
            TimeZoneInfo norwayTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            // Convert the DateTime to the specified time zone
            DateTime convertedDateTime = TimeZoneInfo.ConvertTime(originalDateTime, norwayTimeZone);

            return convertedDateTime;
        }
    }
}
