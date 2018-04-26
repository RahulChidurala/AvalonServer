using System;
using AvalonServer.Entities;
using AvalonServer.Gameplay.CreateGame;

namespace AvalonDatalayer
{
    public class GameDatabase : IGameGateway
    {        

        public GameDatabase()
        {

        }

        public int CreateGame(Game game)
        {

            int gameId;
            using(var db = new GameContext())
            {
                db.Games.Add(game);
                db.SaveChanges();
                gameId = game.GameId;
            }

            return gameId;
        }

        public void DeleteGame(Game game)
        {
            throw new NotImplementedException();
        }

        public Game[] GetAllGames()
        {
            throw new NotImplementedException();
        }

        public Game GetGame(int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
