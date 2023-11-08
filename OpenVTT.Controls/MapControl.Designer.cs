namespace OpenVTT.Controls
{
    partial class MapControl
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
            this.btnReSetFogOfWar = new System.Windows.Forms.Button();
            this.btnImidiateFogOfWar = new System.Windows.Forms.Button();
            this.lblLayer = new System.Windows.Forms.Label();
            this.cbxScenes = new System.Windows.Forms.ComboBox();
            this.btnNewScene = new System.Windows.Forms.Button();
            this.btnLayerDown = new System.Windows.Forms.Button();
            this.btnLayerUp = new System.Windows.Forms.Button();
            this.btnPoligonFogOfWar = new System.Windows.Forms.Button();
            this.btnRevealAll = new System.Windows.Forms.Button();
            this.btnRectangleFogOfWar = new System.Windows.Forms.Button();
            this.btnCoverAll = new System.Windows.Forms.Button();
            this.btnSetActive = new System.Windows.Forms.Button();
            this.btnImportImage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReSetFogOfWar
            // 
            this.btnReSetFogOfWar.Location = new System.Drawing.Point(229, 50);
            this.btnReSetFogOfWar.Name = "btnReSetFogOfWar";
            this.btnReSetFogOfWar.Size = new System.Drawing.Size(87, 20);
            this.btnReSetFogOfWar.TabIndex = 38;
            this.btnReSetFogOfWar.Text = "Pre-Place";
            this.btnReSetFogOfWar.UseVisualStyleBackColor = true;
            this.btnReSetFogOfWar.Click += new System.EventHandler(this.btnReSetFogOfWar_Click);
            // 
            // btnImidiateFogOfWar
            // 
            this.btnImidiateFogOfWar.BackColor = System.Drawing.Color.GreenYellow;
            this.btnImidiateFogOfWar.Location = new System.Drawing.Point(136, 50);
            this.btnImidiateFogOfWar.Name = "btnImidiateFogOfWar";
            this.btnImidiateFogOfWar.Size = new System.Drawing.Size(87, 20);
            this.btnImidiateFogOfWar.TabIndex = 37;
            this.btnImidiateFogOfWar.Text = "Immidiate";
            this.btnImidiateFogOfWar.UseVisualStyleBackColor = false;
            this.btnImidiateFogOfWar.Click += new System.EventHandler(this.btnImidiateFogOfWar_Click);
            // 
            // lblLayer
            // 
            this.lblLayer.AutoSize = true;
            this.lblLayer.Location = new System.Drawing.Point(366, 50);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new System.Drawing.Size(89, 13);
            this.lblLayer.TabIndex = 36;
            this.lblLayer.Text = "Layer: 0 { 0 to 0 }";
            // 
            // cbxScenes
            // 
            this.cbxScenes.FormattingEnabled = true;
            this.cbxScenes.Location = new System.Drawing.Point(459, 29);
            this.cbxScenes.Name = "cbxScenes";
            this.cbxScenes.Size = new System.Drawing.Size(104, 21);
            this.cbxScenes.TabIndex = 35;
            this.cbxScenes.SelectedIndexChanged += new System.EventHandler(this.cbxScenes_SelectedIndexChanged);
            // 
            // btnNewScene
            // 
            this.btnNewScene.Location = new System.Drawing.Point(459, 3);
            this.btnNewScene.Name = "btnNewScene";
            this.btnNewScene.Size = new System.Drawing.Size(87, 20);
            this.btnNewScene.TabIndex = 34;
            this.btnNewScene.Text = "New Scene";
            this.btnNewScene.UseVisualStyleBackColor = true;
            this.btnNewScene.Click += new System.EventHandler(this.btnNewScene_Click);
            // 
            // btnLayerDown
            // 
            this.btnLayerDown.Location = new System.Drawing.Point(366, 28);
            this.btnLayerDown.Name = "btnLayerDown";
            this.btnLayerDown.Size = new System.Drawing.Size(87, 20);
            this.btnLayerDown.TabIndex = 33;
            this.btnLayerDown.Text = "Layer Down";
            this.btnLayerDown.UseVisualStyleBackColor = true;
            this.btnLayerDown.Click += new System.EventHandler(this.btnLayerDown_Click);
            // 
            // btnLayerUp
            // 
            this.btnLayerUp.Location = new System.Drawing.Point(366, 3);
            this.btnLayerUp.Name = "btnLayerUp";
            this.btnLayerUp.Size = new System.Drawing.Size(87, 20);
            this.btnLayerUp.TabIndex = 32;
            this.btnLayerUp.Text = "Layer Up";
            this.btnLayerUp.UseVisualStyleBackColor = true;
            this.btnLayerUp.Click += new System.EventHandler(this.btnLayerUp_Click);
            // 
            // btnPoligonFogOfWar
            // 
            this.btnPoligonFogOfWar.Location = new System.Drawing.Point(229, 28);
            this.btnPoligonFogOfWar.Name = "btnPoligonFogOfWar";
            this.btnPoligonFogOfWar.Size = new System.Drawing.Size(87, 20);
            this.btnPoligonFogOfWar.TabIndex = 31;
            this.btnPoligonFogOfWar.Text = "Polygon";
            this.btnPoligonFogOfWar.UseVisualStyleBackColor = true;
            this.btnPoligonFogOfWar.Click += new System.EventHandler(this.btnPoligonFogOfWar_Click);
            // 
            // btnRevealAll
            // 
            this.btnRevealAll.Location = new System.Drawing.Point(229, 3);
            this.btnRevealAll.Name = "btnRevealAll";
            this.btnRevealAll.Size = new System.Drawing.Size(87, 20);
            this.btnRevealAll.TabIndex = 30;
            this.btnRevealAll.Text = "Reveal All";
            this.btnRevealAll.UseVisualStyleBackColor = true;
            this.btnRevealAll.Click += new System.EventHandler(this.btnRevealAll_Click);
            // 
            // btnRectangleFogOfWar
            // 
            this.btnRectangleFogOfWar.BackColor = System.Drawing.Color.GreenYellow;
            this.btnRectangleFogOfWar.Location = new System.Drawing.Point(136, 28);
            this.btnRectangleFogOfWar.Name = "btnRectangleFogOfWar";
            this.btnRectangleFogOfWar.Size = new System.Drawing.Size(87, 20);
            this.btnRectangleFogOfWar.TabIndex = 29;
            this.btnRectangleFogOfWar.Text = "Rectangle";
            this.btnRectangleFogOfWar.UseVisualStyleBackColor = false;
            this.btnRectangleFogOfWar.Click += new System.EventHandler(this.btnRectangleFogOfWar_Click);
            // 
            // btnCoverAll
            // 
            this.btnCoverAll.Location = new System.Drawing.Point(136, 3);
            this.btnCoverAll.Name = "btnCoverAll";
            this.btnCoverAll.Size = new System.Drawing.Size(87, 20);
            this.btnCoverAll.TabIndex = 28;
            this.btnCoverAll.Text = "Cover All";
            this.btnCoverAll.UseVisualStyleBackColor = true;
            this.btnCoverAll.Click += new System.EventHandler(this.btnCoverAll_Click);
            // 
            // btnSetActive
            // 
            this.btnSetActive.Location = new System.Drawing.Point(3, 28);
            this.btnSetActive.Name = "btnSetActive";
            this.btnSetActive.Size = new System.Drawing.Size(87, 20);
            this.btnSetActive.TabIndex = 27;
            this.btnSetActive.Text = "Set Active";
            this.btnSetActive.UseVisualStyleBackColor = true;
            this.btnSetActive.Click += new System.EventHandler(this.btnSetActive_Click);
            // 
            // btnImportImage
            // 
            this.btnImportImage.Location = new System.Drawing.Point(3, 3);
            this.btnImportImage.Name = "btnImportImage";
            this.btnImportImage.Size = new System.Drawing.Size(87, 20);
            this.btnImportImage.TabIndex = 26;
            this.btnImportImage.Text = "Import Map";
            this.btnImportImage.UseVisualStyleBackColor = true;
            this.btnImportImage.Click += new System.EventHandler(this.btnImportImage_Click);
            // 
            // MapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReSetFogOfWar);
            this.Controls.Add(this.btnImidiateFogOfWar);
            this.Controls.Add(this.lblLayer);
            this.Controls.Add(this.cbxScenes);
            this.Controls.Add(this.btnNewScene);
            this.Controls.Add(this.btnLayerDown);
            this.Controls.Add(this.btnLayerUp);
            this.Controls.Add(this.btnPoligonFogOfWar);
            this.Controls.Add(this.btnRevealAll);
            this.Controls.Add(this.btnRectangleFogOfWar);
            this.Controls.Add(this.btnCoverAll);
            this.Controls.Add(this.btnSetActive);
            this.Controls.Add(this.btnImportImage);
            this.Name = "MapControl";
            this.Size = new System.Drawing.Size(571, 71);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReSetFogOfWar;
        private System.Windows.Forms.Button btnImidiateFogOfWar;
        private System.Windows.Forms.Label lblLayer;
        private System.Windows.Forms.ComboBox cbxScenes;
        private System.Windows.Forms.Button btnNewScene;
        private System.Windows.Forms.Button btnLayerDown;
        private System.Windows.Forms.Button btnLayerUp;
        private System.Windows.Forms.Button btnPoligonFogOfWar;
        private System.Windows.Forms.Button btnRevealAll;
        private System.Windows.Forms.Button btnRectangleFogOfWar;
        private System.Windows.Forms.Button btnCoverAll;
        private System.Windows.Forms.Button btnSetActive;
        private System.Windows.Forms.Button btnImportImage;
    }
}
