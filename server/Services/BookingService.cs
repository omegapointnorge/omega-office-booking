using Microsoft.AspNetCore.Mvc;
using server.Helpers;
using server.Models.Domain;
using server.Models.DTOs;
using server.Models.DTOs.Request;
using server.Models.DTOs.Response;
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

        public async Task<ActionResult<CreateBookingResponse>> CreateBookingAsync(CreateBookingRequest bookingRequest, string user)
        {


            var booking = new Booking
            {
                UserId = user,
                SeatId = bookingRequest.SeatId,
                BookingDateTime = DateTime.Now
            };

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveAsync();

            var createBookingResponse = new CreateBookingResponse(booking);

            return createBookingResponse;
        }
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookings()
        {
            try
            {
                IEnumerable<Booking> bookingList = await _bookingRepository.GetAsync();
                var bookingDtoList = Mappers.MapBookingDtos(bookingList);
                return bookingDtoList;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ActionResult<IEnumerable<MyBookingsResponse>>> GetAllBookingsForUser(String userId)
        {
            try
            {
                var bookings = await _bookingRepository.GetBookingsWithSeatForUserAsync(userId);
                return Mappers.MapMyBookingsResponse(bookings);
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
    }
}