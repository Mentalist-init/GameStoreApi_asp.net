using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

List<GameDto> games = [
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

//GET /games
app.MapGet("games" , () => games); //() => handler

app.MapGet("games/{id}" , (int id) => games.Find(game => game.Id==id));


app.Run();
