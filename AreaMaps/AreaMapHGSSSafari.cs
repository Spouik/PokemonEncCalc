using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    class AreaMapHGSSSafari
    {

        private static decimal[] percent = new decimal[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
        private static int[] plainsDays = new int[] { 0, 10, 50, 90, 130, 170, 210 };
        private static int[] forestDays = new int[] { 0, 20, 60, 100, 140, 180, 220 };
        private static int[] rockDays = new int[] { 0, 30, 70, 110, 150, 190, 230 };
        private static int[] waterDays = new int[] { 0, 40, 80, 120, 160, 200, 240 };

        protected Version version { get; set; }
        protected int map { get; set; }
        protected bool water { get; set; }

        protected EncounterSlot[] WalkMorning;
        protected EncounterSlot[] WalkDay;
        protected EncounterSlot[] WalkNight;
        protected EncounterSlot[] WalkMorningBlock;
        protected EncounterSlot[] WalkDayBlock;
        protected EncounterSlot[] WalkNightBlock;
        protected int[][] NbWalkBlocks;

        protected EncounterSlot[] SurfMorning;
        protected EncounterSlot[] SurfDay;
        protected EncounterSlot[] SurfNight;
        protected EncounterSlot[] SurfMorningBlock;
        protected EncounterSlot[] SurfDayBlock;
        protected EncounterSlot[] SurfNightBlock;
        protected int[][] NbSurfBlocks;

        protected EncounterSlot[] OldRodMorning;
        protected EncounterSlot[] OldRodDay;
        protected EncounterSlot[] OldRodNight;
        protected EncounterSlot[] OldRodMorningBlock;
        protected EncounterSlot[] OldRodDayBlock;
        protected EncounterSlot[] OldRodNightBlock;
        protected int[][] NbOldRodBlocks;

        protected EncounterSlot[] GoodRodMorning;
        protected EncounterSlot[] GoodRodDay;
        protected EncounterSlot[] GoodRodNight;
        protected EncounterSlot[] GoodRodMorningBlock;
        protected EncounterSlot[] GoodRodDayBlock;
        protected EncounterSlot[] GoodRodNightBlock;
        protected int[][] NbGoodRodBlocks;

        protected EncounterSlot[] SuperRodMorning;
        protected EncounterSlot[] SuperRodDay;
        protected EncounterSlot[] SuperRodNight;
        protected EncounterSlot[] SuperRodMorningBlock;
        protected EncounterSlot[] SuperRodDayBlock;
        protected EncounterSlot[] SuperRodNightBlock;
        protected int[][] NbSuperRodBlocks;


        internal AreaMapHGSSSafari(byte[] data, Version version, int idMap)
        {
            if (data.Length != 912)
                return;     // Wrong data size. Should be 912 bytes.

            this.version = version;
            map = idMap;
            water = new[] { 1, 4, 5, 7, 8 }.Contains(map);

            byte[] grassM = new byte[40];
            byte[] grassD = new byte[40];
            byte[] grassN = new byte[40];
            byte[] grassBM = new byte[40];
            byte[] grassBD = new byte[40];
            byte[] grassBN = new byte[40];
            byte[] grassB_nb = new byte[40];

            byte[] surfM = new byte[40];
            byte[] surfD = new byte[40];
            byte[] surfN = new byte[40];
            byte[] surfB = new byte[36];
            byte[] surfB_nb = new byte[12];

            byte[] oldM = new byte[40];
            byte[] oldD = new byte[40];
            byte[] oldN = new byte[40];
            byte[] oldB = new byte[24];
            byte[] oldB_nb = new byte[8];

            byte[] goodM = new byte[40];
            byte[] goodD = new byte[40];
            byte[] goodN = new byte[40];
            byte[] goodB = new byte[24];
            byte[] goodB_nb = new byte[8];

            byte[] superM = new byte[40];
            byte[] superD = new byte[40];
            byte[] superN = new byte[40];
            byte[] superB = new byte[24];
            byte[] superB_nb = new byte[8];

            grassM = data.Skip(8).Take(40).ToArray();
            grassD = data.Skip(48).Take(40).ToArray();
            grassN = data.Skip(88).Take(40).ToArray();
            grassBM = data.Skip(128).Take(40).ToArray();
            grassBD = data.Skip(168).Take(40).ToArray();
            grassBN = data.Skip(208).Take(40).ToArray();
            grassB_nb = data.Skip(248).Take(40).ToArray();
            surfM = data.Skip(288).Take(40).ToArray();
            surfD = data.Skip(328).Take(40).ToArray();
            surfN = data.Skip(368).Take(40).ToArray();
            surfB = data.Skip(408).Take(36).ToArray();
            surfB_nb = data.Skip(444).Take(12).ToArray();
            oldM = data.Skip(456).Take(40).ToArray();
            oldD = data.Skip(496).Take(40).ToArray();
            oldN = data.Skip(536).Take(40).ToArray();
            oldB = data.Skip(576).Take(24).ToArray();
            oldB_nb = data.Skip(600).Take(8).ToArray();
            goodM = data.Skip(608).Take(40).ToArray();
            goodD = data.Skip(648).Take(40).ToArray();
            goodN = data.Skip(688).Take(40).ToArray();
            goodB = data.Skip(728).Take(24).ToArray();
            goodB_nb = data.Skip(752).Take(8).ToArray();
            superM = data.Skip(760).Take(40).ToArray();
            superD = data.Skip(800).Take(40).ToArray();
            superN = data.Skip(840).Take(40).ToArray();
            superB = data.Skip(880).Take(24).ToArray();
            superB_nb = data.Skip(904).Take(8).ToArray();


            createEncounterSlotArray(ref WalkMorning, grassM, percent);
            createEncounterSlotArray(ref WalkDay, grassD, percent);
            createEncounterSlotArray(ref WalkNight, grassN, percent);
            createEncounterSlotArray(ref WalkMorningBlock, grassBM, percent);
            createEncounterSlotArray(ref WalkDayBlock, grassBD, percent);
            createEncounterSlotArray(ref WalkNightBlock, grassBN, percent);

            createEncounterSlotArray(ref SurfMorning, surfM, percent);
            createEncounterSlotArray(ref SurfDay, surfD, percent);
            createEncounterSlotArray(ref SurfNight, surfN, percent);
            createEncounterSlotArray(ref SurfMorningBlock, surfB.Take(12).ToArray(), percent);
            createEncounterSlotArray(ref SurfDayBlock, surfB.Skip(12).Take(12).ToArray(), percent);
            createEncounterSlotArray(ref SurfNightBlock, surfB.Skip(24).Take(12).ToArray(), percent);

            createEncounterSlotArray(ref OldRodMorning, oldM, percent);
            createEncounterSlotArray(ref OldRodDay, oldD, percent);
            createEncounterSlotArray(ref OldRodNight, oldN, percent);
            createEncounterSlotArray(ref OldRodMorningBlock, oldB.Take(8).ToArray(), percent);
            createEncounterSlotArray(ref OldRodDayBlock, oldB.Skip(8).Take(8).ToArray(), percent);
            createEncounterSlotArray(ref OldRodNightBlock, oldB.Skip(16).Take(8).ToArray(), percent);

            createEncounterSlotArray(ref GoodRodMorning, goodM, percent);
            createEncounterSlotArray(ref GoodRodDay, goodD, percent);
            createEncounterSlotArray(ref GoodRodNight, goodN, percent);
            createEncounterSlotArray(ref GoodRodMorningBlock, goodB.Take(8).ToArray(), percent);
            createEncounterSlotArray(ref GoodRodDayBlock, goodB.Skip(8).Take(8).ToArray(), percent);
            createEncounterSlotArray(ref GoodRodNightBlock, goodB.Skip(16).Take(8).ToArray(), percent);

            createEncounterSlotArray(ref SuperRodMorning, superM, percent);
            createEncounterSlotArray(ref SuperRodDay, superD, percent);
            createEncounterSlotArray(ref SuperRodNight, superN, percent);
            createEncounterSlotArray(ref SuperRodMorningBlock, superB.Take(8).ToArray(), percent);
            createEncounterSlotArray(ref SuperRodDayBlock, superB.Skip(8).Take(8).ToArray(), percent);
            createEncounterSlotArray(ref SuperRodNightBlock, superB.Skip(16).Take(8).ToArray(), percent);

            createBlockRequirementArray(ref NbWalkBlocks, grassB_nb);
            createBlockRequirementArray(ref NbSurfBlocks, surfB_nb);
            createBlockRequirementArray(ref NbOldRodBlocks, oldB_nb);
            createBlockRequirementArray(ref NbGoodRodBlocks, goodB_nb);
            createBlockRequirementArray(ref NbSuperRodBlocks, superB_nb);



        }

        protected void createEncounterSlotArray(ref EncounterSlot[] slotArray, byte[] data, decimal[] percentArray)
        {
            Pokemon p;
            short speciesID;

            int nbSlots = data.Length / 4;

            slotArray = new EncounterSlot[nbSlots];

            for (int i = 0; i < nbSlots; i++)
            {
                speciesID = BitConverter.ToInt16(data, 4 * i);
                p = PokemonTables.getPokemon(speciesID, version);

                slotArray[i] = new EncounterSlot(p, data[4 * i + 2], data[4 * i + 2], percentArray[i]);
            }

        }

        protected void createBlockRequirementArray(ref int[][] blockArray, byte[] data)
        {
            int nbSlots = data.Length / 4;
            blockArray = new int[nbSlots][];
            for(int k = 0; k < nbSlots; k++)
            {
                blockArray[k] = new int[] { 0, 0, 0, 0 };
                if (new byte[] { 1, 2, 3, 4 }.Contains(data[4 * k])) blockArray[k][data[4 * k] - 1] += data[4 * k + 1];
                if (new byte[] { 1, 2, 3, 4 }.Contains(data[4 * k + 2])) blockArray[k][data[4 * k + 2] - 1] += data[4 * k + 3];

            }
        }

        protected bool checkBlockRequirement(int[] requiredBlocks, int plains, int forest, int rock, int water, int days)
        {
            int plainLv = 0, forestLv = 0, rockLv = 0, waterLv = 0;
            for(int k = 0; k < 7; k++)
            {
                if (days >= plainsDays[k]) plainLv = (k + 1) * plains;
                if (days >= forestDays[k]) forestLv = (k + 1) * forest;
                if (days >= rockDays[k]) rockLv = (k + 1) * rock;
                if (days >= waterDays[k]) waterLv = (k + 1) * water;
            }

            return requiredBlocks[0] <= plainLv && requiredBlocks[1] <= forestLv && requiredBlocks[2] <= rockLv && requiredBlocks[3] <= waterLv;
        }
            

        internal bool isThereWater() { return water; }

        internal EncounterSlot[] getSlots(EncounterType type, int timeOfDay = 0, int plainsBks = 0, int forestBks = 0, int rockBks = 0, int waterBks = 0, int days = 0)
        {
            EncounterSlot[] returnSlots = null, selectedSlots = null;

            returnSlots = new EncounterSlot[10];

            switch (type)
            {
                case EncounterType.Walking:
                    selectedSlots = WalkMorning;
                    if (timeOfDay == 1) selectedSlots = WalkDay;
                    if (timeOfDay == 2) selectedSlots = WalkNight;
                    for (int k = 0; k < 10; k++)
                    {
                        returnSlots[k] = new EncounterSlot(selectedSlots[k]);
                        if (checkBlockRequirement(NbWalkBlocks[k], plainsBks, forestBks, rockBks, waterBks, days))
                        {
                            if (timeOfDay == 0) returnSlots[k] = new EncounterSlot(WalkMorningBlock[k]);
                            if (timeOfDay == 1) returnSlots[k] = new EncounterSlot(WalkDayBlock[k]);
                            if (timeOfDay == 2) returnSlots[k] = new EncounterSlot(WalkNightBlock[k]);
                        }
                    }

                    break;
                case EncounterType.Surf:
                    selectedSlots = SurfMorning;
                    if (timeOfDay == 1) selectedSlots = SurfDay;
                    if (timeOfDay == 2) selectedSlots = SurfNight;
                    for (int k = 0; k < 10; k++)
                        returnSlots[k] = new EncounterSlot(selectedSlots[k]);

                    for (int k = 0; k < 3; k++)
                    {

                        if (checkBlockRequirement(NbSurfBlocks[k], plainsBks, forestBks, rockBks, waterBks, days))
                        {
                            if (timeOfDay == 0) returnSlots[k] = new EncounterSlot(SurfMorningBlock[k]);
                            if (timeOfDay == 1) returnSlots[k] = new EncounterSlot(SurfDayBlock[k]);
                            if (timeOfDay == 2) returnSlots[k] = new EncounterSlot(SurfNightBlock[k]);
                        }
                    }
                    break;
                case EncounterType.OldRod:
                    selectedSlots = OldRodMorning;
                    if (timeOfDay == 1) selectedSlots = OldRodDay;
                    if (timeOfDay == 2) selectedSlots = OldRodNight;
                    for (int k = 0; k < 10; k++)
                        returnSlots[k] = new EncounterSlot(selectedSlots[k]);

                    for (int k = 0; k < 2; k++)
                    {

                        if (checkBlockRequirement(NbOldRodBlocks[k], plainsBks, forestBks, rockBks, waterBks, days))
                        {
                            if (timeOfDay == 0) returnSlots[k] = new EncounterSlot(OldRodMorningBlock[k]);
                            if (timeOfDay == 1) returnSlots[k] = new EncounterSlot(OldRodDayBlock[k]);
                            if (timeOfDay == 2) returnSlots[k] = new EncounterSlot(OldRodNightBlock[k]);
                        }
                    }
                    break;
                case EncounterType.GoodRod:
                    selectedSlots = GoodRodMorning;
                    if (timeOfDay == 1) selectedSlots = GoodRodDay;
                    if (timeOfDay == 2) selectedSlots = GoodRodNight;
                    for (int k = 0; k < 10; k++)
                        returnSlots[k] = new EncounterSlot(selectedSlots[k]);

                    for (int k = 0; k < 2; k++)
                    {

                        if (checkBlockRequirement(NbGoodRodBlocks[k], plainsBks, forestBks, rockBks, waterBks, days))
                        {
                            if (timeOfDay == 0) returnSlots[k] = new EncounterSlot(GoodRodMorningBlock[k]);
                            if (timeOfDay == 1) returnSlots[k] = new EncounterSlot(GoodRodDayBlock[k]);
                            if (timeOfDay == 2) returnSlots[k] = new EncounterSlot(GoodRodNightBlock[k]);
                        }
                    }
                    break;
                case EncounterType.SuperRod:
                    selectedSlots = SuperRodMorning;
                    if (timeOfDay == 1) selectedSlots = SuperRodDay;
                    if (timeOfDay == 2) selectedSlots = SuperRodNight;
                    for (int k = 0; k < 10; k++)
                        returnSlots[k] = new EncounterSlot(selectedSlots[k]);

                    for (int k = 0; k < 2; k++)
                    {

                        if (checkBlockRequirement(NbSuperRodBlocks[k], plainsBks, forestBks, rockBks, waterBks, days))
                        {
                            if (timeOfDay == 0) returnSlots[k] = new EncounterSlot(SuperRodMorningBlock[k]);
                            if (timeOfDay == 1) returnSlots[k] = new EncounterSlot(SuperRodDayBlock[k]);
                            if (timeOfDay == 2) returnSlots[k] = new EncounterSlot(SuperRodNightBlock[k]);
                        }
                    }
                    break;
                default: return null;

            }

            return returnSlots;

        }


    }
}
