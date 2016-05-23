using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            byte[] grassLevel = new byte[12];
            byte[] grass = new byte[24];
            byte[] swarmgrass = new byte[2];
            byte[] day = new byte[24];
            byte[] night = new byte[24];
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
            grassLevel = data.Skip(8).Take(12).ToArray();
            grass = data.Skip(20).Take(24).ToArray();
            day = data.Skip(44).Take(24).ToArray();
            night = data.Skip(68).Take(24).ToArray();
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

            Pokemon p;
            short species;
            byte formid;

            if (grass[0] != 0)
            {

                // Regular slots (morning)
                WalkSlots = new EncounterSlot[12];
                for (int i = 0; i < 12; i++)
                {
                    species = (short)(BitConverter.ToInt16(grass, 2 * i) & 0x3FF);
                    formid = (byte)(grass[2 * i + 1] << 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    WalkSlots[i] = new EncounterSlot(p, grassLevel[i], grassLevel[i], percentGrass[i]);
                }
                // Regular slots (day)
                DaySlots = new EncounterSlot[12];
                for (int i = 0; i < 12; i++)
                {
                    species = (short)(BitConverter.ToInt16(day, 2 * i) & 0x3FF);
                    formid = (byte)(day[2 * i + 1] << 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    DaySlots[i] = new EncounterSlot(p, grassLevel[i], grassLevel[i], percentGrass[i]);
                }
                // Regular slots (night)
                NightSlots = new EncounterSlot[12];
                for (int i = 0; i < 12; i++)
                {
                    species = (short)(BitConverter.ToInt16(night, 2 * i) & 0x3FF);
                    formid = (byte)(night[2 * i + 1] << 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    NightSlots[i] = new EncounterSlot(p, grassLevel[i], grassLevel[i], percentGrass[i]);
                }


                // swarm (Replaces slots 0 and 1 when active)
                SwarmWalkSlots = new EncounterSlot[2];
            
                species = (short)(BitConverter.ToInt16(swarmgrass, 0) & 0x3FF);
                formid = (byte)(swarmgrass[1] << 2);
                p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                SwarmWalkSlots[0] = new EncounterSlot(p, grassLevel[0], grassLevel[0], percentGrass[0]);
                SwarmWalkSlots[1] = new EncounterSlot(p, grassLevel[0], grassLevel[0], percentGrass[0]);

                // hoenn radio (Replaces slots 2, 3, 4 and 5 when active)
                HoennRadioSlots = new EncounterSlot[4];

                for (int i = 0; i < 2; i++)
                {
                    species = (short)(BitConverter.ToInt16(hoenn, 2 * i) & 0x3FF);
                    formid = (byte)(hoenn[2 * i + 1] << 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    HoennRadioSlots[i] = new EncounterSlot(p, grassLevel[2 * i + 2], grassLevel[2 * i + 2], percentGrass[2 * i + 2]);
                    HoennRadioSlots[i+2] = new EncounterSlot(p, grassLevel[2 * i + 3], grassLevel[2 * i + 3], percentGrass[2 * i + 3]);
                }

                // sinnoh radio (Replaces slots 2, 3, 4 and 5 when active)
                SinnohRadioSlots = new EncounterSlot[4];

                for (int i = 0; i < 2; i++)
                {
                    species = (short)(BitConverter.ToInt16(sinnoh, 2 * i) & 0x3FF);
                    formid = (byte)(sinnoh[2 * i + 1] << 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    SinnohRadioSlots[i] = new EncounterSlot(p, grassLevel[2 * i + 2], grassLevel[2 * i + 2], percentGrass[2 * i + 2]);
                    SinnohRadioSlots[i+2] = new EncounterSlot(p, grassLevel[2 * i + 3], grassLevel[2 * i + 3], percentGrass[2 * i + 3]);
                }
            }

            if (surf[0] != 0)
            {
                
                // Regular surf slots
                SurfSlots = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(surf, 4 * i + 2) & 0x3FF);
                    formid = (byte)(surf[4 * i + 3] << 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    SurfSlots[i] = new EncounterSlot(p, surf[4 * i], surf[4 * i + 1], percentSurf[i]);
                }

                // Surf swarm (Replaces slot 0 when active)
                SwarmSurfSlots = new EncounterSlot[1];
                species = (short)(BitConverter.ToInt16(swarmsurf, 0) & 0x3FF);
                formid = (byte)(swarmsurf[1] << 2);
                p = Utils.PokemonList[species - 1];
                if (formid > 0)
                    if (p.FormCount() > formid)
                        if (p.Forms[formid] != null)
                            p = p.Forms[formid];
                SwarmSurfSlots[0] = new EncounterSlot(p, surf[0], surf[1], percentSurf[0]);
            }

            if(rocksmash[0] != 0)
            {
                // Rock Smash slots
                RockSmashSlots = new EncounterSlot[2];
                for (int i = 0; i < 2; i++)
                {
                    species = (short)(BitConverter.ToInt16(rocksmash, 4 * i + 2) & 0x3FF);
                    formid = (byte)(rocksmash[4 * i + 3] << 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    RockSmashSlots[i] = new EncounterSlot(p, rocksmash[4 * i], rocksmash[4 * i + 1], percentRockSmash[i]);
                }
            }

            if (oldRod[0] != 0)
            {
                // Old Rod
                OldRodSlots = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(oldRod, 4 * i + 2) & 0x3FF);
                    formid = (byte)(oldRod[4 * i + 3] << 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    OldRodSlots[i] = new EncounterSlot(p, oldRod[4 * i], oldRod[4 * i + 1], percentOldRod[i]);
                }

                // Good Rod
                GoodRodSlots = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(goodRod, 4 * i + 2) & 0x3FF);
                    formid = (byte)(goodRod[4 * i + 3] << 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    GoodRodSlots[i] = new EncounterSlot(p, goodRod[4 * i], goodRod[4 * i + 1], percentGoodRod[i]);
                }

                // Super Rod
                SuperRodSlots = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    species = (short)(BitConverter.ToInt16(superRod, 4 * i + 2) & 0x3FF);
                    formid = (byte)(superRod[4 * i + 3] << 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    SuperRodSlots[i] = new EncounterSlot(p, superRod[4 * i], superRod[4 * i + 1], percentSuperRod[i]);
                }

                // Fish during night
                NightGoodRodSlots = new EncounterSlot[1];
                NightSuperRodSlots = new EncounterSlot[1];
                species = (short)(BitConverter.ToInt16(nightfish, 0) & 0x3FF);
                formid = (byte)(nightfish[1] << 2);
                p = Utils.PokemonList[species - 1];
                if (formid > 0)
                    if (p.FormCount() > formid)
                        if (p.Forms[formid] != null)
                            p = p.Forms[formid];
                NightGoodRodSlots[0] = new EncounterSlot(p, goodRod[12], goodRod[13], percentGoodRod[3]);
                NightSuperRodSlots[0] = new EncounterSlot(p, superRod[4], superRod[5], percentSuperRod[1]);

                // Fish during swarm
                SwarmOldRodSlots = new EncounterSlot[1];
                SwarmGoodRodSlots = new EncounterSlot[3];
                SwarmSuperRodSlots = new EncounterSlot[5];

                species = (short)(BitConverter.ToInt16(nightfish, 0) & 0x3FF);
                formid = (byte)(nightfish[1] << 2);
                p = Utils.PokemonList[species - 1];
                if (formid > 0)
                    if (p.FormCount() > formid)
                        if (p.Forms[formid] != null)
                            p = p.Forms[formid];

                // Olf Rod
                SwarmOldRodSlots[0] = new EncounterSlot(p, oldRod[8], oldRod[9], percentOldRod[2]);

                for (int i = 0, j = 0; i < 5; i++)
                {
                    // Good Rod
                    if(new[] { 0, 2, 3}.Contains(i))
                    {
                        SwarmGoodRodSlots[j] = new EncounterSlot(p, goodRod[4 * i], oldRod[4 * i + 1], percentOldRod[i]);
                        j++; 
                    }

                    // Super Rod
                    SwarmSuperRodSlots[i] = new EncounterSlot(p, superRod[4 * i], superRod[4 * i + 1], percentSuperRod[i]);

                }
            }


        }


    }
}
