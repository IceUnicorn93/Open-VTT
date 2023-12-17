using OpenVTT.Common;

namespace OpenVTT.Controls.Displayer
{
    partial class InformationDisplayPlayer
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
            this.displayImagePictureBox = new OpenVTT.Controls.DrawingPictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.displayImagePictureBox)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // displayImagePictureBox
            // 
            this.displayImagePictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayImagePictureBox.DrawMode = OpenVTT.Common.PictureBoxMode.Rectangle;
            this.displayImagePictureBox.Location = new System.Drawing.Point(0, 100);
            this.displayImagePictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.displayImagePictureBox.Name = "displayImagePictureBox";
            this.displayImagePictureBox.Size = new System.Drawing.Size(1386, 629);
            this.displayImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.displayImagePictureBox.TabIndex = 0;
            this.displayImagePictureBox.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.displayImagePictureBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1386, 729);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1386, 100);
            this.panel1.TabIndex = 1;
            // 
            // lblText
            // 
            this.lblText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblText.Font = new System.Drawing.Font("Arial", 70F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText.ForeColor = System.Drawing.Color.Coral;
            this.lblText.Location = new System.Drawing.Point(0, 0);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(1386, 100);
            this.lblText.TabIndex = 0;
            this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InformationDisplayPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1386, 729);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InformationDisplayPlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "InformationDisplayPlayer";
            this.Load += new System.EventHandler(this.InformationDisplayPlayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.displayImagePictureBox)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DrawingPictureBox displayImagePictureBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblText;
    }
}