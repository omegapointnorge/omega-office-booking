using server.Models.Domain;
using server.Models.DTOs;

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
                    new BookingDto(booking)
                ).ToList();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error mapping BookingDtos: {e.Message}");
            }
            return bookingDtoList;
        }

        public static List<HistoryBookingDto> MapHistoryBookingDtos(IEnumerable<Booking> bookings)
        {
            var historyBookingDtoList = new List<HistoryBookingDto>();
            try
            {
                historyBookingDtoList = bookings.Select(booking =>
                    new HistoryBookingDto(booking)
                ).ToList();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error mapping BookingDtos: {e.Message}");
            }
            return historyBookingDtoList;
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