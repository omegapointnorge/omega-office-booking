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
                myBookingsResponseList = bookings.Select(booking =>
                    new MyBookingsResponse(booking)
                ).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred with {nameof(MapMyBookingsResponse)}: {ex.Message}", ex);
            }
            return myBookingsResponseList;
        }

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

        public static List<RoomDto> MapRoomDtos(IEnumerable<Room> rooms)
        {
            var roomDtos = new List<RoomDto>();
            try
            {
                roomDtos = rooms.Select(room =>
                    new RoomDto(room.Id, room.Name, room.Seats)
                ).ToList();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error mapping BookingDtos: {e.Message}");
            }
            return roomDtos;
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
