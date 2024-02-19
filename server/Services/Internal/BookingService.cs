using Google.Api;
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
                var booking = CreateBookingFromRequest(bookingRequest, user, user.UserName);

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
                IEnumerable<Booking> existedBookingList = await _bookingRepository.GetAsync();
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
                        if (IsSeatAlreadyBooked(existedBookingList, bookingRequest.BookingDateTime, seatId))
                        {
                            //In case of double booked or deleting issues
                            var bookingsToDeleteList = existedBookingList
                            .Where(booking => booking.BookingDateTime.Date == bookingRequest.BookingDateTime.Date && booking.SeatId == seatId)
                            .ToList();

                            bookingsToDeleteList.ForEach(async bookingToDelete => await _bookingRepository.Delete(bookingToDelete));
                        }

                        var booking = CreateBookingFromRequest(bookingRequest, user, EventUserName, seatId);

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
        private Booking CreateBookingFromRequest(CreateBookingRequest bookingRequest, UserClaims user, string userName, int? seatId = null)
        {
            return new Booking()
            {
                UserId = user.Objectidentifier,
                UserName = userName,
                SeatId = seatId ?? bookingRequest.SeatId,
                BookingDateTime = bookingRequest.BookingDateTime,
                BookingDateTime_DayOnly = bookingRequest.BookingDateTime.Date,
            };
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

                await _bookingRepository.Delete(booking);
                await _bookingRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                throw new Exception("An error occurred while deleting the booking.", ex);
            }
        }

        private List<string> ValidateUserBookingRequest(CreateBookingRequest bookingRequest, IEnumerable<Booking> bookingList, UserClaims user)
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


        private List<string> ValidateUserDeleteBookingRequest(Booking? booking, UserClaims user)
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


        private bool HasUserAlreadyBookedForDay(IEnumerable<Booking> bookingList, DateTime bookingDate, string userId)
        {
            var dateOfBookingDate = bookingDate.Date;
            return bookingList.Any(booking => booking.BookingDateTime.Date == dateOfBookingDate && booking.UserId == userId);
        }

        private bool IsSeatAlreadyBooked(IEnumerable<Booking> bookingList, DateTime bookingDate, int seatId)
        {
            var dateOfBookingDate = bookingDate.Date;
            return bookingList.Any(booking => booking.BookingDateTime.Date == dateOfBookingDate && booking.SeatId == seatId);
        }

        private bool isEventAdmin(UserClaims user)
        {
            return user.Role == "EventAdmin";
        }
    }

}