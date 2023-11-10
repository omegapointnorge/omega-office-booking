using Microsoft.AspNetCore.Mvc;
using MiniValidation;
using server.Data;

public static class WebApplicationUserExtensions
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/Users", (IUserRepository repo) => repo.GetAll())
            .Produces<UserDto[]>(StatusCodes.Status200OK);

        app.MapGet("/User/{UserId:int}", async (int UserId, IUserRepository repo) => 
        {
            var User = await repo.Get(UserId);
            if (User == null)
                return Results.Problem($"User with ID {UserId} not found.", statusCode: 404);
            return Results.Ok(User);
        }).ProducesProblem(404).Produces<UserDto>(StatusCodes.Status200OK);

        app.MapPost("/Users", async ([FromBody] UserDto dto, IUserRepository repo) => 
        {
            if (!MiniValidator.TryValidate(dto, out var errors))
                return Results.ValidationProblem(errors);
            var newUser = await repo.Add(dto);
            return Results.Created($"/User/{newUser.Id}", newUser);
        }).ProducesValidationProblem().Produces<UserDto>(StatusCodes.Status201Created);

        app.MapPut("/Users", async ([FromBody] UserDto dto, IUserRepository repo) => 
        {       
            if (!MiniValidator.TryValidate(dto, out var errors))
                return Results.ValidationProblem(errors);
            if (await repo.Get(dto.Id) == null)
                return Results.Problem($"User with Id {dto.Id} not found", statusCode: 404);
            var updatedUser = await repo.Update(dto);
            return Results.Ok(updatedUser);
        }).ProducesValidationProblem().ProducesProblem(404).Produces<UserDto>(StatusCodes.Status200OK);

        app.MapDelete("/Users/{UserId:int}", async (int UserId, IUserRepository repo) => 
        {
            if (await repo.Get(UserId) == null)
                return Results.Problem($"User with Id {UserId} not found", statusCode: 404);
            await repo.Delete(UserId);
            return Results.Ok();
        }).ProducesProblem(404).Produces(StatusCodes.Status200OK);
    }
}