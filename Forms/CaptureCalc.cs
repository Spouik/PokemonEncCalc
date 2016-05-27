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
    public partial class frmCaptureCalc : TranslatableForm
    {

        private List<string> balls;

        private string[][] capturePower = new string[][]{ 
            
           new string[] { "", "", "", "" },
           new string[] { "No Capture Power", "Capture Power Lv.1", "Capture Power Lv.2", "Capture Power Lv.3" },
           new string[] { "Sans Aura Capture", "Aura Capture N.1", "Aura Capture N.2", "Aura Capture N.3" },
           new string[] { "", "", "", "" },
           new string[] { "", "", "", "" },
           new string[] { "", "", "", "" },
           new string[] { "", "", "", "" },
           new string[] { "", "", "", "" }

        };

        private string[][] status = new string[][]{

           new string[] { "", "", "", "", "", "" },
           new string[] { "-None-", "Paralysis", "Poisoned", "Burned", "Asleep", "Frozen" },
           new string[] { "-Aucun-", "Paralysie", "Poison", "Brûlure", "Sommeil", "Gel" },
           new string[] { "", "", "", "", "", "" },
           new string[] { "", "", "", "", "", "" },
           new string[] { "", "", "", "", "", "" },
           new string[] { "", "", "", "", "", "" },
           new string[] { "", "", "", "", "", "" }

        };

        private string[][] resultText = new string[][]{

           new string[] { "", "", "" },
           new string[] { "", "" },
           new string[] { "You have {0} % chance to catch {1} in a {2}.", "You have {3} % chance to get a critical capture", "A critical capture has {4} chances to be successful." },
           new string[] { "Vous avez {0} % de chances de capturer {1} dans une {2}.", "Vous avez {3} % de chances d'effectuer une capture critique", "En cas de capture critique, le Pokémon a {4} de chances de rentrer." },
           new string[] { "", "", "" },
           new string[] { "", "", "" },
           new string[] { "", "", "" },
           new string[] { "", "", "" }

        };

        public frmCaptureCalc()
        {
            InitializeComponent();
            renameControls();
            initializeComboboxes();
        }

        private void initializeComboboxes()
        {
            // cboPokemon
            cboPokemon.Items.AddRange(Utils.NamesCurrentLang.ToArray());
            cboPokemon.SelectedIndex = 0;            
            
            
            // cboGeneration
            for (int i = 3; i < 7; i++)
                cboGeneration.Items.Add(lblGeneration.Text + " " + i);
            cboGeneration.SelectedIndex = 0;



            // cboBalls
            balls = new List<string>();
            switch (Properties.Settings.Default.Language)
            {
                case 2:
                    balls.AddRange(Properties.Resources.ballsFR.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                default:
                    balls.AddRange(Properties.Resources.ballsEN.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                    break;
            }

            // Poké Ball, Great Ball and Ultra Ball alwys first, others are sorted alphabetically
            cboBall.Items.AddRange(balls.Where(s=>balls.IndexOf(s) < 3).ToArray());
            cboBall.Items.AddRange(balls.Where(s => (balls.IndexOf(s) > 2)).OrderBy(s=> s).ToArray());

            cboBall.SelectedIndex = 0;

            // Capture Power
            cboCapturePower.Items.AddRange(capturePower[Properties.Settings.Default.Language]);
            cboCapturePower.SelectedIndex = 0;

            // Status
            cboStatus.Items.AddRange(status[Properties.Settings.Default.Language]);
            cboStatus.SelectedIndex = 0;

        }

        private void cboPokemon_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCaptureRate.Text = Utils.PokemonList[cboPokemon.SelectedIndex].CatchRate.ToString();
            if (cboGeneration.SelectedIndex == 3 && rdORAS.Checked)
                txtCaptureRate.Text = Utils.PokemonList[cboPokemon.SelectedIndex].CatchRateORAS.ToString();

            pctPokemon.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + (cboPokemon.SelectedIndex + 1));

        }

        private void cboGeneration_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboGeneration.SelectedIndex)
            {
                case 0:
                case 1:
                    pnlGen5.Visible = pnlGen6Games.Visible = cboCapturePower.Visible = lblCapturePower.Visible = false;
                    txtCaptureRate.Text = Utils.PokemonList[cboPokemon.SelectedIndex].CatchRate.ToString();
                    break;
                case 2:
                    pnlGen5.Visible = cboCapturePower.Visible = lblCapturePower.Visible = true;
                    pnlGen6Games.Visible = false;
                    txtCaptureRate.Text = Utils.PokemonList[cboPokemon.SelectedIndex].CatchRate.ToString();
                    break;
                case 3:
                    pnlGen5.Visible = false;
                    pnlGen6Games.Visible = cboCapturePower.Visible = lblCapturePower.Visible = true;
                    if (rdORAS.Checked) txtCaptureRate.Text = Utils.PokemonList[cboPokemon.SelectedIndex].CatchRateORAS.ToString();
                    else txtCaptureRate.Text = Utils.PokemonList[cboPokemon.SelectedIndex].CatchRate.ToString();

                    break;
                default: break;
            }
        }

        private void rdXY_CheckedChanged(object sender, EventArgs e)
        {
            txtCaptureRate.Text = Utils.PokemonList[cboPokemon.SelectedIndex].CatchRate.ToString();
        }

        private void rdORAS_CheckedChanged(object sender, EventArgs e)
        {
            txtCaptureRate.Text = Utils.PokemonList[cboPokemon.SelectedIndex].CatchRateORAS.ToString();
        }

        private void cboBall_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ball ball = (Ball)(balls.FindIndex(s => s.Equals(cboBall.SelectedItem)));

            pctBall.Image = (Image)Properties.Resources.ResourceManager.GetObject("b" + (int)ball);

            pnlAlreadyCaught.Visible = pnlDive.Visible = pnlFished.Visible = pnlLevel.Visible = pnlLove.Visible 
            = pnlNest.Visible = pnlNightCave.Visible = pnlTimer.Visible = false;

            switch (ball)
            {
                case Ball.DiveBall: pnlDive.Visible = true; break;
                case Ball.DuskBall: pnlNightCave.Visible = true; break;
                case Ball.LevelBall: pnlLevel.Visible = true; break;
                case Ball.LoveBall: pnlLove.Visible = true; break;
                case Ball.LureBall: pnlFished.Visible = true; break;
                case Ball.NestBall: pnlNest.Visible = true; break;
                case Ball.RepeatBall:pnlAlreadyCaught.Visible = true; break;
                case Ball.TimerBall:
                case Ball.QuickBall:
                    pnlTimer.Visible = true; break;
                default: break;
            }
        }
    }
    
}
