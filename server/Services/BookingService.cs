using Microsoft.AspNetCore.Mvc;
using server.Helpers;
using server.Models.Domain;
using server.Models.DTOs;
using server.Models.DTOs.Internal;
using server.Models.DTOs.Request;
using server.Repository;

namespace server.Services
{
    public class BookingService : IBookingService
    {
        readonly IBookingRepository _bookingRepository;
        private const int SameDayCutoffHour = 16;


        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<ActionResult<BookingDto>> CreateBookingAsync(CreateBookingRequest bookingRequest, User user)
        {


            try
            {
                IEnumerable<Booking> bookingList = await _bookingRepository.GetAsync();
                var booking = new Booking
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    SeatId = bookingRequest.SeatId,
                    BookingDateTime = bookingRequest.BookingDateTime
                };

                string validationError = ValidateBookingRequest(bookingRequest, bookingList, user.UserId);
                if (validationError != null)
                {
                    throw new Exception(validationError);
                }

                await _bookingRepository.AddAsync(booking);
                await _bookingRepository.SaveAsync();
                return new BookingDto(booking);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllActiveBookings()
        {
            try
            {
                IEnumerable<Booking> bookingList = await _bookingRepository.GetAllActiveBookings();
                var bookingDtoList = Mappers.MapBookingDtos(bookingList);
                return bookingDtoList;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookingsForUser(String userId)
        {
            try
            {
                var bookings = await _bookingRepository.GetBookingsWithSeatForUserAsync(userId);
                return Mappers.MapBookingDtos(bookings);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ActionResult> DeleteBookingAsync(int bookingId)
        {
            try
            {
                var booking = new Booking
                {
                    Id = bookingId
                };
                await _bookingRepository.DeleteAndCommit(booking);
                return new StatusCodeResult(StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string ValidateBookingRequest(CreateBookingRequest bookingRequest, IEnumerable<Booking> bookingList, string userId)
        {
            if (DateOnly.FromDateTime(bookingRequest.BookingDateTime) > GetLatestAllowedBookingDate())
            {
                return "Booking date exceeds the latest allowed booking date.";
            }

            if (IsSeatAlreadyBooked(bookingList, bookingRequest))
            {
                return "Seat is already booked for the specified time.";
            }

            if (HasUserAlreadyBookedForDay(bookingList, bookingRequest, userId))
            {
                return "User has already booked for the specified day.";
            }



            return null; // Validation passed
        }

        private static bool HasUserAlreadyBookedForDay(IEnumerable<Booking> bookingList, CreateBookingRequest bookingRequest, string userId)
        {
            var bookingDateTime = bookingRequest.BookingDateTime;
            return bookingList.Any(booking => bookingDateTime.Date == bookingDateTime.Date && booking.UserId == userId);
        }

        private static bool IsSeatAlreadyBooked(IEnumerable<Booking> bookingList, CreateBookingRequest bookingRequest)
        {
            var bookingDateTime = bookingRequest.BookingDateTime;
            return bookingList.Any(booking => booking.BookingDateTime.Date == bookingDateTime.Date && booking.SeatId == bookingRequest.SeatId);
        }

        private static DateOnly GetLatestAllowedBookingDate()
        {
            DateTime now = DateTime.Now;

            now = now.AddDays(3);
            now = now.AddHours(4);
            DateOnly latestAllowedBookingDate = DateOnly.FromDateTime(now);
            TimeSpan sameDayCutoff = new TimeSpan(SameDayCutoffHour, 0, 0);

            if (IsWeekend(now))
            {
                while (now.DayOfWeek != DayOfWeek.Monday)
                {
                    latestAllowedBookingDate = latestAllowedBookingDate.AddDays(1);
                    now = now.AddDays(1);
                }
            }
            else if (now.TimeOfDay > sameDayCutoff)
            {
                latestAllowedBookingDate = DateOnly.FromDateTime(now.Date.AddDays(1));
            }

            return latestAllowedBookingDate;
        }

        private static bool IsWeekend(DateTime date)
        {
            TimeSpan sameDayCutoff = new TimeSpan(SameDayCutoffHour, 0, 0);
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday ||
                   (date.DayOfWeek == DayOfWeek.Friday && date.TimeOfDay > sameDayCutoff);
        }
    }
}