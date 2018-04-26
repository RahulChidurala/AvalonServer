using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.CreateAccount
{
    public class CreateAccountMessages
    {
        public struct Request
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public struct Response
        {
            public bool Success { get; set; }
            public Exception Exception { get; set; }
        }
    }
}