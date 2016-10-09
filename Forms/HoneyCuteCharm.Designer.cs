namespace PokemonEncCalc
{
    partial class frmHoneyCuteCharm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gboIDs = new System.Windows.Forms.GroupBox();
            this.chkNoSecretID = new System.Windows.Forms.CheckBox();
            this.txtSecret = new System.Windows.Forms.MaskedTextBox();
            this.txtTrainer = new System.Windows.Forms.MaskedTextBox();
            this.lblSecretID = new System.Windows.Forms.Label();
            this.lblTrainerID = new System.Windows.Forms.Label();
            this.gboMunchlax = new System.Windows.Forms.GroupBox();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.lblMunchlaxResults = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gboCuteCharm = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblWildGenderRatio = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCuteCharmF4 = new System.Windows.Forms.Label();
            this.lblCuteCharmF3 = new System.Windows.Forms.Label();
            this.lblCuteCharmF2 = new System.Windows.Forms.Label();
            this.lblCuteCharmM = new System.Windows.Forms.Label();
            this.lblCuteCharmF1 = new System.Windows.Forms.Label();
            this.lblCuteCharmMDisp = new System.Windows.Forms.Label();
            this.lblCuteCharmFDisp = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.gboIDs.SuspendLayout();
            this.gboMunchlax.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gboCuteCharm.SuspendLayout();
            this.SuspendLayout();
            // 
            // gboIDs
            // 
            this.gboIDs.Controls.Add(this.chkNoSecretID);
            this.gboIDs.Controls.Add(this.txtSecret);
            this.gboIDs.Controls.Add(this.txtTrainer);
            this.gboIDs.Controls.Add(this.lblSecretID);
            this.gboIDs.Controls.Add(this.lblTrainerID);
            this.gboIDs.Location = new System.Drawing.Point(33, 32);
            this.gboIDs.Name = "gboIDs";
            this.gboIDs.Size = new System.Drawing.Size(350, 158);
            this.gboIDs.TabIndex = 0;
            this.gboIDs.TabStop = false;
            this.gboIDs.Text = "Your game\'s Trainer IDs";
            // 
            // chkNoSecretID
            // 
            this.chkNoSecretID.AutoSize = true;
            this.chkNoSecretID.Location = new System.Drawing.Point(62, 114);
            this.chkNoSecretID.Name = "chkNoSecretID";
            this.chkNoSecretID.Size = new System.Drawing.Size(214, 17);
            this.chkNoSecretID.TabIndex = 2;
            this.chkNoSecretID.Text = "You do not know what is your Secret ID";
            this.chkNoSecretID.UseVisualStyleBackColor = true;
            this.chkNoSecretID.CheckedChanged += new System.EventHandler(this.chkNoSecretID_CheckedChanged);
            // 
            // txtSecret
            // 
            this.txtSecret.Culture = new System.Globalization.CultureInfo("");
            this.txtSecret.Location = new System.Drawing.Point(196, 74);
            this.txtSecret.Mask = "99999";
            this.txtSecret.Name = "txtSecret";
            this.txtSecret.Size = new System.Drawing.Size(100, 20);
            this.txtSecret.TabIndex = 1;
            this.txtSecret.ValidatingType = typeof(int);
            // 
            // txtTrainer
            // 
            this.txtTrainer.Culture = new System.Globalization.CultureInfo("");
            this.txtTrainer.Location = new System.Drawing.Point(196, 38);
            this.txtTrainer.Mask = "99999";
            this.txtTrainer.Name = "txtTrainer";
            this.txtTrainer.Size = new System.Drawing.Size(100, 20);
            this.txtTrainer.TabIndex = 0;
            this.txtTrainer.ValidatingType = typeof(int);
            // 
            // lblSecretID
            // 
            this.lblSecretID.AutoSize = true;
            this.lblSecretID.Location = new System.Drawing.Point(49, 77);
            this.lblSecretID.Name = "lblSecretID";
            this.lblSecretID.Size = new System.Drawing.Size(52, 13);
            this.lblSecretID.TabIndex = 0;
            this.lblSecretID.Text = "Secret ID";
            // 
            // lblTrainerID
            // 
            this.lblTrainerID.AutoSize = true;
            this.lblTrainerID.Location = new System.Drawing.Point(49, 41);
            this.lblTrainerID.Name = "lblTrainerID";
            this.lblTrainerID.Size = new System.Drawing.Size(54, 13);
            this.lblTrainerID.TabIndex = 0;
            this.lblTrainerID.Text = "Trainer ID";
            // 
            // gboMunchlax
            // 
            this.gboMunchlax.Controls.Add(this.lblPercentage);
            this.gboMunchlax.Controls.Add(this.lblMunchlaxResults);
            this.gboMunchlax.Controls.Add(this.pictureBox1);
            this.gboMunchlax.Location = new System.Drawing.Point(400, 32);
            this.gboMunchlax.Name = "gboMunchlax";
            this.gboMunchlax.Size = new System.Drawing.Size(350, 158);
            this.gboMunchlax.TabIndex = 0;
            this.gboMunchlax.TabStop = false;
            this.gboMunchlax.Text = "Munchlax Trees (DPPt)";
            // 
            // lblPercentage
            // 
            this.lblPercentage.BackColor = System.Drawing.Color.Transparent;
            this.lblPercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercentage.Location = new System.Drawing.Point(144, 24);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(77, 123);
            this.lblPercentage.TabIndex = 2;
            // 
            // lblMunchlaxResults
            // 
            this.lblMunchlaxResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMunchlaxResults.Location = new System.Drawing.Point(6, 24);
            this.lblMunchlaxResults.Name = "lblMunchlaxResults";
            this.lblMunchlaxResults.Size = new System.Drawing.Size(215, 123);
            this.lblMunchlaxResults.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PokemonEncCalc.Properties.Resources.HoneyTree;
            this.pictureBox1.Location = new System.Drawing.Point(227, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(99, 93);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // gboCuteCharm
            // 
            this.gboCuteCharm.Controls.Add(this.label9);
            this.gboCuteCharm.Controls.Add(this.label8);
            this.gboCuteCharm.Controls.Add(this.label7);
            this.gboCuteCharm.Controls.Add(this.lblWildGenderRatio);
            this.gboCuteCharm.Controls.Add(this.label6);
            this.gboCuteCharm.Controls.Add(this.lblCuteCharmF4);
            this.gboCuteCharm.Controls.Add(this.lblCuteCharmF3);
            this.gboCuteCharm.Controls.Add(this.lblCuteCharmF2);
            this.gboCuteCharm.Controls.Add(this.lblCuteCharmM);
            this.gboCuteCharm.Controls.Add(this.lblCuteCharmF1);
            this.gboCuteCharm.Controls.Add(this.lblCuteCharmMDisp);
            this.gboCuteCharm.Controls.Add(this.lblCuteCharmFDisp);
            this.gboCuteCharm.ForeColor = System.Drawing.Color.DarkBlue;
            this.gboCuteCharm.Location = new System.Drawing.Point(33, 214);
            this.gboCuteCharm.Name = "gboCuteCharm";
            this.gboCuteCharm.Size = new System.Drawing.Size(717, 278);
            this.gboCuteCharm.TabIndex = 1;
            this.gboCuteCharm.TabStop = false;
            this.gboCuteCharm.Text = "Odds to find a shiny using Cute Charm (Generation 4)";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.Control;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label9.Location = new System.Drawing.Point(532, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 40);
            this.label9.TabIndex = 2;
            this.label9.Text = "25% ♂ \r\n75% ♀ ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label9.UseCompatibleTextRendering = true;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label8.Location = new System.Drawing.Point(412, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 40);
            this.label8.TabIndex = 2;
            this.label8.Text = "50% ♂ \r\n50% ♀ ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label8.UseCompatibleTextRendering = true;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label7.Location = new System.Drawing.Point(292, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 40);
            this.label7.TabIndex = 2;
            this.label7.Text = "75% ♂ \r\n25% ♀ ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label7.UseCompatibleTextRendering = true;
            // 
            // lblWildGenderRatio
            // 
            this.lblWildGenderRatio.BackColor = System.Drawing.SystemColors.Control;
            this.lblWildGenderRatio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWildGenderRatio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildGenderRatio.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblWildGenderRatio.Location = new System.Drawing.Point(172, 32);
            this.lblWildGenderRatio.Name = "lblWildGenderRatio";
            this.lblWildGenderRatio.Size = new System.Drawing.Size(480, 20);
            this.lblWildGenderRatio.TabIndex = 2;
            this.lblWildGenderRatio.Text = "Wild Pokémon gender ratio";
            this.lblWildGenderRatio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWildGenderRatio.UseCompatibleTextRendering = true;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label6.Location = new System.Drawing.Point(172, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 40);
            this.label6.TabIndex = 2;
            this.label6.Text = "87.5% ♂ \r\n12.5% ♀ ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label6.UseCompatibleTextRendering = true;
            // 
            // lblCuteCharmF4
            // 
            this.lblCuteCharmF4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblCuteCharmF4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCuteCharmF4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCuteCharmF4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCuteCharmF4.Location = new System.Drawing.Point(532, 91);
            this.lblCuteCharmF4.Name = "lblCuteCharmF4";
            this.lblCuteCharmF4.Size = new System.Drawing.Size(120, 80);
            this.lblCuteCharmF4.TabIndex = 1;
            this.lblCuteCharmF4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCuteCharmF3
            // 
            this.lblCuteCharmF3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblCuteCharmF3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCuteCharmF3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCuteCharmF3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCuteCharmF3.Location = new System.Drawing.Point(412, 91);
            this.lblCuteCharmF3.Name = "lblCuteCharmF3";
            this.lblCuteCharmF3.Size = new System.Drawing.Size(120, 80);
            this.lblCuteCharmF3.TabIndex = 1;
            this.lblCuteCharmF3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCuteCharmF2
            // 
            this.lblCuteCharmF2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblCuteCharmF2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCuteCharmF2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCuteCharmF2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCuteCharmF2.Location = new System.Drawing.Point(292, 91);
            this.lblCuteCharmF2.Name = "lblCuteCharmF2";
            this.lblCuteCharmF2.Size = new System.Drawing.Size(120, 80);
            this.lblCuteCharmF2.TabIndex = 1;
            this.lblCuteCharmF2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCuteCharmM
            // 
            this.lblCuteCharmM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.lblCuteCharmM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCuteCharmM.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCuteCharmM.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCuteCharmM.Location = new System.Drawing.Point(172, 171);
            this.lblCuteCharmM.Name = "lblCuteCharmM";
            this.lblCuteCharmM.Size = new System.Drawing.Size(480, 80);
            this.lblCuteCharmM.TabIndex = 1;
            this.lblCuteCharmM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCuteCharmF1
            // 
            this.lblCuteCharmF1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblCuteCharmF1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCuteCharmF1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCuteCharmF1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCuteCharmF1.Location = new System.Drawing.Point(172, 91);
            this.lblCuteCharmF1.Name = "lblCuteCharmF1";
            this.lblCuteCharmF1.Size = new System.Drawing.Size(120, 80);
            this.lblCuteCharmF1.TabIndex = 1;
            this.lblCuteCharmF1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCuteCharmMDisp
            // 
            this.lblCuteCharmMDisp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCuteCharmMDisp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCuteCharmMDisp.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCuteCharmMDisp.Location = new System.Drawing.Point(52, 171);
            this.lblCuteCharmMDisp.Name = "lblCuteCharmMDisp";
            this.lblCuteCharmMDisp.Size = new System.Drawing.Size(120, 80);
            this.lblCuteCharmMDisp.TabIndex = 0;
            this.lblCuteCharmMDisp.Text = "Cute Charm ♂";
            this.lblCuteCharmMDisp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCuteCharmMDisp.UseCompatibleTextRendering = true;
            // 
            // lblCuteCharmFDisp
            // 
            this.lblCuteCharmFDisp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCuteCharmFDisp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCuteCharmFDisp.ForeColor = System.Drawing.Color.Fuchsia;
            this.lblCuteCharmFDisp.Location = new System.Drawing.Point(52, 91);
            this.lblCuteCharmFDisp.Name = "lblCuteCharmFDisp";
            this.lblCuteCharmFDisp.Size = new System.Drawing.Size(120, 80);
            this.lblCuteCharmFDisp.TabIndex = 0;
            this.lblCuteCharmFDisp.Text = "Cute Charm ♀";
            this.lblCuteCharmFDisp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCuteCharmFDisp.UseCompatibleTextRendering = true;
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(250, 511);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(133, 25);
            this.cmdOK.TabIndex = 2;
            this.cmdOK.Text = "GO";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(400, 511);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(133, 25);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            // 
            // frmHoneyCuteCharm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.gboCuteCharm);
            this.Controls.Add(this.gboMunchlax);
            this.Controls.Add(this.gboIDs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmHoneyCuteCharm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pokémon Encounter Calculator - Munchlax Trees & Cute Charm";
            this.gboIDs.ResumeLayout(false);
            this.gboIDs.PerformLayout();
            this.gboMunchlax.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gboCuteCharm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gboIDs;
        private System.Windows.Forms.GroupBox gboMunchlax;
        private System.Windows.Forms.MaskedTextBox txtSecret;
        private System.Windows.Forms.MaskedTextBox txtTrainer;
        private System.Windows.Forms.Label lblSecretID;
        private System.Windows.Forms.Label lblTrainerID;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox gboCuteCharm;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCuteCharmF4;
        private System.Windows.Forms.Label lblCuteCharmF3;
        private System.Windows.Forms.Label lblCuteCharmF2;
        private System.Windows.Forms.Label lblCuteCharmM;
        private System.Windows.Forms.Label lblCuteCharmF1;
        private System.Windows.Forms.Label lblCuteCharmMDisp;
        private System.Windows.Forms.Label lblCuteCharmFDisp;
        private System.Windows.Forms.Label lblWildGenderRatio;
        private System.Windows.Forms.CheckBox chkNoSecretID;
        private System.Windows.Forms.Label lblMunchlaxResults;
        private System.Windows.Forms.Label lblPercentage;
    }
}