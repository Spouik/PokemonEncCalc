using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
   

    class Pokemon
    {
        public short NatID { get; set; }
        public byte Form { get; set; }
        public Type Type1 { get; set; }
        public Type Type2 { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public byte CatchRate { get; set; }
        public byte GenderRatio { get; set; }
        
        // Base stats Gen 6 onwards
        public byte HP { get; set; }
        public byte Atk { get; set; }
        public byte Def { get; set; }
        public byte SpA { get; set; }
        public byte SpD { get; set; }
        public byte Spe { get; set; }

        // Base stats before Gen 6
        public byte HP_Old { get; set; }
        public byte Atk_Old { get; set; }
        public byte Def_Old { get; set; }
        public byte SpA_Old { get; set; }
        public byte SpD_Old { get; set; }
        public byte Spe_Old { get; set; }

        public byte CatchRateORAS { get; set; }

        // PokemonNames
        public string NameDE { get; set; }
        public string NameEN { get; set; }
        public string NameES { get; set; }
        public string NameFR { get; set; }
        public string NameIT { get; set; }
        public string NameJP { get; set; }
        public string NameKR { get; set; }

        // FormNames

        public string FormNameDE { get; set; }
        public string FormNameEN { get; set; }
        public string FormNameES { get; set; }
        public string FormNameFR { get; set; }
        public string FormNameIT { get; set; }
        public string FormNameJP { get; set; }
        public string FormNameKR { get; set; }

        // alternative forms
        public List<Pokemon> Forms { get;}

        public string getName()
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

        public string getFormName()
        {
            switch (Properties.Settings.Default.Language)
            {
                case 1: return FormNameEN == " " ? (NameEN + (NameEN.EndsWith("s") ? "'" : "'s") + " form") : FormNameEN;
                case 2: return FormNameFR == " " ? ("Forme de " + NameFR) : FormNameFR;
                case 3: return FormNameDE == " " ?  NameDE : FormNameDE ;
                case 4: return FormNameES == " " ? ("Forma de " + NameES) : FormNameES;
                case 5: return FormNameIT == " " ? ("Forma di" + NameIT) : FormNameIT;
                case 6: return FormNameJP == " " ? (NameJP + "のすがた") : FormNameJP;
                case 7: return FormNameKR == " " ? (NameKR + "의 모습") : FormNameKR;
                default: return FormNameEN == " " ? (NameEN + (NameEN.EndsWith("s") ? "'" : "'s") + " form") : FormNameEN;
            }
        }


        /*
         * Constructor going to resource to instanciate object
         * resourceID is the Pokemon ID (national Pokedex)
         * 
         */

        public Pokemon(short resourceID)
        {
            NatID = resourceID;
            HP = PokemonEncCalc.Properties.Resources.PokemonData[48 * (resourceID - 1)];
            Atk = PokemonEncCalc.Properties.Resources.PokemonData[48 * (resourceID - 1) + 1];
            Def = PokemonEncCalc.Properties.Resources.PokemonData[48 * (resourceID - 1) + 2];
            Spe = PokemonEncCalc.Properties.Resources.PokemonData[48 * (resourceID - 1) + 3];
            SpA = PokemonEncCalc.Properties.Resources.PokemonData[48 * (resourceID - 1) + 4];
            SpD = PokemonEncCalc.Properties.Resources.PokemonData[48 * (resourceID - 1) + 5];
            Type1 = (Type)PokemonEncCalc.Properties.Resources.PokemonData[48 * (resourceID - 1) + 6];
            Type2 = (Type)PokemonEncCalc.Properties.Resources.PokemonData[48 * (resourceID - 1) + 7];

        }

        /// <summary>
        /// Constructor instanciating the object from raw data
        /// </summary>
        /// <param name="id">1-based Pokémon identifier (used to load names)</param>
        /// <param name="data">48-byte Pokémon data / 24-byte Pokémon data for forms</param>
        public Pokemon(short id, byte[] data)
        {
            if (data.Length == 48)
            {
                NatID = id;
                HP = data[0];
                Atk = data[1];
                Def = data[2];
                SpA = data[3];
                SpD = data[4];
                Spe = data[5];
                Type1 = (Type)data[6];
                Type2 = (Type)data[7];
                CatchRate = data[8];
                GenderRatio = data[18];
                Form = data[29];
                Height = BitConverter.ToUInt16(data, 36);
                Weight = BitConverter.ToUInt16(data, 38);

                //  old gen data
                HP_Old = data[40];
                Atk_Old = data[41];
                Def_Old = data[42];
                SpA_Old = data[43];
                SpD_Old = data[44];
                Spe_Old = data[45];

                CatchRateORAS = data[46];

                Forms = new List<Pokemon>();
            }
            else
            {
                // Alternative forms instanciation
                NatID = id;
                HP = data[0];
                Atk = data[1];
                Def = data[2];
                SpA = data[3];
                SpD = data[4];
                Spe = data[5];
                Type1 = (Type)data[6];
                Type2 = (Type)data[7];
                Form = data[14];
                CatchRate = 0;
                GenderRatio = 0;
                Height = BitConverter.ToUInt16(data, 20);
                Weight = BitConverter.ToUInt16(data, 22);

                HP_Old = data[0];
                Atk_Old = data[1];
                Def_Old = data[2];
                SpA_Old = data[3];
                SpD_Old = data[4];
                Spe_Old = data[5];

            }
                

        }

        public void addForm(Pokemon pokemon)
        {
            Forms.Add(pokemon);
        }

        public int FormCount()
        {
            return Forms.Count;
        }

    }
}
