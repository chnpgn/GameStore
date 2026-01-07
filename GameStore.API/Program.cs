using GameStore.API.Dtos;

const string GetGameEndpoint = "GetGame";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new ( 1, "Pacman Pro", "Creative", 19.99M, new DateOnly(1992, 05, 17)),
    new ( 2, "Street Fight", "Fighting", 79.99M, new DateOnly(1994, 07, 27)),
    new ( 3, "Rescue Team Seal", "Military", 39.99M, new DateOnly(1999, 05, 11)),
];

app.MapGet("/games", () => games);


app.MapGet("/games/{id}", (int id) => games.Find(g => g.Id == id))
    .WithName(GetGameEndpoint);

app.MapPost("/games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpoint, new { id = game.Id }, game);
});

app.MapPut("/games/{id}", (int id, UpdateGameDto updateGameDto) =>
{
    var index = games.FindIndex( g => g.Id == id);

    games[index] = new(
        id,
        updateGameDto.Name,
        updateGameDto.Genre,
        updateGameDto.Price,
        updateGameDto.ReleaseDate
    );

    return Results.NoContent();

});

app.MapDelete("/games/{id}", (int id) =>
{
    games.RemoveAll(g => g.Id == id);

    return Results.NoContent();
});


app.Run();
