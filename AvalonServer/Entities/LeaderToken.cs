using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.Entities
{
    public class LeaderToken
    {
        public int Id { get; set; }

        public string SessionId { get; set; }
        public int MissionId { get; set; }
    }
}