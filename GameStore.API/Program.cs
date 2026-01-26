using GameStore.API.Data;
using GameStore.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000") // Vite / React
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddValidation();
builder.AddGameStoreDb();

var app = builder.Build();
app.UseCors("AllowFrontend");

app.MapGamesEndpoints();
app.MapGenreEndpoints();
app.MigrateDb();

app.Run();
