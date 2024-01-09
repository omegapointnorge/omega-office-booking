using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs;
using server.Models.DTOs.Request;
using server.Models.DTOs.Response;
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
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllFutureBookings()
        {
            var response = await _bookingService.GetAllFutureBookings();
            return new OkObjectResult(response);
        }

        [HttpGet("Bookings/MyPreviousBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllPreviousBookingsForCurrentUser(int itemCount, int pageNumber)
        {
            var userId = String.Empty;
            if (User.Identity?.IsAuthenticated ?? false)
            {
                userId = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value ?? String.Empty;
            };
            var response = await _bookingService.GetPreviousBookingsForUser(userId, itemCount, pageNumber);
            return new OkObjectResult(response);
        }

        [HttpGet("Bookings/MyPreviousBookingsCount")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetCurrentUserPreviousBookingCount()
        {
            var userId = String.Empty;
            if (User.Identity?.IsAuthenticated ?? false)
            {
                userId = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value ?? String.Empty;
            };
            var response = await _bookingService.GetPreviousBookingCountForUser(userId);
            return new OkObjectResult(response);
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateBookingResponse>> CreateBooking(CreateBookingRequest bookingRequest)
        {
            try
            {
                var user = GetUser();
                var booking = await _bookingService.CreateBookingAsync(bookingRequest, user);

                return Ok(booking);
            }
            catch (Exception ex)
            {
                // Log the exception, handle the error appropriately
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpGet("Bookings/{userId}")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookingsForUser(String userId)
        {
            var response = await _bookingService.GetAllFutureBookingsForUser(userId);
            return new OkObjectResult(response);
        }


        [HttpGet("Bookings/MyBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookingsForCurrentUser()
        {
            var userId = String.Empty;
            if (User.Identity?.IsAuthenticated ?? false)
            {
                userId = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value ?? String.Empty;
            };
            var response = await _bookingService.GetAllFutureBookingsForUser(userId);
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
                userId = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? String.Empty;
                // TODO ??
            }
            {
                var result = await _bookingService.DeleteBooking(id, userId);
                return result;
            }
        }

        private User GetUser()
        {
            var userId = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? String.Empty;
            var email = User.FindFirst("preferred_username")?.Value ?? String.Empty;
            var name = User.FindFirst("name")?.Value ?? String.Empty;

            return new User(userId, name, email);
        }

    }
}
