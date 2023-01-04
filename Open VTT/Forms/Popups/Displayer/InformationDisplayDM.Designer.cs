
namespace Open_VTT.Forms.Popups.Displayer
{
    partial class InformationDisplayDM
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
            this.displayImagePictureBox = new Open_VTT.Controls.DrawingPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.displayImagePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // displayImagePictureBox
            // 
            this.displayImagePictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayImagePictureBox.DrawMode = Open_VTT.Other.PictureBoxMode.Rectangle;
            this.displayImagePictureBox.Location = new System.Drawing.Point(0, 0);
            this.displayImagePictureBox.Name = "displayImagePictureBox";
            this.displayImagePictureBox.Size = new System.Drawing.Size(800, 450);
            this.displayImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.displayImagePictureBox.TabIndex = 0;
            this.displayImagePictureBox.TabStop = false;
            // 
            // InformationDisplayDM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.displayImagePictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InformationDisplayDM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "InformationDisplayDM";
            ((System.ComponentModel.ISupportInitialize)(this.displayImagePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DrawingPictureBox displayImagePictureBox;
    }
}