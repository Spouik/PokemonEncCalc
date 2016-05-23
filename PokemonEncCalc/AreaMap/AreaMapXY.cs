using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    class AreaMapXY : AreaMap
    {
        EncounterSlot[] RedFlowersSlots;
        EncounterSlot[] YellowFlowersSlots;
        EncounterSlot[] PurpleFlowersSlots;
        EncounterSlot[] OldRodSlots;
        EncounterSlot[] GoodRodSlots;
        EncounterSlot[] RockSmash;
        EncounterSlot[] OtherSlots;
        EncounterSlot[] Horde1;
        EncounterSlot[] Horde2;
        EncounterSlot[] Horde3;


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
            redflowers = data.Skip(48).Take(48).ToArray();
            yellowflowers = data.Skip(96).Take(48).ToArray();
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
            {
                WalkSlots = new EncounterSlot[12];
                for (int i = 0; i < 12; i++)
                {
                    species = (short)(BitConverter.ToInt16(grass, 4 * i + 2) & 0x3FF);
                    formid = (byte)(grass[4 * i + 3] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    WalkSlots[i] = new EncounterSlot(p, grass[4 * i], grass[4 * i + 1], percentGrass[i]);
                }

            }

            // Red Flowers
            if (redflowers[0] != 0)
            {
                RedFlowersSlots = new EncounterSlot[12];
                for (int i = 0; i < 12; i++)
                {
                    species = (short)(BitConverter.ToInt16(redflowers, 4 * i + 2) & 0x3FF);
                    formid = (byte)(redflowers[4 * i + 3] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    RedFlowersSlots[i] = new EncounterSlot(p, redflowers[4 * i], redflowers[4 * i + 1], percentGrass[i]);
                }

            }

            // Yellow Flowers
            if (yellowflowers[0] != 0)
            {
                YellowFlowersSlots = new EncounterSlot[12];
                for (int i = 0; i < 12; i++)
                {
                    species = (short)(BitConverter.ToInt16(yellowflowers, 4 * i + 2) & 0x3FF);
                    formid = (byte)(yellowflowers[4 * i + 3] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    YellowFlowersSlots[i] = new EncounterSlot(p, yellowflowers[4 * i], yellowflowers[4 * i + 1], percentGrass[i]);
                }

            }

            // Purple Flowers
            if (purpleflowers[0] != 0)
            {
                PurpleFlowersSlots = new EncounterSlot[12];
                for (int i = 0; i < 12; i++)
                {
                    species = (short)(BitConverter.ToInt16(purpleflowers, 4 * i + 2) & 0x3FF);
                    formid = (byte)(purpleflowers[4 * i + 3] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    PurpleFlowersSlots[i] = new EncounterSlot(p, purpleflowers[4 * i], purpleflowers[4 * i + 1], percentGrass[i]);
                }

            }

            // Surf
            if (surf[0] != 0)
            {
                SurfSlots = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(surf, 4 * i + 2) & 0x3FF);
                    formid = (byte)(surf[4 * i + 3] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    SurfSlots[i] = new EncounterSlot(p, surf[4 * i], surf[4 * i + 1], percentSurf[i]);
                }

            }

            // Old Rod
            if (oldrod[0] != 0)
            {
                OldRodSlots = new EncounterSlot[3];
                for (int i = 0; i < 3; i++)
                {
                    species = (short)(BitConverter.ToInt16(oldrod, 4 * i + 2) & 0x3FF);
                    formid = (byte)(oldrod[4 * i + 3] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    OldRodSlots[i] = new EncounterSlot(p, oldrod[4 * i], oldrod[4 * i + 1], percentOldRod[i]);
                }

            }

            // Good Rod
            if (goodrod[0] != 0)
            {
                GoodRodSlots = new EncounterSlot[3];
                for (int i = 0; i < 3; i++)
                {
                    species = (short)(BitConverter.ToInt16(goodrod, 4 * i + 2) & 0x3FF);
                    formid = (byte)(goodrod[4 * i + 3] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    GoodRodSlots[i] = new EncounterSlot(p, goodrod[4 * i], goodrod[4 * i + 1], percentGoodRod[i]);
                }

            }

            // Super Rod
            if (superrod[0] != 0)
            {
                SuperRodSlots = new EncounterSlot[3];
                for (int i = 0; i < 3; i++)
                {
                    species = (short)(BitConverter.ToInt16(superrod, 4 * i + 2) & 0x3FF);
                    formid = (byte)(superrod[4 * i + 3] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    SuperRodSlots[i] = new EncounterSlot(p, superrod[4 * i], superrod[4 * i + 1], percentSuperRod[i]);
                }

            }

            // Rock Smash
            if (rocksmash[0] != 0)
            {
                RockSmash = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(rocksmash, 4 * i + 2) & 0x3FF);
                    formid = (byte)(rocksmash[4 * i + 3] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    RockSmash[i] = new EncounterSlot(p, rocksmash[4 * i], rocksmash[4 * i + 1], percentRockSmash[i]);
                }

            }

            // Others
            if (other[0] != 0)
            {
                OtherSlots = new EncounterSlot[12];
                for (int i = 0; i < 12; i++)
                {
                    species = (short)(BitConverter.ToInt16(other, 4 * i + 2) & 0x3FF);
                    formid = (byte)(other[4 * i + 3] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    OtherSlots[i] = new EncounterSlot(p, other[4 * i], other[4 * i + 1], percentGrass[i]);
                }

            }

            // Horde1
            if (hordes[0] != 0)
            {
                Horde1 = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(hordes, 4 * i + 2) & 0x3FF);
                    formid = (byte)(hordes[4 * i + 3] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
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
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
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
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    Horde3[i] = new EncounterSlot(p, hordes[4 * i + 40], hordes[4 * i + 41], 20);
                }

            }


        }

    }
}
