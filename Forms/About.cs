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
    public partial class frmAbout : TranslatableForm
    {
        public frmAbout()
        {
            InitializeComponent();
            renameControls();
            switch (Properties.Settings.Default.Language)
            {
                case 1:
                    lblAboutEN.Visible = true;
                    break;
                case 2:
                    lblAboutFR.Visible = true;
                    break;
                case 8:
                    lblAboutCHS.Visible = true;
                    break;
                case 9:
                    lblAboutCHT.Visible = true;
                    break;
                default: break;
            }
        }

        private void lklRepository_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/AngefloSH/PokemonEncCalc");
        }
    }
}
