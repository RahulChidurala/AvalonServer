using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.Gameplay.CreatePlayer
{
    public class CreatePlayerMessages
    {
        public struct Request
        {
            public String Username;
            public String Password;
        }

        public struct Response
        {
            public bool Success;
            public List<string> Messages;            
        }

        public struct ViewModel
        {

        }
    }
}