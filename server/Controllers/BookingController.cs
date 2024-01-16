using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;
using server.Models.DTOs.Internal;
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
            var result = await _bookingService.GetAllBookings();

            if (result.Value.IsSuccess)
                return new OkObjectResult(result.Value.BookingDto);
            return StatusCode(500);
        }

        [HttpGet("Bookings/MyPreviousBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetPreviousBookingsForUser()
        {
            try
            {
                var user = GetUser();
                var result = await _bookingService.GetPreviousBookingsForUser(user.UserId);
                return new OkObjectResult(result.Value.BookingDto);
            }
            catch (Exception ex)
            {
                // Log the exception, handle the error appropriately
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        
        [HttpPost("create")]
        public async Task<ActionResult<CreateBookingResponse>> CreateBooking(CreateBookingRequest bookingRequest)
        {
            try
            {
                //TODO delete User.Identity?.IsAuthenticated since this is not necessary
                if (User.Identity?.IsAuthenticated ?? false)
                {
                    var user = GetUser();
                    var booking = await _bookingService.CreateBookingAsync(bookingRequest, user.UserId);

                    return CreatedAtRoute(null, booking);
                }
                return StatusCode(500, "User is not Authenticated.");
            }
            catch (Exception ex)
            {
                // Log the exception, handle the error appropriately
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }


        [HttpGet("Bookings/MyActiveBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetActiveBookingsForUser()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                var user = GetUser();
                var result = await _bookingService.GetActiveBookingsForUser(user.UserId);
                if (result.Value.IsSuccess)
                    return new OkObjectResult(result.Value.BookingDto);
            };
            
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
            try
            {
                var deleteResponse = await _bookingService.DeleteBookingAsync(bookingId);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred processing your request.");
            }
        }
        private User GetUser()
        {
            var id = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? String.Empty;
            var name = User.FindFirst("name")?.Value ?? String.Empty;
            User user = new(name, id);
            return user;

        }
    }
}
