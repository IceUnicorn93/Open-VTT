namespace OpenVTT.Controls
{
    partial class TreeViewDisplay
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
            this.tbSearchItem = new System.Windows.Forms.ComboBox();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.tvItems = new System.Windows.Forms.TreeView();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnOpenViewer = new System.Windows.Forms.Button();
            this.networkSync1 = new OpenVTT.Client.NetworkSync();
            this.SuspendLayout();
            // 
            // tbSearchItem
            // 
            this.tbSearchItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchItem.FormattingEnabled = true;
            this.tbSearchItem.Location = new System.Drawing.Point(3, 55);
            this.tbSearchItem.Name = "tbSearchItem";
            this.tbSearchItem.Size = new System.Drawing.Size(229, 21);
            this.tbSearchItem.TabIndex = 12;
            // 
            // btnDisplay
            // 
            this.btnDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisplay.Location = new System.Drawing.Point(143, 3);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(87, 20);
            this.btnDisplay.TabIndex = 11;
            this.btnDisplay.Text = "Display Current";
            this.btnDisplay.UseVisualStyleBackColor = true;
            // 
            // tvItems
            // 
            this.tvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvItems.Location = new System.Drawing.Point(3, 82);
            this.tvItems.Name = "tvItems";
            this.tvItems.Size = new System.Drawing.Size(231, 91);
            this.tvItems.TabIndex = 10;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(143, 29);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(87, 20);
            this.btnRemove.TabIndex = 9;
            this.btnRemove.Text = "Remove Item";
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 29);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(87, 20);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add Item";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnOpenViewer
            // 
            this.btnOpenViewer.Location = new System.Drawing.Point(3, 3);
            this.btnOpenViewer.Name = "btnOpenViewer";
            this.btnOpenViewer.Size = new System.Drawing.Size(87, 20);
            this.btnOpenViewer.TabIndex = 7;
            this.btnOpenViewer.Text = "Open Viewer";
            this.btnOpenViewer.UseVisualStyleBackColor = true;
            // 
            // networkSync1
            // 
            this.networkSync1.Location = new System.Drawing.Point(3, 179);
            this.networkSync1.Name = "networkSync1";
            this.networkSync1.Size = new System.Drawing.Size(227, 88);
            this.networkSync1.TabIndex = 13;
            // 
            // TreeViewDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.networkSync1);
            this.Controls.Add(this.tbSearchItem);
            this.Controls.Add(this.btnDisplay);
            this.Controls.Add(this.tvItems);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnOpenViewer);
            this.Name = "TreeViewDisplay";
            this.Size = new System.Drawing.Size(235, 270);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox tbSearchItem;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.TreeView tvItems;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnOpenViewer;
        private Client.NetworkSync networkSync1;
    }
}
