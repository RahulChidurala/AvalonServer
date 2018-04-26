using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.LoginToAccount
{
    public class LoginMessages
    {
        public struct Request
        {
            public string Username;
            public string Password;
        }

        public struct Response
        {
            public bool Success;
            public string Session;
            public Exception Exception;
        }
    }
}