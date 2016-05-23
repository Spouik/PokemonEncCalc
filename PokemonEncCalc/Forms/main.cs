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
        decimal[] percentage = { 20, 20, 10, 10, 10, 10, 5, 5, 4, 4, 1, 1 };

        public frmMainPage()
        {
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
            renameComboboxes();
            for (int a = 0; a < currentSlots.Length; a++)
            {
                //currentSlots[a] = new EncounterSlot();
                currentSlots[a].Percentage = percentage[a];
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
            renameControls();
            renameComboboxes();
            loadPokemonNames();
        }

        private void frenchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = 2;
            Utils.changeLanguage(2);
            renameControls();
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
            // Hide every map combobox
            cboMapsRubySapp.Visible = cboMapsEmer.Visible = cboMapsFireLeaf.Visible = cboMapsDP.Visible
                = cboMapsPlat.Visible = cboMapsHGSS.Visible = cboMapsBW.Visible = cboMapsB2W2.Visible
                = cboMapsXY.Visible = cboMapsOR.Visible = cboMapsAS.Visible = false;

            // Select map combobox to show based on selected version
            switch (cboVersion.SelectedIndex)
            {
                case 0:
                case 1:
                    cboMapsRubySapp.Visible = true;
                    break;
                case 2:
                    cboMapsEmer.Visible = true;
                    break;
                case 3:
                case 4:
                    cboMapsFireLeaf.Visible = true;
                    break;
                case 5:
                case 6:
                    cboMapsDP.Visible = true;
                    break;
                case 7:
                    cboMapsPlat.Visible = true;
                    break;
                case 8:
                case 9:
                    cboMapsHGSS.Visible = true;
                    break;
                case 10:
                case 11:
                    cboMapsBW.Visible = true;
                    break;
                case 12:
                case 13:
                    cboMapsB2W2.Visible = true;
                    break;
                case 14:
                case 15:
                    cboMapsXY.Visible = true;
                    break;
                case 16:
                    cboMapsOR.Visible = true;
                    break;
                case 17:
                    cboMapsAS.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private void changeMinLevel(object sender, EventArgs e)
        {
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
            Utils.MapsRS = translateMaps(cboMapsRubySapp, "Maps_RS_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], Utils.MapsRS);
            // Maps E:
            Utils.MapsEmer = translateMaps(cboMapsEmer, "Maps_E_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], Utils.MapsEmer);
            // Maps FRLG:
            Utils.MapsFRLG = translateMaps(cboMapsFireLeaf, "Maps_FRLG_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], Utils.MapsFRLG);
            // Maps DP:
            Utils.MapsDP = translateMaps(cboMapsDP, "Maps_DP_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], Utils.MapsDP);
            // Maps Pt:
            Utils.MapsPt = translateMaps(cboMapsPlat, "Maps_Pt_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], Utils.MapsPt);
            // Maps HGSS:
            Utils.MapsHGSS = translateMaps(cboMapsHGSS, "Maps_HGSS_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], Utils.MapsHGSS);
            // Maps BW:
            Utils.MapsBW = translateMaps(cboMapsBW, "Maps_BW_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], Utils.MapsBW);
            // Maps B2W2:
            Utils.MapsB2W2 = translateMaps(cboMapsB2W2, "Maps_B2W2_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], Utils.MapsB2W2);
            // Maps XY:
            Utils.MapsXY = translateMaps(cboMapsXY, "Maps_XY_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], Utils.MapsXY);
            // Maps OR:
            Utils.MapsOR = translateMaps(cboMapsOR, "Maps_OR_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], Utils.MapsOR);
            // Maps AS:
            Utils.MapsAS = translateMaps(cboMapsAS, "Maps_AS_" + (new[] { "EN", "FR", "DE", "ES", "IT", "JP", "KR" })[Properties.Settings.Default.Language - 1], Utils.MapsAS);



        }

        private static void translateComboBoxes(ComboBox combobox, string resource)
        {
            int value = combobox.SelectedIndex == -1 ? 0 : combobox.SelectedIndex;
            combobox.Items.Clear();
            combobox.Items.AddRange(Properties.Resources.ResourceManager.GetString(resource).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
            combobox.SelectedIndex = value;
        }

        private static List<string> translateMaps(ComboBox combobox, string resource, List<string> mapNames)
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
            return mapNames;
        }

    }
}
