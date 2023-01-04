
namespace Open_VTT.Controls
{
    partial class ScreenSelector
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
            this.cbType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbType
            // 
            this.cbType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(260, 3);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(134, 21);
            this.cbType.TabIndex = 0;
            // 
            // ScreenSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbType);
            this.Name = "ScreenSelector";
            this.Size = new System.Drawing.Size(396, 239);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbType;
    }
}
