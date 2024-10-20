using System;
using GameStore.Api.Dtos;

//Extension Class containing extension methods that are static

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

private static readonly List<GameDto> games = [
    new (
        1,
        "Batman : Arkham City",
        "Action/Story",
        29.99M,
        new DateOnly(2011, 10, 18)
    ),
    new (
        2,
        "Fifa 25",
        "Sports",
        79.99M,
        new DateOnly(2024, 09, 19)
    ),
    new (
        3,
        "Grand Theft Auto V",
        "Action/Adventure",
        49.99M,
        new DateOnly(2013, 9, 17)
    ),
    new (
        4,
        "Red Dead Redemption 2",
        "Action/Adventure",
        59.99M,
        new DateOnly(2018, 10, 26)
    ),

    ];

public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app){

    var group = app.MapGroup("games");

    //GET /games
    group.MapGet("/" , () => games); //() => handler


    group.MapGet("/{id}" , (int id) =>
    {
        GameDto? game = games.Find(game => game.Id==id);

        return game is null ? Results.NotFound() : Results.Ok(game);
    })
    .WithName(GetGameEndpointName);

    // POST /games
    group.MapPost("/", (CreateGameDto newGame) =>
    {
        GameDto game = new(
            games.Count + 1,
            newGame.Name,
            newGame.Genre,
            newGame.Price,
            newGame.ReleaseDate);

        games.Add(game);
        return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
    });

    // PUT /games /1
    group.MapPut("/{id}",(int id, UpdateGameDto updatedGame) =>
    {
        var index = games.FindIndex(game => game.Id == id);

        if(index == -1){
            return Results.NotFound();
        }

        games[index] = new GameDto(
            id,
            updatedGame.Name,
            updatedGame.Genre,
            updatedGame.Price,
            updatedGame.ReleaseDate
        );

        return Results.NoContent();
    });

    //DELETE /games /{id}
    group.MapDelete("/{id}", (int id) =>
    {
        games.RemoveAll(game => game.Id == id);

        return Results.NoContent();
    });

   return group;
   }
}
