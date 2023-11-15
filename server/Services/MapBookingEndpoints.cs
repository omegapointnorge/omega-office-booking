using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniValidation;
using server.Data;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

public static class WebApplicationBookingExtensions
{
    public static void MapBookingEndpoints(this WebApplication app)
    {
        app.MapGet("/api/user/{userId:int}/Bookings", async (int userId, 
            IUserRepository userRepo, IBookingRepository BookingRepo) =>
        {
            if (await userRepo.Get(userId) == null)
                return Results.Problem($"userId with Id {userId} not found", statusCode: 404);
            var Bookings = await BookingRepo.GetBookingfromUserId(userId);
            return Results.Ok(Bookings);
        }).ProducesProblem(404).Produces(StatusCodes.Status200OK);

        app.MapPost("/api/user/{userId:int}/Bookings", async (int userId, [FromBody] BookingDto dto, IBookingRepository repo) => 
        {
            if (dto.UserId != userId)
                return Results.Problem($"seat Id of DTO {dto.UserId} doesn't match with URL data {userId}",
                    statusCode: StatusCodes.Status400BadRequest);
            if (!MiniValidator.TryValidate(dto, out var errors))
                return Results.ValidationProblem(errors);
            var newBooking = await repo.Add(dto);
            return Results.Created($"/Users/{newBooking.UserId}/Bookings", newBooking);
        }).ProducesValidationProblem().ProducesProblem(400).Produces<BookingDto>(StatusCodes.Status201Created);
    }
}