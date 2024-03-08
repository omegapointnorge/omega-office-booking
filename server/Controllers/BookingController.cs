using AzureFunctionsD365.Service;
using Google.Apis.Auth.OAuth2;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
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
        private readonly GraphServiceClient _graphServiceClient;
        private readonly MsalLoginService _msalLoginService;

        public BookingController(IBookingService bookingService, TelemetryClient telemetryClient, RecaptchaEnterprise recaptchaEnterprise, GraphServiceClient graphServiceClient, MsalLoginService msalLoginService)
        {
            _bookingService = bookingService;
            _recaptchaEnterprise = recaptchaEnterprise;
            _graphServiceClient = graphServiceClient;
            _telemetryClient = telemetryClient;
            _msalLoginService = msalLoginService;
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
                string resourceUri = $"https://graph.microsoft.com";

                WebApiConfiguration allConfig = new WebApiConfiguration(_msalLoginService.ClientId, _msalLoginService.ClientSecret, _msalLoginService.TenantId, resourceUri, "https://graph.microsoft.com/v1.0/users");
                string email = "nils.olav.johansen@omegapoint.no";

                var response = MsalLoginService.RetrieveUsers(email, allConfig).Result;
                if (response.IsSuccessStatusCode)
                {
                    //log.LogInformation("The request IsSuccessStatusCode");
                    // Read the content as a string
                    string jsonContent = await response.Content.ReadAsStringAsync();

                    //dynamic jsonObject = JObject.Parse(jsonContent);

                    // Access the 'id' property of the first item in the 'value' array
                    //string userId = jsonObject.value[0].id;
                    return new OkObjectResult(jsonContent);
                }
                else
                {
                    //log.LogInformation("The request failed ",
                        //response.ReasonPhrase);

                    throw new Exception();
                }


               
                var result = await _graphServiceClient.Users.GetAsync(rc =>
                {
                    rc.QueryParameters.Filter = $"mail eq '{email}'";
                });

                string logMessage = result?.Value != null && result.Value.Any()
    ? $"From {email} found {result.Value[0].DisplayName} with id {result.Value[0].Id}"
    : $"No user found with email {email}.";

                var eventData = new Dictionary<string, string>
            {
                { "Background", logMessage }
                };

                _telemetryClient.TrackEvent("Background", eventData);


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