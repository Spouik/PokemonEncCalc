using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonEncCalc
{
    public static class Utils{


        //Current language names
        internal static List<string> NamesCurrentLang;
        internal static List<string> FormNamesCurrentLang;
        internal static List<string> FormNamesCurrentLangSuMo;

        // Current language map names
        internal static List<string> MapNamesGS;
        internal static List<string> MapNamesC;
        internal static List<string> MapNamesRS;
        internal static List<string> MapNamesE;
        internal static List<string> MapNamesFRLG;
        internal static List<string> MapNamesDP;
        internal static List<string> MapNamesPt;
        internal static List<string> MapNamesHGSS;
        internal static List<string> MapNamesBW;
        internal static List<string> MapNamesB2W2;
        internal static List<string> MapNamesXY;
        internal static List<string> MapNamesOR;
        internal static List<string> MapNamesAS;
        internal static List<string> MapNamesSuMo;
        internal static List<string> MapNamesUSUM;
        internal static List<string> MapNamesSafariHGSS;

        // List of all maps from Gen 2 to Gen 7
        internal static List<AreaMapGen2> MapsGold;
        internal static List<AreaMapGen2> MapsSilver;
        internal static List<AreaMapGen2> MapsCrystal;
        internal static List<AreaMapGen3> MapsRuby;
        internal static List<AreaMapGen3> MapsSapphire;
        internal static List<AreaMapGen3> MapsEmerald;
        internal static List<AreaMapGen3> MapsFireRed;
        internal static List<AreaMapGen3> MapsLeafGreen;
        internal static List<AreaMapDPPt> MapsDiamond;
        internal static List<AreaMapDPPt> MapsPearl;
        internal static List<AreaMapDPPt> MapsPlatinum;
        internal static List<AreaMapHGSS> MapsHeartGold;
        internal static List<AreaMapHGSS> MapsSoulSilver;
        internal static List<AreaMapGen5> MapsBlack;
        internal static List<AreaMapGen5> MapsWhite;
        internal static List<AreaMapGen5> MapsBlack2;
        internal static List<AreaMapGen5> MapsWhite2;
        internal static List<AreaMapXY> MapsX;
        internal static List<AreaMapXY> MapsY;
        internal static List<AreaMapORAS> MapsOmegaRuby;
        internal static List<AreaMapORAS> MapsAlphaSapphire;
        internal static List<AreaMapSuMo> MapsSun;
        internal static List<AreaMapSuMo> MapsMoon;
        internal static List<AreaMapSuMo> MapsUltraSun;
        internal static List<AreaMapSuMo> MapsUltraMoon;
        internal static List<AreaMapHGSSSafari> MapsSafariHGSS;


        // Map tables for gen4/gen5 games
        internal static int[] mapTablesDP = new int[] { 178, 176, 177, 53, 179, 180, 181, 182, 54, 55, 8, 9, 23, 24, 25, 26, 27, 28, 63, 69,
                                                        75, 112, 113, 114, 115, 118, 119, 121, 120, 122, 123, 124, 117, 0, 136, 137, 134, 7,
                                                        4, 5, 6, 56, 57, 58, 22, 19, 11, 12, 15, 16, 17, 18, 13, 10, 21, 20, 3, 138, 139, 140,
                                                        141, 142, 144, 143, 146, 145, 147, 148, 149, 150, 157, 156, 159, 158, 160, 161, 162, 163,
                                                        164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 47, 51, 48, 49, 50, 52, 29, 59,
                                                        106, 107, 108, 109, 110, 111, 151, 152, 153, 154, 155, 116, 2, 1, 125, 132 };

        internal static int[] mapTablesPlat = new int[] { 178, 176, 177, 53, 179, 180, 181, 182, 54, 55, 8, 9, 23, 24, 25, 26, 27, 28, 63, 70, 69,
                                                        75, 112, 113, 114, 115, 118, 119, 121, 120, 122, 123, 124, 117, 0, 136, 137, 134, 135, 7,
                                                        4, 5, 6, 56, 57, 58, 22, 19, 11, 12, 15, 16, 17, 18, 13, 10, 21, 20, 3, 138, 139, 140, 141,
                                                        142, 144, 143, 146, 145, 147, 148, 149, 150, 157, 156, 159, 158, 160, 161, 162, 163, 164, 165,
                                                        166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 47, 51, 48, 49, 50, 52, 29, 59, 106, 107,
                                                        108, 109, 110, 111, 151, 152, 153, 154, 155, 116, 2, 1, 125, 132 };

        internal static int[] mapTablesHGSS = new int[] { 66, 69, 70, 97, 20, 0, 95, 98, 133, 140, 139, 141, 14, 15, 16, 99, 93, 94, 128, 41, 42, 132,
                                                        101, 65, 137, 108, 109, 80, 79, 86, 88, 81, 87, 89, 83, 74, 75, 76, 77, 78, 51, 96, 58, 5, 85,
                                                        53, 54, 55, 56, 72, 106, 40, 23, 100, 82, 18, 19, 27, 111, 136, 112, 113, 114, 115, 116, 117,
                                                        118, 119, 120, 121, 92, 122, 123, 124, 125, 126, 127, 129, 130, 131, 103, 104, 105, 1, 3, 4, 8,
                                                        17, 21, 22, 25, 26, 38, 39, 52, 57, 59, 67, 68, 71, 102, 60, 61, 62, 63, 110, 9, 10, 30, 28, 29,
                                                        6, 43, 44, 46, 48, 2 };

        internal static int[] mapTablesBW = new int[] { 132, 133, 134, 141, 94, 144, 12, 14, 18, 33, 10, 11, 114, 2, 8, 9, 89, 90, 91, 92, 44, 45, 46,
                                                        112, 43, 93, 47, 0, 100, 99, 98, 1, 101, 102, 103, 106, 107, 108, 115, 123, 131, 135, 137, 138,
                                                        139, 140, 142, 143, 147, 145, 73, 74, 75, 77, 83, 88, 119, 120, 121, 122, 63, 67, 71, 72, 127,
                                                        146, 104, 6, 7 };

        internal static int[] mapTablesB2W2 = new int[] { 159, 64, 162, 15, 17, 21, 105, 106, 107, 13, 14, 62, 137, 2, 11, 12, 53, 54, 55, 23, 24, 25,
                                                        117, 118, 135, 63, 85, 26, 72, 73, 74, 0, 7, 8, 6, 122, 121, 120, 68, 69, 119, 123, 155, 156,
                                                        157, 158, 160, 161, 172, 163, 164, 124, 165, 173, 169, 170, 125, 128, 130, 131, 138, 146, 154,
                                                        108, 95, 96, 97, 98, 99, 100, 101, 111, 116, 115, 114, 142, 143, 144, 145, 42, 46, 50, 51, 150,
                                                        171, 126, 9, 10, 1, 70, 71 };


        internal static List<string> formList; // contains all forms to translate.
        internal static List<List<string>> controlText;


        static readonly int[] FormIDs = new[] { 201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,
                                                351,351,351,382,383,386,386,386,
                                                412,412,413,413,422,423,479,479,479,479,479,487,492,
                                                550,555,585,585,585,586,586,586,641,642,645,646,646,648,
                                                669,669,669,669,670,670,670,670,670,671,671,671,671,678,681,710,710,710,711,711,711,720
                                                };


        // List of moves for gen 3 to 6 (data)
        internal static List<Move> moveListGen3;
        internal static List<Move> moveListGen4;
        internal static List<Move> moveListGen5;
        internal static List<Move> moveListGen6;

        // List of moves for gen 3 to 6 (names)
        internal static List<string> moveNamesGen3;
        internal static List<string> moveNamesGen4;
        internal static List<string> moveNamesGen5;
        internal static List<string> moveNamesGen6;


        internal static void changeLanguage(int langID)
        {
            string controlNames = "";
            NamesCurrentLang = new List<string>();
            FormNamesCurrentLang = new List<string>();
            FormNamesCurrentLangSuMo = new List<string>();
            switch (langID) {
                case 1:
                    NamesCurrentLang.AddRange(Properties.Resources.pokemonEN.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    FormNamesCurrentLang.AddRange(Properties.Resources.formsEN.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    FormNamesCurrentLangSuMo.AddRange(Properties.Resources.formsSuMoEN.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    controlNames = Properties.Resources.interfaceEN;
                    break;
                case 2:
                    NamesCurrentLang.AddRange(Properties.Resources.pokemonFR.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    FormNamesCurrentLang.AddRange(Properties.Resources.formsFR.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    FormNamesCurrentLangSuMo.AddRange(Properties.Resources.formsSuMoFR.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    controlNames = Properties.Resources.interfaceFR;
                    break;
                case 3:
                    NamesCurrentLang.AddRange(Properties.Resources.pokemonDE.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    FormNamesCurrentLang.AddRange(Properties.Resources.formsDE.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    FormNamesCurrentLangSuMo.AddRange(Properties.Resources.formsSuMoDE.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                case 4:
                    NamesCurrentLang.AddRange(Properties.Resources.pokemonES.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    FormNamesCurrentLang.AddRange(Properties.Resources.formsES.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    FormNamesCurrentLangSuMo.AddRange(Properties.Resources.formsSuMoES.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                case 5:
                    NamesCurrentLang.AddRange(Properties.Resources.pokemonIT.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    FormNamesCurrentLang.AddRange(Properties.Resources.formsIT.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    FormNamesCurrentLangSuMo.AddRange(Properties.Resources.formsSuMoIT.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                case 6:
                    NamesCurrentLang.AddRange(Properties.Resources.pokemonJP.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    FormNamesCurrentLang.AddRange(Properties.Resources.formsJP.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    FormNamesCurrentLangSuMo.AddRange(Properties.Resources.formsSuMoJP.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                case 7:
                    NamesCurrentLang.AddRange(Properties.Resources.pokemonKR.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    FormNamesCurrentLang.AddRange(Properties.Resources.formsKR.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    FormNamesCurrentLangSuMo.AddRange(Properties.Resources.formsSuMoKR.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                default:
                    break;
            }

            initializeMoveNames();

            // 
            List<string> listFormControls = new List<string>();
            formList = new List<string>();
            listFormControls.AddRange(controlNames.Split(new[] { "!!!" }, StringSplitOptions.RemoveEmptyEntries));
            listFormControls.RemoveAt(0);
            controlText = new List<List<string>>();
            foreach(string s in listFormControls)
            {
                List<string> a = new List<string>();
                a.AddRange(s.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                formList.Add(a[0].Split(new[] {" = " }, StringSplitOptions.None)[0]);
                a.RemoveAll(c => c.StartsWith("!"));
                controlText.Add(a);
            }

        }


        internal static void initializeMoves()
        {
            moveListGen3 = new List<Move>();
            moveListGen4 = new List<Move>();
            moveListGen5 = new List<Move>();
            moveListGen6 = new List<Move>();

            for(int i = 0; i < Properties.Resources.MovesGen3.Length / 5; i++)
            {
                moveListGen3.Add(new Move(Properties.Resources.MovesGen3.Skip(5 * i).Take(5).ToArray()));
            }
            for (int i = 0; i < Properties.Resources.MovesGen4.Length / 6; i++)
            {
                moveListGen4.Add(new Move(Properties.Resources.MovesGen4.Skip(6 * i).Take(6).ToArray()));
            }
            for (int i = 0; i < Properties.Resources.MovesGen5.Length / 6; i++)
            {
                moveListGen5.Add(new Move(Properties.Resources.MovesGen5.Skip(6 * i).Take(6).ToArray()));
            }
            for (int i = 0; i < Properties.Resources.MovesGen6.Length / 6; i++)
            {
                moveListGen6.Add(new Move(Properties.Resources.MovesGen6.Skip(6 * i).Take(6).ToArray()));
            }

            initializeMoveNames();
        }

        internal static void initializeMoveNames()
        {
            moveNamesGen3 = new List<string>();
            moveNamesGen4 = new List<string>();
            moveNamesGen5 = new List<string>();
            moveNamesGen6 = new List<string>();

            switch (Properties.Settings.Default.Language)
            {
                case 1:
                    moveNamesGen3.AddRange(Properties.Resources.MovesNames3EN.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    moveNamesGen4.AddRange(Properties.Resources.MovesNames4EN.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    moveNamesGen5.AddRange(Properties.Resources.MovesNames5EN.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    moveNamesGen6.AddRange(Properties.Resources.MovesNames6EN.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                case 2:
                    moveNamesGen3.AddRange(Properties.Resources.MovesNames3FR.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    moveNamesGen4.AddRange(Properties.Resources.MovesNames4FR.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    moveNamesGen5.AddRange(Properties.Resources.MovesNames5FR.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    moveNamesGen6.AddRange(Properties.Resources.MovesNames6FR.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                default:
                    break;
            }
        }

        #region EncounterSlotCalculation


        //
        // Encounter rates calculation
        //
        internal static List<EncounterSlot> calcEncounterRate(EncounterSlot[] slots, Version version, Ability ability = Ability.None, byte repel = 0, byte intimidate = 0)
        {
            List<EncounterSlot> result = new List<EncounterSlot>();
            for(int i = 0; i< slots.Length; i++)
                result.Add(new EncounterSlot(slots[i]));
            result = calcAbility(result, ability, version, intimidate);
            result = calcRepel(result, repel);

            return result;
        }

        //
        // Repel calculation
        //
        private static List<EncounterSlot> calcRepel(List<EncounterSlot> slots, byte repel)
        {
            foreach (EncounterSlot e in slots)
                e.Percentage *= Math.Min(Math.Max(0, (decimal)(e.MaxLevel - repel + 1) / (decimal)(e.MaxLevel - e.MinLevel + 1)), 1);

            slots.RemoveAll(s => s.Percentage == 0);

            return slots;
        }

        private static List<EncounterSlot> calcAbility(List<EncounterSlot> slots, Ability ability, Version version, byte intimidate)
        {

            int slotCount = slots.Count;
            switch (ability)
            {
                case Ability.Static:
                    // counts the number of electrics
                    int nbElec = 0;
                    foreach(EncounterSlot e in slots)
                        if (e.Species.Type1 == Type.Electric || e.Species.Type2 == Type.Electric) nbElec++;

                    // Do nothing if no electric-types
                    if (nbElec == 0) break;

                    // Half electric, half regular slots
                    for(int i = 0; i < slotCount; i++)
                    {
                        if (slots[i].Species.Type1 == Type.Electric || slots[i].Species.Type2 == Type.Electric)
                            slots.Add(new EncounterSlot(slots[i].Species, slots[i].MinLevel, slots[i].MaxLevel, 50m / nbElec));
                        slots[i].Percentage /= 2;
                    }
                    break;

                case Ability.MagnetPull:
                    // counts the number of steels
                    int nbSteels = 0;
                    foreach (EncounterSlot e in slots)
                        if (e.Species.Type1 == Type.Steel || e.Species.Type2 == Type.Steel) nbSteels++;

                    // Do nothing if no steel-types
                    if (nbSteels == 0) break;

                    // Half steel, half regular slots
                    for (int i = 0; i < slotCount; i++)
                    {
                        if (slots[i].Species.Type1 == Type.Steel || slots[i].Species.Type2 == Type.Steel)
                            slots.Add(new EncounterSlot(slots[i].Species, slots[i].MinLevel, slots[i].MaxLevel, 50m / nbSteels));
                        slots[i].Percentage /= 2;
                    }
                    break;

                case Ability.Pressure:
                    // Three cases. 1: Each slot has a single Level. 2: At least one slot has MinLevel != MaxLevel (before Gen 5). 3: At least one slot has MinLevel != MaxLevel (Gen 5 onwards)
                    
                    // Cases 2 and 3.
                    if (slots.Any(e => e.MinLevel != e.MaxLevel))
                    {
                        // Case 3 -> Gen 5 onwards:
                        if(version >= Version.Black)
                        {
                            for(int i = 0; i < slotCount; i++)
                            {
                                slots[i].Percentage /= 2;
                                byte MaxLv = 0;
                                int capped = 0;
                                int lvRange = slots[i].MaxLevel - slots[i].MinLevel + 1;
                                foreach (EncounterSlot e in slots)
                                    if (slots[i].Species.NatID == e.Species.NatID) MaxLv = Math.Max(MaxLv, e.MaxLevel);
                                capped = Math.Max(Math.Min((5 - (MaxLv - slots[i].MaxLevel)), lvRange), 0);

                                slots.Add(new EncounterSlot(slots[i].Species, Math.Min((byte)(slots[i].MinLevel + 5), MaxLv), Math.Min(slots[i].MaxLevel, MaxLv), slots[i].Percentage * (lvRange - capped) / lvRange));
                                slots.Add(new EncounterSlot(slots[i].Species, MaxLv, MaxLv, slots[i].Percentage * capped / lvRange));
                      
                            }
                        }
                        // Case 2 -> Before Gen 5:
                        else
                        {
                            for(int i = 0; i < slotCount; i++)
                            {
                                slots[i].Percentage /= 2;
                                slots.Add(new EncounterSlot(slots[i].Species, slots[i].MaxLevel, slots[i].MaxLevel, slots[i].Percentage));
                            }
                        }
                    }
                    else
                    {
                        // case 1.
                        for(int i = 0; i < slotCount; i++)
                        {
                            // Get Max Level of the species:
                            byte maxLv = 0;
                            foreach(EncounterSlot e in slots)
                                if (slots[i].Species.NatID == e.Species.NatID) maxLv = Math.Max(maxLv, e.MaxLevel);

                            // Copy slot and replace levels.
                            slots.Add(new EncounterSlot(slots[i].Species, maxLv, maxLv, slots[i].Percentage / 2));

                            // Half percentage of slot i:
                            slots[i].Percentage /= 2;
                        }
                    }

                    break;

                case Ability.CuteCharm:
                    // Genderless and fixed-gendered Pokémon are more likely to be found shiny.

                    foreach (EncounterSlot s in slots)
                        if (new[] { 0, 254, 255 }.Contains(s.Species.GenderRatio)) s.EffectivePercentage *= 3;

                    decimal sum = slots.Sum(s => s.EffectivePercentage);

                    foreach (EncounterSlot s in slots)
                        s.EffectivePercentage *= (100 / sum);

                    break;

                case Ability.Intimidate:
                    // Repels half of Pokémon with level below or equals intimidator's Level - 5 (strictly below Intimidator's Lv - 4)
                    intimidate -= 4;
                    int nbSlots = slots.Count;

                    if (intimidate < 1) break;

                    for(int i =0; i < nbSlots; i++)
                    {
                        EncounterSlot s = slots[i];
                        decimal lvRange = Math.Max(0, Math.Min(1, (decimal)(s.MaxLevel - intimidate + 1) / (s.MaxLevel - s.MinLevel + 1)));
                        s.Percentage /= 2;
                        slots.Add(new EncounterSlot(s.Species, intimidate, s.MaxLevel, s.Percentage * lvRange));
                    }

                    //decimal sumPercent = slots.Sum(s => s.Percentage);

                    //foreach (EncounterSlot s in slots)
                    //    s.Percentage *= (100 / sumPercent);

                    slots.RemoveAll(s => s.Percentage == 0m);

                    break;
                default:
                    break;


            }

            return slots;
        }

        //
        // Cute Charm shiny rate calculation
        //

        internal static int cuteCharmShinyRate(List<EncounterSlot> slots)
        {
            decimal cuteCharmRate = 0;
            foreach (EncounterSlot e in slots)
                if (e.Species.GenderRatio != 0 && e.Species.GenderRatio < 254) cuteCharmRate += e.Percentage;

            cuteCharmRate /= 100;

            return (int)(1 / (decimal)(cuteCharmRate / 24576 + (1 - cuteCharmRate) / 8192));
        }

        //
        // Normalize Encounter Slots: 100% scale (Effective percentage) & merge slots with same species.
        // returns an empty list if there  is no Pokémon remaining
        //

        internal static List<EncounterSlot> normalizeSlots(List<EncounterSlot> slots)
        {
            List<EncounterSlot> normalizedSlots = new List<EncounterSlot>();
            List<EncounterSlot> slots2 = new List<EncounterSlot>();
            foreach (EncounterSlot e in slots) slots2.Add(new EncounterSlot(e));
            decimal totalPercent = 0;
            while(slots2.Count != 0)
            {
                EncounterSlot e = slots2[0];
                decimal percent = e.Percentage;
                decimal effPercent = e.EffectivePercentage;
                byte minLv = e.MinLevel;
                byte maxLv = e.MaxLevel;
                for (int i = 1; i < slots2.Count; i++)
                    if (e.Species.Equals(slots2[i].Species))
                    {
                        minLv = Math.Min(minLv, slots2[i].MinLevel);
                        maxLv = Math.Max(maxLv, slots2[i].MaxLevel);
                        percent += slots2[i].Percentage;
                        effPercent += slots2[i].EffectivePercentage;
                    }

                normalizedSlots.Add(new EncounterSlot(e.Species, minLv, maxLv, percent, effPercent));
                totalPercent += effPercent;
                slots2.RemoveAll(s => s.Species.Equals(e.Species));
            }

            // 
            if (totalPercent == 0)
                return new List<EncounterSlot>();

            foreach (EncounterSlot a in normalizedSlots)
                a.EffectivePercentage *= 100 / totalPercent;

            return normalizedSlots;
        }


        /// <summary>
        /// Calculate the mathematical expectation to find a shiny using Cute Charm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        internal static int cuteCharmExpectation(List<EncounterSlot> data)
        {
            decimal c = data.Where(s => new[] { 0, 254, 255 }.Contains(s.Species.GenderRatio)).Sum(s => s.Percentage);
            decimal gendered = data.Sum(s => s.Percentage) - c;
            return (int)Math.Round((gendered + c) / (c / 8192 + (gendered / 24576)));
        }

        #endregion


        #region EncounterSlotLoading

        internal static void loadEncounterSlotData()
        {
            loadSlotsGold();
            loadSlotsSilver();
            loadSlotsCrystal();
            loadSlotsHeartGold();
            loadSlotsRuby();
            loadSlotsSapphire();
            loadSlotsEmerald();
            loadSlotsFireRed();
            loadSlotsLeafGreen();
            loadSlotsDiamond();
            loadSlotsPearl();
            loadSlotsPlatinum();
            loadSlotsHeartGold();
            loadSlotsSoulSilver();
            loadSlotsBlack();
            loadSlotsWhite();
            loadSlotsBlack2();
            loadSlotsWhite2();
            loadSlotsX();
            loadSlotsY();
            loadSlotsOmegaRuby();
            loadSlotsAlphaSapphire();
            loadSlotsSun();
            loadSlotsMoon();
            loadSlotsUltraSun();
            loadSlotsUltraMoon();
            loadSlotsHGSSsafari();
        }



        private static void loadSlotsGold()
        {
            int mapCount = Properties.Resources.GoldSlots.Length / 360;
            MapsGold = new List<AreaMapGen2>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[360];
                data = Properties.Resources.GoldSlots.Skip(360 * i).Take(360).ToArray();
                MapsGold.Add(new AreaMapGen2(data, Version.Gold, i));
            }
        }

        private static void loadSlotsSilver()
        {
            int mapCount = Properties.Resources.SilverSlots.Length / 360;
            MapsSilver = new List<AreaMapGen2>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[360];
                data = Properties.Resources.SilverSlots.Skip(360 * i).Take(360).ToArray();
                MapsSilver.Add(new AreaMapGen2(data, Version.Silver, i));
            }
        }

        private static void loadSlotsCrystal()
        {
            int mapCount = Properties.Resources.CrystalSlots.Length / 360;
            MapsCrystal = new List<AreaMapGen2>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[360];
                data = Properties.Resources.CrystalSlots.Skip(360 * i).Take(360).ToArray();
                MapsCrystal.Add(new AreaMapGen2(data, Version.Crystal, i));
            }
        }

        private static void loadSlotsRuby()
        {
            int mapCount = Properties.Resources.RubySlots.Length / 128;
            MapsRuby = new List<AreaMapGen3>();
            for(int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[128];
                data = Properties.Resources.RubySlots.Skip(128 * i).Take(128).ToArray();
                MapsRuby.Add(new AreaMapGen3(data, Version.Ruby, i));
            }
        }

        private static void loadSlotsSapphire()
        {
            int mapCount = Properties.Resources.SapphireSlots.Length / 128;
            MapsSapphire = new List<AreaMapGen3>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[128];
                data = Properties.Resources.SapphireSlots.Skip(128 * i).Take(128).ToArray();
                MapsSapphire.Add(new AreaMapGen3(data, Version.Sapphire, i));
            }
        }

        private static void loadSlotsEmerald()
        {
            int mapCount = Properties.Resources.EmeraldSlots.Length / 128;
            MapsEmerald = new List<AreaMapGen3>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[128];
                data = Properties.Resources.EmeraldSlots.Skip(128 * i).Take(128).ToArray();
                MapsEmerald.Add(new AreaMapGen3(data, Version.Emerald, i));
            }
        }

        private static void loadSlotsFireRed()
        {
            int mapCount = Properties.Resources.FireRedSlots.Length / 128;
            MapsFireRed = new List<AreaMapGen3>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[128];
                data = Properties.Resources.FireRedSlots.Skip(128 * i).Take(128).ToArray();
                MapsFireRed.Add(new AreaMapGen3(data, Version.FireRed, i));
            }
        }

        private static void loadSlotsLeafGreen()
        {
            int mapCount = Properties.Resources.LeafGreenSlots.Length / 128;
            MapsLeafGreen = new List<AreaMapGen3>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[128];
                data = Properties.Resources.LeafGreenSlots.Skip(128 * i).Take(128).ToArray();
                MapsLeafGreen.Add(new AreaMapGen3(data, Version.LeafGreen, i));
            }
        }

        private static void loadSlotsDiamond()
        {
            int mapCount = Properties.Resources.DiamondSlots.Length / 424;
            MapsDiamond = new List<AreaMapDPPt>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[424];
                data = Properties.Resources.DiamondSlots.Skip(424 * i).Take(424).ToArray();
                MapsDiamond.Add(new AreaMapDPPt(data, Version.Diamond, i));
            }
        }

        private static void loadSlotsPearl()
        {
            int mapCount = Properties.Resources.PearlSlots.Length / 424;
            MapsPearl = new List<AreaMapDPPt>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[424];
                data = Properties.Resources.PearlSlots.Skip(424 * i).Take(424).ToArray();
                MapsPearl.Add(new AreaMapDPPt(data, Version.Pearl, i));
            }
        }

        private static void loadSlotsPlatinum()
        {
            int mapCount = Properties.Resources.PlatinumSlots.Length / 424;
            MapsPlatinum = new List<AreaMapDPPt>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[424];
                data = Properties.Resources.PlatinumSlots.Skip(424 * i).Take(424).ToArray();
                MapsPlatinum.Add(new AreaMapDPPt(data, Version.Platinum, i));
            }
        }

        private static void loadSlotsHeartGold()
        {
            int mapCount = Properties.Resources.HeartGoldSlots.Length / 196;
            MapsHeartGold = new List<AreaMapHGSS>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[196];
                data = Properties.Resources.HeartGoldSlots.Skip(196 * i).Take(196).ToArray();
                MapsHeartGold.Add(new AreaMapHGSS(data, Version.HeartGold, i));
            }
        }

        private static void loadSlotsSoulSilver()
        {
            int mapCount = Properties.Resources.SoulSilverSlots.Length / 196;
            MapsSoulSilver = new List<AreaMapHGSS>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[196];
                data = Properties.Resources.SoulSilverSlots.Skip(196 * i).Take(196).ToArray();
                MapsSoulSilver.Add(new AreaMapHGSS(data, Version.SoulSilver, i));
            }
        }

        private static void loadSlotsBlack()
        {
            int mapCount = Properties.Resources.BlackSlots.Length / 232;
            MapsBlack = new List<AreaMapGen5>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[232];
                data = Properties.Resources.BlackSlots.Skip(232 * i).Take(232).ToArray();
                MapsBlack.Add(new AreaMapGen5(data, Version.Black, i));
            }
        }

        private static void loadSlotsWhite()
        {
            int mapCount = Properties.Resources.WhiteSlots.Length / 232;
            MapsWhite = new List<AreaMapGen5>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[232];
                data = Properties.Resources.WhiteSlots.Skip(232 * i).Take(232).ToArray();
                MapsWhite.Add(new AreaMapGen5(data, Version.White, i));
            }
        }

        private static void loadSlotsBlack2()
        {
            int mapCount = Properties.Resources.Black2Slots.Length / 232;
            MapsBlack2 = new List<AreaMapGen5>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[232];
                data = Properties.Resources.Black2Slots.Skip(232 * i).Take(232).ToArray();
                MapsBlack2.Add(new AreaMapGen5(data, Version.Black2, i));
            }
        }

        private static void loadSlotsWhite2()
        {
            int mapCount = Properties.Resources.White2Slots.Length / 232;
            MapsWhite2 = new List<AreaMapGen5>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[232];
                data = Properties.Resources.White2Slots.Skip(232 * i).Take(232).ToArray();
                MapsWhite2.Add(new AreaMapGen5(data, Version.White2, i));
            }
        }

        private static void loadSlotsX()
        {
            int mapCount = Properties.Resources.XSlots.Length / 376;
            MapsX = new List<AreaMapXY>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[376];
                data = Properties.Resources.XSlots.Skip(376 * i).Take(376).ToArray();
                MapsX.Add(new AreaMapXY(data, Version.X, i));
            }
        }

        private static void loadSlotsY()
        {
            int mapCount = Properties.Resources.YSlots.Length / 376;
            MapsY = new List<AreaMapXY>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[376];
                data = Properties.Resources.YSlots.Skip(376 * i).Take(376).ToArray();
                MapsY.Add(new AreaMapXY(data, Version.Y, i));
            }
        }

        private static void loadSlotsOmegaRuby()
        {
            int mapCount = Properties.Resources.OmegaRubySlots.Length / 244;
            MapsOmegaRuby = new List<AreaMapORAS>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[244];
                data = Properties.Resources.OmegaRubySlots.Skip(244 * i).Take(244).ToArray();
                MapsOmegaRuby.Add(new AreaMapORAS(data, Version.OmegaRuby, i));
            }
        }

        private static void loadSlotsAlphaSapphire()
        {
            int mapCount = Properties.Resources.AlphaSapphireSlots.Length / 244;
            MapsAlphaSapphire = new List<AreaMapORAS>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[244];
                data = Properties.Resources.AlphaSapphireSlots.Skip(244 * i).Take(244).ToArray();
                MapsAlphaSapphire.Add(new AreaMapORAS(data, Version.AlphaSapphire, i));
            }
        }

        private static void loadSlotsSun()
        {
            int mapCount = 64;
            MapsSun = new List<AreaMapSuMo>();
            for(int i = 0; i < mapCount; i++)
            {
                byte[] data = ((byte[])Properties.Resources.ResourceManager.GetObject("S_Location_" + i)).Skip(0x80).ToArray();
                MapsSun.Add(new AreaMapSuMo(data, Version.Sun));
            }
        }

        private static void loadSlotsMoon()
        {
            int mapCount = 64;
            MapsMoon = new List<AreaMapSuMo>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = ((byte[])Properties.Resources.ResourceManager.GetObject("M_Location_" + i)).Skip(0x80).ToArray();
                MapsMoon.Add(new AreaMapSuMo(data, Version.Moon));
            }
        }

        private static void loadSlotsUltraSun()
        {
            int mapCount = 71;
            MapsUltraSun = new List<AreaMapSuMo>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = ((byte[])Properties.Resources.ResourceManager.GetObject("US_Location_" + i)).Skip(0x80).ToArray();
                MapsUltraSun.Add(new AreaMapSuMo(data, Version.UltraSun));
            }
        }

        private static void loadSlotsUltraMoon()
        {
            int mapCount = 71;
            MapsUltraMoon = new List<AreaMapSuMo>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = ((byte[])Properties.Resources.ResourceManager.GetObject("UM_Location_" + i)).Skip(0x80).ToArray();
                MapsUltraMoon.Add(new AreaMapSuMo(data, Version.UltraMoon));
            }
        }



        private static void loadSlotsHGSSsafari()
        {
            int mapCount = 12;
            MapsSafariHGSS = new List<AreaMapHGSSSafari>();
            for (int i = 0; i < mapCount; i++)
            {
                byte[] data = new byte[912];
                data = Properties.Resources.HGSSsafari.Skip(912 * i + 148).Take(912).ToArray();
                MapsSafariHGSS.Add(new AreaMapHGSSSafari(data, Version.HeartGold, i));
            }
                
        }


        #endregion


    }
}
