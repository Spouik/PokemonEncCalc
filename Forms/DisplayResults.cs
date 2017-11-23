using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PokemonEncCalc
{
    public partial class frmDisplayResults : TranslatableForm
    {

        List<EncounterSlot> data;
        string encounterInfo;
        decimal repel;
        bool cuteCharm;


        public frmDisplayResults()
        {
            InitializeComponent();
        }

        internal DialogResult ShowDialog(List<EncounterSlot> slots, string info, bool cuteCharmActive)
        {
            data = Utils.normalizeSlots(slots);
            repel = slots.Sum(s => s.Percentage);
            encounterInfo = info;
            cuteCharm = cuteCharmActive;
            return ShowDialog();
        }

        private void frmDisplayResults_Load(object sender, EventArgs e)
        {
            renameControls();
            // Create objects to display pokémon sprites with percentages
            int nbSlots = data.Count;
            int parentWidth = pnlResults.Width;
            int row = 0, column = 0;
            foreach(EncounterSlot slot in data)
            {
                Panel p = new Panel();
                p.Size = new Size(100, 120);
                PictureBox pct = new PictureBox();
                pct.Size = new Size(96, 96);
                pct.SizeMode = PictureBoxSizeMode.Zoom;
                pct.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + (slot.Species.NatID) + (Properties.Settings.Default.ShinySprites ? "s" : "") + (slot.Species.Form != 0 ? ("_" + slot.Species.Form) : ""));
                Label l = new Label();
                l.Size = new Size(96, 20);
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Text = (Math.Round(slot.EffectivePercentage * 100) / 100).ToString() + " %";
                p.Controls.Add(pct);
                p.Controls.Add(l);
                pct.Location = new Point(2, 2);
                l.Location = new Point(2, 98);
                int a = 40;
                int x = (row + 1) * 4 < nbSlots ? 4 : ((nbSlots - 1) % 4) + 1;
                //int y = nbSlots / 4;
                int b = (parentWidth - (x * 100 + (x - 1) * a)) / 2;
                int d = nbSlots <= 12 ? (pnlResults.Height - (((nbSlots-1) / 4 + 1) * 120 + ((nbSlots-1) / 4) * 40)) / 2 : 20;
                p.Location = new Point(b + column * (100 + a), d + row * (160));
                pnlResults.Controls.Add(p);
                column++;
                if(column == 4) { column = 0; row++; }
            }

            repelDisp();
            cuteCharmDisp();
            lblInfo.Text = encounterInfo;
        }

        private void repelDisp()
        {
            decimal percent = Math.Round(repel * 100) / 100;
            if (percent >= 100) return;
            lblRepel.Text += " " + percent + " %";
            pbarRepel.Value = (int)Math.Round(percent * 10);
            lblRepel.Visible = true;
            pbarRepel.Visible = true;
        }

        private void cuteCharmDisp()
        {
            if (!cuteCharm) return;
            int odds = Utils.cuteCharmExpectation(data);
            pbarCuteCharm.Value = odds == 0 ? 0 : (24576 - odds) * 1000 / 16384;
            lblCuteCharm.Text += odds == 0 ? " 0" : " 1/" + odds;
            lblCuteCharm.Visible = true;
            pbarCuteCharm.Visible = true;
        }
    }
}
