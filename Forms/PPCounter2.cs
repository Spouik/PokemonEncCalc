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
    public partial class frmPPCounter2 : TranslatableForm
    {
        private Move move1;
        private Move move2;
        private Move move3;
        private Move move4;

        private int[] currentPP = new int[] { 0, 0, 0, 0 };
        private int[] maxPP = new int[] { 0, 0, 0, 0 };



        public frmPPCounter2()
        {
            InitializeComponent();
            renameControls();
        }

        internal void ShowDialog(List<Move> moveset, int generation)
        {
            move1 = moveset[0];
            if (moveset.Count > 1) move2 = moveset[1];
            if (moveset.Count > 2) move3 = moveset[2];
            if (moveset.Count > 3) move4 = moveset[3];

            // Moves PP
            currentPP[0] = maxPP[0] = move1.PP;
            currentPP[1] = maxPP[1] = move2 == null ? 0 : move2.PP;
            currentPP[2] = maxPP[2] = move3 == null ? 0 : move3.PP;
            currentPP[3] = maxPP[3] = move4 == null ? 0 : move4.PP;

            // Move names
            switch (generation)
            {
                case 3:
                    lblMove1.Text = Utils.moveNamesGen3[Utils.moveListGen3.IndexOf(move1)];
                    lblMove2.Text = move2 == null ? "-----" : Utils.moveNamesGen3[Utils.moveListGen3.IndexOf(move2)];
                    lblMove3.Text = move3 == null ? "-----" : Utils.moveNamesGen3[Utils.moveListGen3.IndexOf(move3)];
                    lblMove4.Text = move4 == null ? "-----" : Utils.moveNamesGen3[Utils.moveListGen3.IndexOf(move4)];
                    break;

                case 4:
                    lblMove1.Text = Utils.moveNamesGen4[Utils.moveListGen4.IndexOf(move1)];
                    lblMove2.Text = move2 == null ? "-----" : Utils.moveNamesGen4[Utils.moveListGen4.IndexOf(move2)];
                    lblMove3.Text = move3 == null ? "-----" : Utils.moveNamesGen4[Utils.moveListGen4.IndexOf(move3)];
                    lblMove4.Text = move4 == null ? "-----" : Utils.moveNamesGen4[Utils.moveListGen4.IndexOf(move4)];
                    break;
                case 5:
                    lblMove1.Text = Utils.moveNamesGen5[Utils.moveListGen5.IndexOf(move1)];
                    lblMove2.Text = move2 == null ? "-----" : Utils.moveNamesGen5[Utils.moveListGen5.IndexOf(move2)];
                    lblMove3.Text = move3 == null ? "-----" : Utils.moveNamesGen5[Utils.moveListGen5.IndexOf(move3)];
                    lblMove4.Text = move4 == null ? "-----" : Utils.moveNamesGen5[Utils.moveListGen5.IndexOf(move4)];
                    break;
                case 6:
                    lblMove1.Text = Utils.moveNamesGen6[Utils.moveListGen6.IndexOf(move1)];
                    lblMove2.Text = move2 == null ? "-----" : Utils.moveNamesGen6[Utils.moveListGen6.IndexOf(move2)];
                    lblMove3.Text = move3 == null ? "-----" : Utils.moveNamesGen6[Utils.moveListGen6.IndexOf(move3)];
                    lblMove4.Text = move4 == null ? "-----" : Utils.moveNamesGen6[Utils.moveListGen6.IndexOf(move4)];
                    break;
                default:
                    break;
            }

            update();
            ShowDialog();
        }


        private void update()
        {
            lblWarning1.Visible = lblWarning2.Visible = false;

            lblMove1CurrentPP.Text = currentPP[0].ToString();
            lblMove2CurrentPP.Text = currentPP[1].ToString();
            lblMove3CurrentPP.Text = currentPP[2].ToString();
            lblMove4CurrentPP.Text = currentPP[3].ToString();
            lblMove1MaxPP.Text = maxPP[0].ToString();
            lblMove2MaxPP.Text = maxPP[1].ToString();
            lblMove3MaxPP.Text = maxPP[2].ToString();
            lblMove4MaxPP.Text = maxPP[3].ToString();

            // change label colors
            lblMove1CurrentPP.ForeColor = lblMove1MaxPP.ForeColor = lblSeparator1.ForeColor =
            lblMove2CurrentPP.ForeColor = lblMove2MaxPP.ForeColor = lblSeparator2.ForeColor =
            lblMove3CurrentPP.ForeColor = lblMove3MaxPP.ForeColor = lblSeparator3.ForeColor =
            lblMove4CurrentPP.ForeColor = lblMove4MaxPP.ForeColor = lblSeparator4.ForeColor = Color.Black;

            if (currentPP[0] <= maxPP[0] / 2)
                lblMove1CurrentPP.ForeColor = lblMove1MaxPP.ForeColor = lblSeparator1.ForeColor = Color.OrangeRed;
            if (currentPP[0] == 0)
                lblMove1CurrentPP.ForeColor = lblMove1MaxPP.ForeColor = lblSeparator1.ForeColor = Color.Red;

            if (currentPP[1] <= maxPP[1] / 2)
                lblMove2CurrentPP.ForeColor = lblMove2MaxPP.ForeColor = lblSeparator2.ForeColor = Color.OrangeRed;
            if (currentPP[1] == 0)
                lblMove2CurrentPP.ForeColor = lblMove2MaxPP.ForeColor = lblSeparator2.ForeColor = Color.Red;

            if (currentPP[2] <= maxPP[2] / 2)
                lblMove3CurrentPP.ForeColor = lblMove3MaxPP.ForeColor = lblSeparator3.ForeColor = Color.OrangeRed;
            if (currentPP[2] == 0)
                lblMove3CurrentPP.ForeColor = lblMove3MaxPP.ForeColor = lblSeparator3.ForeColor = Color.Red;

            if (currentPP[3] <= maxPP[3] / 2)
                lblMove4CurrentPP.ForeColor = lblMove4MaxPP.ForeColor = lblSeparator4.ForeColor = Color.OrangeRed;
            if (currentPP[3] == 0)
                lblMove4CurrentPP.ForeColor = lblMove4MaxPP.ForeColor = lblSeparator4.ForeColor = Color.Red;

            // PP sum (message when PP are low)
            if (currentPP.Sum() == 0) lblWarning2.Visible = true;
            else if (currentPP.Sum() <= 5) lblWarning1.Visible = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentPP[0] > 0) currentPP[0]--;
            update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentPP[1] > 0) currentPP[1]--;
            update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (currentPP[2] > 0) currentPP[2]--;
            update();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (currentPP[3] > 0) currentPP[3]--;
            update();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (currentPP[0] < maxPP[0]) currentPP[0]++;
            update();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (currentPP[1] < maxPP[1]) currentPP[1]++;
            update();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (currentPP[2] < maxPP[2]) currentPP[2]++;
            update();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (currentPP[3] < maxPP[3]) currentPP[3]++;
            update();
        }
    }
}
