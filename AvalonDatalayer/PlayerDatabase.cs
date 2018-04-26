using AvalonServer.Entities;
using AvalonServer.Gameplay.CreatePlayer;
using System;
 using System.Linq;
using System.Web;

namespace AvalonDatalayer
{
    public class PlayerDatabase : IPlayerGateway
    {
        public string CreatePlayer(Player player)
        {
            using(var db = new AvalonContext())
            {

                String[] usernameArray = { player.DisplayName };
                var playerExists = db.Players.Find(usernameArray);
                if (playerExists == null)
                {
                    db.Players.Add(player);
                    db.SaveChanges();

                } else
                {
                    throw new PlayerGatewayException("Username already exists!");
                }                
            }

            return player.DisplayName;
        }

        public void DeletePlayer(string username)
        {
            using (var db = new AvalonContext())
            {
                String[] usernameArray = { username };
                var playerToDelete = db.Players.Find(usernameArray);
                db.Players.Remove(playerToDelete);
                db.SaveChanges();
            }
        }

        public void DeletePlayerWithAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public Player[] GetAllPlayers()
        {
            using (var db = new AvalonContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                return db.Players.ToArray<Player>();
            }
        }

        public Player GetPlayer(string username)
        {
            using (var db = new AvalonContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                String[] usernameArray = { username };
                return db.Players.Find(usernameArray);
            }
        }

        public Player GetPlayer(int playerId)
        {
            throw new NotImplementedException();
        }

        public Player GetPlayerForAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlayer(Player player)
        {
            using(var db = new AvalonContext())
            {
                String[] usernameArray = { player.DisplayName };
                var retrievedPlayer = db.Players.Find(usernameArray);
                                
                if(retrievedPlayer != null)
                {
                    db.Entry(retrievedPlayer).CurrentValues.SetValues(player);
                    db.SaveChanges();

                } else
                {
                    throw new PlayerGatewayException("Player with username " + player.DisplayName + " does not exist.");
                }               
            }
        }

        int IPlayerGateway.CreatePlayer(Player player)
        {
            throw new NotImplementedException();
        }
    }
}