using System;
using System.Linq;

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

            // Regular grass / cave
            if (grass[2] != 0)
                createEncounterSlotArray(ref WalkSlots, grass, percentGrass);

            // Dark grass (where 'double encounters' can occur)
            if (darkgrass[2] != 0)
                createEncounterSlotArray(ref DarkGrassSlots, darkgrass, percentGrass);

            // Shaking spots
            if (shaking[2] != 0)
                createEncounterSlotArray(ref ShakingSpots, shaking, percentGrass);

            // Surf
            if (surf[2] != 0)
                createEncounterSlotArray(ref SurfSlots, surf, percentSurf);

            // Surf in rippling water
            if (surfrippling[2] != 0)
                createEncounterSlotArray(ref SurfRippling, surfrippling, percentSurf);

            // Fish
            if (surfrippling[2] != 0)
                createEncounterSlotArray(ref SuperRodSlots, fish, percentSuperRod);

            // Fish in rippling water
            if (fishrippling[2] != 0)
                createEncounterSlotArray(ref FishRippling, fishrippling, percentSuperRod);

        }
        

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
                default: return null; 

            }
            

            for (int i = 0; i < selected.Length; i++)
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
                formid = (byte)(data[4 * i + 1] >> 3);
                if (version == Version.Black || version == Version.White) p = PokemonTables.pokemonBWTable[species];
                else p = PokemonTables.pokemonB2W2Table[species];
                if (formid > 0)
                    if (p.FormCount() >= formid)
                        if (p.Forms[formid - 1] != null)
                            p = p.Forms[formid - 1];
                slotArray[i] = new EncounterSlot(p, data[4 * i + 2], data[4 * i + 3], percentArray[i]);
            }
        }
    }
}
