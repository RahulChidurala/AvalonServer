using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.Entities
{
    /**
     * ChosenPlayer to go on a Mission. 
     */
    public class ChosenPlayer
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public int ChosenPlayerId { get; set; }
        public int MissionId { get; set; }
    }
}