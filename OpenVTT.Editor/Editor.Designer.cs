namespace OpenVTT.Editor
{
    partial class Editor
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblEditName = new System.Windows.Forms.Label();
            this.lblEditName2 = new System.Windows.Forms.Label();
            this.lblEditText = new System.Windows.Forms.Label();
            this.tbEditText = new System.Windows.Forms.TextBox();
            this.btnEditNewLabel = new System.Windows.Forms.Button();
            this.btnEditNewTextbox = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblEditName
            // 
            this.lblEditName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEditName.AutoSize = true;
            this.lblEditName.Location = new System.Drawing.Point(370, 11);
            this.lblEditName.Name = "lblEditName";
            this.lblEditName.Size = new System.Drawing.Size(35, 13);
            this.lblEditName.TabIndex = 0;
            this.lblEditName.Text = "Name";
            // 
            // lblEditName2
            // 
            this.lblEditName2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEditName2.AutoSize = true;
            this.lblEditName2.Location = new System.Drawing.Point(411, 11);
            this.lblEditName2.Name = "lblEditName2";
            this.lblEditName2.Size = new System.Drawing.Size(28, 13);
            this.lblEditName2.TabIndex = 1;
            this.lblEditName2.Text = "###";
            // 
            // lblEditText
            // 
            this.lblEditText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEditText.AutoSize = true;
            this.lblEditText.Location = new System.Drawing.Point(370, 37);
            this.lblEditText.Name = "lblEditText";
            this.lblEditText.Size = new System.Drawing.Size(28, 13);
            this.lblEditText.TabIndex = 2;
            this.lblEditText.Text = "Text";
            // 
            // tbEditText
            // 
            this.tbEditText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEditText.Location = new System.Drawing.Point(414, 34);
            this.tbEditText.Name = "tbEditText";
            this.tbEditText.Size = new System.Drawing.Size(139, 20);
            this.tbEditText.TabIndex = 3;
            this.tbEditText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbEditText_KeyDown);
            // 
            // btnEditNewLabel
            // 
            this.btnEditNewLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditNewLabel.Location = new System.Drawing.Point(373, 60);
            this.btnEditNewLabel.Name = "btnEditNewLabel";
            this.btnEditNewLabel.Size = new System.Drawing.Size(78, 23);
            this.btnEditNewLabel.TabIndex = 4;
            this.btnEditNewLabel.Text = "New Label";
            this.btnEditNewLabel.UseVisualStyleBackColor = true;
            this.btnEditNewLabel.Click += new System.EventHandler(this.btnEditNewLabel_Click);
            // 
            // btnEditNewTextbox
            // 
            this.btnEditNewTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditNewTextbox.Location = new System.Drawing.Point(475, 60);
            this.btnEditNewTextbox.Name = "btnEditNewTextbox";
            this.btnEditNewTextbox.Size = new System.Drawing.Size(78, 23);
            this.btnEditNewTextbox.TabIndex = 5;
            this.btnEditNewTextbox.Text = "New Textbox";
            this.btnEditNewTextbox.UseVisualStyleBackColor = true;
            this.btnEditNewTextbox.Click += new System.EventHandler(this.btnEditNewTextbox_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(517, 340);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(42, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnEditNewTextbox);
            this.Controls.Add(this.btnEditNewLabel);
            this.Controls.Add(this.tbEditText);
            this.Controls.Add(this.lblEditText);
            this.Controls.Add(this.lblEditName2);
            this.Controls.Add(this.lblEditName);
            this.Name = "Editor";
            this.Size = new System.Drawing.Size(562, 366);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEditName;
        private System.Windows.Forms.Label lblEditName2;
        private System.Windows.Forms.Label lblEditText;
        private System.Windows.Forms.TextBox tbEditText;
        private System.Windows.Forms.Button btnEditNewLabel;
        private System.Windows.Forms.Button btnEditNewTextbox;
        private System.Windows.Forms.Button btnSave;
    }
}
