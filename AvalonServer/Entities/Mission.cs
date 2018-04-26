using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.Entities
{
    public class Mission
    {
        public int Id { get; set; }
        public int GameId { get; set; }

        /// <summary>
        /// The Mission the game is on. There are 5 missions per game.
        /// </summary>
        public int MissionNumber { get; set; }

        /// <summary>
        /// The Alignment the Mission is won by (Good or Evil).
        /// </summary>
        public Alignment? VictoryAlignment { get; set; }
    }
}