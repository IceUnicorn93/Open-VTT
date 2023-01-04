
namespace Open_VTT.Forms.Popups
{
    partial class DisplayItemGenerator
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
            this.rbItem = new System.Windows.Forms.RadioButton();
            this.rbNode = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxParent = new System.Windows.Forms.ComboBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbItem
            // 
            this.rbItem.AutoSize = true;
            this.rbItem.Checked = true;
            this.rbItem.Location = new System.Drawing.Point(12, 12);
            this.rbItem.Name = "rbItem";
            this.rbItem.Size = new System.Drawing.Size(82, 17);
            this.rbItem.TabIndex = 0;
            this.rbItem.TabStop = true;
            this.rbItem.Text = "Create Child";
            this.rbItem.UseVisualStyleBackColor = true;
            // 
            // rbNode
            // 
            this.rbNode.AutoSize = true;
            this.rbNode.Location = new System.Drawing.Point(12, 35);
            this.rbNode.Name = "rbNode";
            this.rbNode.Size = new System.Drawing.Size(85, 17);
            this.rbNode.TabIndex = 1;
            this.rbNode.Text = "Create Node";
            this.rbNode.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(155, 11);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(121, 20);
            this.tbName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Parent";
            // 
            // cbxParent
            // 
            this.cbxParent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxParent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cbxParent.FormattingEnabled = true;
            this.cbxParent.Location = new System.Drawing.Point(155, 36);
            this.cbxParent.Name = "cbxParent";
            this.cbxParent.Size = new System.Drawing.Size(121, 21);
            this.cbxParent.TabIndex = 5;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(155, 63);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(121, 23);
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // DisplayItemGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 93);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.cbxParent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbNode);
            this.Controls.Add(this.rbItem);
            this.Name = "DisplayItemGenerator";
            this.Text = "DisplayItemGenerator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbItem;
        private System.Windows.Forms.RadioButton rbNode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxParent;
        private System.Windows.Forms.Button btnCreate;
    }
}