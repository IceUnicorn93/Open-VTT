﻿
namespace Open_VTT.Controls
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
            this.btnOpenViewer = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.tvItems = new System.Windows.Forms.TreeView();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.tbSearchItem = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOpenViewer
            // 
            this.btnOpenViewer.Location = new System.Drawing.Point(3, 3);
            this.btnOpenViewer.Name = "btnOpenViewer";
            this.btnOpenViewer.Size = new System.Drawing.Size(79, 20);
            this.btnOpenViewer.TabIndex = 0;
            this.btnOpenViewer.Text = "Open Viewer";
            this.btnOpenViewer.UseVisualStyleBackColor = true;
            this.btnOpenViewer.Click += new System.EventHandler(this.btnOpenViewer_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(114, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(20, 20);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(139, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(20, 20);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "-";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // tvItems
            // 
            this.tvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvItems.Location = new System.Drawing.Point(3, 53);
            this.tvItems.Name = "tvItems";
            this.tvItems.Size = new System.Drawing.Size(157, 215);
            this.tvItems.TabIndex = 3;
            this.tvItems.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvItems_NodeMouseClick);
            // 
            // btnDisplay
            // 
            this.btnDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisplay.Location = new System.Drawing.Point(88, 3);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(20, 20);
            this.btnDisplay.TabIndex = 5;
            this.btnDisplay.Text = "D";
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // tbSearchItem
            // 
            this.tbSearchItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchItem.FormattingEnabled = true;
            this.tbSearchItem.Location = new System.Drawing.Point(3, 26);
            this.tbSearchItem.Name = "tbSearchItem";
            this.tbSearchItem.Size = new System.Drawing.Size(155, 21);
            this.tbSearchItem.TabIndex = 6;
            this.tbSearchItem.TextUpdate += new System.EventHandler(this.tbSearchItem_TextUpdate);
            this.tbSearchItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearchItem_KeyDown);
            // 
            // TreeViewDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbSearchItem);
            this.Controls.Add(this.btnDisplay);
            this.Controls.Add(this.tvItems);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnOpenViewer);
            this.Name = "TreeViewDisplay";
            this.Size = new System.Drawing.Size(161, 270);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenViewer;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.TreeView tvItems;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.ComboBox tbSearchItem;
    }
}
