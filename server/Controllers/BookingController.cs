using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using server.Helpers;
using server.Models.DTOs;
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
                var userClaim = UserUtils.GetCurrentUserClaims(User);
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
                            TrackReCAPTCHATokenError($"The reCAPTCHA score is {score} for {userClaim.UserName}. This is below the threshold of {RecaptchaEnterprise.ReCaptchaThreshold}");
                            throw new InvalidOperationException("The reCAPTCHA score is below the threshold. Please log out and log in again to verify your identity.");
                        }
                    }
                    catch (Exception error)
                    {
                        TrackReCAPTCHATokenError($"{userClaim.UserName}: {error.ToString()}");
                    }

                }

                var booking = await _bookingService.CreateBookingAsync(bookingRequest, userClaim);

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
                var userClaim = UserUtils.GetCurrentUserClaims(User);
                var result = await _bookingService.GetAllBookingsForUserAsync(userClaim.Objectidentifier);

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
                var userClaim = UserUtils.GetCurrentUserClaims(User);
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