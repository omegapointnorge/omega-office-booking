using Microsoft.AspNetCore.Mvc;
using MiniValidation;
using server.Data;

public static class WebApplicationRoomxtensions
{
    public static void MapRoomEndpoints(this WebApplication app)
    {
        app.MapGet("/api/Rooms", (IRoomRepository repo) => repo.GetAll())
            .Produces<RoomDto[]>(StatusCodes.Status200OK);

        app.MapGet("/api/Room/{RoomId:int}", async (int RoomId, IRoomRepository repo) => 
        {
            var Room = await repo.Get(RoomId);
            if (Room == null)
                return Results.Problem($"Room with ID {RoomId} not found.", statusCode: 404);
            return Results.Ok(Room);
        }).ProducesProblem(404).Produces<RoomDto>(StatusCodes.Status200OK);

     
    }
}