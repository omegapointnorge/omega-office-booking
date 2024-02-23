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
        private readonly IBookingRepository _bookingRepository;
        private readonly IEventRepository _eventRepository;
        private const string EventUserName = "Event";


        public BookingService(IBookingRepository bookingRepository, IEventRepository eventRepository)
        {
            _bookingRepository = bookingRepository;
            _eventRepository = eventRepository;
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

                // Create Event entity
                var eventData = new Event { Name = EventUserName };
                await _eventRepository.AddAsync(eventData);

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


                        //Create booking entity
                        var booking = CreateBookingFromRequest(bookingRequest, user, user.UserName, seatId);
                        booking.Event = eventData;

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
            var userId = user.Objectidentifier;
            var user_Name = userName;
            var seat_Id = seatId ?? bookingRequest.SeatId;
            var bookingDateTime = bookingRequest.BookingDateTime;
            var bookingDateTime_DayOnly = bookingRequest.BookingDateTime.Date;
            return new Booking(userId, user_Name, seat_Id, bookingDateTime, bookingDateTime_DayOnly);
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

        public async Task<IEnumerable<HistoryBookingDto>> GetAllBookingsForUserAsync(string userId)
        {
            try
            {
                var bookings = await _bookingRepository.GetBookingsWithSeatForUserAsync(userId);
                var historyBookingDtos = Mappers.MapHistoryBookingDtos(bookings);

                var returnList = new List<HistoryBookingDto>();

                // Add bookings with null EventId directly to the return list
                returnList.AddRange(historyBookingDtos.Where(bookingDto => bookingDto.EventId == null));


                var groupedBookings = groupAndFilterEventBookings(historyBookingDtos);

                // Add the grouped bookings as a single event to the return list
                returnList.AddRange(groupedBookings);

                return returnList.OrderBy(bookingDto => bookingDto.BookingDateTime).ToList();

            }
            catch
            {
                throw;
            }
        }

        private IEnumerable<HistoryBookingDto> groupAndFilterEventBookings(IEnumerable<HistoryBookingDto> bookingDtos)
        {
            var groupedBookings = bookingDtos.Where(bookingDto => bookingDto.EventId != null)
                                              .GroupBy(b => b.EventId);
            var combinedBookings = new List<HistoryBookingDto>();

            foreach (var group in groupedBookings)
            {
                var combinedSeatIds = group.SelectMany(HistoryBookingDto => HistoryBookingDto.SeatIds).ToList().ToArray();
                var combinedRoomIds = group.SelectMany(HistoryBookingDto => HistoryBookingDto.RoomIds).Distinct().ToList().ToArray();
                var bookingDateTime = group.First().BookingDateTime;
                int eventId = (int)group.Key;
                //var eventName = group.First().EventName;
                var eventName = "Arrangement";

                combinedBookings.Add(new HistoryBookingDto(eventId, combinedSeatIds, combinedRoomIds, eventName, bookingDateTime));
            }
            return combinedBookings;
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