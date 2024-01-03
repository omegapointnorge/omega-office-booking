using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;
using server.Services;

namespace server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("bookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookings()
        {
            var response = await _bookingService.GetAllBookings();
            return new OkObjectResult(response);
        }

        [HttpGet("Bookings/{userId}")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookingsForUser(String userId)
        {
            var response = await _bookingService.GetAllBookingsForUser(userId);
            return new OkObjectResult(response);
        }


        [HttpGet("Bookings/MyBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookingsForCurrentUser()
        {
            var userId = String.Empty;
            if (User.Identity?.IsAuthenticated ?? false)
            {
                userId = User.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value ?? String.Empty;
            };
            var response = await _bookingService.GetAllBookingsForUser(userId);
            return new OkObjectResult(response);
        }
        /// <summary>
        /// Deletes a booking with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the booking to delete.</param>
        /// <returns>The result of the delete operation. 200 for succeed deleting, 404 for not found for the given id. and 500 for generic errors</returns>
        [HttpDelete("Bookings/{id}")]
        public async Task<ActionResult> DeleteBooking(int id)
        {

            var userId = String.Empty;
            if (User.Identity?.IsAuthenticated ?? false)
            {
                userId = User.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid")?.Value ?? String.Empty;
                // TODO ??
            }
            {
                var result = await _bookingService.DeleteBooking(id, userId);
                return result;
            }
        }
    }
}
