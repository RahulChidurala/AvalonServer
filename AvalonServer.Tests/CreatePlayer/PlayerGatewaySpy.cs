using AvalonServer.Entities;
using AvalonServer.Gameplay.CreatePlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonServer.Tests.Createplayer
{
    public class PlayerGatewaySpy : GatewaySpy<Player>, IPlayerGateway
    {
        
        #region Implementation of playerGateway
        public int CreatePlayer(Player player)
        {
            var id = Create(player);
            player.Id = id;
            return id;
        }

        public Player GetPlayerForAccount(int accountId)
        {
            Player player = GetPlayerInternalForAccount(accountId);

            if(player == null)
            {
                throw new PlayerGatewayException(PlayerGatewayException.GetMessageFor(PlayerGatewayExceptions.PlayerWithAccountDNE));
            }
            return player;
        }

        public Player GetPlayer(int playerId)
        {
            return Get(playerId);
        }

        public void DeletePlayerWithAccount(int accountId)
        {
            Player player = GetPlayerInternalForAccount(accountId);

            if(player == null)
            {
                throw new PlayerGatewayException(PlayerGatewayException.GetMessageFor(PlayerGatewayExceptions.PlayerWithAccountDNE));
            }

            Delete(player.Id);
        }

        public void UpdatePlayer(Player player)
        {
            if(repo.Keys.Contains(player.Id) == false)
            {
                throw new PlayerGatewayException(PlayerGatewayException.GetMessageFor(PlayerGatewayExceptions.PlayerWithIdDNE));                    
            }

            repo[player.Id] = player;
        }
        #endregion

        #region Helper functions
        private Player GetPlayerInternalForAccount(int accountId)
        {

            Player player = null;

            foreach (KeyValuePair<int, Player> entry in repo)
            {

                if (entry.Value.AccountId == accountId)
                {
                    player = entry.Value;
                    break;
                }
            }

            return player;
        }

        private Player GetPlayerInternal(int playerId)
        {

            Player player = null;

            foreach (KeyValuePair<int, Player> entry in repo)
            {

                if (entry.Value.Id == playerId)
                {
                    player = entry.Value;
                    break;
                }
            }

            return player;
        }        
        #endregion
    }
}
