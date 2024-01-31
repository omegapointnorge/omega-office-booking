using Microsoft.AspNetCore.Mvc;
using server.Helpers;
using server.Models.Domain;
using server.Models.DTOs;
using server.Models.DTOs.Internal;
using server.Models.DTOs.Request;
using server.Repository;
using System.ComponentModel.DataAnnotations;

namespace server.Services.Internal
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

                var validationErrors = ValidateUserBookingRequest(bookingRequest, bookingList, user.UserId);
                if (validationErrors != null && validationErrors.Any())
                {
                    var errorMessages = string.Join(Environment.NewLine, validationErrors.Select(v => $"- {v}"));
                    throw new Exception(errorMessages);
                }
                var booking = new Booking
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    SeatId = bookingRequest.SeatId,
                    BookingDateTime = bookingRequest.BookingDateTime
                };

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
                // Perform model validation
                var validationContext = new ValidationContext(bookingRequest, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(bookingRequest, validationContext, validationResults, validateAllProperties: true);
                if (!isValid)
                {
                    var errorMessages = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                    throw new BadHttpRequestException(errorMessages);
                }

                if (bookingRequest.IsEvent.HasValue && bookingRequest.IsEvent.Value && bookingRequest.SeatList.Any())
                {
                    foreach (var seatId in bookingRequest.SeatList)
                    {
                        if (IsSeatAlreadyBooked(bookingList, bookingRequest.BookingDateTime, seatId))
                        {
                            var bookingToDelete = bookingList.First(booking => booking.BookingDateTime.Date == bookingRequest.BookingDateTime.Date && booking.SeatId == seatId);
                            await _bookingRepository.DeleteAndCommit(bookingToDelete);
                        }

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
                else throw new Exception("Not a Event booking Req");
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


        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookingsForUser(string userId)
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

        private static List<string> ValidateUserBookingRequest(CreateBookingRequest bookingRequest, IEnumerable<Booking> bookingList, string userId)
        {
            List<string> validationResultsList = new();
            if (DateOnly.FromDateTime(bookingRequest.BookingDateTime) > GetLatestAllowedBookingDate())

            {
                validationResultsList.Add("Booking date exceeds the latest allowed booking date.");
            }

            if (IsSeatAlreadyBooked(bookingList, bookingRequest.BookingDateTime, bookingRequest.SeatId))
            {
                var seatId = bookingRequest.SeatId;
                validationResultsList.Add($"Seat {seatId} is already booked for the specified time.");
            }

            if (HasUserAlreadyBookedForDay(bookingList, bookingRequest.BookingDateTime, userId))
            {
                validationResultsList.Add("User has already booked for the specified day.");
            }

            return validationResultsList;
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
                   date.DayOfWeek == DayOfWeek.Friday && date.TimeOfDay > sameDayCutoff;
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