using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.Entities
{
    public class Character
    {
        Characters character;
        
        Alignment GetAlignment()
        {

            switch (character) {

                case Characters.Merlin:
                    return Alignment.Good;
                case Characters.Assasin:
                    return Alignment.Evil;
                case Characters.LoyalServant:
                    return Alignment.Good;
                case Characters.MinionOfModred:
                    return Alignment.Evil;
                default:
                    throw new Exception("Invalid character!");
            }
        }
    }

    enum Characters
    {
        Merlin,
        Assasin,
        LoyalServant,
        MinionOfModred
    }

    enum Alignment
    {
        Good,
        Evil
    }
}