using server.Models.Domain;
using server.Models.DTOs;
using server.Models.DTOs.Response;

namespace server.Helpers
{
    public static class Mappers
    {
        public static List<MyBookingsResponse> MapMyBookingsResponse(IEnumerable<Booking> bookings)
        {
            var myBookingsResponseList = new List<MyBookingsResponse>();
            try
            {
                if (bookings == null)
                {
                    throw new ArgumentNullException(nameof(bookings));
                }

                myBookingsResponseList = bookings.Select(booking =>
                    new MyBookingsResponse(
                        booking.Id,
                        booking.UserId,
                        booking.SeatId,
                        booking.BookingDateTime,
                        booking.Seat.RoomId
                    )
                ).ToList();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error mapping MyBookingsResponse: {e.Message}");
            }
            return myBookingsResponseList;
        }

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
