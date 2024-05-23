using GameStore.Server.Models;

List<Game> games = new()
    {
        new Game()
        {
            Id = 1,
            Name = "Pokemon Scarlet/Violet",
            Genre = "RPG",
            Price = 50.99M,
            ReleaseDate = new DateTime(2022, 11, 18)
        },
        new Game()
        {
            Id = 2,
            Name = "Pokemon Legends Z-A",
            Genre = "JRPG",
            Price = 60.99M,
            ReleaseDate = new DateTime(205, 6, 18)
        },
        new Game()
        {
            Id = 3,
            Name = "Pokemon Sword/Shield",
            Genre = "RPG",
            Price = 55.99M,
            ReleaseDate = new DateTime(2019, 11, 18)
        },
        new Game()
        {
            Id = 4,
            Name = "Pokemon Legends",
            Genre = "JRPG",
            Price = 65.99M,
            ReleaseDate = new DateTime(2022, 1, 18)
        },
        new Game()
        {
            Id = 5,
            Name = "Pokemon SV DLC",
            Genre = "DLC",
            Price = 55.99M,
            ReleaseDate = new DateTime(2023, 11, 18)
        },
    };

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.WithOrigins("http://localhost:5244")
            .AllowAnyHeader()
            .AllowAnyMethod();
}));

var app = builder.Build();

app.UseCors();

var group = app.MapGroup("/games").WithParameterValidation();

//GET /games
group.MapGet("/", () => games);

//GET /games/{id}
group.MapGet("/{id}", (int id) =>
{
    Game? game = games.Find(game => game.Id == id);
    if (game is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(game);
}).WithName("GetGame");

//POST /games
group.MapPost("/", (Game game) =>
{
    game.Id = games.Max(game => game.Id) + 1;
    games.Add(game);

    return Results.CreatedAtRoute("GetGame", new { id = game.Id }, game);
});

//PUT /games/{id}
group.MapPut("/{id}", (int id, Game updatedGame) =>
{
    Game? existingGame = games.Find(game => game.Id == id);

    if (existingGame is null)
    {
        updatedGame.Id = id;
        games.Add(updatedGame);

        return Results.CreatedAtRoute("GetGame", new { id = updatedGame.Id }, updatedGame);
    }

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();
});

//DELETE /games/{id}
group.MapDelete("/{id}", (int id) =>
{
    Game? game = games.Find(game => game.Id == id);

    if (game is null)
    {
        return Results.NotFound();
        //return Results.NoContent();
    }

    games.Remove(game);
    return Results.NoContent();
});

app.Run();
