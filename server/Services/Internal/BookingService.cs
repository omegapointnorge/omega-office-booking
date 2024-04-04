using Microsoft.IdentityModel.Tokens;
using server.Helpers;
using server.Models.Domain;
using server.Models.DTOs;
using server.Models.DTOs.Internal;
using server.Models.DTOs.Request;
using server.Repository;
using server.Services.External;

namespace server.Services.Internal
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRepository<JobExecutionLog> _jobExecutionRepository;

        private readonly ITelemetryService _telemetryClient;

        public BookingService(IBookingRepository bookingRepository, ITelemetryService telemetryClient, IRepository<JobExecutionLog> jobExecutionLogRepository)
        {
            _bookingRepository = bookingRepository;
            _telemetryClient = telemetryClient;
            _jobExecutionRepository = jobExecutionLogRepository;
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

        // in this step if any error, we also creat a _jobExecutionLogRepository item
        public async Task CreateRecurringBookingAsync(IEnumerable<SeatAllocationDetails> seatAssignmentDetails, DateTime bookingDateTime)
        {
            var errorMessage = String.Empty;
            JobExecutionLog job;
            foreach (var seatAssignmentDetail in seatAssignmentDetails)
            {
                var existingBooking = await _bookingRepository.GetBookingBySeatIdAndDateTime(seatAssignmentDetail.SeatId, bookingDateTime.Date);
                if (existingBooking != null)
                {
                    errorMessage += $"Backgroud process: Seat {seatAssignmentDetail.SeatId} is already booked for the specified time {existingBooking.BookingDateTime_DayOnly}.";
                    continue;
                }
                var booking = new Booking(seatAssignmentDetail.User.Objectidentifier, seatAssignmentDetail.User.UserName, seatAssignmentDetail.SeatId, bookingDateTime);
                await _bookingRepository.AddAsync(booking);
            }
            if (errorMessage != String.Empty)
            {
                job = new JobExecutionLog(nameof(CreateRecurringBookingAsync), DateTime.Now, JobExecutionStatus.Failed.ToString(), errorMessage);
            }
            else
            {
                job = new JobExecutionLog(nameof(CreateRecurringBookingAsync), DateTime.Now, JobExecutionStatus.Succeeded.ToString(), null);
            }
            await _jobExecutionRepository.AddAsync(job);
            await _bookingRepository.SaveAsync();
        }

        public async Task InitiateSeatAllocationAsync(IEnumerable<SeatAllocationDetails> seatAssignmentDetails, DateTime bookingDateTime)
        {
            var errorMessage = String.Empty;
            JobExecutionLog job;
            try
            {
                var existedJob = await _jobExecutionRepository.GetAllAsync(j => j.JobName == nameof(InitiateSeatAllocationAsync) && j.Status == JobExecutionStatus.Succeeded.ToString());
                if (existedJob.IsNullOrEmpty())
                {
                    DateTime today = DateTime.Today;

                    List<DateTime> daysBetween = BookingTimeUtils.GetDaysBetween(today, bookingDateTime);

                    foreach (DateTime day in daysBetween)
                    {
                        foreach (var seatAssignmentDetail in seatAssignmentDetails)
                        {
                            var existingBooking = await _bookingRepository.GetBookingBySeatIdAndDateTime(seatAssignmentDetail.SeatId, day.Date);
                            if (existingBooking != null)
                            {
                                errorMessage += $"Backgroud process: Seat {seatAssignmentDetail.SeatId} is already booked for the specified time {day.Date}.";
                                continue;
                            }
                            var booking = new Booking(seatAssignmentDetail.User.Objectidentifier, seatAssignmentDetail.User.UserName, seatAssignmentDetail.SeatId, day);
                            await _bookingRepository.AddAsync(booking);
                        }
                    }

                    job = new JobExecutionLog(nameof(InitiateSeatAllocationAsync), DateTime.Now, JobExecutionStatus.Succeeded.ToString(), null);
                    await _jobExecutionRepository.AddAsync(job);
                    await _bookingRepository.SaveAsync();

                }
       
            }
            catch (Exception ex)
            {
                if (errorMessage != String.Empty)
                {
                    job = new JobExecutionLog(nameof(InitiateSeatAllocationAsync), DateTime.Now, JobExecutionStatus.Failed.ToString(), errorMessage += ex.ToString());
                }
                else
                {
                    job = new JobExecutionLog(nameof(InitiateSeatAllocationAsync), DateTime.Now, JobExecutionStatus.Failed.ToString(), ex.Message);
                }
                await _jobExecutionRepository.AddAsync(job);
                await _bookingRepository.SaveAsync();
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
                var eventName = group.First().EventName;

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

        public static bool IsSeatAlreadyBooked(IEnumerable<Booking> bookingList, DateTime bookingDate, int seatId)
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