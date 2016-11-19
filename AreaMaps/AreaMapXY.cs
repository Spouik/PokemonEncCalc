using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    class AreaMapXY : AreaMap
    {
        protected EncounterSlot[] RedFlowersSlots;
        protected EncounterSlot[] YellowFlowersSlots;
        protected EncounterSlot[] PurpleFlowersSlots;
        protected EncounterSlot[] OldRodSlots;
        protected EncounterSlot[] GoodRodSlots;
        protected EncounterSlot[] RockSmash;
        protected EncounterSlot[] OtherSlots;
        protected EncounterSlot[] Horde1;
        protected EncounterSlot[] Horde2;
        protected EncounterSlot[] Horde3;


        private static decimal[] percentGrass = new decimal[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 5, 4, 1 };
        private static decimal[] percentSurf = new decimal[] { 50, 30, 15, 4, 1 };
        private static decimal[] percentRockSmash = new decimal[] { 50, 30, 15, 4, 1 };
        private static decimal[] percentOldRod = new decimal[] { 60, 35, 5};
        private static decimal[] percentGoodRod = new decimal[] { 60, 35, 5 };
        private static decimal[] percentSuperRod = new decimal[] { 60, 35, 5 };
        private static decimal[] percentHordes = new decimal[] { 60, 35, 5 };


        public AreaMapXY(byte[] data, Version version, int idMap)
        {
            if (data.Length != 376)
                return;     // Wrong data size. Should be 376 bytes.

            this.version = version;
            map = idMap;


            byte[] grass = new byte[48];
            byte[] redflowers = new byte[48];
            byte[] yellowflowers = new byte[48];
            byte[] purpleflowers = new byte[48];
            byte[] surf = new byte[20];
            byte[] rocksmash = new byte[20];
            byte[] oldrod = new byte[12];
            byte[] goodrod = new byte[12];
            byte[] superrod = new byte[12];
            byte[] other = new byte[48];
            byte[] hordes = new byte[60];

            // Copy data to arrays
            grass = data.Skip(0).Take(48).ToArray();
            redflowers = data.Skip(96).Take(48).ToArray();
            yellowflowers = data.Skip(48).Take(48).ToArray();
            purpleflowers = data.Skip(144).Take(48).ToArray();
            surf = data.Skip(192).Take(20).ToArray();
            oldrod = data.Skip(212).Take(12).ToArray();
            goodrod = data.Skip(224).Take(12).ToArray();
            superrod = data.Skip(236).Take(12).ToArray();
            rocksmash = data.Skip(248).Take(20).ToArray();
            other = data.Skip(268).Take(48).ToArray();
            hordes = data.Skip(316).Take(60).ToArray();

            // Create EncounterSlot only if data exists 
            // (checked via minLv of the first slot, which should not be 0 if data exists)

            Pokemon p;
            short species;
            byte formid;

            // Regular grass / cave
            if (grass[0] != 0)
                createEncounterSlotArray(ref WalkSlots, grass, percentGrass);

            // Red Flowers
            if (redflowers[0] != 0)
                createEncounterSlotArray(ref RedFlowersSlots, redflowers, percentGrass);

            // Yellow Flowers
            if (yellowflowers[0] != 0)
                createEncounterSlotArray(ref YellowFlowersSlots, yellowflowers, percentGrass);

            // Purple Flowers
            if (purpleflowers[0] != 0)
                createEncounterSlotArray(ref PurpleFlowersSlots, purpleflowers, percentGrass);

            // Surf
            if (surf[0] != 0)
                createEncounterSlotArray(ref SurfSlots, surf, percentSurf);

            // Old Rod
            if (oldrod[0] != 0)
                createEncounterSlotArray(ref OldRodSlots, oldrod, percentOldRod);

            // Good Rod
            if (goodrod[0] != 0)
                createEncounterSlotArray(ref GoodRodSlots, goodrod, percentGoodRod);

            // Super Rod
            if (superrod[0] != 0)
                createEncounterSlotArray(ref SuperRodSlots, superrod, percentSuperRod);

            // Rock Smash
            if (rocksmash[0] != 0)
                createEncounterSlotArray(ref RockSmash, rocksmash, percentRockSmash);

            // Others
            if (other[0] != 0)
                createEncounterSlotArray(ref OtherSlots, other, percentGrass);


            // Horde1
            if (hordes[0] != 0)
            {
                Horde1 = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(hordes, 4 * i + 2) & 0x3FF);
                    formid = (byte)(hordes[4 * i + 3] >> 2);
                    p = PokemonTables.pokemonXYTable[species];
                    if (formid > 0)
                        if (p.FormCount() >= formid)
                            if (p.Forms[formid - 1] != null)
                                p = p.Forms[formid - 1];
                    Horde1[i] = new EncounterSlot(p, hordes[4 * i], hordes[4 * i + 1], 20);
                }

            }

            // Horde2
            if (hordes[20] != 0)
            {
                Horde2 = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(hordes, 4 * i + 22) & 0x3FF);
                    formid = (byte)(hordes[4 * i + 23] >> 2);
                    p = PokemonTables.pokemonXYTable[species];
                    if (formid > 0)
                        if (p.FormCount() >= formid)
                            if (p.Forms[formid - 1] != null)
                                p = p.Forms[formid - 1];
                    Horde2[i] = new EncounterSlot(p, hordes[4 * i + 20], hordes[4 * i + 21], 20);
                }

            }

            // Horde3
            if (hordes[40] != 0)
            {
                Horde3 = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(hordes, 4 * i + 42) & 0x3FF);
                    formid = (byte)(hordes[4 * i + 43] >> 2);
                    p = PokemonTables.pokemonXYTable[species];
                    if (formid > 0)
                        if (p.FormCount() >= formid)
                            if (p.Forms[formid - 1] != null)
                                p = p.Forms[formid - 1];
                    Horde3[i] = new EncounterSlot(p, hordes[4 * i + 40], hordes[4 * i + 41], 20);
                }

            }


        }

        internal EncounterSlot[] getSlots(EncounterType type)
        {
            EncounterSlot[] selected = null, returnSlots = null;
            switch (type)
            {
                case EncounterType.Walking:
                    if (new[] { 20, 24, 28 }.Contains(map))
                        selected = OtherSlots;
                    else
                        selected = WalkSlots;
                    returnSlots = new EncounterSlot[12];
                    break;

                case EncounterType.RedFlowers:
                    selected = RedFlowersSlots;
                    returnSlots = new EncounterSlot[12];
                    break;
                case EncounterType.YellowFlowers:
                    selected = YellowFlowersSlots;
                    returnSlots = new EncounterSlot[12];
                    break;
                case EncounterType.PurpleFlowers:
                    selected = PurpleFlowersSlots;
                    returnSlots = new EncounterSlot[12];
                    break;

                case EncounterType.Surf:
                    selected = SurfSlots;
                    returnSlots = new EncounterSlot[5];
                    break;
                case EncounterType.RockSmash:
                    selected = RockSmash;
                    returnSlots = new EncounterSlot[5];
                    break;
                case EncounterType.OldRod:
                    selected = OldRodSlots;
                    returnSlots = new EncounterSlot[3];
                    break;
                case EncounterType.GoodRod:
                    selected = GoodRodSlots;
                    returnSlots = new EncounterSlot[3];
                    break;
                case EncounterType.SuperRod:
                    selected = SuperRodSlots;
                    returnSlots = new EncounterSlot[3];
                    break;
                case EncounterType.ShallowWater:
                case EncounterType.TallGrass:
                    selected = OtherSlots;
                    returnSlots = new EncounterSlot[12];
                    break;

                default: return null;

            }
            

            for (int i = 0; i < selected.Length; i++)
            {
                returnSlots[i] = new EncounterSlot(selected[i]);
            }


            return returnSlots;
        }

        internal bool isExistingEncounterType(EncounterType type)
        {
            switch (type)
            {
                case EncounterType.Walking: return !(WalkSlots == null) ? true : (new[] { 20, 24, 28 }.Contains(map));
                case EncounterType.RedFlowers: return !(RedFlowersSlots == null);
                case EncounterType.YellowFlowers: return !(YellowFlowersSlots == null);
                case EncounterType.PurpleFlowers: return !(PurpleFlowersSlots == null);
                case EncounterType.Surf: return !(SurfSlots == null);
                case EncounterType.RockSmash: return !(RockSmash == null);
                case EncounterType.OldRod: return !(OldRodSlots == null);
                case EncounterType.GoodRod: return !(GoodRodSlots == null);
                case EncounterType.SuperRod: return !(SuperRodSlots == null);
                case EncounterType.ShallowWater: return new[] { 25, 30 }.Contains(map);
                case EncounterType.TallGrass: return new[] { 17, 27 }.Contains(map);
                default: return false;
            }
        }

        protected override void createEncounterSlotArray(ref EncounterSlot[] slotArray, byte[] data, decimal[] percentArray)
        {
            Pokemon p;
            short speciesID;

            int nbSlots = data.Length / 4;

            if (percentArray.Length > nbSlots)
                throw new Exception("percentArray is smaller than Encounter slot data.");

            slotArray = new EncounterSlot[nbSlots];
            for (int i = 0; i < nbSlots; i++)
            {
                speciesID = BitConverter.ToInt16(data, 4 * i + 2);
                p = PokemonTables.getPokemon(speciesID, version);
                slotArray[i] = new EncounterSlot(p, data[4 * i], data[4 * i + 1], percentArray[i]);
            }
        }
    }
}
