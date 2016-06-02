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
    public partial class frmPPCounter : TranslatableForm
    {
        private string[] error = new string[]
        {
            "",
            "Please select at least one move.",
            "Veuillez sélectionner au moins une capacité.",
            "",
            "",
            "",
            "",
            ""
        };

        public frmPPCounter()
        {
            InitializeComponent();
            renameControls();
        }

        private void frmPPCounter_Load(object sender, EventArgs e)
        {
            foreach(object o in tabpGen3.Controls)
            {
                if (!(o is ComboBox)) continue;

                ComboBox b = (ComboBox)o;
                b.Items.Add("-----");
                b.Items.AddRange(Utils.moveNamesGen3.ToArray());
                b.SelectedIndex = 0;
            }

            foreach (object o in tabpGen4.Controls)
            {
                if (!(o is ComboBox)) continue;

                ComboBox b = (ComboBox)o;
                b.Items.Add("-----");
                b.Items.AddRange(Utils.moveNamesGen4.ToArray());
                b.SelectedIndex = 0;
            }

            foreach (object o in tabpGen5.Controls)
            {
                if (!(o is ComboBox)) continue;

                ComboBox b = (ComboBox)o;
                b.Items.Add("-----");
                b.Items.AddRange(Utils.moveNamesGen5.ToArray());
                b.SelectedIndex = 0;
            }

            foreach (object o in tabpGen6.Controls)
            {
                if (!(o is ComboBox)) continue;

                ComboBox b = (ComboBox)o;
                b.Items.Add("-----");
                b.Items.AddRange(Utils.moveNamesGen6.ToArray());
                b.SelectedIndex = 0;
            }
        }

        private void cmdGO_Click(object sender, EventArgs e)
        {
            List<Move> moveset = new List<Move>();
            int gen = 0;
            if (tabGenSelect.SelectedIndex == 0)
            {
                foreach(Control c in tabpGen3.Controls)
                {
                    if (!(c is ComboBox)) continue;
                    ComboBox b = (ComboBox)c;
                    if (b.SelectedIndex != 0)
                        moveset.Add(Utils.moveListGen3[Utils.moveNamesGen3.FindIndex(s => s==b.SelectedItem.ToString())]);
                }
                gen = 3;
            }
            if (tabGenSelect.SelectedIndex == 1)
            {
                foreach (Control c in tabpGen4.Controls)
                {
                    if (!(c is ComboBox)) continue;
                    ComboBox b = (ComboBox)c;
                    if (b.SelectedIndex != 0)
                        moveset.Add(Utils.moveListGen4[Utils.moveNamesGen4.FindIndex(s => s == b.SelectedItem.ToString())]);
                }
                gen = 4;
            }
            if (tabGenSelect.SelectedIndex == 2)
            {
                foreach (Control c in tabpGen5.Controls)
                {
                    if (!(c is ComboBox)) continue;
                    ComboBox b = (ComboBox)c;
                    if (b.SelectedIndex != 0)
                        moveset.Add(Utils.moveListGen5[Utils.moveNamesGen5.FindIndex(s => s == b.SelectedItem.ToString())]);
                }
                gen = 5;
            } 
            if (tabGenSelect.SelectedIndex == 3)
            {
                foreach (Control c in tabpGen6.Controls)
                {
                    if (!(c is ComboBox)) continue;
                    ComboBox b = (ComboBox)c;
                    if (b.SelectedIndex != 0)
                        moveset.Add(Utils.moveListGen6[Utils.moveNamesGen6.FindIndex(s => s == b.SelectedItem.ToString())]);
                }
                gen = 6;
            }

            if(moveset.Count == 0)
            {
                MessageBox.Show(error[Properties.Settings.Default.Language], "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmPPCounter2 f = new frmPPCounter2();
            f.ShowDialog(moveset, gen);

        }
    }
}
