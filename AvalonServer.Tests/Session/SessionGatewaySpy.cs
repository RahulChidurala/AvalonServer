using AvalonServer.Entities;
using AvalonServer.SessionWorkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonServer.Tests.Session
{
    class SessionGatewaySpy : ISessionGateway
    {

        Dictionary<string, Entities.Session> store = new Dictionary<string, Entities.Session>(); 

        public void CreateSession(Entities.Session session)
        {
            store.Add(UniqueID(), session);
        }

        public void DeleteSession(string sessionKey)
        {
            store.Remove(sessionKey);
        }

        public Entities.Session GetSession(string sessionKey)
        {
            store.TryGetValue(sessionKey, out Entities.Session session);

            if (session == null)
            {
                throw new Exception("Session does not exist!");
            }

            return session;
        }

        public Entities.Session GetSessionFor(int playerId)
        {
            var sessions = store.Values.Where(a => a.PlayerId == playerId);
            if(sessions == null || sessions.Count() <= 0)
            {
                throw new Exception("Session does not exist for playerId " + playerId);
            }

            if(sessions.Count() != 1)
            {
                throw new Exception("Multiple sessions found for 1 playerId " + playerId);
            }

            return sessions.First();
        }

        public void UpdateSession(Entities.Session session)
        {
            var key = session.Id;
            if(store.ContainsKey(key) == false)
            {
                throw new Exception("Session with key " + session.Id + " does not exist!");
            }

            store[key] = session;
        }

        private string UniqueID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
