using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AvalonServer.Entities;

namespace AvalonServer.Gameplay.CreateGame
{
    public class CreateGameMessages
    {
        public struct Request
        {
            public GameSettings.GameAccessLevel accessLevel;
            public String name;
        }

        public struct Response
        {
            public bool success;
            public int gameId;
            public string[] message;
        }

        public struct ViewModel
        {

        }
    }
}