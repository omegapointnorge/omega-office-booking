using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniValidation;
using server.Data;

public static class WebApplicationBookingExtensions
{
    public static void MapBookingEndpoints(this WebApplication app)
    {
        app.MapGet("/seat/{seatId:int}/Bookings", [Authorize]async (int seatId, 
            ISeatRepository seatRepo, IBookingRepository BookingRepo) =>
        {
            if (await seatRepo.Get(seatId) == null)
                return Results.Problem($"seat with Id {seatId} not found", statusCode: 404);
            var Bookings = await BookingRepo.Get(seatId);
            return Results.Ok(Bookings);
        }).ProducesProblem(404).Produces(StatusCodes.Status200OK);

        app.MapPost("/seat/{seatId:int}/Bookings", [Authorize]async (int seatId, [FromBody] BookingDto dto, IBookingRepository repo) => 
        {   
            if (dto.SeatId != seatId)
                return Results.Problem($"seat Id of DTO {dto.SeatId} doesn't match with URL data {seatId}", 
                    statusCode: StatusCodes.Status400BadRequest);
            if (!MiniValidator.TryValidate(dto, out var errors))
                return Results.ValidationProblem(errors);
            var newBooking = await repo.Add(dto);
            return Results.Created($"/seats/{newBooking.SeatId}/Bookings", newBooking);
        }).ProducesValidationProblem().ProducesProblem(400).Produces<BookingDto>(StatusCodes.Status201Created);
    }
}