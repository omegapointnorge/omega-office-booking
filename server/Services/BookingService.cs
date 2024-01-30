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
        private const string EventUserName = "Event";


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

                string validationError = ValidateUserBookingRequest(bookingRequest, bookingList, user.UserId);
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

        public async Task<ActionResult<IEnumerable<BookingDto>>> CreateEventBookingsForSeatsAsync(CreateBookingRequest bookingRequest, User user)
        {
            try
            {
                IEnumerable<Booking> bookingList = await _bookingRepository.GetAsync();
                List<BookingDto> bookingListDto = new();
                if (bookingRequest.IsEvent.HasValue && bookingRequest.IsEvent.Value)
                {
                    if (bookingRequest.SeatList == null || bookingRequest.SeatList.Count == 0)
                    {
                        throw new Exception("Seat list cannot be null or 0 element");
                    }
                    foreach (var seatId in bookingRequest.SeatList)
                    {
                        var booking = new Booking
                        {
                            UserId = user.UserId,
                            UserName = EventUserName,
                            SeatId = seatId,
                            BookingDateTime = bookingRequest.BookingDateTime
                        };

                        await _bookingRepository.AddAsync(booking);
                        bookingListDto.Add(new BookingDto(booking));
                    }
                    // only save repository once
                    await _bookingRepository.SaveAsync();

                }
                else throw new Exception("No value for seat list for booking Req");
                return bookingListDto;
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

        private static string ValidateUserBookingRequest(CreateBookingRequest bookingRequest, IEnumerable<Booking> bookingList, string userId)
        {
            if (DateOnly.FromDateTime(bookingRequest.BookingDateTime) > GetLatestAllowedBookingDate() && bookingRequest.IsEvent == null)
            {
                return "Booking date exceeds the latest allowed booking date.";
            }

            if (IsSeatAlreadyBooked(bookingList, bookingRequest.BookingDateTime, bookingRequest.SeatId))
            {
                var seatId = bookingRequest.SeatId;
                return $"Seat {seatId} is already booked for the specified time.";
            }

            if (HasUserAlreadyBookedForDay(bookingList, bookingRequest.BookingDateTime, userId))
            {
                return "User has already booked for the specified day.";
            }

            return null; // Validation passed
        }


        private static bool HasUserAlreadyBookedForDay(IEnumerable<Booking> bookingList, DateTime bookingDate, string userId)
        {
            var dateOfBookingDate = bookingDate.Date;
            return bookingList.Any(booking => booking.BookingDateTime.Date == dateOfBookingDate && booking.UserId == userId);
        }

        private static bool IsSeatAlreadyBooked(IEnumerable<Booking> bookingList, DateTime bookingDate, int seatId)
        {
            var dateOfBookingDate = bookingDate.Date;
            return bookingList.Any(booking => booking.BookingDateTime.Date == dateOfBookingDate && booking.SeatId == seatId);
        }


        private static DateOnly GetLatestAllowedBookingDate()
        {
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            DateTime now = TimeZoneInfo.ConvertTime(DateTime.Now, targetTimeZone);
            DateOnly latestAllowedBookingDate = DateOnly.FromDateTime(now);
            TimeSpan sameDayCutoff = new TimeSpan(SameDayCutoffHour, 0, 0);

            if (IsWeekend(now) || now.TimeOfDay > sameDayCutoff)
            {
                latestAllowedBookingDate = GetNextWeekday(latestAllowedBookingDate);
            }
            return latestAllowedBookingDate;
        }

        private static bool IsWeekend(DateTime date)
        {
            TimeSpan sameDayCutoff = new TimeSpan(SameDayCutoffHour, 0, 0);
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday ||
                   (date.DayOfWeek == DayOfWeek.Friday && date.TimeOfDay > sameDayCutoff);
        }

        private static DateOnly GetNextWeekday(DateOnly date)
        {
            DateOnly nextDay = date.AddDays(1);
            while (nextDay.DayOfWeek == DayOfWeek.Saturday || nextDay.DayOfWeek == DayOfWeek.Sunday)
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }
    }
}