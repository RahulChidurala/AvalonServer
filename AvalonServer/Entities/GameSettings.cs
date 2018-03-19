using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.Entities
{
    public class GameSettings
    {
        public enum GameAccessLevel
        {
            GameFriendsOnly,
            GamePublic
        }

        public GameAccessLevel gameAccessLevel { get; set; }
    }
}