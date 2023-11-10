using Microsoft.AspNetCore.Mvc;
using MiniValidation;
using server.Data;

public static class WebApplicationSeatExtensions
{
    public static void MapSeatEndpoints(this WebApplication app)
    {
        app.MapGet("/Seats", (ISeatRepository repo) => repo.GetAll())
            .Produces<SeatDto[]>(StatusCodes.Status200OK);

        app.MapGet("/Seat/{SeatId:int}", async (int SeatId, ISeatRepository repo) => 
        {
            var Seat = await repo.Get(SeatId);
            if (Seat == null)
                return Results.Problem($"Seat with ID {SeatId} not found.", statusCode: 404);
            return Results.Ok(Seat);
        }).ProducesProblem(404).Produces<SeatDetailDto>(StatusCodes.Status200OK);

        app.MapPost("/Seats", async ([FromBody] SeatDetailDto dto, ISeatRepository repo) => 
        {
            if (!MiniValidator.TryValidate(dto, out var errors))
                return Results.ValidationProblem(errors);
            var newSeat = await repo.Add(dto);
            return Results.Created($"/Seat/{newSeat.Id}", newSeat);
        }).ProducesValidationProblem().Produces<SeatDetailDto>(StatusCodes.Status201Created);

        app.MapPut("/Seats", async ([FromBody] SeatDetailDto dto, ISeatRepository repo) => 
        {       
            if (!MiniValidator.TryValidate(dto, out var errors))
                return Results.ValidationProblem(errors);
            if (await repo.Get(dto.Id) == null)
                return Results.Problem($"Seat with Id {dto.Id} not found", statusCode: 404);
            var updatedSeat = await repo.Update(dto);
            return Results.Ok(updatedSeat);
        }).ProducesValidationProblem().ProducesProblem(404).Produces<SeatDetailDto>(StatusCodes.Status200OK);

        app.MapDelete("/Seats/{SeatId:int}", async (int SeatId, ISeatRepository repo) => 
        {
            if (await repo.Get(SeatId) == null)
                return Results.Problem($"Seat with Id {SeatId} not found", statusCode: 404);
            await repo.Delete(SeatId);
            return Results.Ok();
        }).ProducesProblem(404).Produces(StatusCodes.Status200OK);
    }
}