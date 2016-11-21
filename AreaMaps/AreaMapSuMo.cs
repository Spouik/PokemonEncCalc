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
        private EncounterSlot[][][] SOS_Slots;
        internal int NumberTables => Slots.Length / 2;

        internal AreaMapSuMo(byte[] data, Version version)
        {
            if (data.Length % 384 != 0) return; //All tables should be 384-byte long

            int nbTables = data.Length / 384;

            Slots = new EncounterSlot[nbTables][];
            SOS_Slots = new EncounterSlot[nbTables][][];
            for(int i = 0; i < nbTables; i++)
            {
                Slots[i] = new EncounterSlot[10];

                // Regular Slots
                for (int j = 0; j < 10; j++)
                    Slots[i][j] = new EncounterSlot(
                    PokemonTables.getPokemon(BitConverter.ToInt16(data, 384 * i + 4 * j + 16 - 28 * (i %2)), version),
                    data[384 * i + 4 - 28 * (i % 2)], data[384 * i + 5 - 28 * (i % 2)], data[384 * i + j + 6 - 28 * (i % 2)]);

                // SOS Slots
                SOS_Slots[i] = new EncounterSlot[7][];
                for(int sos = 0; sos < 7; sos++)
                {
                    SOS_Slots[i][sos] = new EncounterSlot[10];
                    for(int j = 0; j < 10; j++)
                        SOS_Slots[i][sos][j] = new EncounterSlot(
                        PokemonTables.getPokemon(BitConverter.ToInt16(data, 384 * i + 4 * j + sos * 20 + 56 - 28 * (i % 2)), version),
                        data[384 * i + 4 - 28 * (i % 2)], data[384 * i + 5 - 28 * (i % 2)], data[384 * i + j + 6 - 28 * (i % 2)]);
                }
            }
        }

        /// <summary>
        /// Get regular slots of the specified table
        /// </summary>
        /// <param name="TableNo">Table Number</param>
        /// <param name="Night">Is night ?</param>
        /// <returns>Returns the regular slots of the specified table, or null if the specified table does not exist</returns>
        internal EncounterSlot[] getSlots(int TableNo, bool Night)
        {
            if (TableNo * 2 >= NumberTables) return null;
            return Slots[TableNo * 2 + (Night ? 1 : 0)].Where(s => s.Percentage != 0).ToArray();
        }



    }
}
