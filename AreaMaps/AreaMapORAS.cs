using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    class AreaMapORAS : AreaMap
    {
        EncounterSlot[] TallGrassSlots;
        EncounterSlot[] RockSmashSlots;
        EncounterSlot[] OldRodSlots;
        EncounterSlot[] GoodRodSlots;
        EncounterSlot[] Horde1;
        EncounterSlot[] Horde2;
        EncounterSlot[] Horde3;
        EncounterSlot[] DexNav;

        private static decimal[] percentGrass = new decimal[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 5, 4, 1 };
        private static decimal[] percentSurf = new decimal[] { 50, 30, 15, 4, 1 };
        private static decimal[] percentRockSmash = new decimal[] { 50, 30, 15, 4, 1 };
        private static decimal[] percentOldRod = new decimal[] { 60, 35, 5 };
        private static decimal[] percentGoodRod = new decimal[] { 60, 35, 5 };
        private static decimal[] percentSuperRod = new decimal[] { 60, 35, 5 };
        private static decimal[] percentHordes = new decimal[] { 60, 35, 5 };

        public AreaMapORAS(byte[] data, Version version, int idMap)
        {
            if (data.Length != 244)
                return;     // Wrong data size. Should be 376 bytes.

            this.version = version;
            map = idMap;


            byte[] grass = new byte[48];
            byte[] tallgrass = new byte[48];
            byte[] rocksmash = new byte[20];
            byte[] dexnav = new byte[12];
            byte[] oldrod = new byte[12];
            byte[] goodrod = new byte[12];
            byte[] superrod = new byte[12];
            byte[] surf = new byte[20];
            byte[] hordes = new byte[60];

            // Copy data to arrays
            grass = data.Skip(0).Take(48).ToArray();
            tallgrass = data.Skip(48).Take(48).ToArray();
            rocksmash = data.Skip(96).Take(20).ToArray();
            dexnav = data.Skip(116).Take(12).ToArray();
            oldrod = data.Skip(128).Take(12).ToArray();
            goodrod = data.Skip(140).Take(12).ToArray();
            superrod = data.Skip(152).Take(12).ToArray();
            surf = data.Skip(164).Take(20).ToArray();
            hordes = data.Skip(184).Take(60).ToArray();

            // Create EncounterSlot only if data exists 
            // (checked via minLv of the first slot, which should not be 0 if data exists)

            Pokemon p;
            short species;
            byte formid;

            // Regular grass / cave
            if (grass[0] != 0)
                createEncounterSlotArray(ref WalkSlots, grass, percentGrass);

            // Tall Grass
            if (tallgrass[0] != 0)
                createEncounterSlotArray(ref TallGrassSlots, tallgrass, percentGrass);

            // Rock Smash
            if (rocksmash[0] != 0)
                createEncounterSlotArray(ref RockSmashSlots, rocksmash, percentRockSmash);

            // DexNav
            if (dexnav[0] != 0)
            {
                DexNav = new EncounterSlot[3];
                for (int i = 0; i < 3; i++)
                {
                    species = (short)(BitConverter.ToInt16(dexnav, 4 * i) & 0x3FF);
                    formid = (byte)(dexnav[4 * i + 1] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    DexNav[i] = new EncounterSlot(p, dexnav[4 * i + 2], dexnav[4 * i + 3], 100/3);
                }

            }



            // Old Rod
            if (oldrod[0] != 0)
                createEncounterSlotArray(ref OldRodSlots, oldrod, percentOldRod);

            // Good Rod
            if (goodrod[0] != 0)
                createEncounterSlotArray(ref GoodRodSlots, goodrod, percentGoodRod);

            // Super Rod
            if (superrod[0] != 0)
                createEncounterSlotArray(ref SuperRodSlots, superrod, percentSuperRod);

            // Surf
            if (surf[0] != 0)
                createEncounterSlotArray(ref SurfSlots, surf, percentSurf);


            // Horde1
            if (hordes[0] != 0)
            {
                Horde1 = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(hordes, 4 * i) & 0x3FF);
                    formid = (byte)(hordes[4 * i + 1] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    Horde1[i] = new EncounterSlot(p, hordes[4 * i + 2], hordes[4 * i + 3], 20);
                }

            }

            // Horde2
            if (hordes[20] != 0)
            {
                Horde2 = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(hordes, 4 * i + 20) & 0x3FF);
                    formid = (byte)(hordes[4 * i + 21] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    Horde2[i] = new EncounterSlot(p, hordes[4 * i + 22], hordes[4 * i + 23], 20);
                }

            }

            // Horde3
            if (hordes[40] != 0)
            {
                Horde3 = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(hordes, 4 * i + 40) & 0x3FF);
                    formid = (byte)(hordes[4 * i + 41] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    Horde3[i] = new EncounterSlot(p, hordes[4 * i + 42], hordes[4 * i + 43], 20);
                }

            }


        }

        internal EncounterSlot[] getSlots(EncounterType type)
        {
            EncounterSlot[] selected = null, returnSlots = null;
            switch (type)
            {
                case EncounterType.Diving:
                case EncounterType.Walking:
                    selected = WalkSlots;
                    returnSlots = new EncounterSlot[12];
                    break;

                case EncounterType.TallGrass:
                    selected = TallGrassSlots;
                    returnSlots = new EncounterSlot[12];
                    break;
                case EncounterType.Surf:
                    selected = SurfSlots;
                    returnSlots = new EncounterSlot[5];
                    break;
                case EncounterType.RockSmash:
                    selected = RockSmashSlots;
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
                case EncounterType.Walking: return (WalkSlots == null) ? false : !(new[] { 9, 13, 15, 17, 18, 19 }.Contains(map));
                case EncounterType.Diving: return (WalkSlots == null) ? false : (new[] { 9, 13, 15, 17, 18, 19 }.Contains(map));
                case EncounterType.Surf: return !(SurfSlots == null);
                case EncounterType.RockSmash: return !(RockSmashSlots == null);
                case EncounterType.OldRod: return !(OldRodSlots == null);
                case EncounterType.GoodRod: return !(GoodRodSlots == null);
                case EncounterType.SuperRod: return !(SuperRodSlots == null);
                case EncounterType.TallGrass: return !(TallGrassSlots == null);
                default: return false;
            }
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
                species = (short)(BitConverter.ToInt16(data, 4 * i) & 0x3FF);
                formid = (byte)(data[4 * i + 1] >> 2);
                p = Utils.PokemonList[species - 1];
                if (formid > 0)
                    if (p.FormCount() >= formid)
                        if (p.Forms[formid - 1] != null)
                            p = p.Forms[formid - 1];
                slotArray[i] = new EncounterSlot(p, data[4 * i + 2], data[4 * i + 3], percentArray[i]);
            }
        }
    }
}
