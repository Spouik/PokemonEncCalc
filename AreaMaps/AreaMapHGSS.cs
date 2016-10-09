using System;
using System.Linq;

namespace PokemonEncCalc
{
    class AreaMapHGSS : AreaMap
    {
        EncounterSlot[] OldRodSlots;
        EncounterSlot[] GoodRodSlots;
        EncounterSlot[] DaySlots;
        EncounterSlot[] NightSlots;
        EncounterSlot[] SwarmWalkSlots;
        EncounterSlot[] SwarmSurfSlots;
        EncounterSlot[] SwarmOldRodSlots;
        EncounterSlot[] SwarmGoodRodSlots;
        EncounterSlot[] SwarmSuperRodSlots;
        EncounterSlot[] NightGoodRodSlots;
        EncounterSlot[] NightSuperRodSlots;
        EncounterSlot[] HoennRadioSlots;
        EncounterSlot[] SinnohRadioSlots;
        EncounterSlot[] RockSmashSlots;

        private static decimal[] percentGrass = new decimal[] { 20, 20, 10, 10, 10, 10, 5, 5, 4, 4, 1, 1 };
        private static decimal[] percentSurf = new decimal[] { 60, 30, 5, 4, 1 };
        private static decimal[] percentRockSmash = new decimal[] { 90, 10 };
        private static decimal[] percentOldRod = new decimal[] { 60, 30, 5, 4, 1 };
        private static decimal[] percentGoodRod = new decimal[] { 40, 40, 15, 4, 1 };
        private static decimal[] percentSuperRod = new decimal[] { 40, 40, 15, 4, 1 };


        internal AreaMapHGSS(byte[] data, Version version, int idMap)
        {
            if (data.Length != 196)
                return;     // Wrong data size. Should be 196 bytes.

            this.version = version;
            map = idMap;

            //byte[] grassLevel = new byte[12];
            byte[] grass = new byte[48];
            byte[] swarmgrass = new byte[2];
            byte[] day = new byte[48];
            byte[] night = new byte[48];
            byte[] surf = new byte[20];
            byte[] oldRod = new byte[20];
            byte[] goodRod = new byte[20];
            byte[] superRod = new byte[20];
            byte[] rocksmash = new byte[8];
            byte[] hoenn = new byte[4];
            byte[] sinnoh = new byte[4];
            byte[] swarmsurf = new byte[2];
            byte[] nightfish = new byte[2];
            byte[] swarmfish = new byte[2];


            // Copy data to arrays
            for(int k= 0; k < 12;k++)
            {
                //Levels
                grass[4 * k] = grass[4 * k + 1] = data[8 + k];
                day[4 * k] = day[4 * k + 1] = data[8 + k];
                night[4 * k] = night[4 * k + 1] = data[8 + k];
                //Species
                grass[4 * k + 2] = data[20 + 2 * k];
                grass[4 * k + 3] = data[20 + 2 * k + 1];
                day[4 * k + 2] = data[44 + 2 * k];
                day[4 * k + 3] = data[44 + 2 * k + 1];
                night[4 * k + 2] = data[68 + 2 * k];
                night[4 * k + 3] = data[68 + 2 * k + 1];
            }
            //grassLevel = data.Skip(8).Take(12).ToArray();
            //grass = data.Skip(20).Take(24).ToArray();
            //day = data.Skip(44).Take(24).ToArray();
            //night = data.Skip(68).Take(24).ToArray();
            hoenn = data.Skip(92).Take(4).ToArray();
            sinnoh = data.Skip(96).Take(4).ToArray();
            surf = data.Skip(100).Take(20).ToArray();
            rocksmash = data.Skip(120).Take(8).ToArray();
            oldRod = data.Skip(128).Take(20).ToArray();
            goodRod = data.Skip(148).Take(20).ToArray();
            superRod = data.Skip(168).Take(20).ToArray();
            swarmgrass = data.Skip(188).Take(2).ToArray();
            swarmsurf = data.Skip(190).Take(2).ToArray();
            nightfish = data.Skip(192).Take(2).ToArray();
            swarmfish = data.Skip(194).Take(2).ToArray();

            // Create EncounterSlot only if data exists 
            // (checked via minLv of the first slot, which should not be 0 if data exists)


            if (grass[0] != 0)
            {
                //regular slots (morning, day, night)
                createEncounterSlotArray(ref WalkSlots, grass, percentGrass);
                createEncounterSlotArray(ref DaySlots, day, percentGrass);
                createEncounterSlotArray(ref NightSlots, night, percentGrass);

                // special slots (swarm, radio)
                createSpecialEncounterSlotArray(ref SwarmWalkSlots, swarmgrass, grass, new byte[] { 0, 1 }, percentGrass);
                createSpecialEncounterSlotArray(ref HoennRadioSlots, hoenn, grass, new byte[] { 2, 3, 4, 5 }, percentGrass);
                createSpecialEncounterSlotArray(ref SinnohRadioSlots, sinnoh, grass, new byte[] { 2, 3, 4, 5 }, percentGrass);
            }

            if (surf[0] != 0)
            {
                // regular slots
                createEncounterSlotArray(ref SurfSlots, surf, percentSurf);
                // special slots (swarm)
                createSpecialEncounterSlotArray(ref SwarmSurfSlots, swarmsurf, surf, new byte[] { 0 }, percentSurf);

            }

            if(rocksmash[0] != 0)
                createEncounterSlotArray(ref RockSmashSlots, rocksmash, percentRockSmash);


            if (oldRod[0] != 0)
            {
                // regular slots (old rod, good rod, super rod)
                createEncounterSlotArray(ref OldRodSlots, oldRod, percentOldRod);
                createEncounterSlotArray(ref GoodRodSlots, goodRod, percentGoodRod);
                createEncounterSlotArray(ref SuperRodSlots, superRod, percentSuperRod);

                // special slots (swarm, night)
                // w/ old rod
                createSpecialEncounterSlotArray(ref SwarmOldRodSlots, swarmfish, oldRod, new byte[] { 2 }, percentOldRod);
                // w/ good rod
                createSpecialEncounterSlotArray(ref SwarmGoodRodSlots, swarmfish, goodRod, new byte[] { 0, 2, 3 }, percentGoodRod);
                createSpecialEncounterSlotArray(ref NightGoodRodSlots, nightfish, goodRod, new byte[] { 3 }, percentGoodRod);
                // w/ super rod
                createSpecialEncounterSlotArray(ref SwarmSuperRodSlots, swarmfish, superRod, new byte[] { 0, 1, 2, 3, 4 }, percentSuperRod);
                createSpecialEncounterSlotArray(ref NightSuperRodSlots, nightfish, superRod, new byte[] { 1 }, percentSuperRod);


            }

        }

        internal bool isThereSwarm()
        {
            if (new[] { 78, 102 }.Contains(map)) return false;

            if (WalkSlots == null) return false;
            return (!(WalkSlots[0].Species == SwarmWalkSlots[0].Species));
        }

        internal bool isThereSurfSwarm()
        {
            // Special cases : Maps not recognized via the common check (Cerulean Cave : all floors)
            if (new[] { 139,140,141 }.Contains(map)) return false;

            if (SurfSlots == null) return false;
            return (!(SurfSlots[0].Species == SwarmSurfSlots[0].Species));
        }
        internal bool isThereFishSwarm()
        {
            if (OldRodSlots == null) return false;
            return (!(OldRodSlots[2].Species == SwarmOldRodSlots[0].Species));
        }


        internal bool isThereTimeOfDay()
        {
            if (WalkSlots == null) return false;
            return (!(WalkSlots[0].Equals(DaySlots[0]) && WalkSlots[0].Equals(NightSlots[0])
                && WalkSlots[1].Equals(DaySlots[1]) && WalkSlots[1].Equals(NightSlots[1])
                && WalkSlots[2].Equals(DaySlots[2]) && WalkSlots[2].Equals(NightSlots[2])
                && WalkSlots[3].Equals(DaySlots[3]) && WalkSlots[3].Equals(NightSlots[3])
                && WalkSlots[4].Equals(DaySlots[4]) && WalkSlots[4].Equals(NightSlots[4])
                && WalkSlots[5].Equals(DaySlots[5]) && WalkSlots[5].Equals(NightSlots[5])
                && WalkSlots[6].Equals(DaySlots[6]) && WalkSlots[6].Equals(NightSlots[6])
                && WalkSlots[7].Equals(DaySlots[7]) && WalkSlots[7].Equals(NightSlots[7])
                && WalkSlots[8].Equals(DaySlots[8]) && WalkSlots[8].Equals(NightSlots[8])
                && WalkSlots[9].Equals(DaySlots[9]) && WalkSlots[9].Equals(NightSlots[9])
                && WalkSlots[10].Equals(DaySlots[10]) && WalkSlots[10].Equals(NightSlots[10])
                && WalkSlots[11].Equals(DaySlots[11]) && WalkSlots[11].Equals(NightSlots[11])));
        }

        internal bool isThereFishNight()
        {
            // Special cases : Maps not recognized via the common check (Cerulean Cave : all floors + Cliff Edge Gate)
            if (new[] { 82, 139, 140, 141 }.Contains(map)) return false;

            if (OldRodSlots == null) return false;
            return (!(GoodRodSlots[3].Species == NightGoodRodSlots[0].Species));
        }


        internal bool isThereRadio()
        {
            if (WalkSlots == null) return false;
            // There is actually radio everywhere (where walk slots exist) except for Ruins of Alph (weird signal)
            // Check only if the species of regular slot 0 is an Unown.
            return !(WalkSlots[0].Species.NatID == 201);
        }

        /// <summary>
        /// Gets encounter slot data for a specific encounter type
        /// </summary>
        /// <param name="type">Encounter type</param>
        /// <returns>Encounter slot data, or null if data not available.</returns>
        internal EncounterSlot[] getSlots(EncounterType type, bool swarm = false, int timeOfDay = 0, int radio = 0)
        {
            EncounterSlot[] returnSlots = null, selectedSlots = null;

            switch (type)
            {
                case EncounterType.Walking:
                    returnSlots = new EncounterSlot[12];
                    selectedSlots = WalkSlots;
                    if (timeOfDay == 1) selectedSlots = DaySlots;
                    if (timeOfDay == 2) selectedSlots = NightSlots;
                    break;
                case EncounterType.RockSmash:
                    returnSlots = new EncounterSlot[2];
                    selectedSlots = RockSmashSlots;
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
            switch (type)
            {
                case EncounterType.Walking:
                    if (swarm)
                    {
                        returnSlots[0] = new EncounterSlot(SwarmWalkSlots[0]);
                        returnSlots[1] = new EncounterSlot(SwarmWalkSlots[1]);
                    }
                    if (radio == 1)
                    {
                        returnSlots[2] = new EncounterSlot(HoennRadioSlots[0]);
                        returnSlots[3] = new EncounterSlot(HoennRadioSlots[1]);
                        returnSlots[4] = new EncounterSlot(HoennRadioSlots[2]);
                        returnSlots[5] = new EncounterSlot(HoennRadioSlots[3]);
                    }
                    if (radio == 2)
                    {
                        returnSlots[2] = new EncounterSlot(SinnohRadioSlots[0]);
                        returnSlots[3] = new EncounterSlot(SinnohRadioSlots[1]);
                        returnSlots[4] = new EncounterSlot(SinnohRadioSlots[2]);
                        returnSlots[5] = new EncounterSlot(SinnohRadioSlots[3]);
                    }
                    break;
                case EncounterType.Surf:
                    if (swarm) returnSlots[0] = new EncounterSlot(SwarmSurfSlots[0]);
                    break;
                case EncounterType.OldRod:
                    if (swarm) returnSlots[2] = new EncounterSlot(SwarmOldRodSlots[0]);
                    break;
                case EncounterType.GoodRod:
                    if (timeOfDay == 2) returnSlots[3] = new EncounterSlot(NightGoodRodSlots[0]);
                    if (swarm)
                    {
                        returnSlots[0] = new EncounterSlot(SwarmGoodRodSlots[0]);
                        returnSlots[2] = new EncounterSlot(SwarmGoodRodSlots[1]);
                        returnSlots[3] = new EncounterSlot(SwarmGoodRodSlots[2]);
                    }
                    break;
                case EncounterType.SuperRod:
                    if (timeOfDay == 2) returnSlots[1] = new EncounterSlot(NightSuperRodSlots[0]);
                    if (swarm)
                    {
                        returnSlots[0] = new EncounterSlot(SwarmSuperRodSlots[0]);
                        returnSlots[1] = new EncounterSlot(SwarmSuperRodSlots[1]);
                        returnSlots[2] = new EncounterSlot(SwarmSuperRodSlots[2]);
                        returnSlots[3] = new EncounterSlot(SwarmSuperRodSlots[3]);
                        returnSlots[4] = new EncounterSlot(SwarmSuperRodSlots[4]);
                    }
                    break;
                default:
                    break;
            }
                return returnSlots;
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

        protected override void createEncounterSlotArray(ref EncounterSlot[] slotArray, byte[] data, decimal[] percentArray)
        {
            Pokemon p;
            short species;
            byte formid;

            int nbSlots = data.Length / 4;
            
            slotArray = new EncounterSlot[nbSlots];

            for (int i = 0; i < nbSlots; i++)
            {
                species = (short)(BitConverter.ToInt16(data, 4 * i + 2) & 0x3FF);
                formid = (byte)(data[4 * i + 3] >> 2);
                p = Utils.PokemonList[species - 1];
                if (formid > 0)
                    if (p.FormCount() >= formid)
                        if (p.Forms[formid - 1] != null)
                            p = p.Forms[formid - 1];
                slotArray[i] = new EncounterSlot(p, data[4*i], data[4*i + 1], percentArray[i]);
            }

        }

        protected void createSpecialEncounterSlotArray(ref EncounterSlot[] slotArray, byte[] data, byte[] regularData, byte[] affectedSlots, decimal[] percentage)
        {

            Pokemon p;
            short species;
            byte formid;

            int nbSlots = affectedSlots.Length;
            decimal r = data.Length / (2*nbSlots);

            if (percentage.Length < nbSlots)
                throw new Exception("percentage is smaller than Encounter slot data.");

            slotArray = new EncounterSlot[nbSlots];
            for (int s = 0; s < nbSlots; s++)
            {
                species = (short)(BitConverter.ToInt16(data, 2 * (int)(s * r)) & 0x3FF);
                formid = (byte)(data[2 * (int)(s * r) + 1] >> 2);
                p = Utils.PokemonList[species - 1];
                if (formid > 0)
                    if (p.FormCount() >= formid)
                        if (p.Forms[formid - 1] != null)
                            p = p.Forms[formid - 1];
                slotArray[s] = new EncounterSlot(p, regularData[4 * affectedSlots[s]], regularData[4 * affectedSlots[s]+1], percentage[affectedSlots[s]]);
            }
        }
    }
}
