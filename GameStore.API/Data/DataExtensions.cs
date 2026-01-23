using System;
using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        context.Database.Migrate();
    }

    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        var conn = builder.Configuration.GetConnectionString("GameStore");
        
        builder.Services.AddSqlite<GameStoreContext>(
            conn,
            optionsAction: o => o.UseSeeding((context, _) =>
            {
                if (!context.Set<Genre>().Any())
                {
                    context.Set<Genre>().AddRange(
                        new Genre { Name = "Fighting" },
                        new Genre { Name = "RPG" },
                        new Genre { Name = "Platformer" },
                        new Genre { Name = "Racing" },
                        new Genre { Name = "Sports" }
                    );
                    context.SaveChanges();
                }
            })
        );
    }
}
