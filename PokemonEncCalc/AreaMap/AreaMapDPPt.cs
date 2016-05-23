using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    class AreaMapDPPt : AreaMap
    {
        EncounterSlot[] OldRodSlots;
        EncounterSlot[] GoodRodSlots;
        EncounterSlot[] Day;
        EncounterSlot[] Night;
        EncounterSlot[] Swarm;
        EncounterSlot[] PokeRadar;

        // GBA-slot specific EncounterSlot
        EncounterSlot[] Ruby;
        EncounterSlot[] Sapphire;
        EncounterSlot[] Emerald;
        EncounterSlot[] FireRed;
        EncounterSlot[] LeafGreen;


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
                Pokemon p;
                short species;
                byte formid;

                // Regular slots
                WalkSlots = new EncounterSlot[12];
                for (int i = 0; i < 12; i++)
                {
                    species = (short)(BitConverter.ToInt16(grass, 8 * i + 4) & 0x3FF);
                    formid = (byte)(grass[8 * i + 5] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if(p.Forms[formid] != null)
                                p = p.Forms[formid];
                    WalkSlots[i] = new EncounterSlot(p, grass[8 * i + 1], grass[8 * i], percentGrass[i]);
                }

                // swarm (Replaces slots 0 and 1 when active)
                Swarm = new EncounterSlot[2];
                for(int s = 0; s < 2; s++) {
                    species = (short)(BitConverter.ToInt16(swarm, 4*s) & 0x3FF);
                    formid = (byte)(swarm[4*s+1] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    Swarm[s] = new EncounterSlot(p, grass[1+8*s], grass[8*s], percentGrass[s]);         
                }

                // Day (Replaces slots 2 and 3, regular ones are for morning)
                Day = new EncounterSlot[2];
                for (int s = 0; s < 2; s++)
                {
                    species = (short)(BitConverter.ToInt16(day, 4 * s) & 0x3FF);
                    formid = (byte)(day[4 * s+1] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    Day[s] = new EncounterSlot(p, grass[17 + 8 * s], grass[16 + 8 * s], percentGrass[s+2]);
                }
                // Night (Replaces slots 2 and 3, regular ones are for morning)
                Night = new EncounterSlot[2];
                for (int s = 0; s < 2; s++)
                {
                    species = (short)(BitConverter.ToInt16(night, 4 * s) & 0x3FF);
                    formid = (byte)(night[4 * s+1] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    Night[s] = new EncounterSlot(p, grass[17 + 8 * s], grass[16 + 8 * s], percentGrass[s + 2]);
                }

                // PokéRadar (Replaces slots 4, 5, 10 and 11 when active)
                PokeRadar = new EncounterSlot[4];
                int[] r = new[] { 4, 5, 10, 11 };  // slots affected
                for (int s = 0; s < 4; s++)
                {
                    species = (short)(BitConverter.ToInt16(pokeRadar, 4 * s) & 0x3FF);
                    formid = (byte)(pokeRadar[4 * s+1] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    PokeRadar[s] = new EncounterSlot(p, grass[1 + 8 * r[s]], grass[8 * r[s]], percentGrass[r[s]]);
                }
                // Ruby (Replaces slots 8 and 9 when active)
                Ruby = new EncounterSlot[2];
                r = new[] {8,9 };  // slots affected
                for (int s = 0; s < 2; s++)
                {
                    species = (short)(BitConverter.ToInt16(ruby, 4 * s) & 0x3FF);
                    formid = (byte)(ruby[4 * s+1] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    Ruby[s] = new EncounterSlot(p, grass[1 + 8 * r[s]], grass[8 * r[s]], percentGrass[r[s]]);
                }
                // Sapphire (Replaces slots 8 and 9 when active)
                Sapphire = new EncounterSlot[2];
                for (int s = 0; s < 2; s++)
                {
                    species = (short)(BitConverter.ToInt16(sapphire, 4 * s) & 0x3FF);
                    formid = (byte)(sapphire[4 * s+1] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    Sapphire[s] = new EncounterSlot(p, grass[1 + 8 * r[s]], grass[8 * r[s]], percentGrass[r[s]]);
                }
                // Emerald (Replaces slots 8 and 9 when active)
                Emerald = new EncounterSlot[2];
                for (int s = 0; s < 2; s++)
                {
                    species = (short)(BitConverter.ToInt16(emerald, 4 * s) & 0x3FF);
                    formid = (byte)(emerald[4 * s+1] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    Emerald[s] = new EncounterSlot(p, grass[1 + 8 * r[s]], grass[8 * r[s]], percentGrass[r[s]]);
                }
                // FireRed (Replaces slots 8 and 9 when active)
                FireRed = new EncounterSlot[2];
                for (int s = 0; s < 2; s++)
                {
                    species = (short)(BitConverter.ToInt16(firered, 4 * s) & 0x3FF);
                    formid = (byte)(firered[4 * s+1] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    FireRed[s] = new EncounterSlot(p, grass[1 + 8 * r[s]], grass[8 * r[s]], percentGrass[r[s]]);
                }
                // LeafGreen (Replaces slots 8 and 9 when active)
                LeafGreen = new EncounterSlot[2];
                for (int s = 0; s < 2; s++)
                {
                    species = (short)(BitConverter.ToInt16(leafgreen, 4 * s) & 0x3FF);
                    formid = (byte)(leafgreen[4 * s+1] >> 2);
                    p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    LeafGreen[s] = new EncounterSlot(p, grass[1 + 8 * r[s]], grass[8 * r[s]], percentGrass[r[s]]);
                }
            }

            if (surf[0] != 0)
            {
                SurfSlots = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    short species = (short)(BitConverter.ToInt16(surf, 8 * i + 4) & 0x3FF);
                    byte formid = (byte)(surf[8 * i + 5] >> 2);
                    Pokemon p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    SurfSlots[i] = new EncounterSlot(p, surf[8 * i + 1], surf[8 * i], percentSurf[i]);
                }
            }

            if (oldRod[0] != 0)
            {
                OldRodSlots = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    short species = (short)(BitConverter.ToInt16(oldRod, 8 * i + 4) & 0x3FF);
                    byte formid = (byte)(oldRod[8 * i + 5] >> 2);
                    Pokemon p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    OldRodSlots[i] = new EncounterSlot(p, oldRod[8 * i + 1], oldRod[8 * i], percentOldRod[i]);
                }
            }

            if (goodRod[0] != 0)
            {
                GoodRodSlots = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    short species = (short)(BitConverter.ToInt16(goodRod, 8 * i + 4) & 0x3FF);
                    byte formid = (byte)(goodRod[8 * i + 5] >> 2);
                    Pokemon p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    GoodRodSlots[i] = new EncounterSlot(p, goodRod[8 * i + 1], goodRod[8 * i], percentGoodRod[i]);
                }
            }

            if (superRod[0] != 0)
            {
                SuperRodSlots = new EncounterSlot[5];
                for (int i = 0; i < 5; i++)
                {
                    short species = (short)(BitConverter.ToInt16(superRod, 8 * i + 4) & 0x3FF);
                    byte formid = (byte)(superRod[8 * i + 5] >> 2);
                    Pokemon p = Utils.PokemonList[species - 1];
                    if (formid > 0)
                        if (p.FormCount() > formid)
                            if (p.Forms[formid] != null)
                                p = p.Forms[formid];
                    SuperRodSlots[i] = new EncounterSlot(p, superRod[8 * i + 1], superRod[8 * i], percentSuperRod[i]);
                }
            }


        }

        internal bool isThereSwarm()
        {
            return (!(WalkSlots[0].Species == Swarm[0].Species && WalkSlots[1].Species == Swarm[1].Species));
        }

        internal bool isTherePokeRadar()
        {
            return (!(WalkSlots[4].Species == PokeRadar[0].Species && WalkSlots[5].Species == PokeRadar[1].Species
                   && WalkSlots[10].Species == PokeRadar[2].Species && WalkSlots[11].Species == PokeRadar[3].Species));
        }



    }
}
