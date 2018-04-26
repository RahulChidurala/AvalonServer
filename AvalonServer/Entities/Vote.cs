using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.Entities
{
    public class Vote
    {
        public int Id { get; set; }

        public string SessionId { get; set; }
        public string MissionID { get; set; }
        public VoteType VoteType { get; set; }
    }

    public enum VoteType
    {
        Success,
        Fail
    }
}