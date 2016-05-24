using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    internal class AreaMapGen3 : AreaMap
    {
        EncounterSlot[] OldRodSlots;
        EncounterSlot[] GoodRodSlots;
        EncounterSlot[] RockSmashSlots;

        private static decimal[] percentGrass = new decimal[] {20, 20, 10, 10, 10, 10, 5, 5, 4, 4, 1, 1 };
        private static decimal[] percentSurf = new decimal[] { 60, 30, 5, 4, 1 };
        private static decimal[] percentRockSmash = new decimal[] { 60, 30, 5, 4, 1 };
        private static decimal[] percentOldRod = new decimal[] { 70, 30 };
        private static decimal[] percentGoodRod = new decimal[] { 60, 20, 20 };
        private static decimal[] percentSuperRod = new decimal[] { 40, 40, 15, 4, 1 };

        internal AreaMapGen3(byte[] data, Version version, int idMap)
        {
            if (data.Length != 128)
                return;     // Wrong data size. Should be 128 bytes.

            this.version = version;
            map = idMap;

            byte[] grass = new byte[48];
            byte[] surf = new byte[20];
            byte[] rockSmash = new byte[20];
            byte[] oldRod = new byte[8];
            byte[] goodRod = new byte[12];
            byte[] superRod = new byte[20];

            // Copy data to arrays
            grass = data.Take(48).ToArray();
            surf = data.Skip(48).Take(20).ToArray();
            rockSmash = data.Skip(68).Take(20).ToArray();
            oldRod = data.Skip(88).Take(8).ToArray();
            goodRod = data.Skip(96).Take(12).ToArray();
            superRod = data.Skip(108).Take(20).ToArray();

            // Create EncounterSlot only if data exists 
            // (checked via minLv of the first slot, which should not be 0 if data exists)
            if (grass[0] != 0)
            {
                WalkSlots = new EncounterSlot[12];
                for(int i = 0; i < 12; i++)
                {
                    short species = BitConverter.ToInt16(grass, 4 * i + 2);
                    WalkSlots[i] = new EncounterSlot(Utils.PokemonList[species - 1], grass[4 * i], grass[4 * i + 1], percentGrass[i]);
                }
            }

            if (surf[0] != 0)
            {
                SurfSlots = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    short species = BitConverter.ToInt16(surf, 4 * i + 2);
                    SurfSlots[i] = new EncounterSlot(Utils.PokemonList[species - 1], surf[4 * i], surf[4 * i + 1], percentSurf[i]);
                }
            }

            if (rockSmash[0] != 0)
            {
                RockSmashSlots = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    short species = BitConverter.ToInt16(rockSmash, 4 * i + 2);
                    RockSmashSlots[i] = new EncounterSlot(Utils.PokemonList[species - 1], rockSmash[4 * i], rockSmash[4 * i + 1], percentRockSmash[i]);
                }
            }

            if (oldRod[0] != 0)
            {
                OldRodSlots = new EncounterSlot[2];
                for (int i = 0; i < 2; i++)
                {
                    short species = BitConverter.ToInt16(oldRod, 4 * i + 2);
                    OldRodSlots[i] = new EncounterSlot(Utils.PokemonList[species - 1], oldRod[4 * i], oldRod[4 * i + 1], percentOldRod[i]);
                }
            }

            if (goodRod[0] != 0)
            {
                GoodRodSlots = new EncounterSlot[3];
                for (int i = 0; i < 3; i++)
                {
                    short species = BitConverter.ToInt16(goodRod, 4 * i + 2);
                    GoodRodSlots[i] = new EncounterSlot(Utils.PokemonList[species - 1], goodRod[4 * i], goodRod[4 * i + 1], percentGoodRod[i]);
                }
            }

            if (superRod[0] != 0)
            {
                SuperRodSlots = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    short species = BitConverter.ToInt16(superRod, 4 * i + 2);
                    SuperRodSlots[i] = new EncounterSlot(Utils.PokemonList[species - 1], superRod[4 * i], superRod[4 * i + 1], percentSuperRod[i]);
                }
            }


        }


        internal bool isExistingEncounterType(EncounterType type)
        {
            switch (type)
            {
                case EncounterType.Walking: return !(WalkSlots == null);
                case EncounterType.Surf: return !(SurfSlots == null);
                case EncounterType.RockSmash: return !(RockSmashSlots == null);
                case EncounterType.OldRod: return !(OldRodSlots == null);
                case EncounterType.GoodRod: return !(GoodRodSlots == null);
                case EncounterType.SuperRod: return !(SuperRodSlots == null);
                default: return false;
            }
        }


        /// <summary>
        /// Gets encounter slot data for a specific encounter type
        /// </summary>
        /// <param name="type">Encounter type</param>
        /// <returns>Encounter slot data, or null if data not available.</returns>
        internal EncounterSlot[] getSlots(EncounterType type)
        {
            EncounterSlot[] returnSlots = null, selectedSlots = null;

            switch (type)
            {
                case EncounterType.Walking:
                    returnSlots = new EncounterSlot[12];
                    selectedSlots = WalkSlots;
                    break;
                case EncounterType.Surf:
                    returnSlots = new EncounterSlot[5];
                    selectedSlots = SurfSlots;
                    break;
                case EncounterType.RockSmash:
                    returnSlots = new EncounterSlot[5];
                    selectedSlots = RockSmashSlots;
                    break;
                case EncounterType.OldRod:
                    returnSlots = new EncounterSlot[2];
                    selectedSlots = OldRodSlots;
                    break;
                case EncounterType.GoodRod:
                    returnSlots = new EncounterSlot[3];
                    selectedSlots = GoodRodSlots;
                    break;
                case EncounterType.SuperRod:
                    returnSlots = new EncounterSlot[5];
                    selectedSlots = SuperRodSlots;
                    break;
                default: return null;
            }

            for(int i =0; i< selectedSlots.Length; i++)
            {
                returnSlots[i] = new EncounterSlot(selectedSlots[i]);
            }

            return returnSlots;

        }

    }
}
