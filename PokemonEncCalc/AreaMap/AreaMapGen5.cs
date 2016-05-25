using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    class AreaMapGen5 : AreaMap
    {
        EncounterSlot[] DarkGrassSlots;
        EncounterSlot[] ShakingSpots;
        EncounterSlot[] SurfRippling;
        EncounterSlot[] FishRippling;


        private static decimal[] percentGrass = new decimal[] { 20, 20, 10, 10, 10, 10, 5, 5, 4, 4, 1, 1 };
        private static decimal[] percentSurf = new decimal[] { 60, 30, 5, 4, 1 };
        private static decimal[] percentSuperRod = new decimal[] { 40, 40, 15, 4, 1 };

        // Lucky Power percentage (Lv 1)
        private static decimal[] percentGrassLv1 = new decimal[] { 10, 10, 10, 10, 10, 10, 10, 10, 5, 5, 5, 5 };
        private static decimal[] percentSurfLv1 = new decimal[] { 50, 30, 10, 5, 5 };
        private static decimal[] percentSuperRodLv1 = new decimal[] { 40, 35, 15, 5, 5 };

        // Lucky Power percentage (Lv 2)
        private static decimal[] percentGrassLv2 = new decimal[] { 5, 5, 5, 5, 10, 10, 10, 10, 10, 10, 10, 10 };
        private static decimal[] percentSurfLv2 = new decimal[] { 40, 30, 10, 10, 10 };
        private static decimal[] percentSuperRodLv2 = new decimal[] { 30, 30, 20, 10, 10 };

        // Lucky Power percentage (Lv 3)
        private static decimal[] percentGrassLv3 = new decimal[] { 1, 1, 4, 4, 5, 5, 10, 10, 10, 10, 20, 20 };
        private static decimal[] percentSurfLv3 = new decimal[] { 30, 20, 10, 20, 20 };
        private static decimal[] percentSuperRodLv3 = new decimal[] { 20, 20, 20, 20, 20 };


        public AreaMapGen5(byte[] data, Version version, int idMap)
        {
            if (data.Length != 232)
                return;     // Wrong data size. Should be 232 bytes.

            this.version = version;
            map = idMap;


            byte[] grass = new byte[48];
            byte[] darkgrass = new byte[48];
            byte[] shaking = new byte[48];
            byte[] surf = new byte[20];
            byte[] surfrippling = new byte[20];
            byte[] fish = new byte[20];
            byte[] fishrippling = new byte[20];

            // Copy data to arrays
            grass = data.Skip(8).Take(48).ToArray();
            darkgrass = data.Skip(56).Take(48).ToArray();
            shaking = data.Skip(104).Take(48).ToArray();
            surf = data.Skip(152).Take(20).ToArray();
            surfrippling = data.Skip(172).Take(20).ToArray();
            fish = data.Skip(192).Take(20).ToArray();
            fishrippling = data.Skip(212).Take(20).ToArray();

            // Create EncounterSlot only if data exists 
            // (checked via minLv of the first slot, which should not be 0 if data exists)

            Pokemon p;
            short species;
            byte formid;

            // Regular grass / cave
            if (grass[2] != 0)
            { 
                WalkSlots = new EncounterSlot[12];
                for (int i = 0; i < 12; i++)
                {
                    species = (short)(BitConverter.ToInt16(grass, 4 * i) & 0x7FF);
                    formid = (byte)(grass[4 * i + 1] >> 3);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() >= formid)
                            if (p.Forms[formid-1] != null)
                                p = p.Forms[formid-1];
                    WalkSlots[i] = new EncounterSlot(p, grass[4 * i + 2], grass[4 * i + 3], percentGrass[i]);
                }
                
            }

            // Dark grass (where 'double encounter' can occur)
            if (darkgrass[2] != 0)
            {
                DarkGrassSlots = new EncounterSlot[12];
                for (int i = 0; i < 12; i++)
                {
                    species = (short)(BitConverter.ToInt16(darkgrass, 4 * i) & 0x7FF);
                    formid = (byte)(darkgrass[4 * i + 1] >> 3);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() >= formid)
                            if (p.Forms[formid - 1] != null)
                                p = p.Forms[formid - 1];
                    DarkGrassSlots[i] = new EncounterSlot(p, darkgrass[4 * i + 2], darkgrass[4 * i + 3], percentGrass[i]);
                }

            }

            // Shaking spots
            if (shaking[2] != 0)
            {
                ShakingSpots = new EncounterSlot[12];
                for (int i = 0; i < 12; i++)
                {
                    species = (short)(BitConverter.ToInt16(shaking, 4 * i) & 0x7FF);
                    formid = (byte)(shaking[4 * i + 1] >> 3);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() >= formid)
                            if (p.Forms[formid - 1] != null)
                                p = p.Forms[formid - 1];
                    ShakingSpots[i] = new EncounterSlot(p, shaking[4 * i + 2], shaking[4 * i + 3], percentGrass[i]);
                }

            }

            // Surf
            if (surf[2] != 0)
            {
                SurfSlots = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(surf, 4 * i) & 0x7FF);
                    formid = (byte)(surf[4 * i + 1] >> 3);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() >= formid)
                            if (p.Forms[formid - 1] != null)
                                p = p.Forms[formid - 1];
                    SurfSlots[i] = new EncounterSlot(p, surf[4 * i + 2], surf[4 * i + 3], percentSurf[i]);
                }

            }

            // Surf in rippling water
            if (surfrippling[2] != 0)
            {
                SurfRippling = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(surfrippling, 4 * i) & 0x7FF);
                    formid = (byte)(surfrippling[4 * i + 1] >> 3);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() >= formid)
                            if (p.Forms[formid - 1] != null)
                                p = p.Forms[formid - 1];
                    SurfRippling[i] = new EncounterSlot(p, surfrippling[4 * i + 2], surfrippling[4 * i + 3], percentSurf[i]);
                }

            }

            // Fish
            if (surfrippling[2] != 0)
            {
                SuperRodSlots = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(fish, 4 * i) & 0x7FF);
                    formid = (byte)(fish[4 * i + 1] >> 3);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() >= formid)
                            if (p.Forms[formid - 1] != null)
                                p = p.Forms[formid - 1];
                    SuperRodSlots[i] = new EncounterSlot(p, fish[4 * i + 2], fish[4 * i + 3], percentSuperRod[i]);
                }

            }

            // Fish in rippling water
            if (fishrippling[2] != 0)
            {
                FishRippling = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(fishrippling, 4 * i) & 0x7FF);
                    formid = (byte)(fishrippling[4 * i + 1] >> 3);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() >= formid)
                            if (p.Forms[formid - 1] != null)
                                p = p.Forms[formid - 1];
                    FishRippling[i] = new EncounterSlot(p, fishrippling[4 * i + 2], fishrippling[4 * i + 3], percentSuperRod[i]);
                }

            }


        }

        // 

        internal EncounterSlot[] getSlots(EncounterType type)
        {
            EncounterSlot[] selected = null, returnSlots = null;
            switch(type)
            {
                case EncounterType.Walking:
                    selected = WalkSlots;
                    returnSlots = new EncounterSlot[12];
                    break;
                case EncounterType.DarkGrass:
                    selected = DarkGrassSlots;
                    returnSlots = new EncounterSlot[12];
                    break;
                case EncounterType.ShakingGrass:
                    selected = ShakingSpots;
                    returnSlots = new EncounterSlot[12];
                    break;
                case EncounterType.Surf:
                    selected = SurfSlots;
                    returnSlots = new EncounterSlot[5];
                    break;
                case EncounterType.RipplingSurf:
                    selected = SurfRippling;
                    returnSlots = new EncounterSlot[5];
                    break;
                case EncounterType.SuperRod:
                    selected = SuperRodSlots;
                    returnSlots = new EncounterSlot[5];
                    break;
                case EncounterType.RipplingFish:
                    selected = FishRippling;
                    returnSlots = new EncounterSlot[5];
                    break;
                default: break; 

            }

            for(int i = 0; i < selected.Length; i++)
            {
                returnSlots[i] = new EncounterSlot(selected[i]);
            }


            return returnSlots;
        }

        internal static decimal[] getPercentages(EncounterType type, int powerLevel)
        {
            switch (type)
            {
                case EncounterType.Walking:
                case EncounterType.DarkGrass:
                case EncounterType.ShakingGrass:
                    switch (powerLevel)
                    {
                        case 1: return percentGrassLv1;
                        case 2: return percentGrassLv2;
                        case 3: return percentGrassLv3;
                        default: return percentGrass;
                    }
                case EncounterType.Surf:
                case EncounterType.RipplingSurf:
                    switch (powerLevel)
                    {
                        case 1: return percentSurfLv1;
                        case 2: return percentSurfLv2;
                        case 3: return percentSurfLv3;
                        default: return percentSurf;
                    }

                case EncounterType.SuperRod:
                case EncounterType.RipplingFish:
                    switch (powerLevel)
                    {
                        case 1: return percentSuperRodLv1;
                        case 2: return percentSuperRodLv2;
                        case 3: return percentSuperRodLv3;
                        default: return percentSuperRod;
                    }

                default: return null;
            }
        }

        internal bool isExistingEncounterType(EncounterType type)
        {
            switch (type)
            {
                case EncounterType.Walking: return !(WalkSlots == null);
                case EncounterType.DarkGrass: return !(DarkGrassSlots == null);
                case EncounterType.ShakingGrass: return !(ShakingSpots == null);
                case EncounterType.Surf: return !(SurfSlots == null);
                case EncounterType.RipplingSurf: return !(SurfRippling == null);
                case EncounterType.SuperRod: return !(SuperRodSlots == null);
                case EncounterType.RipplingFish: return !(FishRippling == null);
                default: return false;
            }
        }
    }
}
