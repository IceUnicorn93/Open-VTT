
namespace Open_VTT.Forms.Popups
{
    partial class Config
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPlayerSize = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.screenSelector1 = new Open_VTT.Controls.ScreenSelector();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbAutoSaveAction = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbDisplayChangesInstantly = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayerSize)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nudPlayerSize);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 59);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Grid Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "\"";
            // 
            // nudPlayerSize
            // 
            this.nudPlayerSize.Location = new System.Drawing.Point(108, 19);
            this.nudPlayerSize.Name = "nudPlayerSize";
            this.nudPlayerSize.Size = new System.Drawing.Size(59, 20);
            this.nudPlayerSize.TabIndex = 1;
            this.nudPlayerSize.ValueChanged += new System.EventHandler(this.nudPlayerSize_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Player Window Sie";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.screenSelector1);
            this.groupBox2.Location = new System.Drawing.Point(12, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(764, 396);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Player Screen Selector";
            // 
            // screenSelector1
            // 
            this.screenSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screenSelector1.Location = new System.Drawing.Point(3, 16);
            this.screenSelector1.Name = "screenSelector1";
            this.screenSelector1.Size = new System.Drawing.Size(758, 377);
            this.screenSelector1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbAutoSaveAction);
            this.groupBox3.Location = new System.Drawing.Point(216, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 59);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Autosave";
            // 
            // cbAutoSaveAction
            // 
            this.cbAutoSaveAction.AutoSize = true;
            this.cbAutoSaveAction.Location = new System.Drawing.Point(6, 20);
            this.cbAutoSaveAction.Name = "cbAutoSaveAction";
            this.cbAutoSaveAction.Size = new System.Drawing.Size(148, 17);
            this.cbAutoSaveAction.TabIndex = 0;
            this.cbAutoSaveAction.Text = "Auto Save (Action Based)";
            this.cbAutoSaveAction.UseVisualStyleBackColor = true;
            this.cbAutoSaveAction.CheckedChanged += new System.EventHandler(this.cbAutoSaveAction_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbDisplayChangesInstantly);
            this.groupBox4.Location = new System.Drawing.Point(422, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(219, 59);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Fog of War";
            // 
            // cbDisplayChangesInstantly
            // 
            this.cbDisplayChangesInstantly.AutoSize = true;
            this.cbDisplayChangesInstantly.Location = new System.Drawing.Point(20, 20);
            this.cbDisplayChangesInstantly.Name = "cbDisplayChangesInstantly";
            this.cbDisplayChangesInstantly.Size = new System.Drawing.Size(146, 17);
            this.cbDisplayChangesInstantly.TabIndex = 0;
            this.cbDisplayChangesInstantly.Text = "Display Changes instantly";
            this.cbDisplayChangesInstantly.UseVisualStyleBackColor = true;
            this.cbDisplayChangesInstantly.CheckedChanged += new System.EventHandler(this.cbDisplayChangesInstantly_CheckedChanged);
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 485);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Config";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Config";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayerSize)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudPlayerSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Controls.ScreenSelector screenSelector1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbAutoSaveAction;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox cbDisplayChangesInstantly;
    }
}