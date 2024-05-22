using GameStore.Client.Models;

namespace GameStore.Client;

public static class GameClient
{
    private static readonly List<Game> games = new()
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

    public static Game[] GetGames()
    {
        return games.ToArray();
    }

    public static void AddGame(Game game)
    {
        game.Id = games.Max(game => game.Id) + 1;
        games.Add(game);
    }

    public static Game GetGame(int id)
    {
        return games.Find(game => game.Id == id) ?? throw new Exception("Could not find game!");
    }

    public static void UpdateGame(Game updatedGame)
    {
        Game existingGame = GetGame(updatedGame.Id);
        existingGame.Name = updatedGame.Name;
        existingGame.Genre = updatedGame.Genre;
        existingGame.Price = updatedGame.Price;
        existingGame.ReleaseDate = updatedGame.ReleaseDate;
    }

    public static void DeleteGame(int id)
    {
        Game game = GetGame(id);
        games.Remove(game);
    }

}