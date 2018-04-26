using AvalonServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.Gameplay.JoinGame
{
    public class JoinGameMessages
    {

        public struct Request
        {
            public int gameId { get; set; }
            public string playerUsername { get; set; }
        }

        public struct Response
        {
            public bool JoinedGame { get; set; }
            public string ErrorMessage { get; set; }
        }

        public struct ViewModel
        {

        }
    }
}