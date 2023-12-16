namespace OpenVTT.Controls.Forms
{
    partial class ScriptDesigner
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
            this.designer1 = new OpenVTT.UiDesigner.UserControls.Designer();
            this.SuspendLayout();
            // 
            // designer1
            // 
            this.designer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.designer1.IsNotesDesigner = false;
            this.designer1.LoadPath = null;
            this.designer1.Location = new System.Drawing.Point(0, 0);
            this.designer1.Name = "designer1";
            this.designer1.Size = new System.Drawing.Size(1040, 562);
            this.designer1.TabIndex = 0;
            // 
            // ScriptDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 562);
            this.Controls.Add(this.designer1);
            this.Name = "ScriptDesigner";
            this.Text = "ScriptDesigner";
            this.ResumeLayout(false);

        }

        #endregion

        private UiDesigner.UserControls.Designer designer1;
    }
}