namespace PokemonEncCalc
{
    partial class frmDisplayResults
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDisplayResults));
            this.lblRepel = new System.Windows.Forms.Label();
            this.lblCuteCharm = new System.Windows.Forms.Label();
            this.pbarRepel = new System.Windows.Forms.ProgressBar();
            this.pbarCuteCharm = new System.Windows.Forms.ProgressBar();
            this.cmdClose = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.pnlResults = new System.Windows.Forms.Panel();
            this.pnlBars = new System.Windows.Forms.Panel();
            this.pnlBars.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRepel
            // 
            this.lblRepel.AutoSize = true;
            this.lblRepel.Location = new System.Drawing.Point(3, 10);
            this.lblRepel.Name = "lblRepel";
            this.lblRepel.Size = new System.Drawing.Size(136, 13);
            this.lblRepel.TabIndex = 4;
            this.lblRepel.Text = "Encounter rate with Repel :";
            this.lblRepel.Visible = false;
            // 
            // lblCuteCharm
            // 
            this.lblCuteCharm.AutoSize = true;
            this.lblCuteCharm.Location = new System.Drawing.Point(3, 48);
            this.lblCuteCharm.Name = "lblCuteCharm";
            this.lblCuteCharm.Size = new System.Drawing.Size(192, 13);
            this.lblCuteCharm.TabIndex = 4;
            this.lblCuteCharm.Text = "Odds to find a shiny using Cute Charm :";
            this.lblCuteCharm.Visible = false;
            // 
            // pbarRepel
            // 
            this.pbarRepel.Location = new System.Drawing.Point(6, 26);
            this.pbarRepel.Maximum = 1000;
            this.pbarRepel.Name = "pbarRepel";
            this.pbarRepel.Size = new System.Drawing.Size(920, 10);
            this.pbarRepel.TabIndex = 3;
            this.pbarRepel.Visible = false;
            // 
            // pbarCuteCharm
            // 
            this.pbarCuteCharm.Location = new System.Drawing.Point(6, 64);
            this.pbarCuteCharm.Maximum = 1000;
            this.pbarCuteCharm.Name = "pbarCuteCharm";
            this.pbarCuteCharm.Size = new System.Drawing.Size(920, 10);
            this.pbarCuteCharm.TabIndex = 3;
            this.pbarCuteCharm.Visible = false;
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(397, 91);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(146, 32);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(12, 4);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(920, 70);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlResults
            // 
            this.pnlResults.Location = new System.Drawing.Point(12, 76);
            this.pnlResults.Name = "pnlResults";
            this.pnlResults.Size = new System.Drawing.Size(920, 460);
            this.pnlResults.TabIndex = 0;
            // 
            // pnlBars
            // 
            this.pnlBars.Controls.Add(this.cmdClose);
            this.pnlBars.Controls.Add(this.lblRepel);
            this.pnlBars.Controls.Add(this.lblCuteCharm);
            this.pnlBars.Controls.Add(this.pbarCuteCharm);
            this.pnlBars.Controls.Add(this.pbarRepel);
            this.pnlBars.Location = new System.Drawing.Point(0, 546);
            this.pnlBars.Name = "pnlBars";
            this.pnlBars.Size = new System.Drawing.Size(944, 131);
            this.pnlBars.TabIndex = 5;
            // 
            // frmDisplayResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 681);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.pnlResults);
            this.Controls.Add(this.pnlBars);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDisplayResults";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DisplayResults";
            this.Load += new System.EventHandler(this.frmDisplayResults_Load);
            this.pnlBars.ResumeLayout(false);
            this.pnlBars.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlResults;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.ProgressBar pbarCuteCharm;
        private System.Windows.Forms.ProgressBar pbarRepel;
        private System.Windows.Forms.Label lblCuteCharm;
        private System.Windows.Forms.Label lblRepel;
        private System.Windows.Forms.Panel pnlBars;
    }
}