using System;
using System.Linq;

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

        protected override void createEncounterSlotArray(ref EncounterSlot[] slotArray, byte[] data, decimal[] percentArray)
        {

            Pokemon p;
            short species;
            byte formid;

            int nbSlots = data.Length / 4;

            if (percentArray.Length > nbSlots)
                throw new Exception("percentArray is smaller than Encounter slot data.");

            slotArray = new EncounterSlot[nbSlots];
            for (int i = 0; i < nbSlots; i++)
            {
                species = (short)(BitConverter.ToInt16(data, 4 * i + 2) & 0x3FF);
                formid = (byte)(data[4 * i + 3] >> 2);
                if (version == Version.Emerald) p = PokemonTables.pokemonEmeraldTable[species];
                else if (version == Version.FireRed || version == Version.LeafGreen) p = PokemonTables.pokemonFRLGTable[species];
                else p = PokemonTables.pokemonRSTable[species];
                if (formid > 0)
                    if (p.FormCount() >= formid)
                        if (p.Forms[formid - 1] != null)
                            p = p.Forms[formid - 1];
                slotArray[i] = new EncounterSlot(p, data[4 * i], data[4 * i + 1], percentArray[i]);
            }

        }

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
                createEncounterSlotArray(ref WalkSlots, grass, percentGrass);
                
            if (surf[0] != 0)
                createEncounterSlotArray(ref SurfSlots, surf, percentSurf);

            if (rockSmash[0] != 0)
                createEncounterSlotArray(ref RockSmashSlots, rockSmash, percentRockSmash);

            if (oldRod[0] != 0)
                createEncounterSlotArray(ref OldRodSlots, oldRod, percentOldRod);

            if (goodRod[0] != 0)
                createEncounterSlotArray(ref GoodRodSlots, goodRod, percentGoodRod);

            if (superRod[0] != 0)
                createEncounterSlotArray(ref SuperRodSlots, superRod, percentSuperRod);

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
        /// Get encounter slot data for a specific encounter type
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
