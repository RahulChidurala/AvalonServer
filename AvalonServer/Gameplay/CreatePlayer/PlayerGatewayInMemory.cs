using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AvalonServer.Entities;

namespace AvalonServer.Gameplay.CreatePlayer
{
    public class PlayerGatewayInMemory : IPlayerGateway
    {

        private Dictionary<string, Player> players = new Dictionary<string, Player>();

        public string CreatePlayer(Player player)
        {
            if(players.ContainsKey(player.Username))
            {
                throw new PlayerGatewayException("Username already exists!");
            }

            players.Add(player.Username, player);

            return player.Username;
        }

        public void DeletePlayer(string username)
        {
            throw new NotImplementedException();
        }

        public Player[] GetAllPlayers()
        {
            var playersArray = new List<Player>();

            foreach (KeyValuePair<string, Player> entry in players) {

                playersArray.Add(entry.Value);
            };

            return playersArray.ToArray();

        }

        public Player GetPlayer(string username)
        {
            throw new NotImplementedException();
        }
    }
}