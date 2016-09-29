using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    class AreaMapGen2 : AreaMap
    {

        EncounterSlot[] OldRodSlots;
        EncounterSlot[] GoodRodSlots;
        EncounterSlot[] RockSmashSlots;
        EncounterSlot[] Swarm;

        private static decimal[] percentGrass = new decimal[] { 30, 30, 20, 10, 5, 4, 1 };
        private static decimal[] percentSurf = new decimal[] { 60, 30, 10 };
        private static decimal[] percentRockSmash = new decimal[] { 90, 10 };
        private static decimal[] percentOldRod = new decimal[] { 70, 30 };
        private static decimal[] percentGoodRod = new decimal[] { 60, 20, 20 };
        private static decimal[] percentSuperRod = new decimal[] { 40, 40, 15, 4, 1 };

    }
}
