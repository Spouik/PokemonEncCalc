using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEncCalc
{
    internal static class PokemonTables
    {

        internal static readonly short[] FormTable =  { 201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,
                                                        201,201,201,201,201,201,201,201,201,201, // Gen 2

                                                        351,351,351,386,386,386, //Gen 3

                                                        412,412,413,413,421,422,423,479,479,479,479,479,487,492, //Gen 4

                                                        550,555,585,585,585,586,586,586,648,646,646,647,641,642,645, //Gen 5

                                                        94,678,676,676,676,676,676,676,676,676,676,282,181,3,6,6,150,150,257,308,229,
                                                        306,354,248,212,127,242,448,460,681,9,115,130,359,65,214,303,310,445,381,380,
                                                        710,710,710,711,711,711,669,669,669,669,670,670,670,670,670,671,671,671,671, // XY

                                                        260,254,302,334,475,531,319,80,208,18,362,719,376,382,383,384,25,25,25,25,25,25,
                                                        720,323,428,373,15 // ORAS
                                                    };

        internal static readonly short[] FormTableSuMo = {
            386, 386, 386, 413, 413, 492, 487, 479, 479, 479, 479, 479, 351, 351, 351, 421, 422, 423, 550, 555, 648, 646, 646, 647, 641, 642, 645,
            94, // M-Gengar
            678, 676, 676, 676, 676, 676, 676, 676, 676, 676,
            282, 181, 3, 6, 6, 150, 150, 257, 308, 229, 306, 354, 248, 212, 127, 142, 448, 460, // XY Megas
            681, // Aegislash
            9, 115, 130, 359, 65, 214, 303, 310, 445, 380, 381, // Other XY Megas
            710, 710, 710, 711, 711, 711, 670, 670, 670, 670, 670, // Pumpkaboo/Gourgeist/Floette
            260, 254, 302, 334, 475, 531, 319, 80, 208, 18, 362, 719, 376, 382, 383, 384, 720, 323, 428, 373, 15, // ORAS Megas / Primals / Hoopa

            // Sun/Moon new forms
            746, 741, 741, 741, 745,
            19, 20, 20, 26, 27, 28, 37, 38, 52, 53, 74, 75, 76, 88, 89, 103, 105, // Alolan formes
            658, 658, 718, 718, 718, 718, // Ash-Greninja / Zygarde
            774, 774, 774, 774, 774, 774, 774, 774, 774, 774, 774, 774, 774, // Minior
            50, 51, // Alolan Diglett/Dugtrio
            778, 778, 778, // Mimikyu
            801, // Magearna
            25, 25, 25, 25, 25, 25, // Cap Pikachu
            735, 738, 754, 758, 784 // Totem Pokémon
        };
        internal static readonly short[] FormTableUSUM = {
            386, 386, 386, 413, 413, 492, 487, 479, 479, 479, 479, 479, 351, 351, 351, 421, 422, 423, 550, 555, 648, 646, 646, 647, 641, 642, 645,
            94, // M-Gengar
            678, 676, 676, 676, 676, 676, 676, 676, 676, 676,
            282, 181, 3, 6, 6, 150, 150, 257, 308, 229, 306, 354, 248, 212, 127, 142, 448, 460, // XY Megas
            681, // Aegislash
            9, 115, 130, 359, 65, 214, 303, 310, 445, 380, 381, // Other XY Megas
            710, 710, 710, 711, 711, 711, 670, 670, 670, 670, 670, // Pumpkaboo/Gourgeist/Floette
            260, 254, 302, 334, 475, 531, 319, 80, 208, 18, 362, 719, 376, 382, 383, 384, 720, 323, 428, 373, 15, // ORAS Megas / Primals / Hoopa

            // Sun/Moon new forms
            746, 741, 741, 741, 745, 745, // 2nd 745 is Dusk Lyanroc
            19, 20, 20, 26, 27, 28, 37, 38, 52, 53, 74, 75, 76, 88, 89, 103, 105, 105, // Alolan formes + Totem Alolan Marowak
            658, 658, 718, 718, 718, 718, // Ash-Greninja / Zygarde
            774, 774, 774, 774, 774, 774, 774, 774, 774, 774, 774, 774, 774, // Minior
            50, 51, // Alolan Diglett/Dugtrio
            778, 778, 778, // Mimikyu
            801, // Magearna
            25, 25, 25, 25, 25, 25, 25, //  Cap Pikachu
            735, 738, 754, 758, 784, // Totem Pokémon
            800, 800, 800, //Necrozma
            752, 777, 743, //USUM Totem
            744 // Special Rockruff (evolves into Dusk Lycanroc)
        };

        // Pokémon forms with no data, but have appearance change
        // These will take the default form data
        internal static readonly short[] OtherFormTableSuMo =
        {
            201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201, 201,
            412, 412, 585, 585, 585, 586, 586, 586, 669, 669, 669, 669, 671, 671, 671, 671
        };


        internal static PokemonGS[] pokemonGSTable { get; set; }
        internal static PokemonCrystal[] pokemonCrystalTable { get; set; }
        internal static PokemonRS[] pokemonRSTable { get; set; }
        internal static PokemonEmerald[] pokemonEmeraldTable { get; set; }
        internal static PokemonFRLG[] pokemonFRLGTable { get; set; }
        internal static PokemonDP[] pokemonDPTable { get; set; }
        internal static PokemonPt[] pokemonPtTable { get; set; }
        internal static PokemonHGSS[] pokemonHGSSTable { get; set; }
        internal static PokemonBW[] pokemonBWTable { get; set; }
        internal static PokemonB2W2[] pokemonB2W2Table { get; set; }
        internal static PokemonXY[] pokemonXYTable { get; set; }
        internal static PokemonORAS[] pokemonORASTable { get; set; }
        internal static PokemonSuMo[] pokemonSuMoTable { get; set; }
        internal static PokemonUSUM[] pokemonUSUMTable { get; set; }

        internal static void PopulatePokemonTables()
        {
            // Instanciate tables
            pokemonGSTable = new PokemonGS[PokemonGS.RELEASED_POKEMON + 1];
            pokemonCrystalTable = new PokemonCrystal[PokemonCrystal.RELEASED_POKEMON + 1];
            pokemonRSTable = new PokemonRS[PokemonRS.RELEASED_POKEMON + 1];
            pokemonEmeraldTable = new PokemonEmerald[PokemonEmerald.RELEASED_POKEMON + 1];
            pokemonFRLGTable = new PokemonFRLG[PokemonFRLG.RELEASED_POKEMON + 1];
            pokemonDPTable = new PokemonDP[PokemonDP.RELEASED_POKEMON + 1];
            pokemonPtTable = new PokemonPt[PokemonPt.RELEASED_POKEMON + 1];
            pokemonHGSSTable = new PokemonHGSS[PokemonHGSS.RELEASED_POKEMON + 1];
            pokemonBWTable = new PokemonBW[PokemonBW.RELEASED_POKEMON + 1];
            pokemonB2W2Table = new PokemonB2W2[PokemonB2W2.RELEASED_POKEMON + 1];
            pokemonXYTable = new PokemonXY[PokemonXY.RELEASED_POKEMON + 1];
            pokemonORASTable = new PokemonORAS[PokemonORAS.RELEASED_POKEMON + 1];
            pokemonSuMoTable = new PokemonSuMo[PokemonSuMo.RELEASED_POKEMON + 1];
            pokemonUSUMTable = new PokemonUSUM[PokemonUSUM.RELEASED_POKEMON + 1];

            // Populate tables 
            // Gen 2-3 tables now use gen 4 Pokémon data. 
            // TODO: Change data to match gen when they'll be added.           
            for (short i = 0; i <= PokemonGS.RELEASED_POKEMON; i++)
            {
                pokemonGSTable[i] = new PokemonGS(i, Properties.Resources.PokemonInfo4.Skip(PokemonGS.SIZE * i).Take(PokemonGS.SIZE).ToArray());
                setNames(pokemonGSTable[i]);
            }

            for (short i = 0; i <= PokemonCrystal.RELEASED_POKEMON; i++)
            {
                pokemonCrystalTable[i] = new PokemonCrystal(i, Properties.Resources.PokemonInfo4.Skip(PokemonCrystal.SIZE * i).Take(PokemonCrystal.SIZE).ToArray());
                setNames(pokemonCrystalTable[i]);
            }
            
            for (short i = 0; i <= PokemonRS.RELEASED_POKEMON; i++)
            {
                pokemonRSTable[i] = new PokemonRS(i, Properties.Resources.PokemonInfo4.Skip(PokemonRS.SIZE * i).Take(PokemonRS.SIZE).ToArray());
                setNames(pokemonRSTable[i]);
            }

            for (short i = 0; i <= PokemonEmerald.RELEASED_POKEMON; i++)
            {
                pokemonEmeraldTable[i] = new PokemonEmerald(i, Properties.Resources.PokemonInfo4.Skip(PokemonEmerald.SIZE * i).Take(PokemonEmerald.SIZE).ToArray());
                setNames(pokemonEmeraldTable[i]);
            }

            for (short i = 0; i <= PokemonFRLG.RELEASED_POKEMON; i++)
            {
                pokemonFRLGTable[i] = new PokemonFRLG(i, Properties.Resources.PokemonInfo4.Skip(PokemonFRLG.SIZE * i).Take(PokemonFRLG.SIZE).ToArray());
                setNames(pokemonFRLGTable[i]);
            }

            for (short i = 0; i <= PokemonDP.RELEASED_POKEMON; i++)
            { 
                pokemonDPTable[i] = new PokemonDP(i, Properties.Resources.PokemonInfo4.Skip(PokemonDP.SIZE * i).Take(PokemonDP.SIZE).ToArray());
                setNames(pokemonDPTable[i]);
            }

            for (short i = 0; i <= PokemonPt.RELEASED_POKEMON; i++)
            { 
                pokemonPtTable[i] = new PokemonPt(i, Properties.Resources.PokemonInfo4.Skip(PokemonPt.SIZE * i).Take(PokemonPt.SIZE).ToArray());
                setNames(pokemonPtTable[i]);
            }

            for (short i = 0; i <= PokemonHGSS.RELEASED_POKEMON; i++)
            {
                pokemonHGSSTable[i] = new PokemonHGSS(i, Properties.Resources.PokemonInfo4.Skip(PokemonHGSS.SIZE * i).Take(PokemonHGSS.SIZE).ToArray());
                setNames(pokemonHGSSTable[i]);
            }

            for (short i = 0; i <= PokemonBW.RELEASED_POKEMON; i++)
            {
                pokemonBWTable[i] = new PokemonBW(i, Properties.Resources.PokemonInfo5.Skip(PokemonBW.SIZE * i).Take(PokemonBW.SIZE).ToArray());
                setNames(pokemonBWTable[i]);
            }

            for (short i = 0; i <= PokemonB2W2.RELEASED_POKEMON; i++)
            {
                pokemonB2W2Table[i] = new PokemonB2W2(i, Properties.Resources.PokemonInfo5.Skip(PokemonB2W2.SIZE * i).Take(PokemonB2W2.SIZE).ToArray());
                setNames(pokemonB2W2Table[i]);
            }

            for (short i = 0; i <= PokemonXY.RELEASED_POKEMON; i++)
            {
                pokemonXYTable[i] = new PokemonXY(i, Properties.Resources.PokemonInfo6.Skip(PokemonXY.SIZE * i).Take(PokemonXY.SIZE).ToArray());
                if (new[] { 382, 383 }.Contains(i)) pokemonXYTable[i].CatchRate = 5;
                if (i == 384) pokemonXYTable[i].CatchRate = 3;
                setNames(pokemonXYTable[i]);
            }

            for (short i = 0; i <= PokemonORAS.RELEASED_POKEMON; i++)
            {
                pokemonORASTable[i] = new PokemonORAS(i, Properties.Resources.PokemonInfo6.Skip(PokemonORAS.SIZE * i).Take(PokemonORAS.SIZE).ToArray());
                setNames(pokemonORASTable[i]);
            }

            for (short i = 0; i <= PokemonSuMo.RELEASED_POKEMON; i++)
            {
                pokemonSuMoTable[i] = new PokemonSuMo(i, Properties.Resources.PokemonInfo7.Skip(PokemonSuMo.SIZE * i).Take(PokemonSuMo.SIZE).ToArray());
                setNames(pokemonSuMoTable[i]);
            }

            for (short i = 0; i <= PokemonUSUM.RELEASED_POKEMON; i++)
            {
                pokemonUSUMTable[i] = new PokemonUSUM(i, Properties.Resources.PokemonInfoU.Skip(PokemonUSUM.SIZE * i).Take(PokemonUSUM.SIZE).ToArray());
                setNames(pokemonUSUMTable[i]);
            }

            // Add forms
            // Gen 2-3 tables now use gen 4 Pokémon data. 
            // TODO: Change data to match gen when they'll be added.           
            for (short f = 0; f < PokemonGS.RELEASED_FORMS; f++)
            {
                PokemonGS p = new PokemonGS(FormTable[f], Properties.Resources.PokemonInfo4.Skip(PokemonGS.SIZE * (PokemonGS.RELEASED_POKEMON + f + 1))
                    .Take(PokemonGS.SIZE).ToArray());
                pokemonGSTable[FormTable[f]].addForm(p);

                p.Form = (byte)pokemonGSTable[FormTable[f]].Forms.Count;
                setFormNames(p, (short)(PokemonORAS.RELEASED_POKEMON + f));
            }

            for (short f = 0; f < PokemonCrystal.RELEASED_FORMS; f++)
            {
                PokemonCrystal p = new PokemonCrystal(FormTable[f], Properties.Resources.PokemonInfo4.Skip(PokemonCrystal.SIZE * (PokemonCrystal.RELEASED_POKEMON + f + 1))
                    .Take(PokemonCrystal.SIZE).ToArray());
                pokemonCrystalTable[FormTable[f]].addForm(p);

                p.Form = (byte)pokemonCrystalTable[FormTable[f]].Forms.Count;
                setFormNames(p, (short)(PokemonORAS.RELEASED_POKEMON + f));
            }

            for (short f = 0; f < PokemonRS.RELEASED_FORMS; f++)
            {
                PokemonRS p = new PokemonRS(FormTable[f], Properties.Resources.PokemonInfo4.Skip(PokemonRS.SIZE * (PokemonRS.RELEASED_POKEMON + f + 1))
                    .Take(PokemonRS.SIZE).ToArray());
                pokemonRSTable[FormTable[f]].addForm(p);

                p.Form = (byte)pokemonRSTable[FormTable[f]].Forms.Count;
                setFormNames(p, (short)(PokemonORAS.RELEASED_POKEMON + f));
            }

            for (short f = 0; f < PokemonEmerald.RELEASED_FORMS; f++)
            {
                PokemonEmerald p = new PokemonEmerald(FormTable[f], Properties.Resources.PokemonInfo4.Skip(PokemonEmerald.SIZE * (PokemonEmerald.RELEASED_POKEMON + f + 1))
                    .Take(PokemonEmerald.SIZE).ToArray());
                pokemonEmeraldTable[FormTable[f]].addForm(p);

                p.Form = (byte)pokemonEmeraldTable[FormTable[f]].Forms.Count;
                setFormNames(p, (short)(PokemonORAS.RELEASED_POKEMON + f));
            }

            for (short f = 0; f < PokemonFRLG.RELEASED_FORMS; f++)
            {
                PokemonFRLG p = new PokemonFRLG(FormTable[f], Properties.Resources.PokemonInfo4.Skip(PokemonFRLG.SIZE * (PokemonFRLG.RELEASED_POKEMON + f + 1))
                    .Take(PokemonFRLG.SIZE).ToArray());
                pokemonFRLGTable[FormTable[f]].addForm(p);

                p.Form = (byte)pokemonFRLGTable[FormTable[f]].Forms.Count;
                setFormNames(p, (short)(PokemonORAS.RELEASED_POKEMON + f));
            }

            for (short f = 0; f < PokemonDP.RELEASED_FORMS; f++)
            {
                PokemonDP p = new PokemonDP(FormTable[f], Properties.Resources.PokemonInfo4.Skip(PokemonDP.SIZE * (PokemonDP.RELEASED_POKEMON + f + 1))
                    .Take(PokemonDP.SIZE).ToArray());
                pokemonDPTable[FormTable[f]].addForm(p);

                p.Form = (byte)pokemonDPTable[FormTable[f]].Forms.Count;
                setFormNames(p, (short)(PokemonORAS.RELEASED_POKEMON + f));
            }

            for (short f = 0; f < PokemonPt.RELEASED_FORMS; f++)
            {
                PokemonPt p = new PokemonPt(FormTable[f], Properties.Resources.PokemonInfo4.Skip(PokemonPt.SIZE * (PokemonPt.RELEASED_POKEMON + f + 1))
                    .Take(PokemonPt.SIZE).ToArray());
                pokemonPtTable[FormTable[f]].addForm(p);

                p.Form = (byte)pokemonPtTable[FormTable[f]].Forms.Count;
                setFormNames(p, (short)(PokemonORAS.RELEASED_POKEMON + f));
            }

            for (short f = 0; f < PokemonHGSS.RELEASED_FORMS; f++)
            {
                PokemonHGSS p = new PokemonHGSS(FormTable[f], Properties.Resources.PokemonInfo4.Skip(PokemonHGSS.SIZE * (PokemonHGSS.RELEASED_POKEMON + f + 1))
                    .Take(PokemonHGSS.SIZE).ToArray());
                pokemonHGSSTable[FormTable[f]].addForm(p);

                p.Form = (byte)pokemonHGSSTable[FormTable[f]].Forms.Count;
                setFormNames(p, (short)(PokemonORAS.RELEASED_POKEMON + f));
            }

            for (short f = 0; f < PokemonBW.RELEASED_FORMS; f++)
            {
                PokemonBW p = new PokemonBW(FormTable[f], Properties.Resources.PokemonInfo5.Skip(PokemonBW.SIZE * (PokemonBW.RELEASED_POKEMON + f + 1))
                    .Take(PokemonBW.SIZE).ToArray());
                pokemonBWTable[FormTable[f]].addForm(p);

                p.Form = (byte)pokemonBWTable[FormTable[f]].Forms.Count;
                setFormNames(p, (short)(PokemonORAS.RELEASED_POKEMON + f));
            }

            for (short f = 0; f < PokemonB2W2.RELEASED_FORMS; f++)
            {
                PokemonB2W2 p = new PokemonB2W2(FormTable[f], Properties.Resources.PokemonInfo5.Skip(PokemonB2W2.SIZE * (PokemonB2W2.RELEASED_POKEMON + f + 1))
                    .Take(PokemonB2W2.SIZE).ToArray());
                pokemonB2W2Table[FormTable[f]].addForm(p);

                p.Form = (byte)pokemonB2W2Table[FormTable[f]].Forms.Count;
                setFormNames(p, (short)(PokemonORAS.RELEASED_POKEMON + f));
            }

            for (short f = 0; f < PokemonXY.RELEASED_FORMS; f++)
            {
                PokemonXY p = new PokemonXY(FormTable[f], Properties.Resources.PokemonInfo6.Skip(PokemonXY.SIZE * (PokemonXY.RELEASED_POKEMON + f + 1))
                    .Take(PokemonXY.SIZE).ToArray());
                pokemonXYTable[FormTable[f]].addForm(p);

                p.Form = (byte)pokemonXYTable[FormTable[f]].Forms.Count;
                setFormNames(p, (short)(PokemonORAS.RELEASED_POKEMON + f));
            }

            for (short f = 0; f < PokemonORAS.RELEASED_FORMS; f++)
            {
                PokemonORAS p = new PokemonORAS(FormTable[f], Properties.Resources.PokemonInfo6.Skip(PokemonORAS.SIZE * (PokemonORAS.RELEASED_POKEMON + f + 1))
                    .Take(PokemonORAS.SIZE).ToArray());
                pokemonORASTable[FormTable[f]].addForm(p);

                p.Form = (byte)pokemonORASTable[FormTable[f]].Forms.Count;
                setFormNames(p, (short)(PokemonORAS.RELEASED_POKEMON + f));
            }

            for (short f = 0; f < PokemonSuMo.RELEASED_FORMS; f++)
            {
                PokemonSuMo p = new PokemonSuMo(FormTableSuMo[f], Properties.Resources.PokemonInfo7.Skip(PokemonSuMo.SIZE * (PokemonSuMo.RELEASED_POKEMON + f + 1))
                    .Take(PokemonSuMo.SIZE).ToArray());
                pokemonSuMoTable[FormTableSuMo[f]].addForm(p);

                p.Form = (byte)pokemonSuMoTable[FormTableSuMo[f]].Forms.Count;
                setFormNames(p, (short)(PokemonSuMo.RELEASED_POKEMON + f));
            }

            for (short f = 0; f < PokemonUSUM.RELEASED_FORMS; f++)
            {
                PokemonUSUM p = new PokemonUSUM(FormTableUSUM[f], Properties.Resources.PokemonInfoU.Skip(PokemonUSUM.SIZE * (PokemonUSUM.RELEASED_POKEMON + f + 1))
                    .Take(PokemonUSUM.SIZE).ToArray());
                pokemonUSUMTable[FormTableUSUM[f]].addForm(p);

                p.Form = (byte)pokemonUSUMTable[FormTableUSUM[f]].Forms.Count;
                setFormNames(p, (short)(PokemonUSUM.RELEASED_POKEMON + f));
            }

            // Other Sun/moon forms
            for (short f = 0; f < OtherFormTableSuMo.Length; f++)
            {
                PokemonSuMo p = new PokemonSuMo(OtherFormTableSuMo[f], Properties.Resources.PokemonInfo7.Skip(PokemonSuMo.SIZE * (OtherFormTableSuMo[f]))
                    .Take(PokemonSuMo.SIZE).ToArray());
                pokemonSuMoTable[OtherFormTableSuMo[f]].addForm(p);

                p.Form = (byte)pokemonSuMoTable[OtherFormTableSuMo[f]].Forms.Count;
                setFormNames(p, (short)(PokemonSuMo.RELEASED_POKEMON + PokemonSuMo.RELEASED_FORMS + f));

                // USUM
                PokemonUSUM u = new PokemonUSUM(OtherFormTableSuMo[f], Properties.Resources.PokemonInfoU.Skip(PokemonUSUM.SIZE * (OtherFormTableSuMo[f]))
    .Take(PokemonUSUM.SIZE).ToArray());
                pokemonUSUMTable[OtherFormTableSuMo[f]].addForm(u);

                u.Form = (byte)pokemonUSUMTable[OtherFormTableSuMo[f]].Forms.Count;
                setFormNames(u, (short)(PokemonUSUM.RELEASED_POKEMON + PokemonUSUM.RELEASED_FORMS + f));
            }

        }

        private static void setNames(Pokemon p)
        {
            if (p.NatID == 0) return;
            // Pokémon name
            p.NameEN = Properties.Resources.pokemonEN.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
            p.NameFR = Properties.Resources.pokemonFR.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
            p.NameDE = Properties.Resources.pokemonDE.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
            p.NameIT = Properties.Resources.pokemonIT.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
            p.NameES = Properties.Resources.pokemonES.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
            p.NameJP = Properties.Resources.pokemonJP.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
            p.NameKR = Properties.Resources.pokemonKR.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
            p.NameCHS = Properties.Resources.pokemonCHS.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];

            // Pokémon Form name

            if (p is PokemonUSUM) {

                p.FormNameEN = Properties.Resources.formsUSUM_EN.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameFR = Properties.Resources.formsUSUM_FR.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameDE = Properties.Resources.formsUSUM_DE.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameIT = Properties.Resources.formsUSUM_IT.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameES = Properties.Resources.formsUSUM_ES.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameJP = Properties.Resources.formsUSUM_JP.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameKR = Properties.Resources.formsUSUM_KR.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameCHS = Properties.Resources.formsUSUM_CHS.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];

            }
            else if(p is PokemonSuMo)
            {
                p.FormNameEN = Properties.Resources.formsSuMoEN.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameFR = Properties.Resources.formsSuMoFR.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameDE = Properties.Resources.formsSuMoDE.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameIT = Properties.Resources.formsSuMoIT.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameES = Properties.Resources.formsSuMoES.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameJP = Properties.Resources.formsSuMoJP.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameKR = Properties.Resources.formsSuMoKR.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameCHS = Properties.Resources.formsSuMoCHS.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
            }
            else
            {
                p.FormNameEN = Properties.Resources.formsEN.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameFR = Properties.Resources.formsFR.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameDE = Properties.Resources.formsDE.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameIT = Properties.Resources.formsIT.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameES = Properties.Resources.formsES.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameJP = Properties.Resources.formsJP.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameKR = Properties.Resources.formsKR.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
                p.FormNameCHS = Properties.Resources.formsCHS.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
            }
        }

        private static void setFormNames(Pokemon p, short formID)
        {
            if (p.NatID == 0) return;


            // Pokémon name
            p.NameEN = Properties.Resources.pokemonEN.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
            p.NameFR = Properties.Resources.pokemonFR.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
            p.NameDE = Properties.Resources.pokemonDE.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
            p.NameIT = Properties.Resources.pokemonIT.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
            p.NameES = Properties.Resources.pokemonES.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
            p.NameJP = Properties.Resources.pokemonJP.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
            p.NameKR = Properties.Resources.pokemonKR.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];
            p.NameCHS = Properties.Resources.pokemonCHS.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[p.NatID - 1];

            // Pokémon Form name

            // Sun/Moon Form names
            if (p is PokemonUSUM)
            {

                p.FormNameEN = Properties.Resources.formsUSUM_EN.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameFR = Properties.Resources.formsUSUM_FR.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameDE = Properties.Resources.formsUSUM_DE.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameIT = Properties.Resources.formsUSUM_IT.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameES = Properties.Resources.formsUSUM_ES.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameJP = Properties.Resources.formsUSUM_JP.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameKR = Properties.Resources.formsUSUM_KR.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameCHS = Properties.Resources.formsUSUM_CHS.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
            }

            else if (p is PokemonSuMo)
            {

                p.FormNameEN = Properties.Resources.formsSuMoEN.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameFR = Properties.Resources.formsSuMoFR.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameDE = Properties.Resources.formsSuMoDE.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameIT = Properties.Resources.formsSuMoIT.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameES = Properties.Resources.formsSuMoES.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameJP = Properties.Resources.formsSuMoJP.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameKR = Properties.Resources.formsSuMoKR.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameCHS = Properties.Resources.formsSuMoCHS.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
            }
            else
            {

                p.FormNameEN = Properties.Resources.formsEN.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameFR = Properties.Resources.formsFR.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameDE = Properties.Resources.formsDE.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameIT = Properties.Resources.formsIT.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameES = Properties.Resources.formsES.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameJP = Properties.Resources.formsJP.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameKR = Properties.Resources.formsKR.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
                p.FormNameCHS = Properties.Resources.formsCHS.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[formID];
            }

            
        }

        internal static Pokemon getPokemon(short speciesID, Version v)
        {
            short natID = (short)(speciesID & 0x3ff);
            switch (v)
            {
                case Version.Gold:
                case Version.Silver:
                    return pokemonGSTable[natID].getSpeciesForm(speciesID);
                case Version.Crystal:
                    return pokemonCrystalTable[natID].getSpeciesForm(speciesID);
                case Version.Ruby:
                case Version.Sapphire:
                    return pokemonRSTable[natID].getSpeciesForm(speciesID);
                case Version.FireRed:
                case Version.LeafGreen:
                    return pokemonFRLGTable[natID].getSpeciesForm(speciesID);
                case Version.Emerald:
                    return pokemonEmeraldTable[natID].getSpeciesForm(speciesID);
                case Version.Diamond:
                case Version.Pearl:
                    return pokemonDPTable[natID].getSpeciesForm(speciesID);
                case Version.Platinum:
                    return pokemonPtTable[natID].getSpeciesForm(speciesID);
                case Version.HeartGold:
                case Version.SoulSilver:
                    return pokemonHGSSTable[natID].getSpeciesForm(speciesID);
                case Version.Black:
                case Version.White:
                    return pokemonBWTable[natID].getSpeciesForm(speciesID);
                case Version.Black2:
                case Version.White2:
                    return pokemonB2W2Table[natID].getSpeciesForm(speciesID);
                case Version.X:
                case Version.Y:
                    return pokemonXYTable[natID].getSpeciesForm(speciesID);
                case Version.OmegaRuby:
                case Version.AlphaSapphire:
                    return pokemonORASTable[natID].getSpeciesForm(speciesID);
                case Version.Sun:
                case Version.Moon:
                    return pokemonSuMoTable[natID].getSpeciesForm(speciesID);
                case Version.UltraSun:
                case Version.UltraMoon:
                    return pokemonUSUMTable[natID].getSpeciesForm(speciesID);

                default: return null;
            }
            
        }

        // Get pokemon species and keep the format of the previous species.
        internal static Pokemon changePokemon(Pokemon current, short newID)
        {
            if (current is PokemonGS) return getPokemon(newID, Version.Gold);
            if (current is PokemonCrystal) return getPokemon(newID, Version.Crystal);

            if (current is PokemonRS) return getPokemon(newID, Version.Ruby);
            if (current is PokemonEmerald) return getPokemon(newID, Version.Emerald);
            if (current is PokemonFRLG) return getPokemon(newID, Version.FireRed);

            if (current is PokemonDP) return getPokemon(newID, Version.Diamond);
            if (current is PokemonPt) return getPokemon(newID, Version.Platinum);
            if (current is PokemonHGSS) return getPokemon(newID, Version.HeartGold);

            if (current is PokemonBW) return getPokemon(newID, Version.Black);
            if (current is PokemonB2W2) return getPokemon(newID, Version.Black2);

            if (current is PokemonXY) return getPokemon(newID, Version.X);
            if (current is PokemonORAS) return getPokemon(newID, Version.OmegaRuby);

            if (current is PokemonUSUM) return getPokemon(newID, Version.UltraSun);
            if (current is PokemonSuMo) return getPokemon(newID, Version.Sun);


            return null;
        }



    }
}
