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
        private const string EventUserName = "Event";


        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingDto> CreateBookingAsync(CreateBookingRequest bookingRequest, UserClaims user)
        {
            try
            {
                IEnumerable<Booking> bookingList = await _bookingRepository.GetAsync();

                var validationErrors = ValidateUserBookingRequest(bookingRequest, bookingList, user);
                if (validationErrors != null && validationErrors.Any())
                {
                    var errorMessages = string.Join(Environment.NewLine, validationErrors.Select(v => $"- {v}"));
                    throw new Exception(errorMessages);
                }
                var booking = new Booking
                {
                    UserId = user.Objectidentifier,
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

        public async Task<IEnumerable<BookingDto>> CreateEventBookingsForSeatsAsync(CreateBookingRequest bookingRequest, UserClaims user)
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
                            UserId = user.Objectidentifier,
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


        public async Task<IEnumerable<BookingDto>> GetAllActiveBookings()
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


        public async Task<IEnumerable<BookingDto>> GetAllBookingsForUser(string userId)
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


        public async Task DeleteBookingAsync(int bookingId, UserClaims user)
        {
            try
            {
                var booking = await _bookingRepository.GetAsync(b => b.Id == bookingId);

                var validationErrors = ValidateUserDeleteBookingRequest(booking, user);
                if (validationErrors != null && validationErrors.Any())
                {
                    var errorMessages = string.Join(Environment.NewLine, validationErrors.Select(v => $"- {v}"));
                    throw new Exception(errorMessages);
                }

                await _bookingRepository.DeleteAndCommit(booking);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                throw new Exception("An error occurred while deleting the booking.", ex);
            }
        }

        public static List<string> ValidateUserBookingRequest(CreateBookingRequest bookingRequest, IEnumerable<Booking> bookingList, UserClaims user)
        {
            List<string> validationResultsList = new();
            if (isEventAdmin(user))
            {
                return validationResultsList;
            }

            if (DateOnly.FromDateTime(bookingRequest.BookingDateTime) > BookingTimeUtils.GetLatestAllowedBookingDate())
            {
                validationResultsList.Add("Booking date exceeds the latest allowed booking date.");
            }

            if (IsSeatAlreadyBooked(bookingList, bookingRequest.BookingDateTime, bookingRequest.SeatId))
            {
                var seatId = bookingRequest.SeatId;
                validationResultsList.Add($"Seat {seatId} is already booked for the specified time.");
            }

            if (HasUserAlreadyBookedForDay(bookingList, bookingRequest.BookingDateTime, user.Objectidentifier))
            {
                validationResultsList.Add("User has already booked for the specified day.");
            }

            return validationResultsList;
        }


        public static List<string> ValidateUserDeleteBookingRequest(Booking? booking, UserClaims user)
        {
            List<string> validationResultsList = new();

            if (booking == null)
            {
                validationResultsList.Add($"Booking with {booking.Id} not found");
            }

            if (isEventAdmin(user))
            {
                return validationResultsList;
            }

            if (booking.UserId != user.Objectidentifier)
            {
                validationResultsList.Add("User is not authorized to delete the booking.");
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

        private static bool isEventAdmin(UserClaims user)
        {
            return user.Role == "EventAdmin";
        }
    }
}