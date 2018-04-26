using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.Entities
{
    public class GameCharacter
    {
        GameCharacters character;
        
        Alignment GetAlignment()
        {
            switch (character) {

                case GameCharacters.Merlin:
                    return Alignment.Good;
                case GameCharacters.Assasin:
                    return Alignment.Evil;
                case GameCharacters.LoyalServant:
                    return Alignment.Good;
                case GameCharacters.MinionOfModred:
                    return Alignment.Evil;
                default:
                    throw new Exception("Invalid character!");
            }
        }
    }

    public enum GameCharacters
    {
        Merlin,
        Assasin,
        LoyalServant,
        MinionOfModred
    }

    public enum Alignment
    {
        Good,
        Evil
    }
}