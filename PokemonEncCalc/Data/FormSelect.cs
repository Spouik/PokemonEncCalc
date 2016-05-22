using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonEncCalc.Data
{
    public partial class frmFormSelect : Form
    {
        private static EncounterSlot data;
        private static int[] allowedFormChanges = new int[] {   201,
                                                                386,
                                                                412,413,422,423,479,487,
                                                                550,585,586,641,642,645,646,
                                                                669,670,671,678,681,710,711,720 };

        public frmFormSelect()
        {
            InitializeComponent();
        }


        internal DialogResult ShowDialog(EncounterSlot slot)
        {
            data = slot;
            return this.ShowDialog();
        }

        private void FormSelect_Load(object sender, EventArgs e)
        {
            cboFormList.Items.Clear();


            // Checks if form change allowed
            if (allowedFormChanges.Contains(data.Species.NatID))
            {
                // Get form 0:
                Pokemon form0 = Utils.PokemonList[data.Species.NatID - 1];
                if (form0.FormNameEN.Equals(" "))
                    cboFormList.Items.Add(form0.NameEN + "'s form");
                else
                    cboFormList.Items.Add(form0.FormNameEN);
                foreach (Pokemon p in form0.Forms)
                    cboFormList.Items.Add(p.FormNameEN);

                cboFormList.SelectedIndex = data.Species.Form;
            }
            else
            {
                cboFormList.Items.Add(data.Species.NameEN + "'s form");
                cboFormList.SelectedIndex = 0;
            }
        }


        internal static EncounterSlot getResult()
        {
            return data;
        }

        private void cboFormList_SelectedIndexChanged(object sender, EventArgs e)
        {

            data.Species = (cboFormList.SelectedIndex == 0) ? Utils.PokemonList[data.Species.NatID - 1]
                                                            : Utils.PokemonList[data.Species.NatID - 1].Forms[cboFormList.SelectedIndex - 1];

            // Change minisprite
            pctMinisprite.Image = (cboFormList.SelectedIndex == 0) ? (Image)Properties.Resources.ResourceManager.GetObject("_" + data.Species.NatID)
                                                                  : (Image)Properties.Resources.ResourceManager.GetObject("_" + data.Species.NatID + "_" + cboFormList.SelectedIndex);
        }
    }
}
