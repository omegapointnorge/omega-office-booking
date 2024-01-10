using server.DAL;
using server.DAL.Dto;
using server.DAL.Models;

namespace server.Helpers
{
    public static class Mappers
    {
        public static List<BookingDto> MapBookingDtos(IEnumerable<Booking> bookings)
        {
            var bookingDtoList = new List<BookingDto>();
            try
            {
                bookingDtoList = bookings.Select(booking =>
                    new BookingDto(booking.Id, booking.UserId, booking.SeatId, booking.BookingDateTime)
                ).ToList();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error mapping BookingDtos: {e.Message}");
            }
            return bookingDtoList;
        }

        public static List<SeatDto> MapSeatDtos(IEnumerable<Seat>? seats)
        {
            var seatDtoList = new List<SeatDto>();
            try
            {
                if (seats != null)
                {
                    seatDtoList = seats.Select(seat =>
                        new SeatDto(seat.Id, seat.RoomId, seat.Bookings)
                    ).ToList();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error mapping SeatDtos: {e.Message}");
            }
            return seatDtoList;
        }
    }
}
