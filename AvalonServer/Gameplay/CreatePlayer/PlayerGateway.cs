using AvalonServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.Gameplay.CreatePlayer
{
    public interface IPlayerGateway
    {
        /// <summary>
        /// Creates player in stoarge.
        /// </summary>
        /// <exception cref="">Throws PlayerGatewayException.</exception>
        /// <param name="player"></param>
        /// <returns>Player's username</returns>
        string CreatePlayer(Player player);

        /// <summary>
        /// Gets player from storage.
        /// </summary>
        /// <param name="username">Username of the player to retrieve.</param>
        /// <returns>Player with the associated username.</returns>
        Player GetPlayer(string username);
        void DeletePlayer(string username);

        /// <summary>
        /// Gets all players.
        /// </summary>
        /// <returns>An array of all players.</returns>
        Player[] GetAllPlayers();
    }

    public class PlayerGatewayException : Exception
    {
        public PlayerGatewayException() { }
        public PlayerGatewayException(string message) : base(message) { }
    }    
}