using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AvalonServer.Entities;

namespace AvalonServer.SessionWorkers
{
    public interface ISessionGateway
    {
        void CreateSession(Session session);        

        /// <returns>Session or null if sessionKey DNE</returns>
        Session GetSession(string sessionKey);

        /// <exception>Throws SessionGatewayException if sessionKey DNE.</exception>
        void DeleteSession(string sessionKey);

        /// <exception>Throws SessionGatewayException if sessionKey DNE.</exception>
        void UpdateSession(string sessionKey);
    }    
}