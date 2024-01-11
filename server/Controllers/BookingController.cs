using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs;
using server.Models.DTOs.Request;
using server.Models.DTOs.Response;
using server.Request;
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
            var result = await _bookingService.GetAllFutureBookings();

            if (result.Value.IsSuccess)
                return new OkObjectResult(result.Value.BookingDto);
            return StatusCode(500);
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateBookingResponse>> CreateBooking(CreateBookingRequest bookingRequest)
        {
            try
            {
                var user = GetUser();
                var booking = await _bookingService.CreateBookingAsync(bookingRequest, user);
                
                return CreatedAtRoute(null, booking);
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
            var result = await _bookingService.GetAllBookingsForUser(userId);
            if (result.Value.IsSuccess)
                return new OkObjectResult(result.Value.BookingDto);
            return StatusCode(500);
        }


        [HttpGet("Bookings/MyBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookingsForCurrentUser()
        {
            var userId = String.Empty;
            if (User.Identity?.IsAuthenticated ?? false)
            {
                userId = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? String.Empty;
            };
            var result = await _bookingService.GetAllBookingsForUser(userId);
            if (result.Value.IsSuccess)
                return new OkObjectResult(result.Value.BookingDto);
            return StatusCode(500);
        }

        /// <summary>
        /// Deletes a booking with the specified ID.
        /// </summary>
        /// <param name="bookingId">The ID of the booking to delete.</param>
        /// <returns>The result of the delete operation. 200 for succeed deleting, 404 for not found for the given id. and 500 for generic errors</returns>
        [HttpDelete("{bookingId}")]
        public async Task<ActionResult> DeleteBookingAsync(int bookingId)
        {
            if (!(User.Identity?.IsAuthenticated ?? false))
            {
                return Unauthorized();
            }

            try
            {
                var user = GetUser();
                var deleteResponse = await _bookingService.DeleteBookingAsync(bookingId, user.Id);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred processing your request.");
            }
        }


        private User GetUser() {
            var userId = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? String.Empty;
            var email = User.FindFirst("preferred_username")?.Value?? String.Empty;
            var name = User.FindFirst("name")?.Value?? String.Empty;

            return new User(userId, name, email);
        }

    }
}
