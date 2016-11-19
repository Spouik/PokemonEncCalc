using System;
using System.Linq;

namespace PokemonEncCalc
{
    class AreaMapDPPt : AreaMap
    {
        protected EncounterSlot[] OldRodSlots;
        protected EncounterSlot[] GoodRodSlots;
        protected EncounterSlot[] Day;
        protected EncounterSlot[] Night;
        protected EncounterSlot[] Swarm;
        protected EncounterSlot[] PokeRadar;

        // GBA-slot specific EncounterSlot
        protected EncounterSlot[] Ruby;
        protected EncounterSlot[] Sapphire;
        protected EncounterSlot[] Emerald;
        protected EncounterSlot[] FireRed;
        protected EncounterSlot[] LeafGreen;


        private static decimal[] percentGrass = new decimal[] { 20, 20, 10, 10, 10, 10, 5, 5, 4, 4, 1, 1 };
        private static decimal[] percentSurf = new decimal[] { 60, 30, 5, 4, 1 };
        private static decimal[] percentOldRod = new decimal[] { 60, 30, 5, 4, 1 };
        private static decimal[] percentGoodRod = new decimal[] { 40, 40, 15, 4, 1 };
        private static decimal[] percentSuperRod = new decimal[] { 40, 40, 15, 4, 1 };


        internal AreaMapDPPt(byte[] data, Version version, int idMap)
        {
            if (data.Length != 424)
                return;     // Wrong data size. Should be 424 bytes.

            this.version = version;
            map = idMap;

            byte[] grass = new byte[96];
            byte[] swarm = new byte[8];
            byte[] day = new byte[8];
            byte[] night = new byte[8];
            byte[] pokeRadar = new byte[16];
            byte[] ruby = new byte[8];
            byte[] sapphire = new byte[8];
            byte[] emerald = new byte[8];
            byte[] firered = new byte[8];
            byte[] leafgreen = new byte[8];
            byte[] surf = new byte[40];
            byte[] oldRod = new byte[40];
            byte[] goodRod = new byte[40];
            byte[] superRod = new byte[40];

            // Copy data to arrays
            grass = data.Skip(4).Take(96).ToArray();
            swarm = data.Skip(100).Take(8).ToArray();
            day = data.Skip(108).Take(8).ToArray();
            night = data.Skip(116).Take(8).ToArray();
            pokeRadar = data.Skip(124).Take(16).ToArray();
            ruby = data.Skip(164).Take(8).ToArray();
            sapphire = data.Skip(172).Take(8).ToArray();
            emerald = data.Skip(180).Take(8).ToArray();
            firered = data.Skip(188).Take(8).ToArray();
            leafgreen = data.Skip(196).Take(8).ToArray();
            surf = data.Skip(208).Take(40).ToArray();
            oldRod = data.Skip(296).Take(40).ToArray();
            goodRod = data.Skip(340).Take(40).ToArray();
            superRod = data.Skip(384).Take(40).ToArray();

            // Create EncounterSlot only if data exists 
            // (checked via minLv of the first slot, which should not be 0 if data exists)
            if (grass[0] != 0)
            {
                //create regular slots
                createEncounterSlotArray(ref WalkSlots, grass, percentGrass);

                //create special slots
                createSpecialEncounterSlotArray(ref Swarm, swarm, grass, new byte[] { 0, 1 }, percentGrass);                //swarm

                createSpecialEncounterSlotArray(ref Day, day, grass, new byte[] { 2, 3 }, percentGrass);                    //day
                createSpecialEncounterSlotArray(ref Night, night, grass, new byte[] { 2, 3 }, percentGrass);                //night

                createSpecialEncounterSlotArray(ref PokeRadar, pokeRadar, grass, new byte[] { 4,5,10,11}, percentGrass);    //pokeradar

                createSpecialEncounterSlotArray(ref Ruby, ruby, grass, new byte[] { 8, 9 }, percentGrass);                  // GBA slots
                createSpecialEncounterSlotArray(ref Sapphire, sapphire, grass, new byte[] { 8, 9 }, percentGrass);
                createSpecialEncounterSlotArray(ref Emerald, emerald, grass, new byte[] { 8, 9 }, percentGrass);
                createSpecialEncounterSlotArray(ref FireRed, firered, grass, new byte[] { 8, 9 }, percentGrass);
                createSpecialEncounterSlotArray(ref LeafGreen, leafgreen, grass, new byte[] { 8, 9 }, percentGrass);

            }

            if (surf[0] != 0)
                createEncounterSlotArray(ref SurfSlots, surf, percentSurf);

            if (oldRod[0] != 0)
                createEncounterSlotArray(ref OldRodSlots, oldRod, percentOldRod);


            if (goodRod[0] != 0)
                createEncounterSlotArray(ref GoodRodSlots, goodRod, percentGoodRod);


            if (superRod[0] != 0)
                createEncounterSlotArray(ref SuperRodSlots, superRod, percentSuperRod);



        }

        internal bool isThereSwarm()
        {
            if (WalkSlots == null) return false;
            return (!(WalkSlots[0].Species == Swarm[0].Species && WalkSlots[1].Species == Swarm[1].Species));
        }

        internal bool isTherePokeRadar()
        {
            if (WalkSlots == null) return false;
            return (!(WalkSlots[4].Species == PokeRadar[0].Species && WalkSlots[5].Species == PokeRadar[1].Species
                   && WalkSlots[10].Species == PokeRadar[2].Species && WalkSlots[11].Species == PokeRadar[3].Species));
        }

        internal bool isThereGBASlot()
        {
            if (WalkSlots == null) return false;
            return (!(WalkSlots[8].Species == Ruby[0].Species && WalkSlots[9].Species == Ruby[1].Species
                && WalkSlots[8].Species == Sapphire[0].Species && WalkSlots[9].Species == Sapphire[1].Species
                && WalkSlots[8].Species == Emerald[0].Species && WalkSlots[9].Species == Emerald[1].Species
                && WalkSlots[8].Species == FireRed[0].Species && WalkSlots[9].Species == FireRed[1].Species
                && WalkSlots[8].Species == LeafGreen[0].Species && WalkSlots[9].Species == LeafGreen[1].Species));
        }

        internal bool isThereTimeOfDay()
        {
            if (WalkSlots == null) return false;
            return (!(WalkSlots[2].Species == Day[0].Species && WalkSlots[3].Species == Day[1].Species
                    && WalkSlots[2].Species == Night[0].Species && WalkSlots[3].Species == Night[1].Species));
        }

        internal bool isExistingEncounterType(EncounterType type)
        {
            switch (type)
            {
                case EncounterType.Walking: return !(WalkSlots == null);
                case EncounterType.Surf: return !(SurfSlots == null);
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
        internal EncounterSlot[] getSlots(EncounterType type, bool swarm = false, int timeOfDay = 0, int gbaSlot = 0, bool radar = false)
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
                case EncounterType.OldRod:
                    returnSlots = new EncounterSlot[5];
                    selectedSlots = OldRodSlots;
                    break;
                case EncounterType.GoodRod:
                    returnSlots = new EncounterSlot[5];
                    selectedSlots = GoodRodSlots;
                    break;
                case EncounterType.SuperRod:
                    returnSlots = new EncounterSlot[5];
                    selectedSlots = SuperRodSlots;
                    break;
                default: return null;
            }

            for (int i = 0; i < selectedSlots.Length; i++)
            {
                returnSlots[i] = new EncounterSlot(selectedSlots[i]);
            }
            if(type == EncounterType.Walking)
            {
                if (swarm)
                {
                    returnSlots[0] = new EncounterSlot(Swarm[0]);
                    returnSlots[1] = new EncounterSlot(Swarm[1]);
                }
                if (radar)
                {
                    returnSlots[4] = new EncounterSlot(PokeRadar[0]);
                    returnSlots[5] = new EncounterSlot(PokeRadar[1]);
                    returnSlots[10] = new EncounterSlot(PokeRadar[2]);
                    returnSlots[11] = new EncounterSlot(PokeRadar[3]);
                }

                switch (timeOfDay)
                {
                    case 1:
                        returnSlots[2] = new EncounterSlot(Day[0]);
                        returnSlots[3] = new EncounterSlot(Day[1]);
                        break;
                    case 2:
                        returnSlots[2] = new EncounterSlot(Night[0]);
                        returnSlots[3] = new EncounterSlot(Night[1]);
                        break;
                    default: break;
                }
                switch (gbaSlot)
                {
                    case 1:
                        returnSlots[8] = new EncounterSlot(Ruby[0]);
                        returnSlots[9] = new EncounterSlot(Ruby[1]);
                        break;
                    case 2:
                        returnSlots[8] = new EncounterSlot(Sapphire[0]);
                        returnSlots[9] = new EncounterSlot(Sapphire[1]);
                        break;
                    case 3:
                        returnSlots[8] = new EncounterSlot(Emerald[0]);
                        returnSlots[9] = new EncounterSlot(Emerald[1]);
                        break;
                    case 4:
                        returnSlots[8] = new EncounterSlot(FireRed[0]);
                        returnSlots[9] = new EncounterSlot(FireRed[1]);
                        break;
                    case 5:
                        returnSlots[8] = new EncounterSlot(LeafGreen[0]);
                        returnSlots[9] = new EncounterSlot(LeafGreen[1]);
                        break;
                    default: break;
                }

            }

            return returnSlots;

        }

        protected override void createEncounterSlotArray(ref EncounterSlot[] slotArray, byte[] data, decimal[] percentArray)
        {
            Pokemon p;
            short speciesID;
            byte minLv, maxLv;

            int nbSlots = data.Length / 8;

            if(percentArray.Length < nbSlots)
                throw new Exception("percentArray is smaller than Encounter slot data.");

            slotArray = new EncounterSlot[nbSlots];
            for (int i = 0; i < nbSlots; i++)
            {
                speciesID = BitConverter.ToInt16(data, 8 * i + 4);
                p = PokemonTables.getPokemon(speciesID, version);

                maxLv = data[8 * i];
                minLv = data[8 * i + 1];
                if (minLv == 0) minLv = maxLv;
                slotArray[i] = new EncounterSlot(p, minLv, maxLv, percentArray[i]);
            }
        }

        protected void createSpecialEncounterSlotArray(ref EncounterSlot[] slotArray, byte[] data, byte[] regularData, byte[] affectedSlots, decimal[] percentage)
        {

            Pokemon p;
            short speciesID;
            byte minLv, maxLv;

            int nbSlots = data.Length / 4;

            if (percentage.Length < nbSlots)
                throw new Exception("percentage is smaller than Encounter slot data.");

            if (affectedSlots.Length < nbSlots)
                throw new Exception("Number of affected slots is smaller than Encounter slot data.");

            slotArray = new EncounterSlot[nbSlots];
            for (int s = 0; s < nbSlots; s++)
            {
                speciesID = BitConverter.ToInt16(data, 4 * s);
                 p = PokemonTables.getPokemon(speciesID, version);

                maxLv = regularData[8 * affectedSlots[s]];
                minLv = regularData[8 * affectedSlots[s] + 1];
                if (minLv == 0) minLv = maxLv;
                slotArray[s] = new EncounterSlot(p, minLv, maxLv, percentage[affectedSlots[s]]);
            }
        }
    }
}
