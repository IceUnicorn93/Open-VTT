namespace OpenVTT.Controls
{
    partial class ScriptDisplay
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
            this.lblName = new System.Windows.Forms.Label();
            this.pnlState = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 6);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(105, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Test Script by Dustin";
            // 
            // pnlState
            // 
            this.pnlState.BackColor = System.Drawing.SystemColors.Control;
            this.pnlState.Location = new System.Drawing.Point(293, 3);
            this.pnlState.Name = "pnlState";
            this.pnlState.Size = new System.Drawing.Size(89, 19);
            this.pnlState.TabIndex = 1;
            this.pnlState.Click += new System.EventHandler(this.pnlState_Click);
            // 
            // ScriptDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlState);
            this.Controls.Add(this.lblName);
            this.Name = "ScriptDisplay";
            this.Size = new System.Drawing.Size(385, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel pnlState;
    }
}
