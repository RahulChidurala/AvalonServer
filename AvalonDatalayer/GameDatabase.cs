using System;
using System.Collections.Generic;
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
            using(var db = new AvalonContext())
            {                
                
                db.Games.Add(game);
                db.SaveChanges();
                gameId = game.GameId;
            }

            return gameId;
        }

        public void DeleteGame(Game game)
        {
            using (var db = new AvalonContext())
            {
                db.Games.Remove(game);
                db.SaveChanges();                
            }
        }

        public Game[] GetAllGames()
        {
            using (var db = new AvalonContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var allGames = new List<Game>();

                foreach (Game game in db.Games)
                {
                    allGames.Add(game);
                }

                return allGames.ToArray();
            }
        }

        public Game GetGame(int gameId)
        {
            using(var db = new AvalonContext())
            {
                //int[] gameIdArray = { gameId };
                db.Configuration.ProxyCreationEnabled = false;
                return db.Games.Find(gameId);
            }
        }

        public void UpdateGame(Game game)
        {

            using (var db = new AvalonContext())
            {
                var retrievedGame = db.Games.Find(game.GameId);
                if(retrievedGame != null)
                {
                    db.Entry(retrievedGame).CurrentValues.SetValues(game);
                    db.SaveChanges();
                }
                else
                {
                    throw new GameGatewayException("Game with id " + game.GameId + " does not exist!");
                }
            }
        }
    }
}
