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
    public partial class frmHoneyCuteCharm : TranslatableForm
    {

        private List<string> honeyTrees;

        private string[] errorIDs = { "",
                                    "Trainer ID and Secret ID both must be between 0 and 65535",
                                    "L'ID Dresseur et l'ID Secret doivent être compris entre 0 et 65535",
                                    "",
                                    "",
                                    "",
                                    "",
                                    ""};

        public frmHoneyCuteCharm()
        {
            InitializeComponent();
            renameControls();

            honeyTrees = new List<string>();

            switch (Properties.Settings.Default.Language)
            {
                case 1:
                    honeyTrees.AddRange(Properties.Resources.honeyTreesLocationsEN.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                case 2:
                    honeyTrees.AddRange(Properties.Resources.honeyTreesLocationsFR.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                default:
                    honeyTrees.AddRange(Properties.Resources.honeyTreesLocationsEN.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    break;

            }

        }


        private int cuteCharmShiny(int startingPID, int TID, int SID)
        {
            int TSV = (TID ^ SID) / 8;
            int n = 0;
            for(int i = startingPID; i < startingPID + 25; i++)
            {
                int PSV = ((i % 65536) ^ (i / 65536)) / 8;
                if (PSV == TSV) n++;
            }
            return n;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            int TID = txtTrainer.Text == "" ? 0 : int.Parse(txtTrainer.Text);
            int SID = txtSecret.Text == "" ? 0 : int.Parse(txtSecret.Text);
            if(TID > 65535 || SID > 65535)
            {
                MessageBox.Show(errorIDs[Properties.Settings.Default.Language], "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Cute Charm Calc

            int n = 0;
            decimal resultCC = 0;

            // Male Cute Charm. PID range: 0~24
            n = cuteCharmShiny(0, TID, SID);
            resultCC = 2 * n / 75m + 1 / 24576m;
            lblCuteCharmM.Text = resultCC > 0.01m ? Math.Floor(10000 * resultCC) / 100 + " %" : "1/" + Math.Round(1 / resultCC);

            // Female Cute Charm (87.5M / 12.5F). PID range: 50~74
            n = cuteCharmShiny(50, TID, SID);
            resultCC = 2 * n / 75m + 1 / 24576m;
            lblCuteCharmF1.Text = resultCC > 0.01m ? Math.Floor(10000 * resultCC) / 100 + " %" : "1/" + Math.Round(1 / resultCC);

            // Female Cute Charm (75M / 25F). PID range: 75~99
            n = cuteCharmShiny(75, TID, SID);
            resultCC = 2 * n / 75m + 1 / 24576m;
            lblCuteCharmF2.Text = resultCC > 0.01m ? Math.Floor(10000 * resultCC) / 100 + " %" : "1/" + Math.Round(1 / resultCC);

            // Female Cute Charm (50M / 50F). PID range: 150~174
            n = cuteCharmShiny(150, TID, SID);
            resultCC = 2 * n / 75m + 1 / 24576m;
            lblCuteCharmF3.Text = resultCC > 0.01m ? Math.Floor(10000 * resultCC) / 100 + " %" : "1/" + Math.Round(1 / resultCC);

            // Female Cute Charm (25M / 75F). PID range: 200~224
            n = cuteCharmShiny(200, TID, SID);
            resultCC = 2 * n / 75m + 1 / 24576m;
            lblCuteCharmF4.Text = resultCC > 0.01m ? Math.Floor(10000 * resultCC) / 100 + " %" : "1/" + Math.Round(1 / resultCC);

            // Honey tree calc

            int tree1, tree2, tree3, tree4;
            tree1 = SID / 256;
            tree2 = SID % 256;
            tree3 = TID / 256;
            tree4 = TID % 256;

            tree1 = tree1 % 21;

            tree2 = tree2 % 21;
            while(tree2 == tree1)
                tree2 = (tree2 + 1) % 21;

            tree3 = tree3 % 21;
            while (tree3 == tree1 || tree3 == tree2)
                tree3 = (tree3 + 1) % 21;

            tree4 = tree4 % 21;
            while (tree4 == tree1 || tree4 == tree2 || tree4 == tree3)
                tree4 = (tree4 + 1) % 21;

            // Display Results
            lblMunchlaxResults.Text = honeyTrees[tree1] + Environment.NewLine + Environment.NewLine
                                    + honeyTrees[tree2] + Environment.NewLine + Environment.NewLine
                                    + honeyTrees[tree3] + Environment.NewLine + Environment.NewLine
                                    + honeyTrees[tree4];
        }
    }
}
