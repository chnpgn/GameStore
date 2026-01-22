using GameStore.API.Dtos;

namespace GameStore.API.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpoint = "GetGame";
    private static readonly List<GameDto> games = [
    new ( 1, "Pacman Pro", "Creative", 59.99M, new DateOnly(1992, 05, 17)),
    new ( 2, "Street Fight", "Fighting", 79.99M, new DateOnly(1994, 07, 27)),
    new ( 3, "Rescue Team Seal", "Military", 39.99M, new DateOnly(1999, 05, 11)),
    new ( 4, "Karate Kid", "Action", 19.99M, new DateOnly(1995, 07, 19)),
];

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var  group = app.MapGroup("/games");

        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(g => g.Id == id);

            return game is not null ? Results.Ok(game) : Results.NotFound();
        }).WithName(GetGameEndpoint);

        group.MapPost("/", (CreateGameDto newGame) =>
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

        group.MapPut("/{id}", (int id, UpdateGameDto updateGameDto) =>
        {
            var index = games.FindIndex(g => g.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new(
                id,
                updateGameDto.Name,
                updateGameDto.Genre,
                updateGameDto.Price,
                updateGameDto.ReleaseDate
            );

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(g => g.Id == id);

            return Results.NoContent();
        });
    }
}