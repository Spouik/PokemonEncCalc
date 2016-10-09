using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    internal abstract class AreaMap
    {
        protected Version version { get; set; }
        protected int map { get; set; }
        protected EncounterSlot[] WalkSlots;
        protected EncounterSlot[] SurfSlots;
        protected EncounterSlot[] SuperRodSlots;

        protected abstract void createEncounterSlotArray(ref EncounterSlot[] slotArray, byte[] data, decimal[] percentArray);


    }
}
