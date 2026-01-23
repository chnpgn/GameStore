using System;
using GameStore.API.Data;
using GameStore.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Endpoints;

public static class GenreEndpoints
{
    public static void MapGenreEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");

        group.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Genres
                .Select(g => new GenreDto(g.Id, g.Name))
                .AsNoTracking()
                .ToListAsync()
        );
    }
}
