using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    class AreaMap
    {
        Version version { get; set; }
        int map { get; set; }
        EncounterSlot[] WalkSlots;
        EncounterSlot[] SurfSlots;
        EncounterSlot[] SuperRodSlots;
        EncounterSlot[] Swarm;
        EncounterType WalkSlotsType { get; set; }
        EncounterType SurfSlotsType { get; set; }
        EncounterType SuperRodSlotsType { get; set; }


        

    }
}
