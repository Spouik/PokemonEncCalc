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



    }
}
