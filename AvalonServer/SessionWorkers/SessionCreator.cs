using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.SessionWorkers
{
    public interface ISessionTokenCreator<T>
    {
        T CreateSession();
    }

    public class SessionCreatorGuid : ISessionTokenCreator<string>
    {
        public string CreateSession()
        {
            return Guid.NewGuid().ToString();
        }        
    }
}