namespace OpenVTT.UiDesigner.UserControls
{
    partial class OpenVttFileStructure
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
            this.button1 = new System.Windows.Forms.Button();
            this.tbMultiValue = new System.Windows.Forms.RadioButton();
            this.rbSingleValue = new System.Windows.Forms.RadioButton();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(595, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbMultiValue
            // 
            this.tbMultiValue.AutoSize = true;
            this.tbMultiValue.Location = new System.Drawing.Point(476, 9);
            this.tbMultiValue.Name = "tbMultiValue";
            this.tbMultiValue.Size = new System.Drawing.Size(96, 17);
            this.tbMultiValue.TabIndex = 12;
            this.tbMultiValue.Text = "Multiple Values";
            this.tbMultiValue.UseVisualStyleBackColor = true;
            // 
            // rbSingleValue
            // 
            this.rbSingleValue.AutoSize = true;
            this.rbSingleValue.Checked = true;
            this.rbSingleValue.Location = new System.Drawing.Point(386, 9);
            this.rbSingleValue.Name = "rbSingleValue";
            this.rbSingleValue.Size = new System.Drawing.Size(84, 17);
            this.rbSingleValue.TabIndex = 11;
            this.rbSingleValue.TabStop = true;
            this.rbSingleValue.Text = "Single Value";
            this.rbSingleValue.UseVisualStyleBackColor = true;
            // 
            // cbType
            // 
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "Text",
            "Whole Number",
            "Decimal Number"});
            this.cbType.Location = new System.Drawing.Point(254, 8);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(126, 21);
            this.cbType.TabIndex = 10;
            this.cbType.Text = "Decimal Number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Type";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(51, 8);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(160, 20);
            this.tbName.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Name";
            // 
            // Structure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbMultiValue);
            this.Controls.Add(this.rbSingleValue);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.Name = "Structure";
            this.Size = new System.Drawing.Size(628, 34);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton tbMultiValue;
        private System.Windows.Forms.RadioButton rbSingleValue;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
    }
}
