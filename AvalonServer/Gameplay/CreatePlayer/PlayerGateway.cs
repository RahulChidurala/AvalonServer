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
        /// <returns>Player database id.</returns>
        int CreatePlayer(Player player);

        /// <summary>
        /// Gets player from storage for associated AccountId.
        /// </summary>
        /// <param name="accountId">AccountId that the Player is associated with. There should be 1 Player for every Account.</param>
        /// <exception>Throws PlayerGatewayException</exception>
        /// <returns>Player with the associated AccountId.</returns>
        Player GetPlayerForAccount(int accountId);

        /// <summary>
        /// Gets player from storage for datebase PlayerId.
        /// </summary>
        /// <param name="playerId">PlayerId of the Player.</param>
        /// <returns>Null or Player with the PlayerId.</returns>
        Player GetPlayer(int playerId);

        /// <summary>
        /// Deletes Player with AccountId
        /// </summary>
        /// <param name="accountId">AccountId of the associated Player.</param>
        /// <exception>Throws PlayerGatewayException</exception>
        /// <returns>Player with the AccountId.</returns>
        void DeletePlayerWithAccount(int accountId);

        /// <summary>
        /// Updates player.
        /// </summary>
        /// <exception>Throws PlayerGatewayException</exception>
        /// <param name="player"></param>
        void UpdatePlayer(Player player);
    }

    public class PlayerGatewayException : Exception
    {
        public PlayerGatewayException() { }
        public PlayerGatewayException(string message) : base(message) { }

        public static string GetMessageFor(PlayerGatewayExceptions exception)
        {
            var message = "Unknown error!";
            switch(exception)
            {
                case PlayerGatewayExceptions.PlayerWithAccountDNE:
                    message = "Player with AccountId does not exist!";
                    break;
                case PlayerGatewayExceptions.PlayerWithIdDNE:
                    message = "Player with id does not exist";
                    break;
            }

            return message;
        }
    }    

    public enum PlayerGatewayExceptions
    {
        PlayerWithAccountDNE,
        PlayerWithIdDNE
    }
}