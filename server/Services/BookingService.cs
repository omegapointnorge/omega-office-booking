using Microsoft.AspNetCore.Mvc;
using server.Helpers;
using server.Models.Domain;
using server.Models.DTOs;
using server.Models.DTOs.Request;
using server.Models.DTOs.Response;
using server.Services;
using System.Diagnostics.CodeAnalysis;
using server.Helpers;

namespace server.Services
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


            var booking = new Booking
            {
                UserId = user.Id,
                SeatId = bookingRequest.SeatId,
                BookingDateTime = DateTime.Now
            };

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveAsync();

            var createBookingResponse = new CreateBookingResponse(booking);

            return createBookingResponse;
        }
        public async Task<ActionResult<(bool IsSuccess, IEnumerable<BookingDto> BookingDto, string ErrorMessage)>> GetAllFutureBookings()
        {
            try
            {
                IEnumerable<Booking> bookingList = await _bookingRepository.GetAsync();
                var bookingDtoList = Mappers.MapBookingDtos(bookingList);
                return (true, bookingDtoList, null);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error: {ex.Message} | {ex.StackTrace}");
                return (false, null, ex.Message);
            }
        }
            return await _bookingRepository.GetAllFutureBookings();
        }
        public async Task<ActionResult<List<BookingDto>>> GetPreviousBookingsForUser(string userId)
        {
            var bookings = await _bookingRepository.GetAllBookingsForUser(userId);
            var currentDate = DateTime.Now.Date;
            var previousBookings = bookings.Where(b => b.BookingDateTime.Date < currentDate).OrderByDescending(b => b.BookingDateTime).ToList();
            return Mappers.MapBookingDtos(previousBookings);
        }

        public async Task<ActionResult<(bool IsSuccess, IEnumerable<BookingDto> BookingDto, string ErrorMessage)>> GetAllBookingsForUser(string userId)
        {
            try
            {
                IEnumerable<Booking> bookingList = await _bookingRepository.GetAsync();
                bookingList = bookingList.Where(x => x.UserId == userId);
                var bookingDtoList = Mappers.MapBookingDtos(bookingList);
            var currentDate = DateTime.Now.Date;
            var activeBookings = bookings.Where(b => b.BookingDateTime.Date >= currentDate).OrderBy(b => b.BookingDateTime).ToList();
            return (true, bookingDtoList, null);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error: {ex.Message} | {ex.StackTrace}");
                return (false, null, ex.Message);
            }
            //return await _bookingRepository.GetAllBookingsForUser(userId);
        }

        public async Task<ActionResult> DeleteBookingAsync(int bookingId, string userId)
        {
            bool isCurrentUsersBooking = _userRepository.GetBookingByUserIdAndBookingId(bookingId, userId) != null;
            if (isCurrentUsersBooking) return await _bookingRepository.DeleteBooking(bookingId);
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
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