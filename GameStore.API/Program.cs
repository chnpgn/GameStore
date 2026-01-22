using GameStore.API.Data;
using GameStore.API.Dtos;
using GameStore.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var conn = "Data Source=gamestore.db";
builder.Services.AddSqlite<GameStoreContext>(conn);

var app = builder.Build();

app.MapGamesEndpoints();
app.MigrateDb();

app.Run();
