﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PokemonEncCalc
{
    public partial class frmMainPage : TranslatableForm
    {
        // Current Slots : Should match with those displayed on the form
        EncounterSlot[] currentSlots;

        bool updatingSlots = false; //is true when slots are being updated automatically (prevents slot modification via controls during this time)

        // Contains strings for encounter types and encounter options for the current language
        // encounterOptions[0] contains all encounter types, while encounterOptions[1] contains abilities
        // Subsequent entries contain other encounter options (swarm, time of day, seasons...)
        List<List<string>> encounterOptions;

        public frmMainPage()
        {
            loadEncounterOptions((Language)Properties.Settings.Default.Language);
            InitializeComponent();
            if (Properties.Settings.Default.ShinySprites)
                shinyToolStripMenuItem.Checked = true;
            else
                normalToolStripMenuItem.Checked = true;

            switch(Properties.Settings.Default.Language)
            {
                case 1:
                    englishToolStripMenuItem.Checked = true;
                    break;
                case 2:
                    frenchToolStripMenuItem.Checked = true;
                    break;
                case 8:
                    chinesesimplifiedToolStripMenuItem.Checked = true;
                    break;
                case 9:
                    chinesetraditionalToolStripMenuItem.Checked = true;
                    break;
                default: break;
            }
            
        }

        private void cmdCalc_Click(object sender, EventArgs e)
        {
            Version version = (Version)(cboVersion.SelectedIndex + (int)Program.STARTING_VERSION);
            Ability ability = (chkAbility.Checked && pnlAbility.Visible) 
                ? (Ability)(encounterOptions[1].FindIndex(s => s.Equals(cboAbility.SelectedItem)) + 1)
                : Ability.None;
            List<EncounterSlot> result = Utils.calcEncounterRate(currentSlots, version, ability, 
                chkRepel.Checked ? (byte)nudLevelRepel.Value : (byte)0, 
                nudIntimidateLevel.Visible ? (byte)nudIntimidateLevel.Value : (byte)0);

            string resultText = encounterInfoAsText();

            frmDisplayResults r = new frmDisplayResults();
            r.ShowDialog(result, resultText, (ability == Ability.CuteCharm));
        }

        private string encounterInfoAsText()
        {
            string r = "";

            // Map name
            if (cboMapsGS.Visible) r += (string)(cboMapsGS.SelectedItem);
            if (cboMapsCrystal.Visible) r += (string)(cboMapsCrystal.SelectedItem);
            if (cboMapsRubySapp.Visible) r += (string)(cboMapsRubySapp.SelectedItem);
            if (cboMapsEmer.Visible) r += (string)(cboMapsEmer.SelectedItem);
            if (cboMapsFireLeaf.Visible) r += (string)(cboMapsFireLeaf.SelectedItem);
            if (cboMapsDP.Visible) r += (string)(cboMapsDP.SelectedItem);
            if (cboMapsPlat.Visible) r += (string)(cboMapsPlat.SelectedItem);
            if (cboMapsHGSS.Visible) r += (string)(cboMapsHGSS.SelectedItem);
            if (cboMapsBW.Visible) r += (string)(cboMapsBW.SelectedItem);
            if (cboMapsB2W2.Visible) r += (string)(cboMapsB2W2.SelectedItem);
            if (cboMapsXY.Visible) r += (string)(cboMapsXY.SelectedItem);
            if (cboMapsOR.Visible) r += (string)(cboMapsOR.SelectedItem);
            if (cboMapsAS.Visible) r += (string)(cboMapsAS.SelectedItem);
            if (cboMapsSuMo.Visible) r += (string)(cboMapsSuMo.SelectedItem);
            if (cboMapsUSUM.Visible) r += (string)(cboMapsUSUM.SelectedItem);

            // Version
            r += " (" + (string)(cboVersion.SelectedItem) + ")";

            // Encounter Type (or table for Gen7)
            if (cboMapsSuMo.Visible || cboMapsUSUM.Visible) r += " - " + cboTablesSuMo.SelectedItem + Environment.NewLine + Environment.NewLine;
            else r += " - " + cboEncounterType.SelectedItem + Environment.NewLine + Environment.NewLine;

            // Repel
            if (chkRepel.Checked) r += chkRepel.Text + " " + lblLevelRepelDisp.Text + " " + nudLevelRepel.Value;

            // Ability
            if (pnlAbility.Visible && chkAbility.Checked)
            {
                r += (r.EndsWith(Environment.NewLine) ? "" : " - ") + cboAbility.SelectedItem;
                if (cboAbility.SelectedItem.ToString() == encounterOptions[1][4])
                    r += " (" + lblLevelRepelDisp.Text + " " + nudIntimidateLevel.Value.ToString() + ")"; 
            }

            // Swarm
            if (pnlOptionsGen2.Visible && cboSwarmGSC.Enabled && cboSwarmGSC.SelectedIndex == 1)
                r += (r.EndsWith(Environment.NewLine) ? "" : " - ") + lblSwarmDPPtDisp.Text;
            if (pnlDPPtOptions.Visible && cboSwarmDPPt.Enabled && cboSwarmDPPt.SelectedIndex == 1)
                r += (r.EndsWith(Environment.NewLine) ? "" : " - ") + lblSwarmDPPtDisp.Text;
            if (pnlHGSSOptions.Visible && cboSwarmHGSS.Enabled && cboSwarmHGSS.SelectedIndex == 1)
                r += (r.EndsWith(Environment.NewLine) ? "" : " - ") + lblSwarmHGSSDisp.Text;

            // Bug Catching Contest
            if (pnlOptionsGen2.Visible && chkBugCatching.Enabled && chkBugCatching.Checked)
                r += (r.EndsWith(Environment.NewLine) ? "" : " - ") + chkBugCatching.Text;

            // Time
            if (pnlOptionsGen2.Visible && cboTimeGSC.Enabled)
                r += (r.EndsWith(Environment.NewLine) ? "" : " - ") + cboTimeGSC.SelectedItem;
            if (pnlDPPtOptions.Visible && cboTimeDPPt.Enabled)
                r += (r.EndsWith(Environment.NewLine) ? "" : " - ") + cboTimeDPPt.SelectedItem;
            if (pnlHGSSOptions.Visible && cboTimeHGSS.Enabled)
                r += (r.EndsWith(Environment.NewLine) ? "" : " - ") + cboTimeHGSS.SelectedItem;
            if (pnl_SunMoonOptions.Visible) r += (r.EndsWith(Environment.NewLine) ? "" : " - ") + cboDayNightSuMo.SelectedItem;

            // GBA Slot
            if (pnlDPPtOptions.Visible && cboGBASlot.Enabled && cboGBASlot.SelectedIndex > 0)
                r += (r.EndsWith(Environment.NewLine) ? "" : " - ") + cboGBASlot.SelectedItem;

            // PokéRadar
            if (pnlDPPtOptions.Visible && chkRadarDPPt.Checked)
                r += (r.EndsWith(Environment.NewLine) ? "" : " - ") + chkRadarDPPt.Text;

            // Radio
            if (pnlHGSSOptions.Visible && cboRadio.Enabled && cboRadio.SelectedIndex > 0)
                r += (r.EndsWith(Environment.NewLine) ? "" : " - ") + cboRadio.SelectedItem;

            // Season
            if (pnlGen5Options.Visible && cboSeason.Enabled)
                r += (r.EndsWith(Environment.NewLine) ? "" : " - ") + cboSeason.SelectedItem;

           


            return r;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMainPage_Load(object sender, EventArgs e)
        {
            Text = "Pokémon Encounter Calculator - Ver. " + Program.VERSION;
            Utils.changeLanguage(Properties.Settings.Default.Language);
            renameControls();
            renameMenuStrip();
            renameComboboxes();
            updateEncounterOptions();
        }

        private void renameMenuStrip()
        {
            if (Utils.controlText[0] == null) return;  // if nothing to translate, abort function and don't translate user interface


            foreach (string line in Utils.controlText[0])
            {


                string[] stringSplit = line.Split(new[] { " = " }, StringSplitOptions.None);
                if (stringSplit.Length < 2)
                    continue; // Error : invalid format (expected "control = text")

                string control = stringSplit[0];
                string text = stringSplit[1];

                // rename menustrip items
                Control[] c = mstMenu.Controls.Find(control, true);
                if (c.Length > 0)
                    c[0].Text = text;


            }
        }


        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = 1;
            Utils.changeLanguage(1);
            loadEncounterOptions(Language.English);
            frenchToolStripMenuItem.Checked = false;
            englishToolStripMenuItem.Checked = true;
            chinesesimplifiedToolStripMenuItem.Checked = false;
            chinesetraditionalToolStripMenuItem.Checked = false;
            updateEncounterOptions();
            renameControls();
            renameMenuStrip();
            renameComboboxes();
            repopulateComboboxes(currentSlots[0].Species.getNbReleased());
            update();
        }

        private void frenchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = 2;
            Utils.changeLanguage(2);
            loadEncounterOptions(Language.French);
            frenchToolStripMenuItem.Checked = true;
            englishToolStripMenuItem.Checked = false;
            chinesesimplifiedToolStripMenuItem.Checked = false;
            chinesetraditionalToolStripMenuItem.Checked = false;
            updateEncounterOptions();
            renameControls();
            renameMenuStrip();
            renameComboboxes();
            repopulateComboboxes(currentSlots[0].Species.getNbReleased());
            update();
        }

        private void chinesesimplifiedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = 8;
            Utils.changeLanguage(8);
            loadEncounterOptions(Language.ChineseSimplified);
            frenchToolStripMenuItem.Checked = false;
            englishToolStripMenuItem.Checked = false;
            chinesesimplifiedToolStripMenuItem.Checked = true;
            chinesetraditionalToolStripMenuItem.Checked = false;
            updateEncounterOptions();
            renameControls();
            renameMenuStrip();
            renameComboboxes();
            repopulateComboboxes(currentSlots[0].Species.getNbReleased());
            update();
        }

        private void chinesetraditionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = 9;
            Utils.changeLanguage(9);
            loadEncounterOptions(Language.ChineseTraditional);
            frenchToolStripMenuItem.Checked = false;
            englishToolStripMenuItem.Checked = false;
            chinesesimplifiedToolStripMenuItem.Checked = false;
            chinesetraditionalToolStripMenuItem.Checked = true;
            updateEncounterOptions();
            renameControls();
            renameMenuStrip();
            renameComboboxes();
            repopulateComboboxes(currentSlots[0].Species.getNbReleased());
            update();
        }

        private void changeMinisprite(object sender, EventArgs e)
        {
            if (!(sender is ComboBox))  //Checks if it is a combobox (only comboboxes can handle this method)
                return;

            if (!((ComboBox)sender).Name.StartsWith("cboSlot")) //Checks combobox name (expected "cboSlot" + x, where x between 0 and 11)
                return;

            if (!((ComboBox)sender).Visible)
                return;

            int slot;
            if (!int.TryParse(((ComboBox)sender).Name.Substring(7), out slot))
                return;

            if (updatingSlots)
                return;

            // Change Slot
            if(currentSlots != null)
                if(slot < currentSlots.Length)
                    currentSlots[slot] = new EncounterSlot(PokemonTables.changePokemon(currentSlots[slot].Species, (short)(((ComboBox)sender).SelectedIndex + 1)),
                                                    (byte)((NumericUpDown)gboSlots.Controls.Find("nudMinLv" + slot, true)[0]).Value,
                                                    (byte)((NumericUpDown)gboSlots.Controls.Find("nudMaxLv" + slot, true)[0]).Value,
                                                    Decimal.Parse(((Label)gboSlots.Controls.Find("lblPercent" + slot, true)[0]).Text.Split(new[] { ' ' })[0]));

            // Change minisprite
            update();

        }

        private void cboVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hide every map combobox and every option
            cboMapsRubySapp.Visible = cboMapsEmer.Visible = cboMapsFireLeaf.Visible = cboMapsDP.Visible
                = cboMapsPlat.Visible = cboMapsHGSS.Visible = cboMapsBW.Visible = cboMapsB2W2.Visible
                = cboMapsXY.Visible = cboMapsOR.Visible = cboMapsAS.Visible = pnlAbility.Visible = pnlDPPtOptions.Visible
                = pnlHGSSOptions.Visible = pnlGen5Options.Visible = pnlLuckyPower.Visible
                = lblHelpRoute120.Visible = lblHelpTurnback.Visible 
                = cboMapsGS.Visible = cboMapsCrystal.Visible = pnlOptionsGen2.Visible 
                = cboMapsSuMo.Visible = cboMapsUSUM.Visible = pnl_SunMoonOptions.Visible = pnlHGSSSafari.Visible = false;




            // Select map combobox to show based on selected version
            Version ver = (Version)(cboVersion.SelectedIndex + (int)Version.Gold);
            switch (ver)
            {
                case Version.Gold:
                case Version.Silver:
                    repopulateComboboxes(PokemonGS.RELEASED_POKEMON);
                    cboMapsGS.Visible = true;
                    pnlOptionsGen2.Visible = true;
                    changeEncounterOptionsGoldSilver();
                    break;
                case Version.Crystal:
                    repopulateComboboxes(PokemonCrystal.RELEASED_POKEMON);
                    cboMapsCrystal.Visible = true;
                    pnlOptionsGen2.Visible = true;
                    changeEncounterOptionsCrystal();
                    break;
                case Version.Ruby:
                case Version.Sapphire:
                    repopulateComboboxes(PokemonRS.RELEASED_POKEMON);
                    cboMapsRubySapp.Visible = true;
                    changeEncounterOptionsRubySapp();
                    break;
                case Version.Emerald:
                    repopulateComboboxes(PokemonEmerald.RELEASED_POKEMON);
                    cboMapsEmer.Visible = true;
                    pnlAbility.Visible = true;
                    changeEncounterOptionsEmerald();
                    break;
                case Version.FireRed:
                case Version.LeafGreen:
                    repopulateComboboxes(PokemonFRLG.RELEASED_POKEMON);
                    cboMapsFireLeaf.Visible = true;
                    changeEncounterOptionsFireLeaf();
                    break;
                case Version.Diamond:
                case Version.Pearl:
                    repopulateComboboxes(PokemonDP.RELEASED_POKEMON);
                    cboMapsDP.Visible = true;
                    pnlAbility.Visible = true;
                    pnlDPPtOptions.Visible = true;
                    changeEncounterOptionsDP();
                    break;
                case Version.Platinum:
                    repopulateComboboxes(PokemonPt.RELEASED_POKEMON);
                    cboMapsPlat.Visible = true;
                    pnlAbility.Visible = true;
                    pnlDPPtOptions.Visible = true;
                    changeEncounterOptionsPlat();
                    break;
                case Version.HeartGold:
                case Version.SoulSilver:
                    repopulateComboboxes(PokemonHGSS.RELEASED_POKEMON);
                    cboMapsHGSS.Visible = true;
                    pnlAbility.Visible = true;
                    pnlHGSSOptions.Visible = true;
                    changeEncounterOptionsHGSS();
                    break;
                case Version.Black:
                case Version.White:
                    repopulateComboboxes(PokemonBW.RELEASED_POKEMON);
                    cboMapsBW.Visible = true;
                    pnlAbility.Visible = true;
                    pnlGen5Options.Visible = true;
                    changeEncounterOptionsBW();
                    break;
                case Version.Black2:
                case Version.White2:
                    repopulateComboboxes(PokemonB2W2.RELEASED_POKEMON);
                    cboMapsB2W2.Visible = true;
                    pnlAbility.Visible = true;
                    pnlGen5Options.Visible = true;
                    pnlLuckyPower.Visible = true;
                    changeEncounterOptionsB2W2();
                    break;
                case Version.X:
                case Version.Y:
                    repopulateComboboxes(PokemonXY.RELEASED_POKEMON);
                    cboMapsXY.Visible = true;
                    pnlAbility.Visible = true;
                    changeEncounterOptionsXY();
                    break;
                case Version.OmegaRuby:
                    repopulateComboboxes(PokemonORAS.RELEASED_POKEMON);
                    cboMapsOR.Visible = true;
                    pnlAbility.Visible = true;
                    changeEncounterOptionsOR();
                    break;
                case Version.AlphaSapphire:
                    repopulateComboboxes(PokemonORAS.RELEASED_POKEMON);
                    cboMapsAS.Visible = true;
                    pnlAbility.Visible = true;
                    changeEncounterOptionsAS();
                    break;
                case Version.Sun:
                case Version.Moon:
                    repopulateComboboxes(PokemonSuMo.RELEASED_POKEMON);
                    cboMapsSuMo.Visible = true;
                    pnlAbility.Visible = true;
                    pnl_SunMoonOptions.Visible = true;
                    changeEncounterOptionsSuMo();
                    break;
                case Version.UltraSun:
                case Version.UltraMoon:
                    repopulateComboboxes(PokemonUSUM.RELEASED_POKEMON);
                    cboMapsUSUM.Visible = true;
                    pnlAbility.Visible = true;
                    pnl_SunMoonOptions.Visible = true;
                    changeEncounterOptionsUSUM();
                    break;
                default:
                    break;
            }
        }

        private void repopulateComboboxes(int n)
        {
            foreach (Control o in gboSlots.Controls)
            {
                if (!(o is ComboBox)) continue;

                ComboBox c = (ComboBox)o;
            
                c.Items.Clear();
                c.Items.AddRange(Utils.NamesCurrentLang.Take(n).ToArray());
            }
        }

        private void changeMinLevel(object sender, EventArgs e)
        {
            if (updatingSlots)
                return;

            if (!(sender is NumericUpDown))
                return;

            if (!((NumericUpDown)sender).Name.StartsWith("nudMinLv"))
                return;

            int slot;
            if (!Int32.TryParse(((NumericUpDown)sender).Name.Substring(8), out slot))
                return;

            currentSlots[slot].MinLevel = (byte)((NumericUpDown)sender).Value;


        }

        private void changeMaxLevel(object sender, EventArgs e)
        {
            if (updatingSlots)
                return;

            if (!(sender is NumericUpDown))
                return;

            if (!((NumericUpDown)sender).Name.StartsWith("nudMaxLv"))
                return;

            int slot;
            if (!Int32.TryParse(((NumericUpDown)sender).Name.Substring(8), out slot))
                return;

            currentSlots[slot].MaxLevel = (byte)((NumericUpDown)sender).Value;
        }

        private void changeForm(object sender, EventArgs e)
        {
            int slot = 0;
            Int32.TryParse(((PictureBox)sender).Name.Substring(7), out slot);
            frmFormSelect formSelect = new frmFormSelect();
            if (formSelect.ShowDialog(currentSlots[slot]) == DialogResult.OK)
                currentSlots[slot] = frmFormSelect.getResult();


            update();
        }



        private void renameComboboxes()
        {
            // Rename Comboboxes
            // Version:
            translateComboBoxes(cboVersion, "versions" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1]);
            // Maps GS:
            translateMaps(cboMapsGS, "Maps_GS_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1], ref Utils.MapNamesGS);
            // Maps C:
            translateMaps(cboMapsCrystal, "Maps_C_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1], ref Utils.MapNamesC);
            // Maps RS:
            translateMaps(cboMapsRubySapp, "Maps_RS_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1], ref Utils.MapNamesRS);
            // Maps E:
            translateMaps(cboMapsEmer, "Maps_E_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1], ref Utils.MapNamesE);
            // Maps FRLG:
            translateMaps(cboMapsFireLeaf, "Maps_FRLG_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1], ref Utils.MapNamesFRLG);
            // Maps DP:
            translateMaps(cboMapsDP, "Maps_DP_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1], ref Utils.MapNamesDP);
            // Maps Pt:
            translateMaps(cboMapsPlat, "Maps_Pt_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1], ref Utils.MapNamesPt);
            // Maps HGSS:
            translateMaps(cboMapsHGSS, "Maps_HGSS_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1], ref Utils.MapNamesHGSS);
            // Maps BW:
            translateMaps(cboMapsBW, "Maps_BW_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1], ref Utils.MapNamesBW);
            // Maps B2W2:
            translateMaps(cboMapsB2W2, "Maps_B2W2_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1], ref Utils.MapNamesB2W2);
            // Maps XY:
            translateMaps(cboMapsXY, "Maps_XY_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1], ref Utils.MapNamesXY);
            // Maps OR:
            translateMaps(cboMapsOR, "Maps_OR_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1], ref Utils.MapNamesOR);
            // Maps AS:
            translateMaps(cboMapsAS, "Maps_AS_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1], ref Utils.MapNamesAS);
            // Maps SuMo:
            translateMaps(cboMapsSuMo, "Maps_SuMo_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1], ref Utils.MapNamesSuMo);
            // Maps USUM:
            translateMaps(cboMapsUSUM, "Maps_USUM_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1], ref Utils.MapNamesUSUM);
            // Maps HGSS Safari:
            translateMaps(cboSafariArea, "Maps_HGSS_Safari" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR", "CHS", "CHT" })[Properties.Settings.Default.Language - 1], ref Utils.MapNamesSafariHGSS);


        }

        private static void translateComboBoxes(ComboBox combobox, string resource)
        {
            int value = combobox.SelectedIndex == -1 ? 0 : combobox.SelectedIndex;
            combobox.Items.Clear();
            combobox.Items.AddRange(Properties.Resources.ResourceManager.GetString(resource).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
            combobox.SelectedIndex = value;
        }

        private static void translateMaps(ComboBox combobox, string resource, ref List<string> mapNames)
        {
            int value = -1;
            if (mapNames != null)
                value = combobox.SelectedIndex == -1 ? -1 : mapNames.FindIndex(s => s.Equals(combobox.SelectedItem));
            combobox.Items.Clear();
            mapNames = new List<string>();
            mapNames.AddRange(Properties.Resources.ResourceManager.GetString(resource).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
            combobox.Items.AddRange(Properties.Resources.ResourceManager.GetString(resource).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));

            combobox.SelectedIndex = 0;
            if (value == -1) return;

            if (combobox.Items.Contains(mapNames[value]))
                combobox.SelectedItem = mapNames[value];
        }

        private void chkLuckyPower_CheckedChanged(object sender, EventArgs e)
        {
            // Activate the lucky power combobox by checking this checkbox
            cboLuckyPower.Enabled = chkLuckyPower.Checked;
            changeLuckyPowerPercentage();

        }

        private void loadEncounterOptions(Language l)
        {
            List<string> encounteroptionstrings = new List<string>();
            encounterOptions = new List<List<string>>();
            switch (l)
            {
                case Language.English:
                    encounteroptionstrings.AddRange(Properties.Resources.encounter_options_EN.Split(new[] { "!!" }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                case Language.French:
                    encounteroptionstrings.AddRange(Properties.Resources.encounter_options_FR.Split(new[] { "!!" }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                case Language.ChineseSimplified:
                    encounteroptionstrings.AddRange(Properties.Resources.encounter_options_CHS.Split(new[] { "!!" }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                case Language.ChineseTraditional:
                    encounteroptionstrings.AddRange(Properties.Resources.encounter_options_CHT.Split(new[] { "!!" }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                default:
                    break;
            }
            foreach(string s in encounteroptionstrings)
            {
                List<string> list = new List<string>();
                list.AddRange(s.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
                list.RemoveAll(a => a.StartsWith("!"));  //Removes all comment lines

                encounterOptions.Add(list);
            }
            if(encounterOptions.Count > 0)
                encounterOptions.RemoveAt(0);
        }

        private void updateEncounterOptions()
        {
            int[] comboboxIndexes = new int[12];

            comboboxIndexes[0] = Math.Max(cboSwarmDPPt.SelectedIndex, 0);
            comboboxIndexes[1] = Math.Max(cboSwarmHGSS.SelectedIndex, 0);
            comboboxIndexes[2] = Math.Max(cboSwarmGen5.SelectedIndex, 0);
            comboboxIndexes[3] = Math.Max(cboTimeDPPt.SelectedIndex, 0);
            comboboxIndexes[4] = Math.Max(cboTimeHGSS.SelectedIndex, 0);
            comboboxIndexes[5] = Math.Max(cboRadio.SelectedIndex, 0);
            comboboxIndexes[6] = Math.Max(cboSeason.SelectedIndex, 0);
            comboboxIndexes[7] = Math.Max(cboLuckyPower.SelectedIndex, 0);
            comboboxIndexes[8] = Math.Max(cboGBASlot.SelectedIndex, 0);
            comboboxIndexes[9] = Math.Max(cboSwarmGSC.SelectedIndex, 0);
            comboboxIndexes[10] = Math.Max(cboTimeGSC.SelectedIndex, 0);
            comboboxIndexes[11] = Math.Max(cboDayNightSuMo.SelectedIndex, 0);


            // Refresh comboboxes
            cboSwarmDPPt.Items.Clear();
            cboSwarmHGSS.Items.Clear();
            cboSwarmGen5.Items.Clear();
            cboTimeDPPt.Items.Clear();
            cboTimeHGSS.Items.Clear();
            cboRadio.Items.Clear();
            cboSeason.Items.Clear();
            cboLuckyPower.Items.Clear();
            cboGBASlot.Items.Clear();
            cboSwarmGSC.Items.Clear();
            cboTimeGSC.Items.Clear();
            cboDayNightSuMo.Items.Clear();


            try
            {
                cboSwarmDPPt.Items.AddRange(encounterOptions[2].ToArray());
                cboSwarmHGSS.Items.AddRange(encounterOptions[2].ToArray());
                cboSwarmGen5.Items.AddRange(encounterOptions[2].ToArray());
                cboSwarmGSC.Items.AddRange(encounterOptions[2].ToArray());
                cboTimeDPPt.Items.AddRange(encounterOptions[3].ToArray());
                cboTimeHGSS.Items.AddRange(encounterOptions[3].ToArray());
                cboTimeGSC.Items.AddRange(encounterOptions[3].ToArray());
                cboRadio.Items.AddRange(encounterOptions[4].ToArray());
                cboSeason.Items.AddRange(encounterOptions[5].ToArray());
                cboLuckyPower.Items.AddRange(encounterOptions[6].ToArray());
                cboGBASlot.Items.AddRange(encounterOptions[7].ToArray());
                cboDayNightSuMo.Items.Add(encounterOptions[3][1]);
                cboDayNightSuMo.Items.Add(encounterOptions[3][2]);

                cboSwarmDPPt.SelectedIndex = comboboxIndexes[0];
                cboSwarmHGSS.SelectedIndex = comboboxIndexes[1];
                cboSwarmGen5.SelectedIndex = comboboxIndexes[2];
                cboTimeDPPt.SelectedIndex = comboboxIndexes[3];
                cboTimeHGSS.SelectedIndex = comboboxIndexes[4];
                cboRadio.SelectedIndex = comboboxIndexes[5];
                cboSeason.SelectedIndex = comboboxIndexes[6];
                cboLuckyPower.SelectedIndex = comboboxIndexes[7];
                cboGBASlot.SelectedIndex = comboboxIndexes[8];
                cboSwarmGSC.SelectedIndex = comboboxIndexes[9];
                cboTimeGSC.SelectedIndex = comboboxIndexes[10];
                cboDayNightSuMo.SelectedIndex = comboboxIndexes[11];
            }
            catch (ArgumentOutOfRangeException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        // Forces repel enabling if Hustle / Pressure / Vital Spirit is selected
        private void cboAbility_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!pnlAbility.Visible)
                return;




            if(cboAbility.SelectedItem.Equals(encounterOptions[1][2]) && chkAbility.Checked)
            {
                chkRepel.Enabled = false;
                chkRepel.Checked = true;
            }
            else
            {
                EncounterType type = (EncounterType)encounterOptions[0].FindIndex(s => s.Equals((string)cboEncounterType.SelectedItem));
                chkRepel.Enabled = false;
                switch (type)
                {
                    case EncounterType.Walking:
                    case EncounterType.DarkGrass:
                    case EncounterType.Surf:
                    case EncounterType.RedFlowers:
                    case EncounterType.YellowFlowers:
                    case EncounterType.PurpleFlowers:
                    case EncounterType.TallGrass:
                    case EncounterType.Diving:
                    case EncounterType.ShallowWater:
                        chkRepel.Enabled = true;
                        break;
                    case EncounterType.RockSmash:
                        chkRepel.Enabled = (cboVersion.SelectedIndex < ((int)Version.Diamond - (int)Program.STARTING_VERSION));
                        break;
                    default:
                        chkRepel.Checked = false;
                        break;
                }
            }

            lblIntimidateLevel.Visible = false;
            nudIntimidateLevel.Visible = false;

            if (cboAbility.SelectedItem.Equals(encounterOptions[1][4]) && chkAbility.Checked)
            {
                lblIntimidateLevel.Visible = true;
                nudIntimidateLevel.Visible = true;
                chkRepel.Enabled = false;
                chkRepel.Checked = false;
            }

        }

        // Repel level enable when checked
        private void chkRepel_CheckedChanged(object sender, EventArgs e)
        {
            nudLevelRepel.Enabled = chkRepel.Checked;
            lblLevelRepelDisp.Enabled = chkRepel.Checked;
        }

        private void chkAbility_checkedChanged(object sender, EventArgs e)
        {
            if (!pnlAbility.Visible)
                return;

            cboAbility.Enabled = chkAbility.Checked;
            if (cboAbility.SelectedItem.Equals(encounterOptions[1][2]) && chkAbility.Checked)
            {
                chkRepel.Enabled = false;
                chkRepel.Checked = true;
            }
            else
            {
                EncounterType type = (EncounterType)encounterOptions[0].FindIndex(s => s.Equals((string)cboEncounterType.SelectedItem));
                chkRepel.Enabled = false;
                switch (type)
                {
                    case EncounterType.Walking:
                    case EncounterType.DarkGrass:
                    case EncounterType.Surf:
                    case EncounterType.RedFlowers:
                    case EncounterType.YellowFlowers:
                    case EncounterType.PurpleFlowers:
                    case EncounterType.TallGrass:
                    case EncounterType.Diving:
                    case EncounterType.ShallowWater:
                        chkRepel.Enabled = true;
                        break;
                    case EncounterType.RockSmash:
                        chkRepel.Enabled = (cboVersion.SelectedIndex < ((int)Version.Diamond - (int)Program.STARTING_VERSION));
                        break;
                    default:
                        chkRepel.Checked = false;
                        break;
                }
            }

            lblIntimidateLevel.Visible = false;
            nudIntimidateLevel.Visible = false;

            if (cboAbility.SelectedItem.Equals(encounterOptions[1][4]) && chkAbility.Checked)
            {
                lblIntimidateLevel.Visible = true;
                nudIntimidateLevel.Visible = true;
                chkRepel.Enabled = false;
                chkRepel.Checked = false;
            }

        }


        #region loadingEncounterSlots

        private void changeEncounterOptionsUSUM(object sender, EventArgs e)
        {
            changeEncounterOptionsUSUM();
        }

        private void changeEncounterOptionsUSUM()
        {
            if (cboMapsUSUM.SelectedItem == null || cboMapsUSUM.Items.Count == 0)
                return;

            if (!cboMapsUSUM.Visible) return;

            int selectedMap = cboMapsUSUM.SelectedIndex == -1 ? 0 : Utils.MapNamesUSUM.FindIndex(s => s.Equals((string)cboMapsUSUM.SelectedItem));

            int selectedTable = cboTablesSuMo.SelectedIndex;
            cboTablesSuMo.Items.Clear();
            for (int i = 0; i < Utils.MapsUltraSun[selectedMap].NumberTables; i++)
                cboTablesSuMo.Items.Add("Table " + i);
            if (cboTablesSuMo.Items.Count > selectedTable && selectedTable != -1) cboTablesSuMo.SelectedIndex = selectedTable;
            else cboTablesSuMo.SelectedIndex = 0;
        }

        private void changeEncounterOptionsSuMo(object sender, EventArgs e)
        {
            changeEncounterOptionsSuMo();
        }

        private void changeEncounterOptionsSuMo()
        {
            if (cboMapsSuMo.SelectedItem == null || cboMapsSuMo.Items.Count == 0)
                return;

            if (!cboMapsSuMo.Visible) return;

            int selectedMap = cboMapsSuMo.SelectedIndex == -1 ? 0 : Utils.MapNamesSuMo.FindIndex(s => s.Equals((string)cboMapsSuMo.SelectedItem));

            int selectedTable = cboTablesSuMo.SelectedIndex;
            cboTablesSuMo.Items.Clear();
            for (int i = 0; i < Utils.MapsSun[selectedMap].NumberTables; i++)
                cboTablesSuMo.Items.Add("Table " + i);
            if (cboTablesSuMo.Items.Count > selectedTable && selectedTable != -1) cboTablesSuMo.SelectedIndex = selectedTable;
            else cboTablesSuMo.SelectedIndex = 0;
        }


        private void changeEncounterOptionsAS(object sender, EventArgs e)
        {
            changeEncounterOptionsAS();
        }

        private void changeEncounterOptionsAS()
        {
            if (cboMapsAS.SelectedItem == null || cboMapsAS.Items.Count == 0)
                return;

            if (!cboMapsAS.Visible) return;

            string encounterType = cboEncounterType.SelectedItem == null ? "" : (string)cboEncounterType.SelectedItem;
            int selectedMap = cboMapsAS.SelectedIndex == -1 ? 0 : Utils.MapNamesAS.FindIndex(s => s.Equals((string)cboMapsAS.SelectedItem));

            if (new[] { 65, 66 }.Contains(selectedMap)) lblHelpRoute120.Visible = true;
            else lblHelpRoute120.Visible = false;
            

            cboEncounterType.Items.Clear();

            if (Utils.MapsAlphaSapphire[selectedMap].isExistingEncounterType(EncounterType.Walking))
                cboEncounterType.Items.Add(encounterOptions[0][0]);
            if (Utils.MapsAlphaSapphire[selectedMap].isExistingEncounterType(EncounterType.TallGrass))
                cboEncounterType.Items.Add(encounterOptions[0][10]);
            if (Utils.MapsAlphaSapphire[selectedMap].isExistingEncounterType(EncounterType.Diving))
                cboEncounterType.Items.Add(encounterOptions[0][15]);
            if (Utils.MapsAlphaSapphire[selectedMap].isExistingEncounterType(EncounterType.Surf))
                cboEncounterType.Items.Add(encounterOptions[0][1]);
            if (Utils.MapsAlphaSapphire[selectedMap].isExistingEncounterType(EncounterType.RockSmash))
                cboEncounterType.Items.Add(encounterOptions[0][2]);
            if (Utils.MapsAlphaSapphire[selectedMap].isExistingEncounterType(EncounterType.OldRod))
                cboEncounterType.Items.Add(encounterOptions[0][3]);
            if (Utils.MapsAlphaSapphire[selectedMap].isExistingEncounterType(EncounterType.GoodRod))
                cboEncounterType.Items.Add(encounterOptions[0][4]);
            if (Utils.MapsAlphaSapphire[selectedMap].isExistingEncounterType(EncounterType.SuperRod))
                cboEncounterType.Items.Add(encounterOptions[0][5]);

            if (cboEncounterType.Items.Contains(encounterType))
            {
                cboEncounterType.SelectedItem = encounterType;
            }
            else
            {
                cboEncounterType.SelectedIndex = 0;
            }
        }

        private void changEncounterOptionsOR(object sender, EventArgs e)
        {
            changeEncounterOptionsOR();
        }

        private void changeEncounterOptionsOR()
        {
            if (cboMapsOR.SelectedItem == null || cboMapsOR.Items.Count == 0)
                return;

            if (!cboMapsOR.Visible) return;

            string encounterType = cboEncounterType.SelectedItem == null ? "" : (string)cboEncounterType.SelectedItem;
            int selectedMap = cboMapsOR.SelectedIndex == -1 ? 0 : Utils.MapNamesOR.FindIndex(s => s.Equals((string)cboMapsOR.SelectedItem));

            if (new[] { 65, 66 }.Contains(selectedMap)) lblHelpRoute120.Visible = true;
            else lblHelpRoute120.Visible = false;

            cboEncounterType.Items.Clear();

            if (Utils.MapsOmegaRuby[selectedMap].isExistingEncounterType(EncounterType.Walking))
                cboEncounterType.Items.Add(encounterOptions[0][0]);
            if (Utils.MapsOmegaRuby[selectedMap].isExistingEncounterType(EncounterType.TallGrass))
                cboEncounterType.Items.Add(encounterOptions[0][10]);
            if (Utils.MapsOmegaRuby[selectedMap].isExistingEncounterType(EncounterType.Diving))
                cboEncounterType.Items.Add(encounterOptions[0][15]);
            if (Utils.MapsOmegaRuby[selectedMap].isExistingEncounterType(EncounterType.Surf))
                cboEncounterType.Items.Add(encounterOptions[0][1]);
            if (Utils.MapsOmegaRuby[selectedMap].isExistingEncounterType(EncounterType.RockSmash))
                cboEncounterType.Items.Add(encounterOptions[0][2]);
            if (Utils.MapsOmegaRuby[selectedMap].isExistingEncounterType(EncounterType.OldRod))
                cboEncounterType.Items.Add(encounterOptions[0][3]);
            if (Utils.MapsOmegaRuby[selectedMap].isExistingEncounterType(EncounterType.GoodRod))
                cboEncounterType.Items.Add(encounterOptions[0][4]);
            if (Utils.MapsOmegaRuby[selectedMap].isExistingEncounterType(EncounterType.SuperRod))
                cboEncounterType.Items.Add(encounterOptions[0][5]);

            if (cboEncounterType.Items.Contains(encounterType))
            {
                cboEncounterType.SelectedItem = encounterType;
            }
            else
            {
                cboEncounterType.SelectedIndex = 0;
            }
        }

        private void changeEncounterOptionsXY(object sender, EventArgs e)
        {
            changeEncounterOptionsXY();
        }

        private void changeEncounterOptionsXY()
        {
            if (cboMapsXY.SelectedItem == null || cboMapsXY.Items.Count == 0)
                return;

            if (!cboMapsXY.Visible) return;

            string encounterType = cboEncounterType.SelectedItem == null ? "" : (string)cboEncounterType.SelectedItem;
            int selectedMap = cboMapsXY.SelectedIndex == -1 ? 0 : Utils.MapNamesXY.FindIndex(s => s.Equals((string)cboMapsXY.SelectedItem));


            cboEncounterType.Items.Clear();

            if (Utils.MapsX[selectedMap].isExistingEncounterType(EncounterType.Walking))
                cboEncounterType.Items.Add(encounterOptions[0][0]);
            if (Utils.MapsX[selectedMap].isExistingEncounterType(EncounterType.TallGrass))
                cboEncounterType.Items.Add(encounterOptions[0][10]);
            if (Utils.MapsX[selectedMap].isExistingEncounterType(EncounterType.RedFlowers))
                cboEncounterType.Items.Add(encounterOptions[0][11]);
            if (Utils.MapsX[selectedMap].isExistingEncounterType(EncounterType.YellowFlowers))
                cboEncounterType.Items.Add(encounterOptions[0][12]);
            if (Utils.MapsX[selectedMap].isExistingEncounterType(EncounterType.PurpleFlowers))
                cboEncounterType.Items.Add(encounterOptions[0][13]);
            if (Utils.MapsX[selectedMap].isExistingEncounterType(EncounterType.ShallowWater))
                cboEncounterType.Items.Add(encounterOptions[0][14]);
            if (Utils.MapsX[selectedMap].isExistingEncounterType(EncounterType.Surf))
                cboEncounterType.Items.Add(encounterOptions[0][1]);
            if (Utils.MapsX[selectedMap].isExistingEncounterType(EncounterType.RockSmash))
                cboEncounterType.Items.Add(encounterOptions[0][2]);
            if (Utils.MapsX[selectedMap].isExistingEncounterType(EncounterType.OldRod))
                cboEncounterType.Items.Add(encounterOptions[0][3]);
            if (Utils.MapsX[selectedMap].isExistingEncounterType(EncounterType.GoodRod))
                cboEncounterType.Items.Add(encounterOptions[0][4]);
            if (Utils.MapsX[selectedMap].isExistingEncounterType(EncounterType.SuperRod))
                cboEncounterType.Items.Add(encounterOptions[0][5]);

            if (cboEncounterType.Items.Contains(encounterType))
            {
                cboEncounterType.SelectedItem = encounterType;
            }
            else
            {
                cboEncounterType.SelectedIndex = 0;
            }
        }

        private void changeEncounterOptionsGen5(object sender, EventArgs e)
        {
            Version currentVersion = (Version)((int)Program.STARTING_VERSION + cboVersion.SelectedIndex);
            switch (currentVersion)
            {
                case Version.Black:
                case Version.White:
                    changeEncounterOptionsBW();
                    break;
                case Version.Black2:
                case Version.White2:
                    changeEncounterOptionsB2W2();
                    break;
                default: break;
            }
        }

        private void changeEncounterOptionsB2W2(object sender, EventArgs e)
        {
            changeEncounterOptionsB2W2();
        }

        private void changeEncounterOptionsB2W2()
        {
            if (cboMapsB2W2.SelectedItem == null || cboMapsB2W2.Items.Count == 0)
                return;

            if (!cboMapsB2W2.Visible) return;

            string encounterType = cboEncounterType.SelectedItem == null ? "" : (string)cboEncounterType.SelectedItem;
            int selectedMap = cboMapsB2W2.SelectedIndex == -1 ? 0 : Utils.mapTablesB2W2[Utils.MapNamesB2W2.FindIndex(s => s.Equals((string)cboMapsB2W2.SelectedItem))];

            lblSeasonDisp.Enabled = cboSeason.Enabled = false;

            // season check
            if (new[] { 2, 26, 42, 46, 64, 131, 138, 146, 150, 165 }.Contains(selectedMap))
            {
                lblSeasonDisp.Enabled = true;
                cboSeason.Enabled = true;
                selectedMap += cboSeason.SelectedIndex;
            }


            cboEncounterType.Items.Clear();

            if (Utils.MapsBlack2[selectedMap].isExistingEncounterType(EncounterType.Walking))
                cboEncounterType.Items.Add(encounterOptions[0][0]);
            if (Utils.MapsBlack2[selectedMap].isExistingEncounterType(EncounterType.DarkGrass))
                cboEncounterType.Items.Add(encounterOptions[0][6]);
            if (Utils.MapsBlack2[selectedMap].isExistingEncounterType(EncounterType.ShakingGrass))
                cboEncounterType.Items.Add(encounterOptions[0][7]);
            if (Utils.MapsBlack2[selectedMap].isExistingEncounterType(EncounterType.Surf))
                cboEncounterType.Items.Add(encounterOptions[0][1]);
            if (Utils.MapsBlack2[selectedMap].isExistingEncounterType(EncounterType.RipplingSurf))
                cboEncounterType.Items.Add(encounterOptions[0][8]);
            if (Utils.MapsBlack2[selectedMap].isExistingEncounterType(EncounterType.SuperRod))
                cboEncounterType.Items.Add(encounterOptions[0][5]);
            if (Utils.MapsBlack2[selectedMap].isExistingEncounterType(EncounterType.RipplingFish))
                cboEncounterType.Items.Add(encounterOptions[0][9]);

            if (cboEncounterType.Items.Contains(encounterType))
            {
                cboEncounterType.SelectedItem = encounterType;
            }
            else
            {
                cboEncounterType.SelectedIndex = 0;
            }
        }

        private void changeEncounterOptionsBW(object sender, EventArgs e)
        {
            changeEncounterOptionsBW();
        }

        private void changeEncounterOptionsBW()
        {
            if (cboMapsBW.SelectedItem == null || cboMapsBW.Items.Count == 0)
                return;

            if (!cboMapsBW.Visible) return;

            string encounterType = cboEncounterType.SelectedItem == null ? "" : (string)cboEncounterType.SelectedItem;
            int selectedMap = cboMapsBW.SelectedIndex == -1 ? 0 : Utils.mapTablesBW[Utils.MapNamesBW.FindIndex(s => s.Equals((string)cboMapsBW.SelectedItem))];

            lblSeasonDisp.Enabled = cboSeason.Enabled = false;
            // season check
            if (new[] { 2, 47, 63, 67, 94, 108, 115, 123, 127 }.Contains(selectedMap))
            {
                lblSeasonDisp.Enabled = true;
                cboSeason.Enabled = true;
                selectedMap += cboSeason.SelectedIndex;
            }


            cboEncounterType.Items.Clear();

            if (Utils.MapsBlack[selectedMap].isExistingEncounterType(EncounterType.Walking))
                cboEncounterType.Items.Add(encounterOptions[0][0]);
            if (Utils.MapsBlack[selectedMap].isExistingEncounterType(EncounterType.DarkGrass))
                cboEncounterType.Items.Add(encounterOptions[0][6]);
            if (Utils.MapsBlack[selectedMap].isExistingEncounterType(EncounterType.ShakingGrass))
                cboEncounterType.Items.Add(encounterOptions[0][7]);
            if (Utils.MapsBlack[selectedMap].isExistingEncounterType(EncounterType.Surf))
                cboEncounterType.Items.Add(encounterOptions[0][1]);
            if (Utils.MapsBlack[selectedMap].isExistingEncounterType(EncounterType.RipplingSurf))
                cboEncounterType.Items.Add(encounterOptions[0][8]);
            if (Utils.MapsBlack[selectedMap].isExistingEncounterType(EncounterType.SuperRod))
                cboEncounterType.Items.Add(encounterOptions[0][5]);
            if (Utils.MapsBlack[selectedMap].isExistingEncounterType(EncounterType.RipplingFish))
                cboEncounterType.Items.Add(encounterOptions[0][9]);

            if (cboEncounterType.Items.Contains(encounterType))
            {
                cboEncounterType.SelectedItem = encounterType;
            }
            else
            {
                cboEncounterType.SelectedIndex = 0;
            }
        }

        private void changeEncounterOptionsHGSS(object sender, EventArgs e)
        {
            changeEncounterOptionsHGSS();
        }

        private void changeSpecialOptionsHGSS(int map)
        {
            foreach (Control c in pnlHGSSOptions.Controls)
                c.Enabled = false;

            //Special map: Safari Zone
            if (map == -1)
            {
                lblTimeHGSSDisp.Enabled = true;
                cboTimeHGSS.Enabled = true;
                return;
            }
            

            if ((string)(cboEncounterType.SelectedItem) == encounterOptions[0][0])
            {
                if (Utils.MapsHeartGold[map].isThereSwarm())
                {
                    lblSwarmHGSSDisp.Enabled = true;
                    cboSwarmHGSS.Enabled = true;
                }

                if (Utils.MapsHeartGold[map].isThereTimeOfDay())
                {
                    lblTimeHGSSDisp.Enabled = true;
                    cboTimeHGSS.Enabled = true;
                }

                if (Utils.MapsHeartGold[map].isThereRadio())
                {
                    lblRadioDisp.Enabled = true;
                    cboRadio.Enabled = true;
                }
            }

            if((string)(cboEncounterType.SelectedItem) == encounterOptions[0][1])
            {
                if (Utils.MapsHeartGold[map].isThereSurfSwarm())
                {
                    lblSwarmHGSSDisp.Enabled = true;
                    cboSwarmHGSS.Enabled = true;
                }
            }

            if ((string)(cboEncounterType.SelectedItem) == encounterOptions[0][3])
            {
                if (Utils.MapsHeartGold[map].isThereFishSwarm())
                {
                    lblSwarmHGSSDisp.Enabled = true;
                    cboSwarmHGSS.Enabled = true;
                }
            }

            if ((string)(cboEncounterType.SelectedItem) == encounterOptions[0][4] || (string)(cboEncounterType.SelectedItem) == encounterOptions[0][5])
            {
                if (Utils.MapsHeartGold[map].isThereFishSwarm())
                {
                    lblSwarmHGSSDisp.Enabled = true;
                    cboSwarmHGSS.Enabled = true;
                }
                if (Utils.MapsHeartGold[map].isThereFishNight())
                {
                    lblTimeHGSSDisp.Enabled = true;
                    cboTimeHGSS.Enabled = true;
                }
            }






        }

        private void changeEncounterOptionsHGSS()
        {
            if (cboMapsHGSS.SelectedItem == null || cboMapsHGSS.Items.Count == 0)
                return;

            if (!cboMapsHGSS.Visible) return;

            string encounterType = cboEncounterType.SelectedItem == null ? "" : (string)cboEncounterType.SelectedItem;

            int selectedMap;

            if (cboMapsHGSS.SelectedIndex == -1) selectedMap = 0; 
            else
            {
                selectedMap = Utils.MapNamesHGSS.FindIndex(s => s.Equals((string)cboMapsHGSS.SelectedItem));
                selectedMap = (selectedMap == 117) ? -1 : selectedMap = Utils.mapTablesHGSS[selectedMap];
                   
            }

            cboEncounterType.Items.Clear();

            if (selectedMap == -1)
            {
                pnlHGSSSafari.Visible = true;
                cboEncounterType.Items.Add(encounterOptions[0][0]);
                int area = cboSafariArea.SelectedIndex;
                if (Utils.MapsSafariHGSS[area].isThereWater())
                {
                    cboEncounterType.Items.Add(encounterOptions[0][1]);
                    cboEncounterType.Items.Add(encounterOptions[0][3]);
                    cboEncounterType.Items.Add(encounterOptions[0][4]);
                    cboEncounterType.Items.Add(encounterOptions[0][5]);
                }
                
            }
            else
            {
                pnlHGSSSafari.Visible = false;
                if (Utils.MapsHeartGold[selectedMap].isExistingEncounterType(EncounterType.Walking))
                    cboEncounterType.Items.Add(encounterOptions[0][0]);
                if (Utils.MapsHeartGold[selectedMap].isExistingEncounterType(EncounterType.Surf))
                    cboEncounterType.Items.Add(encounterOptions[0][1]);
                if (Utils.MapsHeartGold[selectedMap].isExistingEncounterType(EncounterType.RockSmash))
                    cboEncounterType.Items.Add(encounterOptions[0][2]);
                if (Utils.MapsHeartGold[selectedMap].isExistingEncounterType(EncounterType.OldRod))
                    cboEncounterType.Items.Add(encounterOptions[0][3]);
                if (Utils.MapsHeartGold[selectedMap].isExistingEncounterType(EncounterType.GoodRod))
                    cboEncounterType.Items.Add(encounterOptions[0][4]);
                if (Utils.MapsHeartGold[selectedMap].isExistingEncounterType(EncounterType.SuperRod))
                    cboEncounterType.Items.Add(encounterOptions[0][5]);
            }
            if (cboEncounterType.Items.Contains(encounterType))
            {
                cboEncounterType.SelectedItem = encounterType;
            }
            else
            {
                cboEncounterType.SelectedIndex = 0;
            }
        }

        private void changeEncounterOptionsPlat(object sender, EventArgs e)
        {
            changeEncounterOptionsPlat();
        }

        private void changeSpecialOptionsPlat(int map)
        {
            // change swarm, time, gba slot, pokeradar

            foreach (Control c in pnlDPPtOptions.Controls)
                c.Enabled = false;

            if (Utils.MapsPlatinum[map].isThereSwarm())
            {
                lblSwarmDPPtDisp.Enabled = true;
                cboSwarmDPPt.Enabled = true;
            }
            if (Utils.MapsPlatinum[map].isThereTimeOfDay())
            {
                lblTimeDPPtDisp.Enabled = true;
                cboTimeDPPt.Enabled = true;
            }
            if (Utils.MapsPlatinum[map].isThereGBASlot())
            {
                lblGBASlotDisp.Enabled = true;
                cboGBASlot.Enabled = true;
            }
            if (Utils.MapsPlatinum[map].isTherePokeRadar())
                chkRadarDPPt.Enabled = true;

            if (!((string)(cboEncounterType.SelectedItem) == encounterOptions[0][0]))
                foreach (Control c in pnlDPPtOptions.Controls)
                    c.Enabled = false;
        }

        private void changeEncounterOptionsPlat()
        {
            if (cboMapsPlat.SelectedItem == null || cboMapsPlat.Items.Count == 0)
                return;

            if (!cboMapsPlat.Visible) return;

            string encounterType = cboEncounterType.SelectedItem == null ? "" : (string)cboEncounterType.SelectedItem;
            int selectedMap = cboMapsPlat.SelectedIndex == -1 ? 0 : Utils.mapTablesPlat[Utils.MapNamesPt.FindIndex(s => s.Equals((string)cboMapsPlat.SelectedItem))];


            if (new[] { 69,70 }.Contains(selectedMap)) lblHelpTurnback.Visible = true;
            else lblHelpTurnback.Visible = false;
            //
            cboEncounterType.Items.Clear();

            if (Utils.MapsPlatinum[selectedMap].isExistingEncounterType(EncounterType.Walking))
                cboEncounterType.Items.Add(encounterOptions[0][0]);
            if (Utils.MapsPlatinum[selectedMap].isExistingEncounterType(EncounterType.Surf))
                cboEncounterType.Items.Add(encounterOptions[0][1]);
            if (Utils.MapsPlatinum[selectedMap].isExistingEncounterType(EncounterType.OldRod))
                cboEncounterType.Items.Add(encounterOptions[0][3]);
            if (Utils.MapsPlatinum[selectedMap].isExistingEncounterType(EncounterType.GoodRod))
                cboEncounterType.Items.Add(encounterOptions[0][4]);
            if (Utils.MapsPlatinum[selectedMap].isExistingEncounterType(EncounterType.SuperRod))
                cboEncounterType.Items.Add(encounterOptions[0][5]);

            if (cboEncounterType.Items.Contains(encounterType))
            {
                cboEncounterType.SelectedItem = encounterType;
            }
            else
            {
                cboEncounterType.SelectedIndex = 0;
            }
        }

        private void changeEncounterOptionsDP(object sender, EventArgs e)
        {
            changeEncounterOptionsDP();
        }

        private void changeSpecialOptionsDP(int map)
        {
            foreach (Control c in pnlDPPtOptions.Controls)
                c.Enabled = false;

            if (Utils.MapsDiamond[map].isThereSwarm())
            {
                lblSwarmDPPtDisp.Enabled = true;
                cboSwarmDPPt.Enabled = true;
            }
            if (Utils.MapsDiamond[map].isThereTimeOfDay())
            {
                lblTimeDPPtDisp.Enabled = true;
                cboTimeDPPt.Enabled = true;
            }
            if (Utils.MapsDiamond[map].isThereGBASlot())
            {
                lblGBASlotDisp.Enabled = true;
                cboGBASlot.Enabled = true;
            }
            if (Utils.MapsDiamond[map].isTherePokeRadar())
                chkRadarDPPt.Enabled = true;

            if (!((string)(cboEncounterType.SelectedItem) == encounterOptions[0][0]))
                foreach (Control c in pnlDPPtOptions.Controls)
                    c.Enabled = false;
        }

        private void changeEncounterOptionsDP()
        {
            if (cboMapsDP.SelectedItem == null || cboMapsDP.Items.Count == 0)
                return;

            if (!cboMapsDP.Visible) return;

            string encounterType = cboEncounterType.SelectedItem == null ? "" : (string)cboEncounterType.SelectedItem;
            int selectedMap = cboMapsDP.SelectedIndex == -1 ? 0 : Utils.mapTablesDP[Utils.MapNamesDP.FindIndex(s => s.Equals((string)cboMapsDP.SelectedItem))];
            


            cboEncounterType.Items.Clear();

            if (Utils.MapsDiamond[selectedMap].isExistingEncounterType(EncounterType.Walking))
                cboEncounterType.Items.Add(encounterOptions[0][0]);
            if (Utils.MapsDiamond[selectedMap].isExistingEncounterType(EncounterType.Surf))
                cboEncounterType.Items.Add(encounterOptions[0][1]);
            if (Utils.MapsDiamond[selectedMap].isExistingEncounterType(EncounterType.OldRod))
                cboEncounterType.Items.Add(encounterOptions[0][3]);
            if (Utils.MapsDiamond[selectedMap].isExistingEncounterType(EncounterType.GoodRod))
                cboEncounterType.Items.Add(encounterOptions[0][4]);
            if (Utils.MapsDiamond[selectedMap].isExistingEncounterType(EncounterType.SuperRod))
                cboEncounterType.Items.Add(encounterOptions[0][5]);

            if (cboEncounterType.Items.Contains(encounterType))
            {
                cboEncounterType.SelectedItem = encounterType;
            }
            else
            {
                cboEncounterType.SelectedIndex = 0;
            }

        }

        private void changeEncounterOptionsFireLeaf(object sender, EventArgs e)
        {
            changeEncounterOptionsFireLeaf();
        }

        private void changeEncounterOptionsFireLeaf()
        {
            if (cboMapsFireLeaf.SelectedItem == null || cboMapsFireLeaf.Items.Count == 0)
                return;

            if (!cboMapsFireLeaf.Visible) return;

            string encounterType = cboEncounterType.SelectedItem == null ? "" : (string)cboEncounterType.SelectedItem;
            int selectedMap = cboMapsFireLeaf.SelectedIndex == -1 ? 0 : Utils.MapNamesFRLG.FindIndex(s => s.Equals((string)cboMapsFireLeaf.SelectedItem));

            cboEncounterType.Items.Clear();

            if (Utils.MapsFireRed[selectedMap].isExistingEncounterType(EncounterType.Walking))
                cboEncounterType.Items.Add(encounterOptions[0][0]);
            if (Utils.MapsFireRed[selectedMap].isExistingEncounterType(EncounterType.Surf))
                cboEncounterType.Items.Add(encounterOptions[0][1]);
            if (Utils.MapsFireRed[selectedMap].isExistingEncounterType(EncounterType.RockSmash))
                cboEncounterType.Items.Add(encounterOptions[0][2]);
            if (Utils.MapsFireRed[selectedMap].isExistingEncounterType(EncounterType.OldRod))
                cboEncounterType.Items.Add(encounterOptions[0][3]);
            if (Utils.MapsFireRed[selectedMap].isExistingEncounterType(EncounterType.GoodRod))
                cboEncounterType.Items.Add(encounterOptions[0][4]);
            if (Utils.MapsFireRed[selectedMap].isExistingEncounterType(EncounterType.SuperRod))
                cboEncounterType.Items.Add(encounterOptions[0][5]);

            if (cboEncounterType.Items.Contains(encounterType))
            {
                cboEncounterType.SelectedItem = encounterType;
            }
            else
            {
                cboEncounterType.SelectedIndex = 0;
            }
        }

        private void changeEncounterOptionsEmerald(object sender, EventArgs e)
        {
            changeEncounterOptionsEmerald();
        }

        private void changeEncounterOptionsEmerald()
        {
            if (cboMapsEmer.SelectedItem == null || cboMapsEmer.Items.Count == 0)
                return;

            if (!cboMapsEmer.Visible) return;

            string encounterType = cboEncounterType.SelectedItem == null ? "" : (string)cboEncounterType.SelectedItem;
            int selectedMap = cboMapsEmer.SelectedIndex == -1 ? 0 : Utils.MapNamesE.FindIndex(s => s.Equals((string)cboMapsEmer.SelectedItem));

            cboEncounterType.Items.Clear();

            if (Utils.MapsEmerald[selectedMap].isExistingEncounterType(EncounterType.Walking))
                cboEncounterType.Items.Add(encounterOptions[0][0]);
            if (Utils.MapsEmerald[selectedMap].isExistingEncounterType(EncounterType.Surf))
                cboEncounterType.Items.Add(encounterOptions[0][1]);
            if (Utils.MapsEmerald[selectedMap].isExistingEncounterType(EncounterType.RockSmash))
                cboEncounterType.Items.Add(encounterOptions[0][2]);
            if (Utils.MapsEmerald[selectedMap].isExistingEncounterType(EncounterType.OldRod))
                cboEncounterType.Items.Add(encounterOptions[0][3]);
            if (Utils.MapsEmerald[selectedMap].isExistingEncounterType(EncounterType.GoodRod))
                cboEncounterType.Items.Add(encounterOptions[0][4]);
            if (Utils.MapsEmerald[selectedMap].isExistingEncounterType(EncounterType.SuperRod))
                cboEncounterType.Items.Add(encounterOptions[0][5]);

            if (cboEncounterType.Items.Contains(encounterType))
            {
                cboEncounterType.SelectedItem = encounterType;
            }
            else
            {
                cboEncounterType.SelectedIndex = 0;
            }
        }

        private void changeEncounterOptionsRubySapp(object sender, EventArgs e) {
            changeEncounterOptionsRubySapp();
        }

        private void changeEncounterOptionsRubySapp()
        {
            if (cboMapsRubySapp.SelectedItem == null || cboMapsRubySapp.Items.Count == 0)
                return;

            if (!cboMapsRubySapp.Visible) return;

            string encounterType = cboEncounterType.SelectedItem == null ? "" : (string)cboEncounterType.SelectedItem;
            int selectedMap = cboMapsRubySapp.SelectedIndex == -1 ? 0 : Utils.MapNamesRS.FindIndex(s => s.Equals((string)cboMapsRubySapp.SelectedItem));

            cboEncounterType.Items.Clear();

            if (Utils.MapsRuby[selectedMap].isExistingEncounterType(EncounterType.Walking))
                cboEncounterType.Items.Add(encounterOptions[0][0]);
            if (Utils.MapsRuby[selectedMap].isExistingEncounterType(EncounterType.Surf))
                cboEncounterType.Items.Add(encounterOptions[0][1]);
            if (Utils.MapsRuby[selectedMap].isExistingEncounterType(EncounterType.RockSmash))
                cboEncounterType.Items.Add(encounterOptions[0][2]);
            if (Utils.MapsRuby[selectedMap].isExistingEncounterType(EncounterType.OldRod))
                cboEncounterType.Items.Add(encounterOptions[0][3]);
            if (Utils.MapsRuby[selectedMap].isExistingEncounterType(EncounterType.GoodRod))
                cboEncounterType.Items.Add(encounterOptions[0][4]);
            if (Utils.MapsRuby[selectedMap].isExistingEncounterType(EncounterType.SuperRod))
                cboEncounterType.Items.Add(encounterOptions[0][5]);

            if (cboEncounterType.Items.Contains(encounterType))
            {
                cboEncounterType.SelectedItem = encounterType;
            }
            else
            {
                cboEncounterType.SelectedIndex = 0;
            }
                

        }

        private void changeEncounterOptionsCrystal(object sender, EventArgs e)
        {
            changeEncounterOptionsCrystal();
        }

        private void changeSpecialOptionsCrystal(int map)
        {
            // change swarm, time

            foreach (Control c in pnlOptionsGen2.Controls)
                c.Enabled = false;

            

            if ((string)(cboEncounterType.SelectedItem) == encounterOptions[0][0])
            {
                if (Utils.MapsCrystal[map].isThereWalkSwarm())
                    cboSwarmGSC.Enabled = lblSwarmDispGSC.Enabled = true;

                if (Utils.MapsCrystal[map].isThereWalkTime())
                    cboTimeGSC.Enabled = lblTimeDispGSC.Enabled = true;

                if (Utils.MapsCrystal[map].isThereBugContest())
                    chkBugCatching.Enabled = true;
            }
            if(!chkBugCatching.Enabled) chkBugCatching.Checked = false;

            if ((string)(cboEncounterType.SelectedItem) == encounterOptions[0][1])
            {
                if (Utils.MapsCrystal[map].isThereSurfSwarm())
                    cboSwarmGSC.Enabled = lblSwarmDispGSC.Enabled = true;
            }

            if ((string)(cboEncounterType.SelectedItem) == encounterOptions[0][3])
            {
                if (Utils.MapsCrystal[map].isThereFishSwarm())
                    cboSwarmGSC.Enabled = lblSwarmDispGSC.Enabled = true;
            }

            if ((string)(cboEncounterType.SelectedItem) == encounterOptions[0][4] || (string)(cboEncounterType.SelectedItem) == encounterOptions[0][5])
            {
                if (Utils.MapsCrystal[map].isThereFishSwarm())
                    cboSwarmGSC.Enabled = lblSwarmDispGSC.Enabled = true;

                if (Utils.MapsCrystal[map].isThereFishTime())
                    cboTimeGSC.Enabled = lblTimeDispGSC.Enabled = true;
            }
        }

        private void changeEncounterOptionsCrystal()
        {
            if (cboMapsCrystal.SelectedItem == null || cboMapsCrystal.Items.Count == 0)
                return;

            if (!cboMapsCrystal.Visible) return;

            string encounterType = cboEncounterType.SelectedItem == null ? "" : (string)cboEncounterType.SelectedItem;
            int selectedMap = cboMapsCrystal.SelectedIndex == -1 ? 0 : Utils.MapNamesC.FindIndex(s => s.Equals((string)cboMapsCrystal.SelectedItem));

            cboEncounterType.Items.Clear();

            if (Utils.MapsCrystal[selectedMap].isExistingEncounterType(EncounterType.Walking))
                cboEncounterType.Items.Add(encounterOptions[0][0]);
            if (Utils.MapsCrystal[selectedMap].isExistingEncounterType(EncounterType.Surf))
                cboEncounterType.Items.Add(encounterOptions[0][1]);
            if (Utils.MapsCrystal[selectedMap].isExistingEncounterType(EncounterType.RockSmash))
                cboEncounterType.Items.Add(encounterOptions[0][2]);
            if (Utils.MapsCrystal[selectedMap].isExistingEncounterType(EncounterType.OldRod))
                cboEncounterType.Items.Add(encounterOptions[0][3]);
            if (Utils.MapsCrystal[selectedMap].isExistingEncounterType(EncounterType.GoodRod))
                cboEncounterType.Items.Add(encounterOptions[0][4]);
            if (Utils.MapsCrystal[selectedMap].isExistingEncounterType(EncounterType.SuperRod))
                cboEncounterType.Items.Add(encounterOptions[0][5]);

            if (cboEncounterType.Items.Contains(encounterType))
            {
                cboEncounterType.SelectedItem = encounterType;
            }
            else
            {
                cboEncounterType.SelectedIndex = 0;
            }


        }

        private void changeEncounterOptionsGoldSilver(object sender, EventArgs e)
        {
            changeEncounterOptionsGoldSilver();
        }

        private void changeSpecialOptionsGoldSilver(int map)
        {
            // change swarm, time

            foreach (Control c in pnlOptionsGen2.Controls)
                c.Enabled = false;

            

            if ((string)(cboEncounterType.SelectedItem) == encounterOptions[0][0])
            {
                if (Utils.MapsGold[map].isThereWalkSwarm())
                    cboSwarmGSC.Enabled = lblSwarmDispGSC.Enabled = true;

                if (Utils.MapsGold[map].isThereWalkTime())
                    cboTimeGSC.Enabled = lblTimeDispGSC.Enabled = true;

                if (Utils.MapsGold[map].isThereBugContest())
                    chkBugCatching.Enabled = true;
            }
            if(!chkBugCatching.Enabled) chkBugCatching.Checked = false;

            if ((string)(cboEncounterType.SelectedItem) == encounterOptions[0][1])
            {
                if (Utils.MapsGold[map].isThereSurfSwarm())
                    cboSwarmGSC.Enabled = lblSwarmDispGSC.Enabled = true;
            }

            if ((string)(cboEncounterType.SelectedItem) == encounterOptions[0][3])
            {
                if (Utils.MapsGold[map].isThereFishSwarm())
                    cboSwarmGSC.Enabled = lblSwarmDispGSC.Enabled = true;
            }

            if ((string)(cboEncounterType.SelectedItem) == encounterOptions[0][4] || (string)(cboEncounterType.SelectedItem) == encounterOptions[0][5])
            {
                if (Utils.MapsGold[map].isThereFishSwarm())
                    cboSwarmGSC.Enabled = lblSwarmDispGSC.Enabled = true;

                if (Utils.MapsGold[map].isThereFishTime())
                    cboTimeGSC.Enabled = lblTimeDispGSC.Enabled = true;
            }
        }

        private void changeEncounterOptionsGoldSilver()
        {
            if (cboMapsGS.SelectedItem == null || cboMapsGS.Items.Count == 0)
                return;

            if (!cboMapsGS.Visible) return;

            string encounterType = cboEncounterType.SelectedItem == null ? "" : (string)cboEncounterType.SelectedItem;
            int selectedMap = cboMapsGS.SelectedIndex == -1 ? 0 : Utils.MapNamesGS.FindIndex(s => s.Equals((string)cboMapsGS.SelectedItem));

            cboEncounterType.Items.Clear();

            if (Utils.MapsGold[selectedMap].isExistingEncounterType(EncounterType.Walking))
                cboEncounterType.Items.Add(encounterOptions[0][0]);
            if (Utils.MapsGold[selectedMap].isExistingEncounterType(EncounterType.Surf))
                cboEncounterType.Items.Add(encounterOptions[0][1]);
            if (Utils.MapsGold[selectedMap].isExistingEncounterType(EncounterType.RockSmash))
                cboEncounterType.Items.Add(encounterOptions[0][2]);
            if (Utils.MapsGold[selectedMap].isExistingEncounterType(EncounterType.OldRod))
                cboEncounterType.Items.Add(encounterOptions[0][3]);
            if (Utils.MapsGold[selectedMap].isExistingEncounterType(EncounterType.GoodRod))
                cboEncounterType.Items.Add(encounterOptions[0][4]);
            if (Utils.MapsGold[selectedMap].isExistingEncounterType(EncounterType.SuperRod))
                cboEncounterType.Items.Add(encounterOptions[0][5]);

            if (cboEncounterType.Items.Contains(encounterType))
            {
                cboEncounterType.SelectedItem = encounterType;
            }
            else
            {
                cboEncounterType.SelectedIndex = 0;
            }


        }


        void loadSlotData(object sender, EventArgs e)
        {
            loadSlotData();
        }

        void loadSlotData()
        {
            int currentMap;
            EncounterSlot[] newSlots = null;
            EncounterType type = (EncounterType)encounterOptions[0].FindIndex(s => s.Equals((string)cboEncounterType.SelectedItem));
            Version currentVersion = (Version)((int)Program.STARTING_VERSION + cboVersion.SelectedIndex);
            int gba = 0, time = 0, radio = 0;
            int selectedTableSuMo = cboTablesSuMo.SelectedIndex;
            chkRepel.Enabled = false;

            Ability selectedAbility = (Ability)(encounterOptions[1].FindIndex(s=> s==(string)(cboAbility.SelectedItem)) + 1);
            if (selectedAbility == Ability.None) selectedAbility = Ability.Static;

            cboAbility.Items.Clear();
            cboAbility.Items.Add(encounterOptions[1][0]);
            cboAbility.Items.Add(encounterOptions[1][1]);

            if (currentVersion >= Version.Sun)
            {
                chkRepel.Checked = false; // No repel (the whole table is affected by the same levels)

            }
            else
                switch (type)
                {
                    case EncounterType.Walking:
                    case EncounterType.DarkGrass:
                    case EncounterType.Surf:
                    case EncounterType.RedFlowers:
                    case EncounterType.YellowFlowers:
                    case EncounterType.PurpleFlowers:
                    case EncounterType.TallGrass:
                    case EncounterType.Diving:
                    case EncounterType.ShallowWater:
                        cboAbility.Items.Add(encounterOptions[1][2]);
                        cboAbility.Items.Add(encounterOptions[1][4]);
                        chkRepel.Enabled = true;
                        break;
                    case EncounterType.RockSmash:
                        cboAbility.Items.Add(encounterOptions[1][4]);
                        chkRepel.Enabled = (cboVersion.SelectedIndex < ((int)Version.Diamond - (int)Program.STARTING_VERSION));
                        chkRepel.Checked &= chkRepel.Enabled; //Force uncheck if the checkbox is disabled.
                        break;
                    case EncounterType.OldRod:
                    case EncounterType.GoodRod:
                    case EncounterType.SuperRod:
                        if (currentVersion < Version.X)  // Keen-Eye / Intimidate doesn't work on Gen6 due to fishing mechanics
                            cboAbility.Items.Add(encounterOptions[1][4]);

                        chkRepel.Checked = false;
                        break;
                    default:
                        chkRepel.Checked = false;
                        break;
                }

            switch (currentVersion)
            {
                case Version.Gold:
                    currentMap = Utils.MapNamesGS.FindIndex(s => s.Equals((string)cboMapsGS.SelectedItem));
                    changeSpecialOptionsGoldSilver(currentMap);
                    if (chkBugCatching.Checked) newSlots = Utils.MapsGold[currentMap].getBugContestSlots();
                    else newSlots = Utils.MapsGold[currentMap].getSlots(type, cboSwarmGSC.Enabled && cboSwarmGSC.SelectedIndex == 1, cboTimeGSC.Enabled ? cboTimeGSC.SelectedIndex : 0);
                    break;

                case Version.Silver:
                    currentMap = Utils.MapNamesGS.FindIndex(s => s.Equals((string)cboMapsGS.SelectedItem));
                    changeSpecialOptionsGoldSilver(currentMap);
                    if (chkBugCatching.Checked) newSlots = Utils.MapsSilver[currentMap].getBugContestSlots();
                    else newSlots = Utils.MapsSilver[currentMap].getSlots(type, cboSwarmGSC.Enabled && cboSwarmGSC.SelectedIndex == 1, cboTimeGSC.Enabled ? cboTimeGSC.SelectedIndex : 0);
                    break;

                case Version.Crystal:
                    currentMap = Utils.MapNamesC.FindIndex(s => s.Equals((string)cboMapsCrystal.SelectedItem));
                    changeSpecialOptionsCrystal(currentMap);
                    if (chkBugCatching.Checked) newSlots = Utils.MapsCrystal[currentMap].getBugContestSlots();
                    else newSlots = Utils.MapsCrystal[currentMap].getSlots(type, cboSwarmGSC.Enabled && cboSwarmGSC.SelectedIndex == 1, cboTimeGSC.Enabled ? cboTimeGSC.SelectedIndex : 0);
                    break;

                case Version.Ruby:
                    currentMap = Utils.MapNamesRS.FindIndex(s => s.Equals((string)cboMapsRubySapp.SelectedItem));
                    newSlots = Utils.MapsRuby[currentMap].getSlots(type);
                    break;

                case Version.Sapphire:
                    currentMap = Utils.MapNamesRS.FindIndex(s => s.Equals((string)cboMapsRubySapp.SelectedItem));
                    newSlots = Utils.MapsSapphire[currentMap].getSlots(type);
                    break;

                case Version.Emerald:
                    currentMap = Utils.MapNamesE.FindIndex(s => s.Equals((string)cboMapsEmer.SelectedItem));
                    newSlots = Utils.MapsEmerald[currentMap].getSlots(type);
                    break;
                case Version.FireRed:
                    currentMap = Utils.MapNamesFRLG.FindIndex(s => s.Equals((string)cboMapsFireLeaf.SelectedItem));
                    newSlots = Utils.MapsFireRed[currentMap].getSlots(type);
                    break;
                case Version.LeafGreen:
                    currentMap = Utils.MapNamesFRLG.FindIndex(s => s.Equals((string)cboMapsFireLeaf.SelectedItem));
                    newSlots = Utils.MapsLeafGreen[currentMap].getSlots(type);
                    break;
                case Version.Diamond:
                    cboAbility.Items.Add(encounterOptions[1][3]);
                    currentMap = Utils.mapTablesDP[Utils.MapNamesDP.FindIndex(s => s.Equals((string)cboMapsDP.SelectedItem))];
                    changeSpecialOptionsDP(currentMap);
                    gba = cboGBASlot.Enabled ? cboGBASlot.SelectedIndex : 0;
                    time = cboTimeDPPt.Enabled ? cboTimeDPPt.SelectedIndex : 0;
                    newSlots = Utils.MapsDiamond[currentMap].getSlots(type, (cboSwarmDPPt.Enabled && (cboSwarmDPPt.SelectedIndex == 1)), time, gba, chkRadarDPPt.Checked && chkRadarDPPt.Enabled);
                    break;
                case Version.Pearl:
                    cboAbility.Items.Add(encounterOptions[1][3]);
                    currentMap = Utils.mapTablesDP[Utils.MapNamesDP.FindIndex(s => s.Equals((string)cboMapsDP.SelectedItem))];
                    changeSpecialOptionsDP(currentMap);
                    gba = cboGBASlot.Enabled ? cboGBASlot.SelectedIndex : 0;
                    time = cboTimeDPPt.Enabled ? cboTimeDPPt.SelectedIndex : 0;
                    newSlots = Utils.MapsPearl[currentMap].getSlots(type, (cboSwarmDPPt.Enabled && (cboSwarmDPPt.SelectedIndex == 1)), time, gba, chkRadarDPPt.Checked && chkRadarDPPt.Enabled);
                    break;

                case Version.Platinum:
                    cboAbility.Items.Add(encounterOptions[1][3]);
                    currentMap = Utils.mapTablesPlat[Utils.MapNamesPt.FindIndex(s => s.Equals((string)cboMapsPlat.SelectedItem))];
                    changeSpecialOptionsPlat(currentMap);
                    gba = cboGBASlot.Enabled ? cboGBASlot.SelectedIndex : 0;
                    time = cboTimeDPPt.Enabled ? cboTimeDPPt.SelectedIndex : 0;
                    newSlots = Utils.MapsPlatinum[currentMap].getSlots(type, (cboSwarmDPPt.Enabled && (cboSwarmDPPt.SelectedIndex == 1)), time, gba, chkRadarDPPt.Checked && chkRadarDPPt.Enabled);
                    break;
                case Version.HeartGold:
                    cboAbility.Items.Add(encounterOptions[1][3]);
                    //currentMap = Utils.mapTablesHGSS[Utils.MapNamesHGSS.FindIndex(s => s.Equals((string)cboMapsHGSS.SelectedItem))];
                    currentMap = Utils.MapNamesHGSS.FindIndex(s => s.Equals((string)cboMapsHGSS.SelectedItem));
                    currentMap = currentMap == 117 ? -1 : Utils.mapTablesHGSS[currentMap];
                    changeSpecialOptionsHGSS(currentMap);
                    radio = cboRadio.Enabled ? cboRadio.SelectedIndex : 0;
                    time = cboTimeHGSS.Enabled ? cboTimeHGSS.SelectedIndex : 0;
                    if (currentMap == -1)
                    {
                        newSlots = Utils.MapsSafariHGSS[cboSafariArea.SelectedIndex].getSlots(type, time,
                            (int)nudSafariPlainsBks.Value, (int)nudSafariForestBks.Value, (int)nudSafariRockBks.Value, (int)nudSafariWaterBks.Value,
                            (int)nudSafariDays.Value);
                    }
                    else
                    {
                        newSlots = Utils.MapsHeartGold[currentMap].getSlots(type, (cboSwarmHGSS.Enabled && (cboSwarmHGSS.SelectedIndex == 1)), time, radio);
                    }
                    break;
                case Version.SoulSilver:
                    cboAbility.Items.Add(encounterOptions[1][3]);
                    //currentMap = Utils.mapTablesHGSS[Utils.MapNamesHGSS.FindIndex(s => s.Equals((string)cboMapsHGSS.SelectedItem))];
                    currentMap = Utils.MapNamesHGSS.FindIndex(s => s.Equals((string)cboMapsHGSS.SelectedItem));
                    currentMap = currentMap == 117 ? -1 : Utils.mapTablesHGSS[currentMap];
                    changeSpecialOptionsHGSS(currentMap);
                    radio = cboRadio.Enabled ? cboRadio.SelectedIndex : 0;
                    time = cboTimeHGSS.Enabled ? cboTimeHGSS.SelectedIndex : 0;
                    if (currentMap == -1)
                    {
                        newSlots = Utils.MapsSafariHGSS[cboSafariArea.SelectedIndex].getSlots(type, time,
                            (int)nudSafariPlainsBks.Value, (int)nudSafariForestBks.Value, (int)nudSafariRockBks.Value, (int)nudSafariWaterBks.Value,
                            (int)nudSafariDays.Value);

                    }
                    else
                    {
                        newSlots = Utils.MapsSoulSilver[currentMap].getSlots(type, (cboSwarmHGSS.Enabled && (cboSwarmHGSS.SelectedIndex == 1)), time, radio);
                    }
                    break;
                case Version.Black:
                    currentMap = Utils.mapTablesBW[Utils.MapNamesBW.FindIndex(s => s.Equals((string)cboMapsBW.SelectedItem))];
                    currentMap += (cboSeason.Enabled) ? cboSeason.SelectedIndex : 0;
                    newSlots = Utils.MapsBlack[currentMap].getSlots(type);
                    break;
                case Version.White:
                    currentMap = Utils.mapTablesBW[Utils.MapNamesBW.FindIndex(s => s.Equals((string)cboMapsBW.SelectedItem))];
                    currentMap += (cboSeason.Enabled) ? cboSeason.SelectedIndex : 0;
                    newSlots = Utils.MapsWhite[currentMap].getSlots(type);
                    break;
                case Version.Black2:
                    currentMap = Utils.mapTablesB2W2[Utils.MapNamesB2W2.FindIndex(s => s.Equals((string)cboMapsB2W2.SelectedItem))];
                    currentMap += (cboSeason.Enabled) ? cboSeason.SelectedIndex : 0;
                    newSlots = Utils.MapsBlack2[currentMap].getSlots(type);
                    break;
                case Version.White2:
                    currentMap = Utils.mapTablesB2W2[Utils.MapNamesB2W2.FindIndex(s => s.Equals((string)cboMapsB2W2.SelectedItem))];
                    currentMap += (cboSeason.Enabled) ? cboSeason.SelectedIndex : 0;
                    newSlots = Utils.MapsWhite2[currentMap].getSlots(type);
                    break;
                case Version.X:
                    currentMap = Utils.MapNamesXY.FindIndex(s => s.Equals((string)cboMapsXY.SelectedItem));
                    newSlots = Utils.MapsX[currentMap].getSlots(type);
                    break;
                case Version.Y:
                    currentMap = Utils.MapNamesXY.FindIndex(s => s.Equals((string)cboMapsXY.SelectedItem));
                    newSlots = Utils.MapsY[currentMap].getSlots(type);
                    break;
                case Version.OmegaRuby:
                    currentMap = Utils.MapNamesOR.FindIndex(s => s.Equals((string)cboMapsOR.SelectedItem));
                    newSlots = Utils.MapsOmegaRuby[currentMap].getSlots(type);
                    break;
                case Version.AlphaSapphire:
                    currentMap = Utils.MapNamesAS.FindIndex(s => s.Equals((string)cboMapsAS.SelectedItem));
                    newSlots = Utils.MapsAlphaSapphire[currentMap].getSlots(type);
                    break;
                case Version.Sun:
                    currentMap = Utils.MapNamesSuMo.FindIndex(s => s.Equals((string)cboMapsSuMo.SelectedItem));
                    newSlots = Utils.MapsSun[currentMap].getSlots(selectedTableSuMo, cboDayNightSuMo.SelectedIndex == 1);
                    break;
                case Version.Moon:
                    currentMap = Utils.MapNamesSuMo.FindIndex(s => s.Equals((string)cboMapsSuMo.SelectedItem));
                    newSlots = Utils.MapsMoon[currentMap].getSlots(selectedTableSuMo, cboDayNightSuMo.SelectedIndex == 1);
                    break;
                case Version.UltraSun:
                    currentMap = Utils.MapNamesUSUM.FindIndex(s => s.Equals((string)cboMapsUSUM.SelectedItem));
                    newSlots = Utils.MapsUltraSun[currentMap].getSlots(selectedTableSuMo, cboDayNightSuMo.SelectedIndex == 1);
                    break;

                case Version.UltraMoon:
                    currentMap = Utils.MapNamesUSUM.FindIndex(s => s.Equals((string)cboMapsUSUM.SelectedItem));
                    newSlots = Utils.MapsUltraMoon[currentMap].getSlots(selectedTableSuMo, cboDayNightSuMo.SelectedIndex == 1);
                    break;

                default:
                    break;

            }

            if (cboAbility.Items.Contains(encounterOptions[1][(int)selectedAbility - 1]))
                cboAbility.SelectedItem = encounterOptions[1][(int)selectedAbility - 1];
            else
            {
                cboAbility.SelectedIndex = 0;
                chkAbility.Checked = false;
            }


            if (newSlots == null)
                return;
         
            updateSlots(newSlots);
            changeLuckyPowerPercentage();
        }

        void changeLuckyPowerPercentage()
        {
            if (!(cboLuckyPower.Visible)) return;
            EncounterType type = (EncounterType)encounterOptions[0].FindIndex(s => s.Equals((string)cboEncounterType.SelectedItem));
            int luckyPowerLevel = cboLuckyPower.Enabled ? cboLuckyPower.SelectedIndex + 1 : 0;
            decimal[] percent = AreaMapGen5.getPercentages(type, luckyPowerLevel);
            for(int i = 0;  i < currentSlots.Length; i++)
            {
                currentSlots[i].Percentage = percent[i];
            }
            updateSlots(currentSlots);
        }


        private void updateSlots(EncounterSlot[] newSlots)
        {
            //

            currentSlots = newSlots;

            update();


            
        }



        #endregion

        private void cboLuckyPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeLuckyPowerPercentage();
        }

        private void displayTurnback_on(object sender, EventArgs e)
        {
            pnlTurnback.Visible = true;
            pnlTurnback.BringToFront();
        }

        private void displayTurnback_off(object sender, EventArgs e)
        {
            pnlTurnback.Visible = false;
        }

        private void displayRoute120_on(object sender, EventArgs e)
        {
            pnlRoute120.Visible = true;
            pnlRoute120.BringToFront();
        }

        private void displayRoute120_off(object sender, EventArgs e)
        {
            pnlRoute120.Visible = false;
        }

        private void honeyCuteCharmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHoneyCuteCharm f = new frmHoneyCuteCharm();
            f.ShowDialog();
        }

        private void captureCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCaptureCalc c = new frmCaptureCalc();
            c.ShowDialog();
        }

        private void frmMainPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();                
        }

        private void ppCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPPCounter p = new frmPPCounter();
            p.ShowDialog();
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            normalToolStripMenuItem.Checked = true;
            shinyToolStripMenuItem.Checked = false;
            Properties.Settings.Default.ShinySprites = false;
            update();

        }

        private void shinyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            normalToolStripMenuItem.Checked = false;
            shinyToolStripMenuItem.Checked = true;
            Properties.Settings.Default.ShinySprites = true;
            update();
        }

        private void update()
        {
            if (currentSlots == null) return;

            updatingSlots = true;

            decimal totalPercent = currentSlots.Sum(s => s.Percentage);

            foreach (Control b in gboSlots.Controls)
            {
                b.Visible = false;
            }

            

            for (int i = 0; i < currentSlots.Length; i++)
            {
                ((ComboBox)(gboSlots.Controls.Find("cboSlot" + i, false)[0])).Visible = true;
                ((NumericUpDown)(gboSlots.Controls.Find("nudMinLv" + i, false)[0])).Visible = true;
                ((NumericUpDown)(gboSlots.Controls.Find("nudMaxLv" + i, false)[0])).Visible = true;
                ((PictureBox)(gboSlots.Controls.Find("pctPoke" + i, false)[0])).Visible = true;
                ((Label)(gboSlots.Controls.Find("lblPercent" + i, false)[0])).Visible = true;

                ((ComboBox)(gboSlots.Controls.Find("cboSlot" + i, false)[0])).SelectedIndex = currentSlots[i].Species.NatID - 1;
                ((NumericUpDown)(gboSlots.Controls.Find("nudMinLv" + i, false)[0])).Value = currentSlots[i].MinLevel;
                ((NumericUpDown)(gboSlots.Controls.Find("nudMaxLv" + i, false)[0])).Value = currentSlots[i].MaxLevel;
                // Change minisprite
                if (Properties.Settings.Default.ShinySprites)
                    ((PictureBox)gboSlots.Controls.Find("pctPoke" + i, false)[0]).Image = (Image)Properties.Resources.ResourceManager.GetObject
                        ("m" + (currentSlots[i].Species.NatID) + "s" + (currentSlots[i].Species.Form != 0 ? ("_" + currentSlots[i].Species.Form) : ""));
                else
                    ((PictureBox)gboSlots.Controls.Find("pctPoke" + i, false)[0]).Image = (Image)Properties.Resources.ResourceManager.GetObject
                        ("m" + (currentSlots[i].Species.NatID) + (currentSlots[i].Species.Form != 0 ? ("_" + currentSlots[i].Species.Form) : ""));


                ((Label)(gboSlots.Controls.Find("lblPercent" + i, false)[0])).Text = totalPercent == 100 ? 
                                                                                    currentSlots[i].Percentage + " %" :
                                                                                    currentSlots[i].Percentage + "/" + (int)totalPercent;

            }


            updatingSlots = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout a = new frmAbout();
            a.ShowDialog();
        }

        private void chkBugCatching_CheckedChanged(object sender, EventArgs e)
        {
            cboTimeGSC.Enabled = lblTimeDispGSC.Enabled = chkBugCatching.Checked;
        }

        private void nudSafariPlainsBks_ValueChanged(object sender, EventArgs e)
        {
            updateBlockNumber();
            loadSlotData();
        }


        private void updateBlockNumber()
        {
            nudSafariPlainsBks.Maximum = 30 - nudSafariForestBks.Value - nudSafariRockBks.Value - nudSafariWaterBks.Value;
            nudSafariForestBks.Maximum = 30 - nudSafariPlainsBks.Value - nudSafariRockBks.Value - nudSafariWaterBks.Value;
            nudSafariRockBks.Maximum = 30 - nudSafariPlainsBks.Value - nudSafariForestBks.Value - nudSafariWaterBks.Value;
            nudSafariWaterBks.Maximum = 30 - nudSafariPlainsBks.Value - nudSafariForestBks.Value - nudSafariRockBks.Value;
            lblSafariBlocksCount.Text = "(" + (nudSafariPlainsBks.Value + nudSafariForestBks.Value + nudSafariRockBks.Value + nudSafariWaterBks.Value) + "/30)";

        }

        private void nudSafariForestBks_ValueChanged(object sender, EventArgs e)
        {
            updateBlockNumber();
            loadSlotData();
        }

        private void nudSafariRockBks_ValueChanged(object sender, EventArgs e)
        {
            updateBlockNumber();
            loadSlotData();
        }

        private void nudSafariWaterBks_ValueChanged(object sender, EventArgs e)
        {
            updateBlockNumber();
            loadSlotData();
        }

        private void nudLevelRepel_Click(object sender, EventArgs e)
        {
            // Sélectionne toute la réponse dans NumericUpDown
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
    }
}
