using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;
using server.Repository;

namespace server.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;
        public BookingService(IBookingRepository bookingRepository, IUserRepository userRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
        }

        public async Task<ActionResult<List<BookingDetailsDto>>> GetAllBookings()
        {
            var bookingDtos = await _bookingRepository.GetAllBookings();
            return await ProcessBookingDetails(bookingDtos);
        }

        public async Task<ActionResult<List<BookingDetailsDto>>> GetAllBookingsForUser(string userId)
        {
            var bookingDtos = await _bookingRepository.GetAllBookingsForUser(userId);
            return await ProcessBookingDetails(bookingDtos);
        }

        private async Task<List<BookingDetailsDto>> ProcessBookingDetails(List<BookingDto> bookingDtos)
        {
            var rooms = await _roomRepository.GetAllRooms();

            foreach (var room in rooms)
            {
                room.Seats = await _roomRepository.GetAllSeatsForRoom(room.Id);
            }

            List<BookingDetailsDto> bookingDetailsDtos = new List<BookingDetailsDto>();

            foreach (var booking in bookingDtos)
            {
                int seatId = booking.SeatId;
                var roomName = rooms.Find(room => room.Seats.Exists(seat => seat.Id == seatId))?.Name;
                bookingDetailsDtos.Add(new BookingDetailsDto(booking, roomName));
            }

            return bookingDetailsDtos;
        }

        public async Task<ActionResult> DeleteBooking(int bookingId, String userId)
        {
            bool isThisBookingBelongToCurrentUser = _userRepository.GetBookingByUserIdAndBookingId(bookingId, userId) != null;
            if (isThisBookingBelongToCurrentUser) return await _bookingRepository.DeleteBooking(bookingId);
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
