using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
   

    internal abstract class Pokemon
    {

        protected byte[] Data;
        internal short NatID { get; set; }

        internal abstract List<Pokemon> Forms { get; }

        internal byte Form { get; set; }
        internal abstract Type Type1 { get; set; }
        internal abstract Type Type2 { get; set; }
        internal abstract int Height { get; set; }
        internal abstract int Weight { get; set; }
        internal abstract byte CatchRate { get; set; }
        internal abstract byte GenderRatio { get; set; }

        // Base stats
        internal abstract byte HP { get; set; }
        internal abstract byte Atk { get; set; }
        internal abstract byte Def { get; set; }
        internal abstract byte SpA { get; set; }
        internal abstract byte SpD { get; set; }
        internal abstract byte Spe { get; set; }


        //public byte CatchRateORAS { get; set; }

        // PokemonNames
        internal string NameDE { get; set; }
        internal string NameEN { get; set; }
        internal string NameES { get; set; }
        internal string NameFR { get; set; }
        internal string NameIT { get; set; }
        internal string NameJP { get; set; }
        internal string NameKR { get; set; }
        internal string NameCHS { get; set; }
		
        // FormNames

        internal string FormNameDE { get; set; }
        internal string FormNameEN { get; set; }
        internal string FormNameES { get; set; }
        internal string FormNameFR { get; set; }
        internal string FormNameIT { get; set; }
        internal string FormNameJP { get; set; }
        internal string FormNameKR { get; set; }
        internal string FormNameCHS { get; set; }

        internal abstract int FormCount();

        internal string getName()
        {
            switch (Properties.Settings.Default.Language)
            {
                case 1: return NameEN;
                case 2: return NameFR;
                case 3: return NameDE;
                case 4: return NameES;
                case 5: return NameIT;
                case 6: return NameJP;
                case 7: return NameKR;
                case 8: return NameCHS;
                default: return NameEN;
            }
        }

        internal string getFormName()
        {
            switch (Properties.Settings.Default.Language)
            {
                case 1: return FormNameEN == " " ? (NameEN + (NameEN.EndsWith("s") ? "'" : "'s") + " form") : FormNameEN;
                case 2: return FormNameFR == " " ? ("Forme de " + NameFR) : FormNameFR;
                case 3: return FormNameDE == " " ? NameDE : FormNameDE;
                case 4: return FormNameES == " " ? ("Forma de " + NameES) : FormNameES;
                case 5: return FormNameIT == " " ? ("Forma di" + NameIT) : FormNameIT;
                case 6: return FormNameJP == " " ? (NameJP + "のすがた") : FormNameJP;
                case 7: return FormNameKR == " " ? (NameKR + "의 모습") : FormNameKR;
                case 8: return FormNameCHS == " " ? (NameCHS + "的形态") : FormNameCHS;
                default: return FormNameEN == " " ? (NameEN + (NameEN.EndsWith("s") ? "'" : "'s") + " form") : FormNameEN;
            }
        }

        internal virtual Pokemon getSpeciesForm(short speciesID)
        {
            short formID = (short)((speciesID >> 10) & 0x3f);
            if (this is PokemonBW || this is PokemonB2W2 || this is PokemonSuMo) formID = (short)((speciesID >> 11) & 0x1f);

            if (formID == 0) return this;
            if (formID > FormCount()) return this;
            if (Forms[formID - 1] == null) return this;
            return Forms[formID - 1];
        }

        internal virtual int getNbReleased()
        {
            return 0;
        }
    }
}
