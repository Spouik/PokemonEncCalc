namespace PokemonEncCalc
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mstMenu = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frenchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gboSlots = new System.Windows.Forms.GroupBox();
            this.cboSlot0 = new System.Windows.Forms.ComboBox();
            this.gboAutoFill = new System.Windows.Forms.GroupBox();
            this.cboSlot1 = new System.Windows.Forms.ComboBox();
            this.cboSlot2 = new System.Windows.Forms.ComboBox();
            this.cboSlot3 = new System.Windows.Forms.ComboBox();
            this.cboSlot4 = new System.Windows.Forms.ComboBox();
            this.cboSlot6 = new System.Windows.Forms.ComboBox();
            this.cboSlot5 = new System.Windows.Forms.ComboBox();
            this.cboSlot7 = new System.Windows.Forms.ComboBox();
            this.cboSlot8 = new System.Windows.Forms.ComboBox();
            this.cboSlot10 = new System.Windows.Forms.ComboBox();
            this.cboSlot9 = new System.Windows.Forms.ComboBox();
            this.cboSlot11 = new System.Windows.Forms.ComboBox();
            this.mstMenu.SuspendLayout();
            this.gboSlots.SuspendLayout();
            this.SuspendLayout();
            // 
            // mstMenu
            // 
            this.mstMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.mstMenu.Location = new System.Drawing.Point(0, 0);
            this.mstMenu.Name = "mstMenu";
            this.mstMenu.Size = new System.Drawing.Size(1075, 24);
            this.mstMenu.TabIndex = 0;
            this.mstMenu.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.frenchToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.englishToolStripMenuItem.Text = "English";
            // 
            // frenchToolStripMenuItem
            // 
            this.frenchToolStripMenuItem.Name = "frenchToolStripMenuItem";
            this.frenchToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.frenchToolStripMenuItem.Text = "Français";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // gboSlots
            // 
            this.gboSlots.Controls.Add(this.cboSlot11);
            this.gboSlots.Controls.Add(this.cboSlot7);
            this.gboSlots.Controls.Add(this.cboSlot3);
            this.gboSlots.Controls.Add(this.cboSlot9);
            this.gboSlots.Controls.Add(this.cboSlot5);
            this.gboSlots.Controls.Add(this.cboSlot1);
            this.gboSlots.Controls.Add(this.cboSlot10);
            this.gboSlots.Controls.Add(this.cboSlot6);
            this.gboSlots.Controls.Add(this.cboSlot2);
            this.gboSlots.Controls.Add(this.cboSlot8);
            this.gboSlots.Controls.Add(this.cboSlot4);
            this.gboSlots.Controls.Add(this.cboSlot0);
            this.gboSlots.Location = new System.Drawing.Point(12, 27);
            this.gboSlots.Name = "gboSlots";
            this.gboSlots.Size = new System.Drawing.Size(622, 452);
            this.gboSlots.TabIndex = 1;
            this.gboSlots.TabStop = false;
            this.gboSlots.Text = "Selected area Pokémon";
            // 
            // cboSlot0
            // 
            this.cboSlot0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSlot0.FormattingEnabled = true;
            this.cboSlot0.Location = new System.Drawing.Point(85, 34);
            this.cboSlot0.Name = "cboSlot0";
            this.cboSlot0.Size = new System.Drawing.Size(132, 21);
            this.cboSlot0.TabIndex = 0;
            // 
            // gboAutoFill
            // 
            this.gboAutoFill.Location = new System.Drawing.Point(640, 27);
            this.gboAutoFill.Name = "gboAutoFill";
            this.gboAutoFill.Size = new System.Drawing.Size(423, 452);
            this.gboAutoFill.TabIndex = 2;
            this.gboAutoFill.TabStop = false;
            this.gboAutoFill.Text = "Encounter Slots Auto Fill";
            // 
            // cboSlot1
            // 
            this.cboSlot1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSlot1.FormattingEnabled = true;
            this.cboSlot1.Location = new System.Drawing.Point(85, 66);
            this.cboSlot1.Name = "cboSlot1";
            this.cboSlot1.Size = new System.Drawing.Size(132, 21);
            this.cboSlot1.TabIndex = 0;
            // 
            // cboSlot2
            // 
            this.cboSlot2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSlot2.FormattingEnabled = true;
            this.cboSlot2.Location = new System.Drawing.Point(85, 98);
            this.cboSlot2.Name = "cboSlot2";
            this.cboSlot2.Size = new System.Drawing.Size(132, 21);
            this.cboSlot2.TabIndex = 0;
            // 
            // cboSlot3
            // 
            this.cboSlot3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSlot3.FormattingEnabled = true;
            this.cboSlot3.Location = new System.Drawing.Point(85, 130);
            this.cboSlot3.Name = "cboSlot3";
            this.cboSlot3.Size = new System.Drawing.Size(132, 21);
            this.cboSlot3.TabIndex = 0;
            // 
            // cboSlot4
            // 
            this.cboSlot4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSlot4.FormattingEnabled = true;
            this.cboSlot4.Location = new System.Drawing.Point(85, 162);
            this.cboSlot4.Name = "cboSlot4";
            this.cboSlot4.Size = new System.Drawing.Size(132, 21);
            this.cboSlot4.TabIndex = 0;
            // 
            // cboSlot6
            // 
            this.cboSlot6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSlot6.FormattingEnabled = true;
            this.cboSlot6.Location = new System.Drawing.Point(85, 226);
            this.cboSlot6.Name = "cboSlot6";
            this.cboSlot6.Size = new System.Drawing.Size(132, 21);
            this.cboSlot6.TabIndex = 0;
            // 
            // cboSlot5
            // 
            this.cboSlot5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSlot5.FormattingEnabled = true;
            this.cboSlot5.Location = new System.Drawing.Point(85, 194);
            this.cboSlot5.Name = "cboSlot5";
            this.cboSlot5.Size = new System.Drawing.Size(132, 21);
            this.cboSlot5.TabIndex = 0;
            // 
            // cboSlot7
            // 
            this.cboSlot7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSlot7.FormattingEnabled = true;
            this.cboSlot7.Location = new System.Drawing.Point(85, 258);
            this.cboSlot7.Name = "cboSlot7";
            this.cboSlot7.Size = new System.Drawing.Size(132, 21);
            this.cboSlot7.TabIndex = 0;
            // 
            // cboSlot8
            // 
            this.cboSlot8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSlot8.FormattingEnabled = true;
            this.cboSlot8.Location = new System.Drawing.Point(85, 290);
            this.cboSlot8.Name = "cboSlot8";
            this.cboSlot8.Size = new System.Drawing.Size(132, 21);
            this.cboSlot8.TabIndex = 0;
            // 
            // cboSlot10
            // 
            this.cboSlot10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSlot10.FormattingEnabled = true;
            this.cboSlot10.Location = new System.Drawing.Point(85, 354);
            this.cboSlot10.Name = "cboSlot10";
            this.cboSlot10.Size = new System.Drawing.Size(132, 21);
            this.cboSlot10.TabIndex = 0;
            // 
            // cboSlot9
            // 
            this.cboSlot9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSlot9.FormattingEnabled = true;
            this.cboSlot9.Location = new System.Drawing.Point(85, 322);
            this.cboSlot9.Name = "cboSlot9";
            this.cboSlot9.Size = new System.Drawing.Size(132, 21);
            this.cboSlot9.TabIndex = 0;
            // 
            // cboSlot11
            // 
            this.cboSlot11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSlot11.FormattingEnabled = true;
            this.cboSlot11.Location = new System.Drawing.Point(85, 386);
            this.cboSlot11.Name = "cboSlot11";
            this.cboSlot11.Size = new System.Drawing.Size(132, 21);
            this.cboSlot11.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 596);
            this.Controls.Add(this.gboAutoFill);
            this.Controls.Add(this.gboSlots);
            this.Controls.Add(this.mstMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mstMenu;
            this.Name = "Form1";
            this.Text = "Form1";
            this.mstMenu.ResumeLayout(false);
            this.mstMenu.PerformLayout();
            this.gboSlots.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mstMenu;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frenchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox gboSlots;
        private System.Windows.Forms.ComboBox cboSlot0;
        private System.Windows.Forms.GroupBox gboAutoFill;
        private System.Windows.Forms.ComboBox cboSlot11;
        private System.Windows.Forms.ComboBox cboSlot7;
        private System.Windows.Forms.ComboBox cboSlot3;
        private System.Windows.Forms.ComboBox cboSlot9;
        private System.Windows.Forms.ComboBox cboSlot5;
        private System.Windows.Forms.ComboBox cboSlot1;
        private System.Windows.Forms.ComboBox cboSlot10;
        private System.Windows.Forms.ComboBox cboSlot6;
        private System.Windows.Forms.ComboBox cboSlot2;
        private System.Windows.Forms.ComboBox cboSlot8;
        private System.Windows.Forms.ComboBox cboSlot4;
    }
}

