namespace OpenVTT.Editor
{
    partial class AddNode
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbNodeName = new System.Windows.Forms.TextBox();
            this.btnSafe = new System.Windows.Forms.Button();
            this.rbNode = new System.Windows.Forms.RadioButton();
            this.rbLeaf = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // tbNodeName
            // 
            this.tbNodeName.Location = new System.Drawing.Point(12, 22);
            this.tbNodeName.Name = "tbNodeName";
            this.tbNodeName.Size = new System.Drawing.Size(156, 20);
            this.tbNodeName.TabIndex = 1;
            // 
            // btnSafe
            // 
            this.btnSafe.Location = new System.Drawing.Point(174, 20);
            this.btnSafe.Name = "btnSafe";
            this.btnSafe.Size = new System.Drawing.Size(49, 23);
            this.btnSafe.TabIndex = 2;
            this.btnSafe.Text = "Save";
            this.btnSafe.UseVisualStyleBackColor = true;
            this.btnSafe.Click += new System.EventHandler(this.btnSafe_Click);
            // 
            // rbNode
            // 
            this.rbNode.AutoSize = true;
            this.rbNode.Checked = true;
            this.rbNode.Location = new System.Drawing.Point(50, 4);
            this.rbNode.Name = "rbNode";
            this.rbNode.Size = new System.Drawing.Size(51, 17);
            this.rbNode.TabIndex = 3;
            this.rbNode.TabStop = true;
            this.rbNode.Text = "Node";
            this.rbNode.UseVisualStyleBackColor = true;
            // 
            // rbLeaf
            // 
            this.rbLeaf.AutoSize = true;
            this.rbLeaf.Location = new System.Drawing.Point(107, 4);
            this.rbLeaf.Name = "rbLeaf";
            this.rbLeaf.Size = new System.Drawing.Size(46, 17);
            this.rbLeaf.TabIndex = 4;
            this.rbLeaf.Text = "Leaf";
            this.rbLeaf.UseVisualStyleBackColor = true;
            // 
            // AddNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 50);
            this.Controls.Add(this.rbLeaf);
            this.Controls.Add(this.rbNode);
            this.Controls.Add(this.btnSafe);
            this.Controls.Add(this.tbNodeName);
            this.Controls.Add(this.label1);
            this.Name = "AddNode";
            this.Text = "Add Node";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNodeName;
        private System.Windows.Forms.Button btnSafe;
        private System.Windows.Forms.RadioButton rbNode;
        private System.Windows.Forms.RadioButton rbLeaf;
    }
}