using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.Entities
{
    public class Character
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public GameCharacter GameCharacter { get; set; }
    }
}