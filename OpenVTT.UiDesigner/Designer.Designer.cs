namespace OpenVTT.UiDesigner
{
    partial class Designer
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
            this.lbControls = new System.Windows.Forms.ListBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnFromTemplate = new System.Windows.Forms.Button();
            this.pgControl = new System.Windows.Forms.PropertyGrid();
            this.pnlDesigner = new System.Windows.Forms.Panel();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnStreanDeck = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbControls
            // 
            this.lbControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbControls.FormattingEnabled = true;
            this.lbControls.Location = new System.Drawing.Point(3, 3);
            this.lbControls.Name = "lbControls";
            this.lbControls.Size = new System.Drawing.Size(174, 316);
            this.lbControls.TabIndex = 0;
            this.lbControls.SelectedIndexChanged += new System.EventHandler(this.lbControls_SelectedIndexChanged);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNew.Location = new System.Drawing.Point(3, 327);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(54, 23);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Visible = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnFromTemplate
            // 
            this.btnFromTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFromTemplate.Location = new System.Drawing.Point(3, 356);
            this.btnFromTemplate.Name = "btnFromTemplate";
            this.btnFromTemplate.Size = new System.Drawing.Size(86, 23);
            this.btnFromTemplate.TabIndex = 2;
            this.btnFromTemplate.Text = "Fom Template";
            this.btnFromTemplate.UseVisualStyleBackColor = true;
            this.btnFromTemplate.Click += new System.EventHandler(this.btnFromTemplate_Click);
            // 
            // pgControl
            // 
            this.pgControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgControl.Location = new System.Drawing.Point(617, 3);
            this.pgControl.Name = "pgControl";
            this.pgControl.Size = new System.Drawing.Size(312, 376);
            this.pgControl.TabIndex = 3;
            // 
            // pnlDesigner
            // 
            this.pnlDesigner.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDesigner.Location = new System.Drawing.Point(183, 3);
            this.pnlDesigner.Name = "pnlDesigner";
            this.pnlDesigner.Size = new System.Drawing.Size(428, 376);
            this.pnlDesigner.TabIndex = 4;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoad.Location = new System.Drawing.Point(63, 327);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(54, 23);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(123, 327);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(54, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnStreanDeck
            // 
            this.btnStreanDeck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStreanDeck.Location = new System.Drawing.Point(95, 356);
            this.btnStreanDeck.Name = "btnStreanDeck";
            this.btnStreanDeck.Size = new System.Drawing.Size(82, 23);
            this.btnStreanDeck.TabIndex = 7;
            this.btnStreanDeck.Text = "StreamDeck";
            this.btnStreanDeck.UseVisualStyleBackColor = true;
            this.btnStreanDeck.Click += new System.EventHandler(this.btnStreanDeck_Click);
            // 
            // Designer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnStreanDeck);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.pnlDesigner);
            this.Controls.Add(this.pgControl);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnFromTemplate);
            this.Controls.Add(this.lbControls);
            this.Name = "Designer";
            this.Size = new System.Drawing.Size(932, 382);
            this.Load += new System.EventHandler(this.Designer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbControls;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnFromTemplate;
        private System.Windows.Forms.PropertyGrid pgControl;
        private System.Windows.Forms.Panel pnlDesigner;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnStreanDeck;
    }
}
