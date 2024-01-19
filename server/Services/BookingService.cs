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

                if (CanBookSeatAndUser(bookingList, bookingRequest, user.UserId))
                {
                    await _bookingRepository.AddAsync(booking);
                    await _bookingRepository.SaveAsync();
                    var createBookingResponse = new BookingDto(booking);

                    return createBookingResponse;
                }
                else throw new Exception("The seat may have been taken while you are booking, try again if you don't have a booking for the same day.");

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

        private static bool CanBookSeatAndUser(IEnumerable<Booking> bookingList, CreateBookingRequest bookingRequest, String userId)
        {
            var bookingDateTime = bookingRequest.BookingDateTime;
            // Check if the seat already has a booking on the same day,as well as user,
            return !bookingList.Any(booking => booking.BookingDateTime.Date == bookingDateTime.Date && (booking.SeatId == bookingRequest.SeatId || booking.UserId == userId));
        }
    }
}