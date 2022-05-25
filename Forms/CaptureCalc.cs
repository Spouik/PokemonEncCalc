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
        private List<Pokemon> PokemonList;
        private int generation = 0;

        private string[][] capturePower = new string[][]{ 
            
           new string[] { "", "", "", "" },
           new string[] { "No Capture Power", "Capture Power Lv.1", "Capture Power Lv.2", "Capture Power Lv.3" },
           new string[] { "Sans Aura Capture", "Aura Capture N.1", "Aura Capture N.2", "Aura Capture N.3" },
           new string[] { "", "", "", "" },
           new string[] { "", "", "", "" },
           new string[] { "", "", "", "" },
           new string[] { "", "", "", "" },
           new string[] { "", "", "", "" },
           new string[] { "无捕获之力", "捕获之力Lv.1", "捕获之力Lv.2", "捕获之力Lv.3" },
           new string[] { "無捕獲之力", "捕獲之力Lv.1", "捕獲之力Lv.2", "捕獲之力Lv.3" }

        };

        private string[][] status = new string[][]{

           new string[] { "", "", "", "", "", "" },
           new string[] { "-None-", "Paralysis", "Poisoned", "Burned", "Asleep", "Frozen" },
           new string[] { "-Aucun-", "Paralysie", "Poison", "Brûlure", "Sommeil", "Gel" },
           new string[] { "", "", "", "", "", "" },
           new string[] { "", "", "", "", "", "" },
           new string[] { "", "", "", "", "", "" },
           new string[] { "", "", "", "", "", "" },
           new string[] { "", "", "", "", "", "" },
           new string[] { "无", "麻痹", "中毒", "灼伤", "睡眠", "冰冻" },
           new string[] { "無", "麻痺", "中毒", "灼傷", "睡眠", "冰凍" }

        };

        private string[][] messageBoxes = new string[][] {
           new string[] { "", "", "" },
           new string[] { "{0} is not available in generation {1}", "The {0} is not available in generation {1}", "" },
           new string[] { "{0} n'est pas disponible en génération {1}", "La {0} n'est pas disponible en génération {1}", "" },
           new string[] { "", "", "" },
           new string[] { "", "", "" },
           new string[] { "", "", "" },
           new string[] { "", "", "" },
           new string[] { "", "", "" },
           new string[] { "{0}在第{1}世代不可用", "这个{0}在第{1}世代不可用", "" },
           new string[] { "{0}在第{1}世代不可用", "這個{0}在第{1}世代不可用", "" },

        };

        private string[] oras = new string[] { "", "ORAS", "ROSA", "", "", "", "", "", "ORAS", "ORAS" };
        private string[] sunmoon = new string[] { "", "SM", "SL", "", "", "", "", "", "SM", "SM" };
        private string[] ultra = new string[] { "", "USUM", "USUL", "", "", "", "", "", "USUM", "USUM" };
        private int[] ultrabeasts = new int[] { 793, 794, 795, 796, 797, 798, 799, 803, 804, 805, 806 };

        public frmCaptureCalc()
        {
            InitializeComponent();
            chkUltra.Text = sunmoon[Properties.Settings.Default.Language];
            renameControls();
            InitializeComboboxes();
        }

        private void InitializeComboboxes()
        {
            balls = new List<string>();
            PokemonList = new List<Pokemon>();

            // Balls
            switch (Properties.Settings.Default.Language)
            {
                case 2:
                    balls.AddRange(Properties.Resources.ballsFR.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                case 8:
                    balls.AddRange(Properties.Resources.ballsCHS.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                case 9:
                    balls.AddRange(Properties.Resources.ballsCHT.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
                    break;
                default:
                    balls.AddRange(Properties.Resources.ballsEN.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
                    break;
            }


            // cboGeneration
            for (int i = 3; i <= 7; i++)
                cboGeneration.Items.Add(lblGeneration.Text + " " + i);
            cboGeneration.SelectedIndex = 0;


            // Capture Power
            cboCapturePower.Items.AddRange(capturePower[Properties.Settings.Default.Language]);
            cboCapturePower.SelectedIndex = 0;

            // Status
            cboStatus.Items.AddRange(status[Properties.Settings.Default.Language]);
            cboStatus.SelectedIndex = 0;

        }

        /// <summary>
        /// Refreshes the list of Pokémon and Balls available in the selected generation.
        /// </summary>
        private void RefreshComboboxes()
        {
            // Pokémon List
            int p = cboPokemon.SelectedIndex;
            if (cboPokemon.Items.Count == 0) p = 0;
            cboPokemon.Items.Clear();
            PokemonList.Clear();
            switch (generation)
            {
                case 3:
                    PokemonList.AddRange(PokemonTables.pokemonEmeraldTable);
                    break;
                case 4:
                    PokemonList.AddRange(PokemonTables.pokemonHGSSTable);
                    break;
                case 5:
                    PokemonList.AddRange(PokemonTables.pokemonB2W2Table);
                    break;
                case 6:
                    if(chkORAS.Checked)
                        PokemonList.AddRange(PokemonTables.pokemonORASTable);
                    else
                        PokemonList.AddRange(PokemonTables.pokemonXYTable);
                    break;
                case 7:
                    if (chkUltra.Checked)
                        PokemonList.AddRange(PokemonTables.pokemonUSUMTable);
                    else
                        PokemonList.AddRange(PokemonTables.pokemonSuMoTable);
                    break;
                default: break;
            }
            switch (Properties.Settings.Default.Language)
            {
                case 2:
                    cboPokemon.Items.AddRange(PokemonList.Where(i => i.NatID != 0).Select(s => s.NameFR).ToArray());
                    break;
                case 8:
                    cboPokemon.Items.AddRange(PokemonList.Where(i => i.NatID != 0).Select(s => s.NameCHS).ToArray());
                    break;
                case 9:
                    cboPokemon.Items.AddRange(PokemonList.Where(i => i.NatID != 0).Select(s => s.NameCHT).ToArray());
                    break;
                default:
                    cboPokemon.Items.AddRange(PokemonList.Where(i => i.NatID != 0).Select(s => s.NameEN).ToArray());
                    break;
            }
            cboPokemon.SelectedIndex = cboPokemon.Items.Count > p ? p : cboPokemon.Items.Count - 1;

            // Balls
            int bs = 0;
            if(cboBall.SelectedIndex != -1)
                bs = balls.FindIndex(s => s == cboBall.SelectedItem.ToString());

            List<int> availableBalls = new List<int>();
            switch (generation)
            {
                case 3:
                    availableBalls.AddRange(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
                    break;
                case 4:
                    availableBalls.AddRange(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 });
                    break;
                case 5:
                case 6:
                    availableBalls.AddRange(new[] { 0, 1, 2, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 });
                    break;
                case 7:
                    availableBalls.AddRange(new[] { 0, 1, 2, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22 });
                    break;
            }
            cboBall.Items.Clear();
            List<string> b = new List<string>();
            foreach(int i in availableBalls) b.Add(balls[i]);

            // PokéBall, GreatBall and UltraBall always first, others sorted alphabetically
            cboBall.Items.AddRange(b.Where(s => b.IndexOf(s) < 3).ToArray());
            cboBall.Items.AddRange(b.Where(s => (b.IndexOf(s) > 2)).OrderBy(s => s).ToArray());

            cboBall.SelectedItem = cboBall.Items.Contains(balls[bs]) ? balls[bs] : cboBall.Items[0];
        }

        private void cboPokemon_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtCaptureRate.Text = PokemonList[cboPokemon.SelectedIndex + 1].CatchRate.ToString();

            pctPokemon.Image = (Image)Properties.Resources.ResourceManager.GetObject("m" + (cboPokemon.SelectedIndex + 1) 
                + (Properties.Settings.Default.ShinySprites ? "s" : ""));

        }

        private void cboGeneration_SelectedIndexChanged(object sender, EventArgs e)
        {
            generation = cboGeneration.SelectedIndex + 3;
            RefreshComboboxes();
            pnlGen5.Visible = chkORAS.Visible = chkUltra.Visible = cboCapturePower.Visible = lblCapturePower.Visible = pnlPokedexGen6.Visible = false;
            switch (generation)
            {
                case 5:
                    pnlGen5.Visible = cboCapturePower.Visible = lblCapturePower.Visible = true;
                    break;
                case 6:
                    chkORAS.Visible = cboCapturePower.Visible = lblCapturePower.Visible = pnlPokedexGen6.Visible = true;
                    break;
                case 7:
                    pnlPokedexGen6.Visible = true;
                    chkUltra.Visible = true;
                    break;
                default: break;
            }
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

        private void cmdGO_Click(object sender, EventArgs e)
        {
            Ball ball = (Ball)(balls.FindIndex(s => s.Equals(cboBall.SelectedItem)));
            Ball[] kurtBalls = new Ball[] {Ball.FastBall, Ball.FriendBall, Ball.HeavyBall, Ball.LevelBall,
                                            Ball.LoveBall, Ball.LureBall, Ball.MoonBall};

            Ball[] gen4Balls = new Ball[] { Ball.DuskBall, Ball.QuickBall, Ball.HealBall };

            // Get standard information for capture calculation
            int catchRate = int.Parse(txtCaptureRate.Text);
            decimal ballBonus = 1;
            decimal statusBonus = 1;
            int currentHP = int.Parse(txtCurrentHP.Text);
            int maxHP = int.Parse(txtMaxHP.Text);
            Pokemon p = PokemonList[cboPokemon.SelectedIndex + 1];

            // Get specific informaton for capture calculation
            int pokedex = 0;
            decimal capturePower = 1;
            decimal darkGrass = 1;
            decimal criticalCatch = 0;

            pnlResult3_4.Visible = pnlResult5_6.Visible = false;

            // Specific capture modifiers (gen 5 & 6)
            // Capture power & Pokedex
            if (generation == 5)
            {    // Generation 5
                capturePower = (new[] { 1m, 1.1m, 1.2m, 1.3m })[cboCapturePower.SelectedIndex];
                pokedex = (int)nudPokedexGen5.Value;
            }
            if (generation == 6)
            {    // Generation 6
                capturePower = (new[] { 1m, 1.5m, 2m, 2.5m })[cboCapturePower.SelectedIndex];
                pokedex = (int)nudPokedexGen6.Value;
            }
            if (generation == 7) //Generation 7 (no capture power)
                pokedex = (int)nudPokedexGen6.Value;

            // Dark Grass (gen 5)
            if(generation == 5 && chkDarkGrass.Checked)
            {
                darkGrass = 1229m/4096m;
                if (pokedex > 30) darkGrass = 2048m / 4096m;
                if (pokedex > 150) darkGrass = 2867m / 4096m;
                if (pokedex > 300) darkGrass = 3277m / 4096m;
                if (pokedex > 450) darkGrass = 3686m / 4096m;
                if (pokedex > 600) darkGrass = 1m;
            }

            // Critical capture (generation 5 onwards)
            if(generation >= 5)
            {
                if (pokedex > 30) criticalCatch = 0.5m;
                if (pokedex > 150) criticalCatch = 1m;
                if (pokedex > 300) criticalCatch = 1.5m;
                if (pokedex > 450) criticalCatch = 2m;
                if (pokedex > 600) criticalCatch = 2.5m;
            }


            // Apply ball modifiers
            switch (ball)
            {
                case Ball.DiveBall:
                    if (rdUnderWater.Checked) ballBonus = 3.5m;
                    break;

                case Ball.DuskBall:
                    if (rdNightCave.Checked) ballBonus = generation == 7 ? 3 : 3.5m;
                    break;

                case Ball.FastBall:
                    if (p.Spe >= 100)
                    {
                        if (generation == 4) catchRate *= 4;
                        if (generation == 7) ballBonus = 4;
                    }
                    break;

                case Ball.GreatBall:
                case Ball.SafariBall:
                case Ball.SportBall:
                    ballBonus = 1.5m;
                    break;

                case Ball.HeavyBall:
                    // Apply catchRate modifiers
                    if (generation == 4)
                    {
                        if (p.Weight < 2048) catchRate -= 20; // Should apply if weight < 102.4kg, but actually applies to < 204.8kg due to a bug.
                        if (p.Weight >= 2048) catchRate += 20;
                        if (p.Weight >= 3072) catchRate += 10; // actually +30, as the condition above is true if this one is true
                        if (p.Weight >= 4096) catchRate += 10; // actually +40, for the same reason as above
                    }
                    if(generation == 7)
                    {
                        if (p.Weight < 1000) catchRate -= 20;
                        if (p.Weight > 2000) catchRate += 20;
                        if (p.Weight > 3000) catchRate += 10; // actually +30, as the condition above is true if this one is true
                    }
                    break;

                case Ball.LevelBall:
                    decimal levelDiff = nudLevelOwn.Value / nudLevelWild.Value;
                    if (generation == 4)
                    {
                        if (levelDiff > 1) catchRate *= 2;
                        if (levelDiff > 2) catchRate *= 2; // *4 if 2 < levelDiff <= 4 (as condition above is always true if this one is true)
                        if (levelDiff > 4) catchRate *= 2; // *8 if levelDiff > 4
                    }
                    if (generation == 7)
                    {
                        if (levelDiff > 1) ballBonus = 2;
                        if (levelDiff > 2) ballBonus *= 2; // *4 if 2 < levelDiff <= 4 (as condition above is always true if this one is true)
                        if (levelDiff > 4) ballBonus *= 2; // *8 if levelDiff > 4
                    }
                    break;

                case Ball.LoveBall:
                    if (rdLove.Checked)
                    {
                        if (generation == 4) catchRate *= 8;
                        if (generation == 7) ballBonus = 8;
                    }
                    break;

                case Ball.LureBall:
                    if (rdFished.Checked)
                    {
                        if (generation == 4) catchRate *= 3;
                        if (generation == 7) ballBonus = 5;
                    }
                    break;

                case Ball.MoonBall:
                    if (new[] { 29, 30, 31, 32, 33, 34, 35, 36, 39, 40, 300, 301, 517, 518 }.Contains(p.NatID))
                    {
                        if (generation == 4) catchRate *= 4;
                        if (generation == 7) ballBonus = 4;
                    }
                    break;

                case Ball.NestBall:
                    int a = generation <= 4 ? 40 : 41;
                    ballBonus = ((a - (int)nudNest.Value)) / 10;
                    if (generation == 7) ballBonus = (8 - 0.2m * ((int)nudNest.Value - 1));
                    if (ballBonus < 1) ballBonus = 1;
                    break;

                case Ball.NetBall:
                    if (p.Type1 == Type.Bug || p.Type2 == Type.Bug || p.Type1 == Type.Water || p.Type2 == Type.Water)
                        ballBonus = (generation == 7) ? 3.5m : 3;
                    break;

                case Ball.QuickBall:
                    if (nudTurn.Value == 1) ballBonus = generation == 4 ? 4 : 5; // x4 if gen 4, x5 otherwise          
                    break;

                case Ball.RepeatBall:
                    if (rdAlreadyCaught.Checked) ballBonus = (generation == 7) ? 3.5m : 3;
                    break;

                case Ball.TimerBall:
                    int turnsPassed = (int)nudTurn.Value - 1;
                    if (generation <= 4) ballBonus = (turnsPassed + 10) / 10;
                    if (generation == 5) ballBonus = (decimal)turnsPassed * 1229 / 4096 + 1;
                    if (generation >= 6) ballBonus = 1 + 0.3m * turnsPassed;
                    if (ballBonus > 4) ballBonus = 4;
                    break;

                case Ball.UltraBall:
                    ballBonus = 2;
                    break;

                case Ball.BeastBall:
                    ballBonus = ultrabeasts.Contains(p.NatID) ? 5 : 410m / 4096m;
                    break;

                default:
                    ballBonus = 1;
                    break;
            }
            // Check Ultra Beasts with ordinary balls
            if (ultrabeasts.Contains(p.NatID) && ball != Ball.BeastBall) ballBonus = 410m / 4096m;
            // Checks catchRate value (should be between 1 and 255)
            if (generation == 7)
            {
                if (catchRate <= 0) catchRate = chkUltra.Checked ? 1 : 0;
            }
            else
            {
                if (catchRate < 0) catchRate = 1; // Does not apply if catchRate == 0 (due to a bug), but as no Pokémon can have a catchRate of 20, this never happens.
            }

            if (catchRate > 255) catchRate = 255;

            // status modifier
            if (cboStatus.SelectedIndex > 0) statusBonus = 1.5m;
            if (cboStatus.SelectedIndex > 3) statusBonus = generation > 4 ? 2.5m : 2;

            // Probability calculation
            decimal pr;
            decimal cc;
            decimal ccCapture;
            if (generation <= 4)
            {
                pr = CatchCalcGen3_4(catchRate, ballBonus, statusBonus, currentHP, maxHP);
                lblCaptureResult3.Text = Math.Round(10000 * pr) / 100 + " %";
                pnlResult3_4.Visible = true;
            }
            if(generation == 5)
            {
                pr = CatchCalcGen5(catchRate, ballBonus, statusBonus, currentHP, maxHP, darkGrass, capturePower);
                cc = criticalChanceGen5_6(catchRate, ballBonus, statusBonus, currentHP, maxHP, darkGrass, capturePower, criticalCatch);
                ccCapture = CriticalCalcGen5(catchRate, ballBonus, statusBonus, currentHP, maxHP, darkGrass, capturePower);
                lblCaptureResult5.Text = Math.Round(10000 * pr) / 100 + " %";
                lblCriticalResult.Text = Math.Round(10000 * cc) / 100 + " %";
                lblCriticalCaptureResult.Text = Math.Round(10000 * ccCapture) / 100 + " %";
                pnlResult5_6.Visible = true;

            }
            if (generation >= 6)
            {
                if (generation == 7) capturePower = 1;
                pr = catchCalcGen6(catchRate, ballBonus, statusBonus, currentHP, maxHP, capturePower);
                cc = criticalChanceGen5_6(catchRate, ballBonus, statusBonus, currentHP, maxHP, 1, capturePower, criticalCatch);
                ccCapture = criticalCalcGen6(catchRate, ballBonus, statusBonus, currentHP, maxHP, capturePower);
                lblCaptureResult5.Text = Math.Round(10000 * pr) / 100 + " %";
                lblCriticalResult.Text = Math.Round(10000 * cc) / 100 + " %";
                lblCriticalCaptureResult.Text = Math.Round(10000 * ccCapture) / 100 + " %";
                pnlResult5_6.Visible = true;
            }

        }

        private decimal CatchCalcGen3_4(int catchRate, decimal bonusBall, decimal bonusStatus, int currentHP, int maxHP)
        {
            decimal a = Math.Floor(Math.Floor((3 * maxHP - 2 * currentHP) * Math.Floor(catchRate * bonusBall) / (3 * maxHP)) * bonusStatus);
            if (a >= 255) return 1; // Capture guaranteed
            if (a == 0) return 0;

            decimal b = Math.Floor(1048560 / (decimal)Math.Floor(Math.Sqrt((double)Math.Floor(Math.Sqrt((double)Math.Floor(16711680m / a))))));

            decimal r = b / 65536;
            for (int i = 0; i < 3; i++)
                r *= (b / 65536);

            return r;
        }

        // Round functions
        // Round to the nearest 1/4096th
        private decimal Round(decimal a)
        {
            return Math.Floor(4096 * a + 0.5m) / 4096;
        }

        // Round down to the nearest 1/4096th
        private decimal Down(decimal a)
        {
            return Math.Floor(4096 * a) / 4096;
        }

        private decimal CatchCalcGen5(int catchRate, decimal bonusBall, decimal bonusStatus, int currentHP, int maxHP, decimal darkGrass, decimal capturePower)
        {
            decimal a = Down(Round(Down(Round(Round((3 * maxHP - 2 * currentHP) * darkGrass) * catchRate * bonusBall) / (3 * maxHP)) * bonusStatus) * capturePower);
            if (a >= 255) return 1;
            if (a == 0) return 0;

            decimal b = Down(65536 / Round((decimal)Math.Sqrt((double)Round((decimal)Math.Sqrt((double)Round(255m / a))))));

            decimal result = b / 65536;
            for (int i = 0; i < 2; i++)
                result *= (b / 65536);

            return result;
        }

        private decimal CriticalCalcGen5(int catchRate, decimal bonusBall, decimal bonusStatus, int currentHP, int maxHP, decimal darkGrass, decimal capturePower)
        {
            decimal a = Down(Round(Down(Round(Round((3 * maxHP - 2 * currentHP) * darkGrass) * catchRate * bonusBall) / (3 * maxHP)) * bonusStatus) * capturePower);
            if (a >= 255) return 1;
            if (a == 0) return 0;

            decimal b = Down(65536 / Round((decimal)Math.Sqrt((double)Round((decimal)Math.Sqrt((double)Round(255m / a))))));

            return b / 65536;
        }

        private decimal catchCalcGen6(int catchRate, decimal bonusBall, decimal bonusStatus, int currentHP, int maxHP, decimal capturePower)
        {
            decimal a = Down(Round(Down(Round(Round((3 * maxHP - 2 * currentHP)) * catchRate * bonusBall) / (3 * maxHP)) * bonusStatus) * capturePower);
            if (a >= 255) return 1;
            if (a == 0) return 0;

            decimal b = Down(65536 / (decimal)Math.Pow((double)(255/ a), 3d/16d));

            decimal result = b / 65536;
            for (int i = 0; i < 2; i++)
                result *= (b / 65536);

            return result;
        }

        private decimal criticalCalcGen6(int catchRate, decimal bonusBall, decimal bonusStatus, int currentHP, int maxHP, decimal capturePower)
        {
            decimal a = Down(Round(Down(Round(Round((3 * maxHP - 2 * currentHP)) * catchRate * bonusBall) / (3 * maxHP)) * bonusStatus) * capturePower);
            if (a >= 255) return 1;
            if (a == 0) return 0;

            decimal b = Down(65536 / (decimal)Math.Pow((double)(255 / a), 3d / 16d));

            return b / 65536;
        }

        private decimal criticalChanceGen5_6(int catchRate, decimal bonusBall, decimal bonusStatus, int currentHP, int maxHP, decimal darkGrass, decimal capturePower, decimal criticalRate) 
        {
            decimal a = Down(Round(Down(Round(Round((3 * maxHP - 2 * currentHP) * darkGrass) * catchRate * bonusBall) / (3 * maxHP)) * bonusStatus) * capturePower);
            if (a > 255) a = 255;
            return Math.Min(1, Down(a * criticalRate / 6) / 256);
        }

        private void chkORAS_CheckedChanged(object sender, EventArgs e)
        {
            chkORAS.Text = chkORAS.Checked ? oras[Properties.Settings.Default.Language] : "XY";
            PokemonList.Clear();
            if (chkORAS.Checked) PokemonList.AddRange(PokemonTables.pokemonORASTable);
            else PokemonList.AddRange(PokemonTables.pokemonXYTable);
            txtCaptureRate.Text = PokemonList[cboPokemon.SelectedIndex + 1].CatchRate.ToString();
        }

        private void chkUltra_CheckedChanged(object sender, EventArgs e)
        {
            int p = cboPokemon.SelectedIndex;
            if (cboPokemon.Items.Count == 0) p = 0;
            ((CheckBox)sender).Text = ((CheckBox)sender).Checked ?
                ultra[Properties.Settings.Default.Language] : sunmoon[Properties.Settings.Default.Language];
            PokemonList.Clear();
            if (chkUltra.Checked) PokemonList.AddRange(PokemonTables.pokemonUSUMTable);
            else PokemonList.AddRange(PokemonTables.pokemonSuMoTable);
            RefreshComboboxes();
            cboPokemon.SelectedIndex = cboPokemon.Items.Count > p ? p : cboPokemon.Items.Count - 1;
            txtCaptureRate.Text = PokemonList[cboPokemon.SelectedIndex + 1].CatchRate.ToString();
        }
    }
    
}
