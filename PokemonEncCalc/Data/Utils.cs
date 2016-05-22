using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonEncCalc
{
    public static class Utils{
        internal static List<Pokemon> PokemonList;
        static List<string> NamesEN;
        static List<string> NamesFR;
        static List<string> NamesDE;
        static List<string> NamesES;
        static List<string> NamesIT;
        static List<string> NamesJP;
        static List<string> NamesKR;
        static List<string> FormNamesEN;
        static List<string> FormNamesFR;
        static List<string> FormNamesDE;
        static List<string> FormNamesES;
        static List<string> FormNamesIT;
        static List<string> FormNamesJP;
        static List<string> FormNamesKR;

        //Current language names
        internal static List<string> NamesCurrentLang;
        internal static List<string> FormNamesCurrentLang;

        // Current language map names
        internal static List<string> MapsRS;
        internal static List<string> MapsEmer;
        internal static List<string> MapsFRLG;
        internal static List<string> MapsDP;
        internal static List<string> MapsPt;
        internal static List<string> MapsHGSS;
        internal static List<string> MapsBW;
        internal static List<string> MapsB2W2;
        internal static List<string> MapsXY;
        internal static List<string> MapsOR;
        internal static List<string> MapsAS;

        internal static string controlText;


        static readonly int[] FormIDs = new[] { 201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,201,
                                                351,351,351,382,383,386,386,386,
                                                412,412,413,413,422,423,479,479,479,479,479,487,492,
                                                550,555,585,585,585,586,586,586,641,642,645,646,646,648,
                                                669,669,669,669,670,670,670,670,670,671,671,671,671,678,681,710,710,710,711,711,711,720
                                                };


        private static void initializePokemonNames()
        {
            NamesEN = new List<string>();
            NamesFR = new List<string>();
            NamesDE = new List<string>();
            NamesES = new List<string>();
            NamesIT = new List<string>();
            NamesJP = new List<string>();
            NamesKR = new List<string>();
            FormNamesEN = new List<string>();
            FormNamesFR = new List<string>();
            FormNamesDE = new List<string>();
            FormNamesES = new List<string>();
            FormNamesIT = new List<string>();
            FormNamesJP = new List<string>();
            FormNamesKR = new List<string>();

            NamesEN.AddRange(Properties.Resources.pokemonEN.Split(new [] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries));
            NamesFR.AddRange(Properties.Resources.pokemonFR.Split(new [] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            NamesDE.AddRange(Properties.Resources.pokemonDE.Split(new [] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            NamesES.AddRange(Properties.Resources.pokemonES.Split(new [] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            NamesIT.AddRange(Properties.Resources.pokemonIT.Split(new [] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            NamesJP.AddRange(Properties.Resources.pokemonJP.Split(new [] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            NamesKR.AddRange(Properties.Resources.pokemonKR.Split(new [] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));

            FormNamesEN.AddRange(Properties.Resources.formsEN.Split(new [] { '\r', '\n'}, StringSplitOptions.RemoveEmptyEntries));
            FormNamesFR.AddRange(Properties.Resources.formsEN.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            FormNamesDE.AddRange(Properties.Resources.formsEN.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            FormNamesES.AddRange(Properties.Resources.formsEN.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            FormNamesIT.AddRange(Properties.Resources.formsEN.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            FormNamesJP.AddRange(Properties.Resources.formsEN.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            FormNamesKR.AddRange(Properties.Resources.formsEN.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));


        }

        internal static void initializePokemonList()
        {
            initializePokemonNames();
            PokemonList = new List<Pokemon>();
            int len = Properties.Resources.PokemonData.Length / 48;

            // Instanciate all 721 Pokemon (regular Forms only)
            for (short i = 0; i < len; i++)
            {
                byte[] data = new byte[48];
                Array.Copy(Properties.Resources.PokemonData, (i) * 48, data, 0, 48);
                PokemonList.Add(new Pokemon((short)(i + 1), data));
                PokemonList[i].NameEN = NamesEN[i];
                PokemonList[i].NameFR = NamesFR[i];
                PokemonList[i].NameES = NamesES[i];
                PokemonList[i].NameDE = NamesDE[i];
                PokemonList[i].NameIT = NamesIT[i];
                PokemonList[i].NameJP = NamesJP[i];
                PokemonList[i].NameKR = NamesKR[i];

                PokemonList[i].FormNameEN = FormNamesEN[i];
                PokemonList[i].FormNameDE = FormNamesDE[i];
                PokemonList[i].FormNameES = FormNamesES[i];
                PokemonList[i].FormNameFR = FormNamesFR[i];
                PokemonList[i].FormNameIT = FormNamesIT[i];
                PokemonList[i].FormNameJP = FormNamesJP[i];
                PokemonList[i].FormNameKR = FormNamesKR[i];


            }
            initializeForms();
            controlText = changeLanguage(Properties.Settings.Default.Language);
        }

        private static void initializeForms()
        {
            // Add Mega Evolutions
            int len = Properties.Resources.MegasData.Length / 26;

            for (short i = 0; i < len; i++)
            {
                byte[] data = new byte[24];
                short natID = BitConverter.ToInt16(Properties.Resources.MegasData, (i) * 26);
                Array.Copy(Properties.Resources.MegasData, (i) * 26 + 2, data, 0, 24);
                
                PokemonList[natID - 1].addForm(new Pokemon(natID,  data));
            }

            // Add other alternative forms
            len = Properties.Resources.FormsData.Length / 48;
            for (short i = 0; i < len; i++)
            {
                byte[] data = new byte[48];
                Array.Copy(Properties.Resources.FormsData, (i) * 48, data, 0, 48);
                Pokemon p = new Pokemon((short)FormIDs[i], data);
                p.FormNameEN = FormNamesEN[721 + i];
                p.FormNameDE = FormNamesDE[721 + i];
                p.FormNameES = FormNamesES[721 + i];
                p.FormNameFR = FormNamesFR[721 + i];
                p.FormNameIT = FormNamesIT[721 + i];
                p.FormNameJP = FormNamesJP[721 + i];
                p.FormNameKR = FormNamesKR[721 + i];
                PokemonList[FormIDs[i]-1].addForm(p);
            }

            //throw new NotImplementedException();
        }

        internal static string changeLanguage(int langID)
        {
            string controlNames = "";
            switch (langID) {
                case 1:
                    NamesCurrentLang = NamesEN;
                    FormNamesCurrentLang = FormNamesEN;
                    controlNames = Properties.Resources.interfaceEN;
                    break;
                case 2:
                    NamesCurrentLang = NamesFR;
                    FormNamesCurrentLang = FormNamesFR;
                    controlNames = Properties.Resources.interfaceFR;
                    break;
                case 3:
                    NamesCurrentLang = NamesDE;
                    FormNamesCurrentLang = FormNamesDE;
                    break;
                case 4:
                    NamesCurrentLang = NamesES;
                    FormNamesCurrentLang = FormNamesES;
                    break;
                case 5:
                    NamesCurrentLang = NamesIT;
                    FormNamesCurrentLang = FormNamesIT;
                    break;
                case 6:
                    NamesCurrentLang = NamesJP;
                    FormNamesCurrentLang = FormNamesJP;
                    break;
                case 7:
                    NamesCurrentLang = NamesKR;
                    FormNamesCurrentLang = FormNamesKR;
                    break;
                default:
                    break;
            }
            return controlNames;

        }





        //
        // Encounter rates calculation
        //
        internal static List<EncounterSlot> calcEncounterRate(EncounterSlot[] slots, Version version, Ability ability = Ability.None, byte repel = 0)
        {
            List<EncounterSlot> result = new List<EncounterSlot>();
            result.AddRange(slots);
            result = calcAbility(result, ability, version);
            result = calcRepel(result, repel);

            return normalizeSlots(result);
        }

        //
        // Repel calculation
        //
        private static List<EncounterSlot> calcRepel(List<EncounterSlot> slots, byte repel)
        {
            foreach (EncounterSlot e in slots)
                e.Percentage *= Math.Min(Math.Max(0, (decimal)(e.MaxLevel - repel) / (decimal)(e.MinLevel - repel)), 1);

            return slots;
        }

        private static List<EncounterSlot> calcAbility(List<EncounterSlot> slots, Ability ability, Version version)
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
                            slots.Add(new EncounterSlot(slots[i].Species, slots[i].MinLevel, slots[i].MaxLevel, 50 / nbElec));
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
                            slots.Add(new EncounterSlot(slots[i].Species, slots[i].MinLevel, slots[i].MaxLevel, 50 / nbSteels));
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
        // Normalize Encounter Slots: 100% scale & merge slots with same species.
        //

        internal static List<EncounterSlot> normalizeSlots(List<EncounterSlot> slots)
        {
            List<EncounterSlot> normalizedSlots = new List<EncounterSlot>();
            decimal totalPercent = 0;
            while(slots.Count != 0)
            {
                EncounterSlot e = slots[0];
                decimal percent = e.Percentage;
                byte minLv = e.MinLevel;
                byte maxLv = e.MaxLevel;
                for (int i = 1; i < slots.Count; i++)
                    if (e.Species.Equals(slots[i].Species))
                    {
                        minLv = Math.Min(minLv, slots[i].MinLevel);
                        maxLv = Math.Max(maxLv, slots[i].MaxLevel);
                        percent += slots[i].Percentage;
                    }

                normalizedSlots.Add(new EncounterSlot(e.Species, minLv, maxLv, percent));
                totalPercent += percent;
                slots.RemoveAll(s => s.Species.Equals(e.Species));
            }

            foreach (EncounterSlot a in normalizedSlots)
                a.Percentage *= 100 / totalPercent;

            return normalizedSlots;
        }


    }
}
