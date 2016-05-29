namespace PokemonEncCalc
{
    partial class frmFormSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormSelect));
            this.lblChooseFormText = new System.Windows.Forms.Label();
            this.cboFormList = new System.Windows.Forms.ComboBox();
            this.pctMinisprite = new System.Windows.Forms.PictureBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pctMinisprite)).BeginInit();
            this.SuspendLayout();
            // 
            // lblChooseFormText
            // 
            this.lblChooseFormText.AutoSize = true;
            this.lblChooseFormText.Location = new System.Drawing.Point(54, 33);
            this.lblChooseFormText.Name = "lblChooseFormText";
            this.lblChooseFormText.Size = new System.Drawing.Size(148, 13);
            this.lblChooseFormText.TabIndex = 0;
            this.lblChooseFormText.Text = "Choose the Pokémon\'s Form :";
            // 
            // cboFormList
            // 
            this.cboFormList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFormList.FormattingEnabled = true;
            this.cboFormList.Location = new System.Drawing.Point(145, 67);
            this.cboFormList.Name = "cboFormList";
            this.cboFormList.Size = new System.Drawing.Size(158, 21);
            this.cboFormList.TabIndex = 1;
            this.cboFormList.SelectedIndexChanged += new System.EventHandler(this.cboFormList_SelectedIndexChanged);
            // 
            // pctMinisprite
            // 
            this.pctMinisprite.Location = new System.Drawing.Point(83, 62);
            this.pctMinisprite.Name = "pctMinisprite";
            this.pctMinisprite.Size = new System.Drawing.Size(32, 32);
            this.pctMinisprite.TabIndex = 2;
            this.pctMinisprite.TabStop = false;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(240, 120);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(86, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOK
            // 
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(111, 120);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(86, 23);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            // 
            // frmFormSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 164);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.pctMinisprite);
            this.Controls.Add(this.cboFormList);
            this.Controls.Add(this.lblChooseFormText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFormSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormSelect";
            this.Load += new System.EventHandler(this.FormSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pctMinisprite)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblChooseFormText;
        private System.Windows.Forms.ComboBox cboFormList;
        private System.Windows.Forms.PictureBox pctMinisprite;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
    }
}