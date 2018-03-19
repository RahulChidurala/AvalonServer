using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.Entities
{
    public class Game
    {
        public String Name { get; }
        public GameSettings Settings { get; }        

        public int GameId { get; set; }
        public List<Player> Players { get; set; }

        public Game(String name, GameSettings settings)
        {
            this.Name = name;
            this.Settings = settings;

            Players = new List<Player>();
        }
    }
}