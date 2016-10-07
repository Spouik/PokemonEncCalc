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
        private decimal _percentage;
        public decimal Percentage {
            get {
                return _percentage;
            }
            set {
                // EffectivePercentage change must follow the same change ratio as Percentage
                decimal newPercentage = Math.Max(Math.Min(value, 100), 0);

                if (_percentage == 0) _effectivePercentage = newPercentage;
                else _effectivePercentage *= newPercentage / _percentage;

                _percentage = newPercentage;
            } }
        private decimal _effectivePercentage;
        public decimal EffectivePercentage {
            get {
                return _effectivePercentage;
            }
            set {
                _effectivePercentage = value;
            }
        }

        internal EncounterSlot(Pokemon species, byte MinLv, byte MaxLv, decimal percent, decimal effPercent = 0)
        {
            Species = species;
            MinLevel = Math.Max(Math.Min(MinLv, (byte)100), (byte)1);
            MaxLevel = Math.Max(Math.Min(MaxLv, (byte)100), (byte)1);
            Percentage = Math.Max(Math.Min(percent, 100), 0);
            EffectivePercentage = (effPercent == 0) ? percent : effPercent;
        }

        public EncounterSlot(EncounterSlot encounterSlot)
        {
            Species = encounterSlot.Species;
            MinLevel = encounterSlot.MinLevel;
            MaxLevel = encounterSlot.MaxLevel;
            Percentage = encounterSlot.Percentage;
            EffectivePercentage = encounterSlot.EffectivePercentage;
        }

        internal bool Equals(EncounterSlot a)
        {
            return (Species == a.Species && MinLevel == a.MinLevel && MaxLevel == a.MaxLevel 
                && Percentage == a.Percentage && EffectivePercentage == a.EffectivePercentage);
        }
    }
}
