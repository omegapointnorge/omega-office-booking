using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs;
using server.Repository;
using server.Models.DTOs.Request;
using server.Models.DTOs.Response;
using server.Services;
using System.Diagnostics.CodeAnalysis;


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
        public async Task<ActionResult<List<BookingDto>>> GetAllActiveBookings()
        {
            return await _bookingRepository.GetAllActiveBookings();
        }

        public async Task<ActionResult<List<BookingDto>>> GetAllBookingsForUser(String userId)
        {
            return await _bookingRepository.GetAllBookingsForUser(userId);
        }

        public async Task<ActionResult> DeleteBookingAsync(int bookingId, String userId)
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

            return await _bookingRepository.GetAllBookingsForUser(userId);
        }

        private DateTime ConvertToTimeZone(DateTime originalDateTime, string timeZoneId)  {
            // Get the time zone information
            TimeZoneInfo norwayTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            // Convert the DateTime to the specified time zone
            DateTime convertedDateTime = TimeZoneInfo.ConvertTime(originalDateTime, norwayTimeZone);

            return convertedDateTime;
        }
    }
}
