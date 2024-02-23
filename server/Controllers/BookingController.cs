using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using server.Helpers;
using server.Models.DTOs;
using server.Models.DTOs.Internal;
using server.Models.DTOs.Request;
using server.Services.Internal;

namespace server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly TelemetryClient _telemetryClient;
        private readonly RecaptchaEnterprise _recaptchaEnterprise;


        public BookingController(IBookingService bookingService, TelemetryClient telemetryClient, RecaptchaEnterprise recaptchaEnterprise)
        {
            _bookingService = bookingService;
            _recaptchaEnterprise = recaptchaEnterprise;
            _telemetryClient = telemetryClient;
        }

        [HttpGet("activeBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllActiveBookings()
        {
            try
            {
                var result = await _bookingService.GetAllActiveBookings();
                return new OkObjectResult(result);
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
                if (string.IsNullOrWhiteSpace(bookingRequest.reCAPTCHAToken))
                {
                    TrackReCAPTCHATokenError("reCAPTCHA token is missing or empty.");
                }
                else
                {
                    try
                    {
                        var score = _recaptchaEnterprise.CreateAssessment(bookingRequest);
                        if (score < RecaptchaEnterprise.ReCaptchaThreshold)
                        {
                            TrackReCAPTCHATokenError($"The reCAPTCHA score is {score}. This is below the threshold of {RecaptchaEnterprise.ReCaptchaThreshold}");
                        }
                    }
                    catch (Exception error)
                    {
                        TrackReCAPTCHATokenError(error.ToString());
                    }

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

        [HttpGet("Bookings/MyBookings")]
        public async Task<ActionResult<IEnumerable<HistoryBookingDto>>> GetAllBookingsForUser()
        {
            try
            {
                var user = GetUser();
                var result = await _bookingService.GetAllBookingsForUserAsync(user.Objectidentifier);

                return new OkObjectResult(result);
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
                var userClaim = GetUser();
                await _bookingService.DeleteBookingAsync(bookingId, userClaim);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred processing your request." + ex.Message);
            }
        }

        [HttpGet("OpeningTime")]
        public ActionResult<string> GetOpeningTime()
        {
            try
            {
                var openingTime = BookingTimeUtils.GetOpeningTime();
                return Ok(openingTime);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred processing your request." + ex.Message);
            }
        }
        private UserClaims GetUser()
        {
            var id = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? String.Empty;
            var name = User.FindFirst("name")?.Value ?? String.Empty;
            var role = User.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value ?? String.Empty;

            UserClaims user = new(name, id, role);
            return user;

        }
        // Helper method to track the event
        private void TrackReCAPTCHATokenError(string errorMessage)
        {
            var eventData = new Dictionary<string, string>
            {
                { "ReCAPTCHATokenError", errorMessage }
            };
            _telemetryClient.TrackEvent("ReCAPTCHATokenError", eventData);
        }
    }
}