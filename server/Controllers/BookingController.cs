using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs;
using server.Models.DTOs.Internal;
using server.Models.DTOs.Request;
using server.Services;

namespace server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly RecaptchaEnterprise _recaptchaEnterprise;

        public BookingController(IBookingService bookingService, RecaptchaEnterprise recaptchaEnterprise)
        {
            _bookingService = bookingService;
            _recaptchaEnterprise = recaptchaEnterprise;
        }

        [HttpGet("activeBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllActiveBookings()
        {
            try
            {
                var result = await _bookingService.GetAllActiveBookings();
                return new OkObjectResult(result.Value);
            }
            catch (Exception ex)
            {
                // Log the exception, handle the error appropriately
                return StatusCode(500, "An error occurred processing your request." + ex.Message);
            }
        }
        [HttpPost("create")]
        public async Task<ActionResult<BookingDto>> CreateBooking(CreateBookingRequest bookingRequest)
        {
            try
            {
                var score = _recaptchaEnterprise.CreateAssessment(token: bookingRequest.reCAPTCHAToken);
                if (score < RecaptchaEnterprise.ReCaptchaThreshold)
                {
                    throw new Exception("The reCAPTCHA score is below the threshold.");
                }

                var user = GetUser();
                var booking = await _bookingService.CreateBookingAsync(bookingRequest, user);

                return CreatedAtRoute(null, booking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred processing your request." + ex.Message);
            }
        }

        [HttpPost("CreateEventBookingsForSeatsAsync")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> CreateEventBookingsForSeatsAsync(CreateBookingRequest bookingRequest)
        {
            try
            {
                var user = GetUser();
                var bookingDtoList = await _bookingService.CreateEventBookingsForSeatsAsync(bookingRequest, user);

                return CreatedAtRoute(null, bookingDtoList);
            }
            catch (Exception ex)
            {
                // Log the exception, handle the error appropriately
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("Bookings/MyBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookingsForUser()
        {
            try
            {
                var user = GetUser();
                var result = await _bookingService.GetAllBookingsForUser(user.UserId);

                return new OkObjectResult(result.Value);
            }
            catch (Exception ex)
            {
                // Log the exception, handle the error appropriately
                return StatusCode(500, "An error occurred processing your request." + ex.Message);
            }
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
                return StatusCode(500, "An error occurred processing your request." + ex.Message);
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