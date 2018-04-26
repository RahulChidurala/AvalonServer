using AvalonServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.Gameplay.CreateGame
{
    public class GameGatewayInMemory: IGameGateway
    {
        private Dictionary<int, Game> games = new Dictionary<int, Game>();
        private int highestId = 0;

        public int CreateGame(Game game)
        {
            var gameId = highestId;
            games[gameId] = game;
            highestId++;
            return gameId;
        }

        public Game GetGame(int gameId)
        {
            if(games.ContainsKey(gameId) == false)
            {
                return null;

            } else
            {
                return games[gameId];
            }           
        }

        public void DeleteGame(Game game)
        {
            throw new NotImplementedException();
        }

        public Game[] GetAllGames()
        {
            throw new NotImplementedException();
        }

        public void UpdateGame(Game game)
        {
            throw new NotImplementedException();
        }        
    }
}