using AvalonServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static AvalonServer.Entities.GameSettings;

namespace AvalonServer.CreateGame
{
    public class CreateGameMessages
    {
        public struct Request
        {
            public Session Session;
            public string GameName;
            public GameAccessLevel AccessLevel;            
        }

        public struct Response
        {
            public bool Success;
            public int? GameId;
            public Exception Exception;
        }
    }
}