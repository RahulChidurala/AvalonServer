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
            FriendsOnly,
            Public
        }

        public GameAccessLevel gameAccessLevel { get; set; }
    }
}