using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.SessionWorkers
{
    public interface ISessionCreator
    {
        string CreateSession();
    }

    public class SessionCreatorGuid : ISessionCreator
    {
        public string CreateSession()
        {
            return Guid.NewGuid().ToString();
        }
    }
}