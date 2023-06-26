namespace OpenVTT.Client
{
    partial class NetworkSync
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
            this.panelState = new System.Windows.Forms.Panel();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnPull = new System.Windows.Forms.Button();
            this.btnPush = new System.Windows.Forms.Button();
            this.lblConnectionState = new System.Windows.Forms.Label();
            this.lblCommand = new System.Windows.Forms.Label();
            this.lblQueue = new System.Windows.Forms.Label();
            this.panelState.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelState
            // 
            this.panelState.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panelState.Controls.Add(this.lblConnectionState);
            this.panelState.Location = new System.Drawing.Point(127, 3);
            this.panelState.Name = "panelState";
            this.panelState.Size = new System.Drawing.Size(94, 23);
            this.panelState.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(3, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(118, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnPull
            // 
            this.btnPull.Enabled = false;
            this.btnPull.Location = new System.Drawing.Point(3, 32);
            this.btnPull.Name = "btnPull";
            this.btnPull.Size = new System.Drawing.Size(118, 23);
            this.btnPull.TabIndex = 2;
            this.btnPull.Text = "Get Server (PULL)";
            this.btnPull.UseVisualStyleBackColor = true;
            this.btnPull.Click += new System.EventHandler(this.btnPull_Click);
            // 
            // btnPush
            // 
            this.btnPush.Enabled = false;
            this.btnPush.Location = new System.Drawing.Point(3, 61);
            this.btnPush.Name = "btnPush";
            this.btnPush.Size = new System.Drawing.Size(118, 23);
            this.btnPush.TabIndex = 3;
            this.btnPush.Text = "Send Server (PUSH)";
            this.btnPush.UseVisualStyleBackColor = true;
            this.btnPush.Click += new System.EventHandler(this.btnPush_Click);
            // 
            // lblConnectionState
            // 
            this.lblConnectionState.AutoSize = true;
            this.lblConnectionState.Location = new System.Drawing.Point(12, 5);
            this.lblConnectionState.Name = "lblConnectionState";
            this.lblConnectionState.Size = new System.Drawing.Size(73, 13);
            this.lblConnectionState.TabIndex = 4;
            this.lblConnectionState.Text = "Disconnected";
            // 
            // lblCommand
            // 
            this.lblCommand.AutoSize = true;
            this.lblCommand.Location = new System.Drawing.Point(127, 66);
            this.lblCommand.Name = "lblCommand";
            this.lblCommand.Size = new System.Drawing.Size(16, 13);
            this.lblCommand.TabIndex = 5;
            this.lblCommand.Text = "---";
            // 
            // lblQueue
            // 
            this.lblQueue.AutoSize = true;
            this.lblQueue.Location = new System.Drawing.Point(127, 37);
            this.lblQueue.Name = "lblQueue";
            this.lblQueue.Size = new System.Drawing.Size(68, 13);
            this.lblQueue.TabIndex = 6;
            this.lblQueue.Text = "Queue: 0 / 0";
            // 
            // NetworkSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblQueue);
            this.Controls.Add(this.lblCommand);
            this.Controls.Add(this.btnPush);
            this.Controls.Add(this.btnPull);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.panelState);
            this.Name = "NetworkSync";
            this.Size = new System.Drawing.Size(227, 88);
            this.panelState.ResumeLayout(false);
            this.panelState.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelState;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnPull;
        private System.Windows.Forms.Button btnPush;
        private System.Windows.Forms.Label lblConnectionState;
        private System.Windows.Forms.Label lblCommand;
        private System.Windows.Forms.Label lblQueue;
    }
}
