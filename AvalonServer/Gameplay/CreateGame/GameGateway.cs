using System;
using AvalonServer.Entities;

namespace AvalonServer.Gameplay.CreateGame
{

    public interface IGameGateway
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="">Throws GameGatewayException</exception>
        /// <param name="game"></param>
        int CreateGame(Game game);
        Game GetGame(int gameId);
        void DeleteGame(Game game);

        /// <summary>
        /// Lists all running games.
        /// </summary>
        /// <returns> Array of Game objects. </returns>
        Game[] GetAllGames();
    }

    public class GameGatewayException: Exception {

        public GameGatewayException() { }
        public GameGatewayException(string message): base(message) { }
    }
}