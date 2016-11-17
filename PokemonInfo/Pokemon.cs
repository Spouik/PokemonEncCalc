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

        // FormNames

        internal string FormNameDE { get; set; }
        internal string FormNameEN { get; set; }
        internal string FormNameES { get; set; }
        internal string FormNameFR { get; set; }
        internal string FormNameIT { get; set; }
        internal string FormNameJP { get; set; }
        internal string FormNameKR { get; set; }

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
                default: return FormNameEN == " " ? (NameEN + (NameEN.EndsWith("s") ? "'" : "'s") + " form") : FormNameEN;
            }
        }


        ///*
        // * Constructor going to resource to instanciate object
        // * resourceID is the Pokemon ID (national Pokedex)
        // * 
        // */

        //public Pokemon(short resourceID)
        //{
        //    NatID = resourceID;
        //    HP = PokemonEncCalc.Properties.Resources.PokemonData[48 * (resourceID - 1)];
        //    Atk = PokemonEncCalc.Properties.Resources.PokemonData[48 * (resourceID - 1) + 1];
        //    Def = PokemonEncCalc.Properties.Resources.PokemonData[48 * (resourceID - 1) + 2];
        //    Spe = PokemonEncCalc.Properties.Resources.PokemonData[48 * (resourceID - 1) + 3];
        //    SpA = PokemonEncCalc.Properties.Resources.PokemonData[48 * (resourceID - 1) + 4];
        //    SpD = PokemonEncCalc.Properties.Resources.PokemonData[48 * (resourceID - 1) + 5];
        //    Type1 = (Type)PokemonEncCalc.Properties.Resources.PokemonData[48 * (resourceID - 1) + 6];
        //    Type2 = (Type)PokemonEncCalc.Properties.Resources.PokemonData[48 * (resourceID - 1) + 7];

        //}

        ///// <summary>
        ///// Constructor instanciating the object from raw data
        ///// </summary>
        ///// <param name="id">1-based Pokémon identifier (used to load names)</param>
        ///// <param name="data">48-byte Pokémon data / 24-byte Pokémon data for forms</param>
        //public Pokemon(short id, byte[] data)
        //{
        //    if (data.Length == 48)
        //    {
        //        NatID = id;
        //        HP = data[0];
        //        Atk = data[1];
        //        Def = data[2];
        //        SpA = data[3];
        //        SpD = data[4];
        //        Spe = data[5];
        //        Type1 = (Type)data[6];
        //        Type2 = (Type)data[7];
        //        CatchRate = data[8];
        //        GenderRatio = data[18];
        //        Form = data[29];
        //        Height = BitConverter.ToUInt16(data, 36);
        //        Weight = BitConverter.ToUInt16(data, 38);

        //        //  old gen data
        //        HP_Old = data[40];
        //        Atk_Old = data[41];
        //        Def_Old = data[42];
        //        SpA_Old = data[43];
        //        SpD_Old = data[44];
        //        Spe_Old = data[45];

        //        CatchRateORAS = data[46];

        //        Forms = new List<Pokemon>();
        //    }
        //    else
        //    {
        //        // Alternative forms instanciation
        //        NatID = id;
        //        HP = data[0];
        //        Atk = data[1];
        //        Def = data[2];
        //        SpA = data[3];
        //        SpD = data[4];
        //        Spe = data[5];
        //        Type1 = (Type)data[6];
        //        Type2 = (Type)data[7];
        //        Form = data[14];
        //        CatchRate = 0;
        //        GenderRatio = 0;
        //        Height = BitConverter.ToUInt16(data, 20);
        //        Weight = BitConverter.ToUInt16(data, 22);

        //        HP_Old = data[0];
        //        Atk_Old = data[1];
        //        Def_Old = data[2];
        //        SpA_Old = data[3];
        //        SpD_Old = data[4];
        //        Spe_Old = data[5];

        //    }


        //}



    }
}
