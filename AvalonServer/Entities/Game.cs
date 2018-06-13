using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AvalonServer.Entities
{
    public class Game
    {
        public String Name { get; set; }
        public GameSettings.GameAccessLevel AccessLevel { get; set; }        

        public int GameId { get; set; }
    }

    public enum GameState
    {
        WaitingForPlayers,
        CharacterAssignmentPhase,
        CharacterRevealPhase,
        PickingPhase,
        VotingPhase,
        VoteRevealPhase,
        EndedPhase1,
        EndedPhase2,
        EndedFinal
    }
}