namespace OpenVTT.UiDesigner.UserControls
{
    partial class ArtworkInformation
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
            this.pbArtwork = new System.Windows.Forms.PictureBox();
            this.btnChooseImage = new System.Windows.Forms.Button();
            this.tbArtworkName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbArtwork)).BeginInit();
            this.SuspendLayout();
            // 
            // pbArtwork
            // 
            this.pbArtwork.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbArtwork.Location = new System.Drawing.Point(3, 3);
            this.pbArtwork.Name = "pbArtwork";
            this.pbArtwork.Size = new System.Drawing.Size(200, 200);
            this.pbArtwork.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbArtwork.TabIndex = 0;
            this.pbArtwork.TabStop = false;
            // 
            // btnChooseImage
            // 
            this.btnChooseImage.Location = new System.Drawing.Point(3, 209);
            this.btnChooseImage.Name = "btnChooseImage";
            this.btnChooseImage.Size = new System.Drawing.Size(200, 23);
            this.btnChooseImage.TabIndex = 1;
            this.btnChooseImage.Text = "Choose Image/Artwork";
            this.btnChooseImage.UseVisualStyleBackColor = true;
            this.btnChooseImage.Click += new System.EventHandler(this.btnChooseImage_Click);
            // 
            // tbArtworkName
            // 
            this.tbArtworkName.Location = new System.Drawing.Point(3, 251);
            this.tbArtworkName.Name = "tbArtworkName";
            this.tbArtworkName.Size = new System.Drawing.Size(200, 20);
            this.tbArtworkName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Display Text for Artwork";
            // 
            // ArtworkInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbArtworkName);
            this.Controls.Add(this.btnChooseImage);
            this.Controls.Add(this.pbArtwork);
            this.Name = "ArtworkInformation";
            this.Size = new System.Drawing.Size(206, 274);
            this.Load += new System.EventHandler(this.ArtworkInformation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbArtwork)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbArtwork;
        private System.Windows.Forms.Button btnChooseImage;
        private System.Windows.Forms.TextBox tbArtworkName;
        private System.Windows.Forms.Label label1;
    }
}
