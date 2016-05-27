using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    class EncounterSlot
    {


        public Pokemon Species { get; set; }
        public byte MinLevel { get; set; }
        public byte MaxLevel { get; set; }
        public decimal Percentage { get; set; }

        internal EncounterSlot(Pokemon species, byte MinLv, byte MaxLv, decimal percent)
        {
            Species = species;
            MinLevel = Math.Max(Math.Min(MinLv, (byte)100), (byte)1);
            MaxLevel = Math.Max(Math.Min(MaxLv, (byte)100), (byte)1);
            Percentage = Math.Max(Math.Min(percent, 100), 0);
        }

        public EncounterSlot(EncounterSlot encounterSlot)
        {
            Species = encounterSlot.Species;
            MinLevel = encounterSlot.MinLevel;
            MaxLevel = encounterSlot.MaxLevel;
            Percentage = encounterSlot.Percentage;
        }

        internal bool Equals(EncounterSlot a)
        {
            return (Species == a.Species && MinLevel == a.MinLevel && MaxLevel == a.MaxLevel);
        }
    }
}
