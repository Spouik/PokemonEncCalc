using System;
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
        EncounterSlot[] currentSlots = new EncounterSlot[12];
        EncounterType currentEncounterType = EncounterType.Walking;
        decimal[] percentage = { 20, 20, 10, 10, 10, 10, 5, 5, 4, 4, 1, 1 };

        bool updatingSlots = false; //is true when slots are being updated automatically (prevents slot modification via controls during this time)

        // Contains strings for encounter types and encounter options for the current language
        // encounterOptions[0] contains all encounter types, while encounterOptions[1] contains abilities
        // Subsequent entries contain other encounter options (swarm, time of day, seasons...)
        List<List<string>> encounterOptions;

        public frmMainPage()
        {
            loadEncounterOptions((Language)Properties.Settings.Default.Language);
            InitializeComponent();
            
        }

        private void cmdCalc_Click(object sender, EventArgs e)
        {
            List<EncounterSlot> result = Utils.calcEncounterRate(currentSlots, Version.Diamond, Ability.None, chkRepel.Checked ? (byte)nudLevelRepel.Value : (byte)0);

            string resultText = "";
            foreach (EncounterSlot s in result)
                resultText += (s.Species.NameEN + " - " + s.Percentage.ToString() + Environment.NewLine);

            

            MessageBox.Show(resultText);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMainPage_Load(object sender, EventArgs e)
        {
            Text = "Pokémon Encounter Calculator - Ver. " + Program.version;
            Utils.changeLanguage(Properties.Settings.Default.Language);
            loadPokemonNames();
            renameControls();
            renameMenuStrip();
            renameComboboxes();
            updateEncounterOptions();
            for (int a = 0; a < currentSlots.Length; a++)
            {
                //currentSlots[a] = new EncounterSlot();
                currentSlots[a].Percentage = percentage[a];
            }
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

        private void loadPokemonNames()
        {
            // Loads Pokémon names in the comboboxes
            foreach (Control b in gboSlots.Controls)
            {
                if (b is ComboBox)
                {
                    int selected = 0;
                    if (((ComboBox)b).Items.Count != 0)
                    {
                        selected = ((ComboBox)b).SelectedIndex;
                        ((ComboBox)b).Items.Clear();
                    }
                    ((ComboBox)b).Items.AddRange(Utils.NamesCurrentLang.ToArray());
                    ((ComboBox)b).SelectedIndex = selected;
                }
            }
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = 1;
            Utils.changeLanguage(1);
            loadEncounterOptions(Language.English);
            updateEncounterOptions();
            renameControls();
            renameMenuStrip();
            renameComboboxes();
            loadPokemonNames();
        }

        private void frenchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = 2;
            Utils.changeLanguage(2);
            loadEncounterOptions(Language.French);
            updateEncounterOptions();
            renameControls();
            renameMenuStrip();
            renameComboboxes();
            loadPokemonNames();
        }

        private void changeMinisprite(object sender, EventArgs e)
        {
            if (!(sender is ComboBox))  //Checks if it is a combobox (only comboboxes can handle this method)
                return;

            if (!((ComboBox)sender).Name.StartsWith("cboSlot")) //Checks combobox name (expected "cboSlot" + x, where x between 0 and 11)
                return;

            int slot;
            if (!int.TryParse(((ComboBox)sender).Name.Substring(7), out slot))
                return;

            if (updatingSlots)
                return;

            // Change Slot
            currentSlots[slot] = new EncounterSlot(Utils.PokemonList[((ComboBox)sender).SelectedIndex],
                                                    (byte)((NumericUpDown)gboSlots.Controls.Find("nudMinLv" + slot, true)[0]).Value,
                                                    (byte)((NumericUpDown)gboSlots.Controls.Find("nudMaxLv" + slot, true)[0]).Value,
                                                    Decimal.Parse(((Label)gboSlots.Controls.Find("lblPercent" + slot, true)[0]).Text.Split(new[] { ' ' })[0]));

            // Change minisprite
            ((PictureBox)gboSlots.Controls.Find("pctPoke" + slot, true)[0]).Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + (((ComboBox)sender).SelectedIndex + 1));


        }

        private void cboVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hide every map combobox and every option
            cboMapsRubySapp.Visible = cboMapsEmer.Visible = cboMapsFireLeaf.Visible = cboMapsDP.Visible
                = cboMapsPlat.Visible = cboMapsHGSS.Visible = cboMapsBW.Visible = cboMapsB2W2.Visible
                = cboMapsXY.Visible = cboMapsOR.Visible = cboMapsAS.Visible = pnlAbility.Visible = pnlDPPtOptions.Visible
                = pnlHGSSOptions.Visible = pnlGen5Options.Visible = pnlLuckyPower.Visible = false;




            // Select map combobox to show based on selected version
            switch (cboVersion.SelectedIndex)
            {
                case 0:
                case 1:
                    cboMapsRubySapp.Visible = true;
                    changeEncounterOptionsRubySapp();
                    break;
                case 2:
                    cboMapsEmer.Visible = true;
                    pnlAbility.Visible = true;
                    changeEncounterOptionsEmerald();
                    break;
                case 3:
                case 4:
                    cboMapsFireLeaf.Visible = true;
                    changeEncounterOptionsFireLeaf();
                    break;
                case 5:
                case 6:
                    cboMapsDP.Visible = true;
                    pnlAbility.Visible = true;
                    pnlDPPtOptions.Visible = true;
                    changeEncounterOptionsDP();
                    break;
                case 7:
                    cboMapsPlat.Visible = true;
                    pnlAbility.Visible = true;
                    pnlDPPtOptions.Visible = true;
                    changeEncounterOptionsPlat();
                    break;
                case 8:
                case 9:
                    cboMapsHGSS.Visible = true;
                    pnlAbility.Visible = true;
                    pnlHGSSOptions.Visible = true;
                    changeEncounterOptionsHGSS();
                    break;
                case 10:
                case 11:
                    cboMapsBW.Visible = true;
                    pnlAbility.Visible = true;
                    pnlGen5Options.Visible = true;
                    changeEncounterOptionsBW();
                    break;
                case 12:
                case 13:
                    cboMapsB2W2.Visible = true;
                    pnlAbility.Visible = true;
                    pnlGen5Options.Visible = true;
                    pnlLuckyPower.Visible = true;
                    changeEncounterOptionsB2W2();
                    break;
                case 14:
                case 15:
                    cboMapsXY.Visible = true;
                    pnlAbility.Visible = true;
                    break;
                case 16:
                    cboMapsOR.Visible = true;
                    pnlAbility.Visible = true;
                    break;
                case 17:
                    cboMapsAS.Visible = true;
                    pnlAbility.Visible = true;
                    break;
                default:
                    break;
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

            ((PictureBox)sender).Image = (currentSlots[slot].Species.Form == 0) ? (Image)Properties.Resources.ResourceManager.GetObject("_" + currentSlots[slot].Species.NatID)
                                                                  : (Image)Properties.Resources.ResourceManager.GetObject("_" + currentSlots[slot].Species.NatID + "_" + currentSlots[slot].Species.Form);

        }



        private void renameComboboxes()
        {
            // Rename Comboboxes
            // Version:
            translateComboBoxes(cboVersion, "versions" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1]);
            // Maps RS:
            translateMaps(cboMapsRubySapp, "Maps_RS_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], ref Utils.MapsRS);
            // Maps E:
            translateMaps(cboMapsEmer, "Maps_E_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], ref Utils.MapsEmer);
            // Maps FRLG:
            translateMaps(cboMapsFireLeaf, "Maps_FRLG_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], ref Utils.MapsFRLG);
            // Maps DP:
            translateMaps(cboMapsDP, "Maps_DP_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], ref Utils.MapsDP);
            // Maps Pt:
            translateMaps(cboMapsPlat, "Maps_Pt_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], ref Utils.MapsPt);
            // Maps HGSS:
            translateMaps(cboMapsHGSS, "Maps_HGSS_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], ref Utils.MapsHGSS);
            // Maps BW:
            translateMaps(cboMapsBW, "Maps_BW_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], ref Utils.MapsBW);
            // Maps B2W2:
            translateMaps(cboMapsB2W2, "Maps_B2W2_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], ref Utils.MapsB2W2);
            // Maps XY:
            translateMaps(cboMapsXY, "Maps_XY_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], ref Utils.MapsXY);
            // Maps OR:
            translateMaps(cboMapsOR, "Maps_OR_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], ref Utils.MapsOR);
            // Maps AS:
            translateMaps(cboMapsAS, "Maps_AS_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], ref Utils.MapsAS);



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
            int value = 0;
            if (mapNames != null)
                value = combobox.SelectedIndex == -1 ? 0 : mapNames.FindIndex(s => s.Equals(combobox.SelectedItem));
            combobox.Items.Clear();
            mapNames = new List<string>();
            mapNames.AddRange(Properties.Resources.ResourceManager.GetString(resource).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
            combobox.Items.AddRange(Properties.Resources.ResourceManager.GetString(resource).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
            if (combobox.Items.Contains(mapNames[value]))
                combobox.SelectedItem = mapNames[value];
            else
                combobox.SelectedIndex = 0;
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
            int[] comboboxIndexes = new int[9];

            comboboxIndexes[0] = Math.Max(cboSwarmDPPt.SelectedIndex, 0);
            comboboxIndexes[1] = Math.Max(cboSwarmHGSS.SelectedIndex, 0);
            comboboxIndexes[2] = Math.Max(cboSwarmGen5.SelectedIndex, 0);
            comboboxIndexes[3] = Math.Max(cboTimeDPPt.SelectedIndex, 0);
            comboboxIndexes[4] = Math.Max(cboTimeHGSS.SelectedIndex, 0);
            comboboxIndexes[5] = Math.Max(cboRadio.SelectedIndex, 0);
            comboboxIndexes[6] = Math.Max(cboSeason.SelectedIndex, 0);
            comboboxIndexes[7] = Math.Max(cboLuckyPower.SelectedIndex, 0);
            comboboxIndexes[8] = Math.Max(cboGBASlot.SelectedIndex, 0);

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

            try
            {
                cboSwarmDPPt.Items.AddRange(encounterOptions[2].ToArray());
                cboSwarmHGSS.Items.AddRange(encounterOptions[2].ToArray());
                cboSwarmGen5.Items.AddRange(encounterOptions[2].ToArray());
                cboTimeDPPt.Items.AddRange(encounterOptions[3].ToArray());
                cboTimeHGSS.Items.AddRange(encounterOptions[3].ToArray());
                cboRadio.Items.AddRange(encounterOptions[4].ToArray());
                cboSeason.Items.AddRange(encounterOptions[5].ToArray());
                cboLuckyPower.Items.AddRange(encounterOptions[6].ToArray());
                cboGBASlot.Items.AddRange(encounterOptions[7].ToArray());

                cboSwarmDPPt.SelectedIndex = comboboxIndexes[0];
                cboSwarmHGSS.SelectedIndex = comboboxIndexes[1];
                cboSwarmGen5.SelectedIndex = comboboxIndexes[2];
                cboTimeDPPt.SelectedIndex = comboboxIndexes[3];
                cboTimeHGSS.SelectedIndex = comboboxIndexes[4];
                cboRadio.SelectedIndex = comboboxIndexes[5];
                cboSeason.SelectedIndex = comboboxIndexes[6];
                cboLuckyPower.SelectedIndex = comboboxIndexes[7];
                cboGBASlot.SelectedIndex = comboboxIndexes[8];
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

            if(cboAbility.SelectedItem.Equals(encounterOptions[0][2]))
            {
                chkRepel.Enabled = false;
                chkRepel.Checked = true;
            }
            else
            {
                chkRepel.Enabled = true;
            }

        }

        // Repel level enable when checked
        private void chkRepel_CheckedChanged(object sender, EventArgs e)
        {
            nudLevelRepel.Enabled = chkRepel.Checked;
            lblLevelRepelDisp.Enabled = chkRepel.Checked;
        }


        #region loadingEncounterSlots



        private void changeEncounterOptionsAS(object sender, EventArgs e)
        {

        }

        private void changEncounterOptionsOR(object sender, EventArgs e)
        {

        }

        private void changeEncounterOptionsXY(object sender, EventArgs e)
        {

        }

        private void changeEncounterOptionsGen5(object sender, EventArgs e)
        {
            Version currentVersion = (Version)((int)Version.Ruby + cboVersion.SelectedIndex);
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
            int selectedMap = cboMapsB2W2.SelectedIndex == -1 ? 0 : Utils.mapTablesB2W2[Utils.MapsB2W2.FindIndex(s => s.Equals((string)cboMapsB2W2.SelectedItem))];

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
            int selectedMap = cboMapsBW.SelectedIndex == -1 ? 0 : Utils.mapTablesBW[Utils.MapsBW.FindIndex(s => s.Equals((string)cboMapsBW.SelectedItem))];

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
            int selectedMap = cboMapsHGSS.SelectedIndex == -1 ? 0 : Utils.mapTablesHGSS[Utils.MapsHGSS.FindIndex(s => s.Equals((string)cboMapsHGSS.SelectedItem))];


            cboEncounterType.Items.Clear();

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
            int selectedMap = cboMapsPlat.SelectedIndex == -1 ? 0 : Utils.mapTablesPlat[Utils.MapsPt.FindIndex(s => s.Equals((string)cboMapsPlat.SelectedItem))];



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
            int selectedMap = cboMapsDP.SelectedIndex == -1 ? 0 : Utils.mapTablesDP[Utils.MapsDP.FindIndex(s => s.Equals((string)cboMapsDP.SelectedItem))];
            


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
            int selectedMap = cboMapsFireLeaf.SelectedIndex == -1 ? 0 : Utils.MapsFRLG.FindIndex(s => s.Equals((string)cboMapsFireLeaf.SelectedItem));

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
            int selectedMap = cboMapsEmer.SelectedIndex == -1 ? 0 : Utils.MapsEmer.FindIndex(s => s.Equals((string)cboMapsEmer.SelectedItem));

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
            int selectedMap = cboMapsRubySapp.SelectedIndex == -1 ? 0 : Utils.MapsRS.FindIndex(s => s.Equals((string)cboMapsRubySapp.SelectedItem));

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


        void loadSlotData(object sender, EventArgs e)
        {
            int currentMap;
            EncounterSlot[] newSlots = null;
            EncounterType type = (EncounterType)encounterOptions[0].FindIndex(s => s.Equals((string)cboEncounterType.SelectedItem));
            Version currentVersion = (Version)((int)Version.Ruby + cboVersion.SelectedIndex);
            int gba = 0, time = 0, radio = 0;

            switch (currentVersion)
            {
                case Version.Ruby:
                    currentMap = Utils.MapsRS.FindIndex(s => s.Equals((string)cboMapsRubySapp.SelectedItem));
                    newSlots = Utils.MapsRuby[currentMap].getSlots(type);
                    break;

                case Version.Sapphire:
                    currentMap = Utils.MapsRS.FindIndex(s => s.Equals((string)cboMapsRubySapp.SelectedItem));
                    newSlots = Utils.MapsSapphire[currentMap].getSlots(type);
                    break;

                case Version.Emerald:
                    currentMap = Utils.MapsEmer.FindIndex(s => s.Equals((string)cboMapsEmer.SelectedItem));
                    newSlots = Utils.MapsEmerald[currentMap].getSlots(type);
                    break;
                case Version.FireRed:
                    currentMap = Utils.MapsFRLG.FindIndex(s => s.Equals((string)cboMapsFireLeaf.SelectedItem));
                    newSlots = Utils.MapsFireRed[currentMap].getSlots(type);
                    break;
                case Version.LeafGreen:
                    currentMap = Utils.MapsFRLG.FindIndex(s => s.Equals((string)cboMapsFireLeaf.SelectedItem));
                    newSlots = Utils.MapsLeafGreen[currentMap].getSlots(type);
                    break;
                case Version.Diamond:
                    currentMap = Utils.mapTablesDP[Utils.MapsDP.FindIndex(s => s.Equals((string)cboMapsDP.SelectedItem))];
                    changeSpecialOptionsDP(currentMap);
                    gba = cboGBASlot.Enabled ? cboGBASlot.SelectedIndex : 0;
                    time = cboTimeDPPt.Enabled ? cboTimeDPPt.SelectedIndex : 0;
                    newSlots = Utils.MapsDiamond[currentMap].getSlots(type, (cboSwarmDPPt.Enabled && (cboSwarmDPPt.SelectedIndex == 1)), time, gba, chkRadarDPPt.Checked && chkRadarDPPt.Enabled);
                    break;
                case Version.Pearl:
                    currentMap = Utils.mapTablesDP[Utils.MapsDP.FindIndex(s => s.Equals((string)cboMapsDP.SelectedItem))];
                    changeSpecialOptionsDP(currentMap);
                    gba = cboGBASlot.Enabled ? cboGBASlot.SelectedIndex : 0;
                    time = cboTimeDPPt.Enabled ? cboTimeDPPt.SelectedIndex : 0;
                    newSlots = Utils.MapsPearl[currentMap].getSlots(type, (cboSwarmDPPt.Enabled && (cboSwarmDPPt.SelectedIndex == 1)), time, gba, chkRadarDPPt.Checked && chkRadarDPPt.Enabled);
                    break;

                case Version.Platinum:
                    currentMap = Utils.mapTablesPlat[Utils.MapsPt.FindIndex(s => s.Equals((string)cboMapsPlat.SelectedItem))];
                    changeSpecialOptionsPlat(currentMap);
                    gba = cboGBASlot.Enabled ? cboGBASlot.SelectedIndex : 0;
                    time = cboTimeDPPt.Enabled ? cboTimeDPPt.SelectedIndex : 0;
                    newSlots = Utils.MapsPlatinum[currentMap].getSlots(type, (cboSwarmDPPt.Enabled && (cboSwarmDPPt.SelectedIndex == 1)), time, gba, chkRadarDPPt.Checked && chkRadarDPPt.Enabled);
                    break;
                case Version.HeartGold:
                    currentMap = Utils.mapTablesHGSS[Utils.MapsHGSS.FindIndex(s => s.Equals((string)cboMapsHGSS.SelectedItem))];
                    changeSpecialOptionsHGSS(currentMap);
                    radio = cboRadio.Enabled ? cboRadio.SelectedIndex : 0;
                    time = cboTimeHGSS.Enabled ? cboTimeHGSS.SelectedIndex : 0;
                    newSlots = Utils.MapsHeartGold[currentMap].getSlots(type, (cboSwarmHGSS.Enabled && (cboSwarmHGSS.SelectedIndex == 1)), time, radio);
                    break;
                case Version.SoulSilver:
                    currentMap = Utils.mapTablesHGSS[Utils.MapsHGSS.FindIndex(s => s.Equals((string)cboMapsHGSS.SelectedItem))];
                    changeSpecialOptionsHGSS(currentMap);
                    radio = cboRadio.Enabled ? cboRadio.SelectedIndex : 0;
                    time = cboTimeHGSS.Enabled ? cboTimeHGSS.SelectedIndex : 0;
                    newSlots = Utils.MapsSoulSilver[currentMap].getSlots(type, (cboSwarmHGSS.Enabled && (cboSwarmHGSS.SelectedIndex == 1)), time, radio);
                    break;
                case Version.Black:
                    currentMap = Utils.mapTablesBW[Utils.MapsBW.FindIndex(s => s.Equals((string)cboMapsBW.SelectedItem))];
                    currentMap += (cboSeason.Enabled) ? cboSeason.SelectedIndex : 0;
                    newSlots = Utils.MapsBlack[currentMap].getSlots(type);
                    break;
                case Version.White:
                    currentMap = Utils.mapTablesBW[Utils.MapsBW.FindIndex(s => s.Equals((string)cboMapsBW.SelectedItem))];
                    currentMap += (cboSeason.Enabled) ? cboSeason.SelectedIndex : 0;
                    newSlots = Utils.MapsWhite[currentMap].getSlots(type);
                    break;
                case Version.Black2:
                    currentMap = Utils.mapTablesB2W2[Utils.MapsB2W2.FindIndex(s => s.Equals((string)cboMapsB2W2.SelectedItem))];
                    currentMap += (cboSeason.Enabled) ? cboSeason.SelectedIndex : 0;
                    newSlots = Utils.MapsBlack2[currentMap].getSlots(type);
                    break;
                case Version.White2:
                    currentMap = Utils.mapTablesB2W2[Utils.MapsB2W2.FindIndex(s => s.Equals((string)cboMapsB2W2.SelectedItem))];
                    currentMap += (cboSeason.Enabled) ? cboSeason.SelectedIndex : 0;
                    newSlots = Utils.MapsWhite2[currentMap].getSlots(type);
                    break;
                default:
                    break;

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
            updatingSlots = true;

            currentSlots = newSlots;

            foreach(Control b in gboSlots.Controls)
            {
                b.Visible = false;
            }

            for(int i = 0; i < currentSlots.Length; i++)
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
                ((PictureBox)gboSlots.Controls.Find("pctPoke" + i, false)[0]).Image = (Image)Properties.Resources.ResourceManager.GetObject
                    ("_" + (currentSlots[i].Species.NatID) + (currentSlots[i].Species.Form !=0 ? ("_"+currentSlots[i].Species.Form) : ""));

                ((Label)(gboSlots.Controls.Find("lblPercent" + i, false)[0])).Text = currentSlots[i].Percentage + " %";

            }


            updatingSlots = false;
            
        }



        #endregion

        private void cboLuckyPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeLuckyPowerPercentage();
        }
    }
}
