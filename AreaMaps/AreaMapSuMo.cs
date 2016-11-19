using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{

    // Since Sun/Moon encounter slot structure changes drastically from other generations, this class is not a child class of AreaMap 
    class AreaMapSuMo
    {
        private EncounterSlot[][] Slots;


        internal AreaMapSuMo(byte[] data, Version version)
        {
            if (data.Length % 336 != 0) return; //All tables should be 336-byte long

            int nbTables = data.Length / 336;

            Slots = new EncounterSlot[nbTables][];
            for(int i = 0; i < nbTables; i++)
            {
                Slots[i] = new EncounterSlot[10];
                //for(int j = 0; j < 10; j++) Slots[i][j] = new EncounterSlot()

            }
        }

    }
}
