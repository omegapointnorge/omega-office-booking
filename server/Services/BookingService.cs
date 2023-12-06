
using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;
using server.Repository;

namespace server.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<ActionResult<List<BookingDto>>> GetAllBookings()
        {
            return await _bookingRepository.GetAllBookings();
        }

        public async Task<ActionResult<List<BookingDto>>> GetAllBookingsForPerson(int userid)
        {
            return await _bookingRepository.GetAllBookingsForPerson(userid);
        }

        public async Task<Microsoft.AspNetCore.Mvc.ActionResult> DeleteBooking(int id)
        {

            return await _bookingRepository.DeleteBooking(id);
        }
    }
}
