using Microsoft.AspNetCore.Mvc;
using server.DAL;
using server.DAL.Dto;
using server.DAL.Models;
using server.DAL.Repository.Interface;
using server.Helpers;
using server.Services.Interface;
using System.Collections.Generic;

namespace server.Services.Impl
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

            //await _userRepository.UpsertUserAsync(user);

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

        public async Task<ActionResult<(bool IsSuccess, IEnumerable<BookingDto> BookingDto, string ErrorMessage)>> GetAllBookingsForUser(string userId)
        {
            try
            {
                IEnumerable<Booking> bookingList = await _bookingRepository.GetAsync();
                bookingList = bookingList.Where(x => x.UserId == userId);
                var bookingDtoList = Mappers.MapBookingDtos(bookingList);
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
            bool isThisBookingBelongToCurrentUser = _userRepository.GetBookingByUserIdAndBookingId(bookingId, userId) != null;
            if (isThisBookingBelongToCurrentUser) return await _bookingRepository.DeleteBooking(bookingId);
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
