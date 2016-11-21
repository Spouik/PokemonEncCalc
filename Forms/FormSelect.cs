using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonEncCalc
{
    public partial class frmFormSelect : TranslatableForm
    {
        private static EncounterSlot data;

        private static Dictionary<int, int[]> allowedFormChanges = new Dictionary<int, int[]>
        {
            { 201, new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27} }, //Unown
            { 386, new[] {1, 2, 3 } }, //Deoxys
            { 412, new[] {1, 2 } }, //Burmy
            { 413, new[] {1, 2 } }, //Wormadam
            { 422, new[] {1 } }, //Shellos
            { 423, new[] {1 } }, //Gastrodon
            { 479, new[] {1, 2, 3, 4, 5} }, //Rotom
            { 487, new[] {1 } }, //Giratina
            { 550, new[] {1 } }, //Basculin
            { 585, new[] {1, 2, 3} }, //Deerling
            { 586, new[] {1, 2, 3} }, //Sawsbuck
            { 641, new[] {1 } }, //Tornadus
            { 642, new[] {1 } }, //Thundurus
            { 645, new[] {1 } }, //Landorus
            { 646, new[] {1, 2 } }, //Kyurem
            { 669, new[] {1, 2, 3, 4} }, //Flabébé
            { 670, new[] {1, 2, 3, 4, 5} }, //Floette
            { 671, new[] {1, 2, 3, 4} }, //Florges
            { 678, new[] {1 } }, //Meowstic
            { 710, new[] {1, 2, 3} }, //Pumpkaboo
            { 711, new[] {1, 2, 3} }, //Gourgeist
            { 741, new[] {1, 2, 3} }, //Oricorio
            { 745, new[] {1 } }, //Lycanroc

            //Alolan formes
            {19, new[] {1 } },
            {20, new[] {1 } },
            {26, new[] {1 } },
            {27, new[] {1 } },
            {28, new[] {1 } },
            {37, new[] {1 } },
            {38, new[] {1 } },
            {50, new[] {1 } },
            {51, new[] {1 } },
            {52, new[] {1 } },
            {53, new[] {1 } },
            {74, new[] {1 } },
            {75, new[] {1 } },
            {76, new[] {1 } },
            {88, new[] {1 } },
            {89, new[] {1 } },
            {103, new[] {1 } },
            {105, new[] {1 } },

        };
                                                                

        public frmFormSelect()
        {
            InitializeComponent();
        }


        internal DialogResult ShowDialog(EncounterSlot slot)
        {
            data = new EncounterSlot(slot);
            return ShowDialog();
        }

        private void FormSelect_Load(object sender, EventArgs e)
        {
            renameControls();
            cboFormList.Items.Clear();


            lblChooseFormText.Text = lblChooseFormText.Text.Replace("{1}", data.Species.getName());
            // Checks if form change allowed
            if (allowedFormChanges.ContainsKey(data.Species.NatID))
            {
                // Get form 0:
                Pokemon form0 = PokemonTables.changePokemon(data.Species, data.Species.NatID);

                cboFormList.Items.Add(form0.getFormName());

                foreach (Pokemon p in form0.Forms)
                    if(allowedFormChanges[data.Species.NatID].Contains(p.Form))
                        cboFormList.Items.Add(p.getFormName());

                cboFormList.SelectedIndex = data.Species.Form == 0 ? 0 : Array.IndexOf(allowedFormChanges[data.Species.NatID], data.Species.Form) + 1;
            }
            else
            {
                cboFormList.Items.Add(data.Species.getFormName());
                cboFormList.SelectedIndex = 0;
            }
        }


        internal static EncounterSlot getResult()
        {
            return data;
        }

        private void cboFormList_SelectedIndexChanged(object sender, EventArgs e)
        {

            data.Species = (cboFormList.SelectedIndex == 0) ? PokemonTables.changePokemon(data.Species, data.Species.NatID)
                                                            : PokemonTables.changePokemon(data.Species, data.Species.NatID).Forms[allowedFormChanges[data.Species.NatID][cboFormList.SelectedIndex - 1]-1];

            // Change minisprite
            pctMinisprite.Image = (Image)Properties.Resources.ResourceManager.GetObject(
                                "m" + data.Species.NatID +
                                (Properties.Settings.Default.ShinySprites ? "s" : "") +
                                (cboFormList.SelectedIndex == 0 ? "" : "_" + cboFormList.SelectedIndex));
                                                                 
        }



    }
}
