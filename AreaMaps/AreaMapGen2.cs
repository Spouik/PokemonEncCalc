using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    class AreaMapGen2 : AreaMap
    {

        EncounterSlot[] WalkDaySlots;
        EncounterSlot[] WalkNightSlots;
        EncounterSlot[] OldRodSlots;
        EncounterSlot[] GoodRodSlots;
        EncounterSlot[] GoodRodNightSlots;
        EncounterSlot[] SuperRodNightSlots;
        EncounterSlot[] RockSmashSlots;
        EncounterSlot[] OutbreakMorningSlots;
        EncounterSlot[] OutbreakDaySlots;
        EncounterSlot[] OutbreakNightSlots;
        EncounterSlot[] OutbreakSurfSlots;
        EncounterSlot[] OutbreakOldRodSlots;
        EncounterSlot[] OutbreakGoodRodSlots;
        EncounterSlot[] OutbreakSuperRodSlots;
        EncounterSlot[] BugCatchingSlots;


        private static decimal[] percentGrass = new decimal[] { 30, 30, 20, 10, 5, 4, 1 };
        private static decimal[] percentSurf = new decimal[] { 60, 30, 10 };
        private static decimal[] percentRockSmash = new decimal[] { 90, 10 };
        private static decimal[] percentBugCatching = new decimal[] { 20, 20, 10, 10, 10, 10, 5, 5, 5, 5 };

        // These rates are not expressed in percentage, but in 'a/256'
        // Should be displayed as 'a/256' in the main form
        private static decimal[] rateOldRod = new decimal[] { 180, 38, 38 };
        private static decimal[] rateGoodRod = new decimal[] { 90, 89, 52, 25 };
        private static decimal[] rateSuperRod = new decimal[] { 103, 76, 52, 25 };

        internal AreaMapGen2(byte[] data, Version version, int idMap)
        {
            if (data.Length != 360)
                return;     // Wrong data size. Should be 360 bytes.

            this.version = version;
            map = idMap;

            byte[] grassM = new byte[28];
            byte[] grassD = new byte[28];
            byte[] grassN = new byte[28];

            byte[] surf = new byte[12];

            byte[] oldRod = new byte[12];
            byte[] goodRodMD = new byte[16];
            byte[] goodRodN = new byte[16];
            byte[] superRodMD = new byte[16];
            byte[] superRodN = new byte[16];

            byte[] rockSmash = new byte[8];

            byte[] outbreakM = new byte[28];
            byte[] outbreakD = new byte[28];
            byte[] outbreakN = new byte[28];
            byte[] outbreakSurf = new byte[12];
            byte[] outbreakOldRod = new byte[12];
            byte[] outbreakGoodRod = new byte[16];
            byte[] outbreakSuperRod = new byte[16];

            byte[] bugCatching = new byte[40];

            // Copy data to arrays
            grassM = data.Take(28).ToArray();
            grassD = data.Skip(28).Take(28).ToArray();
            grassN = data.Skip(56).Take(28).ToArray();

            surf = data.Skip(84).Take(12).ToArray();

            oldRod = data.Skip(96).Take(12).ToArray();
            goodRodMD = data.Skip(108).Take(16).ToArray();
            goodRodN = data.Skip(124).Take(16).ToArray();
            superRodMD = data.Skip(140).Take(16).ToArray();
            superRodN = data.Skip(156).Take(16).ToArray();

            rockSmash = data.Skip(172).Take(8).ToArray();

            outbreakM = data.Skip(180).Take(28).ToArray();
            outbreakD = data.Skip(208).Take(28).ToArray();
            outbreakN = data.Skip(236).Take(28).ToArray();
            outbreakSurf = data.Skip(264).Take(12).ToArray();
            outbreakOldRod = data.Skip(276).Take(12).ToArray();
            outbreakGoodRod = data.Skip(288).Take(16).ToArray();
            outbreakSuperRod = data.Skip(304).Take(16).ToArray();

            bugCatching = data.Skip(320).Take(40).ToArray();

            // Create EncounterSlot only if data exists 
            // For Day/Night slots, create only if they are different from Morning
            // (checked via minLv of the first slot, which should not be 0 if data exists)

            // WalkSlots (used for walk morning)
            if (grassM[0] != 0)
            {
                createEncounterSlotArray(ref WalkSlots, grassM, percentGrass);
                createEncounterSlotArray(ref WalkDaySlots, grassD, percentGrass);
                createEncounterSlotArray(ref WalkNightSlots, grassN, percentGrass);
            }

            if(surf[0] != 0)
                createEncounterSlotArray(ref SurfSlots, surf, percentSurf);


            if (oldRod[0] != 0)
            {
                createEncounterSlotArray(ref OldRodSlots, oldRod, rateOldRod);
                createEncounterSlotArray(ref GoodRodSlots, goodRodMD, rateGoodRod);
                createEncounterSlotArray(ref GoodRodNightSlots, goodRodN, rateGoodRod);
                createEncounterSlotArray(ref SuperRodSlots, superRodMD, rateSuperRod);
                createEncounterSlotArray(ref SuperRodNightSlots, superRodN, rateSuperRod);
            }

            if (rockSmash[0] != 0)
                createEncounterSlotArray(ref RockSmashSlots, rockSmash, percentRockSmash);

            if (outbreakM[0] != 0)
            {
                createEncounterSlotArray(ref OutbreakMorningSlots, outbreakM, percentGrass);
                createEncounterSlotArray(ref OutbreakDaySlots, outbreakD, percentGrass);
                createEncounterSlotArray(ref OutbreakNightSlots, outbreakN, percentGrass);
            }

            if (outbreakSurf[0] != 0)
                createEncounterSlotArray(ref OutbreakSurfSlots, outbreakSurf, percentSurf);

            if (outbreakOldRod[0] != 0)
            {
                createEncounterSlotArray(ref OutbreakOldRodSlots, outbreakOldRod, rateOldRod);
                createEncounterSlotArray(ref OutbreakGoodRodSlots, outbreakGoodRod, rateGoodRod);
                createEncounterSlotArray(ref OutbreakSuperRodSlots, outbreakSuperRod, rateSuperRod);
            }
            if(bugCatching[0] != 0)
            {
                createEncounterSlotArray(ref BugCatchingSlots, bugCatching, percentBugCatching);
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

        internal bool isThereBugContest()
        {
            return BugCatchingSlots != null;
        }

        internal bool isThereWalkSwarm()
        {
            return OutbreakMorningSlots != null;
        }

        internal bool isThereSurfSwarm()
        {
            return OutbreakSurfSlots != null;
        }

        internal bool isThereFishSwarm()
        {
            return OutbreakOldRodSlots != null;
        }

        internal bool isThereWalkTime()
        {
            if (WalkSlots == null) return false;
            for(int i = 0; i < 7; i++)
            {
                if (!WalkSlots[i].Equals(WalkDaySlots[i])) return true;
                if (!WalkSlots[i].Equals(WalkNightSlots[i])) return true;
            }
            
            return false;
        }

        internal bool isThereFishTime()
        {
            if (OldRodSlots == null) return false;
            for (int i = 0; i < 4; i++)
            {
                if (!GoodRodSlots[i].Equals(GoodRodNightSlots[i])) return true;
                if (!SuperRodSlots[i].Equals(SuperRodNightSlots[i])) return true;
            }

            return false;
        }


        internal EncounterSlot[] getBugContestSlots()
        {
            if (BugCatchingSlots == null) return null;

            EncounterSlot[] returnSlots = new EncounterSlot[10];
            for(int i = 0; i < 10; i++)
            {
                returnSlots[i] = new EncounterSlot(BugCatchingSlots[i]);
            }
            return returnSlots;
        }

        /// <summary>
        /// Get encounter slot data for a specific encounter type
        /// </summary>
        /// <param name="type">Encounter type</param>
        /// <returns>Encounter slot data, or null if data not available.</returns>
        internal EncounterSlot[] getSlots(EncounterType type, bool swarm = false, int timeOfDay = 0)
        {
            EncounterSlot[] returnSlots = null, selectedSlots = null;

            switch (type)
            {
                case EncounterType.Walking:
                    returnSlots = new EncounterSlot[7];
                    selectedSlots = WalkSlots;
                    if (timeOfDay == 1 && !swarm) selectedSlots = WalkDaySlots;
                    if (timeOfDay == 2 && !swarm) selectedSlots = WalkNightSlots;
                    if (timeOfDay == 0 && swarm) selectedSlots = OutbreakMorningSlots;
                    if (timeOfDay == 1 && swarm) selectedSlots = OutbreakDaySlots;
                    if (timeOfDay == 2 && swarm) selectedSlots = OutbreakNightSlots;
                    break;
                case EncounterType.Surf:
                    returnSlots = new EncounterSlot[3];
                    selectedSlots = swarm ? OutbreakSurfSlots : SurfSlots;
                    break;
                case EncounterType.RockSmash:
                    returnSlots = new EncounterSlot[2];
                    selectedSlots = RockSmashSlots;
                    break;
                case EncounterType.OldRod:
                    returnSlots = new EncounterSlot[3];
                    selectedSlots = OldRodSlots;
                    selectedSlots = swarm ? OutbreakOldRodSlots : OldRodSlots;
                    break;
                case EncounterType.GoodRod:
                    returnSlots = new EncounterSlot[4];
                    if (swarm) selectedSlots = OutbreakGoodRodSlots;
                    else selectedSlots = (timeOfDay == 2) ? GoodRodNightSlots : GoodRodSlots;
                    break;
                case EncounterType.SuperRod:
                    returnSlots = new EncounterSlot[4];
                    if (swarm) selectedSlots = OutbreakSuperRodSlots;
                    else selectedSlots = (timeOfDay == 2) ? SuperRodNightSlots : SuperRodSlots;
                    break;
                default: return null;
            }

            for (int i = 0; i < selectedSlots.Length; i++)
            {
                returnSlots[i] = new EncounterSlot(selectedSlots[i]);
            }

            return returnSlots;

        }


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
                p = PokemonTables.pokemonGSTable[species];
                if(version == Version.Crystal) p = PokemonTables.pokemonCrystalTable[species];
                if (formid > 0)
                    if (p.FormCount() >= formid)
                        if (p.Forms[formid - 1] != null)
                            p = p.Forms[formid - 1];
                slotArray[i] = new EncounterSlot(p, data[4 * i], data[4 * i + 1], percentArray[i]);
            }
        }
    }
}
