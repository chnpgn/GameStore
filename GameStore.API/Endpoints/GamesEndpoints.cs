using GameStore.API.Data;
using GameStore.API.Dtos;
using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpoint = "GetGame";

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        group.MapGet("/", async (GameStoreContext dbContext) => await dbContext.Games
            .Include(g => g.Genre)
            .Select(game => new GameSummaryDto(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
            ))
            .AsNoTracking()
            .ToListAsync());

        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(new GameDetailsDto(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            ));
        }).WithName(GetGameEndpoint);

        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            GameDetailsDto gameDto = new(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );

            return Results.CreatedAtRoute(GetGameEndpoint, new { id = gameDto.Id }, gameDto);
        });

        group.MapPut("/{id}", async (int id, UpdateGameDto updateGameDto, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);

            if (game is null)
            {
                return Results.NotFound();
            }

            game.Name = updateGameDto.Name;
            game.GenreId = updateGameDto.GenreId;
            game.Price = updateGameDto.Price;
            game.ReleaseDate = updateGameDto.ReleaseDate;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games.Where(g => g.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });
    }
}