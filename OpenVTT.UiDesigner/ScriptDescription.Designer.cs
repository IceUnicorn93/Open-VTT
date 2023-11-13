namespace OpenVTT.UiDesigner
{
    partial class ScriptDescription
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
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblNameAndVersion = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(5, 5);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(75, 13);
            this.lblAuthor.TabIndex = 0;
            this.lblAuthor.Text = "Made by: ###";
            // 
            // lblNameAndVersion
            // 
            this.lblNameAndVersion.AutoSize = true;
            this.lblNameAndVersion.Location = new System.Drawing.Point(5, 25);
            this.lblNameAndVersion.Name = "lblNameAndVersion";
            this.lblNameAndVersion.Size = new System.Drawing.Size(80, 13);
            this.lblNameAndVersion.TabIndex = 1;
            this.lblNameAndVersion.Text = "Name | #.#.#.#";
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(5, 50);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ReadOnly = true;
            this.tbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDescription.Size = new System.Drawing.Size(200, 100);
            this.tbDescription.TabIndex = 2;
            this.tbDescription.Text = "Description of the Plugin";
            // 
            // ScriptDescription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.lblNameAndVersion);
            this.Controls.Add(this.lblAuthor);
            this.Name = "ScriptDescription";
            this.Size = new System.Drawing.Size(210, 155);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblNameAndVersion;
        private System.Windows.Forms.TextBox tbDescription;
    }
}
