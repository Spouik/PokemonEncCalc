namespace PokemonEncCalc
{
    partial class frmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.tabAboutChangelog = new System.Windows.Forms.TabControl();
            this.tabpAbout = new System.Windows.Forms.TabPage();
            this.lblAboutFR = new System.Windows.Forms.Label();
            this.lklRepository = new System.Windows.Forms.LinkLabel();
            this.lblReport = new System.Windows.Forms.Label();
            this.lblAboutEN = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabpChangelog = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabAboutChangelog.SuspendLayout();
            this.tabpAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabpChangelog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabAboutChangelog
            // 
            this.tabAboutChangelog.Controls.Add(this.tabpAbout);
            this.tabAboutChangelog.Controls.Add(this.tabpChangelog);
            this.tabAboutChangelog.Location = new System.Drawing.Point(5, 5);
            this.tabAboutChangelog.Name = "tabAboutChangelog";
            this.tabAboutChangelog.SelectedIndex = 0;
            this.tabAboutChangelog.Size = new System.Drawing.Size(587, 311);
            this.tabAboutChangelog.TabIndex = 0;
            // 
            // tabpAbout
            // 
            this.tabpAbout.Controls.Add(this.lblAboutFR);
            this.tabpAbout.Controls.Add(this.lklRepository);
            this.tabpAbout.Controls.Add(this.lblReport);
            this.tabpAbout.Controls.Add(this.lblAboutEN);
            this.tabpAbout.Controls.Add(this.pictureBox1);
            this.tabpAbout.Location = new System.Drawing.Point(4, 22);
            this.tabpAbout.Name = "tabpAbout";
            this.tabpAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabpAbout.Size = new System.Drawing.Size(579, 285);
            this.tabpAbout.TabIndex = 0;
            this.tabpAbout.Text = "About";
            this.tabpAbout.UseVisualStyleBackColor = true;
            // 
            // lblAboutFR
            // 
            this.lblAboutFR.Location = new System.Drawing.Point(222, 53);
            this.lblAboutFR.Name = "lblAboutFR";
            this.lblAboutFR.Size = new System.Drawing.Size(342, 156);
            this.lblAboutFR.TabIndex = 4;
            this.lblAboutFR.Text = resources.GetString("lblAboutFR.Text");
            this.lblAboutFR.Visible = false;
            // 
            // lklRepository
            // 
            this.lklRepository.AutoSize = true;
            this.lklRepository.Location = new System.Drawing.Point(159, 258);
            this.lklRepository.Name = "lklRepository";
            this.lklRepository.Size = new System.Drawing.Size(241, 13);
            this.lklRepository.TabIndex = 3;
            this.lklRepository.TabStop = true;
            this.lklRepository.Text = "https://github.com/AngefloSH/PokemonEncCalc";
            this.lklRepository.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklRepository_LinkClicked);
            // 
            // lblReport
            // 
            this.lblReport.Location = new System.Drawing.Point(114, 224);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(342, 34);
            this.lblReport.TabIndex = 2;
            this.lblReport.Text = "To report a bug or an error, or for feature request, you can open an issue on the" +
    " repository :";
            // 
            // lblAboutEN
            // 
            this.lblAboutEN.Location = new System.Drawing.Point(222, 53);
            this.lblAboutEN.Name = "lblAboutEN";
            this.lblAboutEN.Size = new System.Drawing.Size(342, 156);
            this.lblAboutEN.TabIndex = 1;
            this.lblAboutEN.Text = resources.GetString("lblAboutEN.Text");
            this.lblAboutEN.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PokemonEncCalc.Properties.Resources.questionMark;
            this.pictureBox1.Location = new System.Drawing.Point(39, 72);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(168, 83);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabpChangelog
            // 
            this.tabpChangelog.Controls.Add(this.textBox1);
            this.tabpChangelog.Location = new System.Drawing.Point(4, 22);
            this.tabpChangelog.Name = "tabpChangelog";
            this.tabpChangelog.Padding = new System.Windows.Forms.Padding(3);
            this.tabpChangelog.Size = new System.Drawing.Size(579, 285);
            this.tabpChangelog.TabIndex = 1;
            this.tabpChangelog.Text = "Changelog";
            this.tabpChangelog.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(23, 23);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(532, 239);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            this.textBox1.WordWrap = false;
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 320);
            this.Controls.Add(this.tabAboutChangelog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmAbout";
            this.Text = "About & Changelog";
            this.tabAboutChangelog.ResumeLayout(false);
            this.tabpAbout.ResumeLayout(false);
            this.tabpAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabpChangelog.ResumeLayout(false);
            this.tabpChangelog.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabAboutChangelog;
        private System.Windows.Forms.TabPage tabpAbout;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tabpChangelog;
        private System.Windows.Forms.Label lblAboutFR;
        private System.Windows.Forms.LinkLabel lklRepository;
        private System.Windows.Forms.Label lblReport;
        private System.Windows.Forms.Label lblAboutEN;
        private System.Windows.Forms.TextBox textBox1;
    }
}