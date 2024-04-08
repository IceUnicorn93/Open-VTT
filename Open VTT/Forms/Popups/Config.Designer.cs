
using OpenVTT.Controls;

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
            this.tabCtrlPages = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nudServerPort = new System.Windows.Forms.NumericUpDown();
            this.tbServerIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pnlTextColor = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlGridColor = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlDmColor = new System.Windows.Forms.Panel();
            this.pnlPlayerColor = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbDisplayGrid = new System.Windows.Forms.CheckBox();
            this.cbDisplayChangesInstantly = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbAutoSaveAction = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.screenSelector1 = new OpenVTT.Controls.ScreenSelector();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbDisplayGridForDM = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPlayerSize = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tpScripting = new System.Windows.Forms.TabPage();
            this.btnOpenDesigner = new System.Windows.Forms.Button();
            this.flpScripts = new System.Windows.Forms.FlowLayoutPanel();
            this.tbStreamDeck = new System.Windows.Forms.TabPage();
            this.streamDeckConfig1 = new OpenVTT.StreamDeck.StreamDeckConfig();
            this.btnTestAnimatedMap = new System.Windows.Forms.Button();
            this.tabCtrlPages.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudServerPort)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayerSize)).BeginInit();
            this.tpScripting.SuspendLayout();
            this.tbStreamDeck.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCtrlPages
            // 
            this.tabCtrlPages.Controls.Add(this.tpGeneral);
            this.tabCtrlPages.Controls.Add(this.tpScripting);
            this.tabCtrlPages.Controls.Add(this.tbStreamDeck);
            this.tabCtrlPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlPages.Location = new System.Drawing.Point(0, 0);
            this.tabCtrlPages.Name = "tabCtrlPages";
            this.tabCtrlPages.SelectedIndex = 0;
            this.tabCtrlPages.Size = new System.Drawing.Size(843, 519);
            this.tabCtrlPages.TabIndex = 5;
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.btnTestAnimatedMap);
            this.tpGeneral.Controls.Add(this.groupBox5);
            this.tpGeneral.Controls.Add(this.groupBox4);
            this.tpGeneral.Controls.Add(this.groupBox3);
            this.tpGeneral.Controls.Add(this.groupBox2);
            this.tpGeneral.Controls.Add(this.groupBox1);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Size = new System.Drawing.Size(835, 493);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.nudServerPort);
            this.groupBox5.Controls.Add(this.tbServerIP);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Location = new System.Drawing.Point(554, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(218, 59);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Notes  Server";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Port";
            // 
            // nudServerPort
            // 
            this.nudServerPort.Location = new System.Drawing.Point(112, 33);
            this.nudServerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudServerPort.Name = "nudServerPort";
            this.nudServerPort.Size = new System.Drawing.Size(52, 20);
            this.nudServerPort.TabIndex = 2;
            this.nudServerPort.Value = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudServerPort.ValueChanged += new System.EventHandler(this.nudServerPort_ValueChanged);
            // 
            // tbServerIP
            // 
            this.tbServerIP.Location = new System.Drawing.Point(6, 32);
            this.tbServerIP.Name = "tbServerIP";
            this.tbServerIP.Size = new System.Drawing.Size(100, 20);
            this.tbServerIP.TabIndex = 1;
            this.tbServerIP.Text = "255.255.255.255";
            this.tbServerIP.TextChanged += new System.EventHandler(this.tbServerIP_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "IP-Adress";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pnlTextColor);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.pnlGridColor);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.pnlDmColor);
            this.groupBox4.Controls.Add(this.pnlPlayerColor);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.cbDisplayGrid);
            this.groupBox4.Controls.Add(this.cbDisplayChangesInstantly);
            this.groupBox4.Location = new System.Drawing.Point(11, 71);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(360, 65);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Fog of War and Grid";
            // 
            // pnlTextColor
            // 
            this.pnlTextColor.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlTextColor.Location = new System.Drawing.Point(334, 37);
            this.pnlTextColor.Name = "pnlTextColor";
            this.pnlTextColor.Size = new System.Drawing.Size(17, 17);
            this.pnlTextColor.TabIndex = 9;
            this.pnlTextColor.Click += new System.EventHandler(this.pnlTextColor_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(265, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Text Color";
            // 
            // pnlGridColor
            // 
            this.pnlGridColor.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlGridColor.Location = new System.Drawing.Point(334, 14);
            this.pnlGridColor.Name = "pnlGridColor";
            this.pnlGridColor.Size = new System.Drawing.Size(17, 17);
            this.pnlGridColor.TabIndex = 7;
            this.pnlGridColor.Click += new System.EventHandler(this.pnlGridColor_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(265, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Grid Color";
            // 
            // pnlDmColor
            // 
            this.pnlDmColor.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlDmColor.Location = new System.Drawing.Point(242, 14);
            this.pnlDmColor.Name = "pnlDmColor";
            this.pnlDmColor.Size = new System.Drawing.Size(17, 17);
            this.pnlDmColor.TabIndex = 5;
            this.pnlDmColor.Click += new System.EventHandler(this.pnlDmColor_Click);
            // 
            // pnlPlayerColor
            // 
            this.pnlPlayerColor.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlPlayerColor.Location = new System.Drawing.Point(242, 37);
            this.pnlPlayerColor.Name = "pnlPlayerColor";
            this.pnlPlayerColor.Size = new System.Drawing.Size(17, 17);
            this.pnlPlayerColor.TabIndex = 4;
            this.pnlPlayerColor.Click += new System.EventHandler(this.pnlPlayerColor_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(173, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Player Color";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(173, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "DM Color";
            // 
            // cbDisplayGrid
            // 
            this.cbDisplayGrid.AutoSize = true;
            this.cbDisplayGrid.Location = new System.Drawing.Point(6, 42);
            this.cbDisplayGrid.Name = "cbDisplayGrid";
            this.cbDisplayGrid.Size = new System.Drawing.Size(82, 17);
            this.cbDisplayGrid.TabIndex = 1;
            this.cbDisplayGrid.Text = "Display Grid";
            this.cbDisplayGrid.UseVisualStyleBackColor = true;
            this.cbDisplayGrid.CheckedChanged += new System.EventHandler(this.cbDisplayGrid_CheckedChanged);
            // 
            // cbDisplayChangesInstantly
            // 
            this.cbDisplayChangesInstantly.AutoSize = true;
            this.cbDisplayChangesInstantly.Location = new System.Drawing.Point(6, 19);
            this.cbDisplayChangesInstantly.Name = "cbDisplayChangesInstantly";
            this.cbDisplayChangesInstantly.Size = new System.Drawing.Size(146, 17);
            this.cbDisplayChangesInstantly.TabIndex = 0;
            this.cbDisplayChangesInstantly.Text = "Display Changes instantly";
            this.cbDisplayChangesInstantly.UseVisualStyleBackColor = true;
            this.cbDisplayChangesInstantly.CheckedChanged += new System.EventHandler(this.cbDisplayChangesInstantly_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbAutoSaveAction);
            this.groupBox3.Location = new System.Drawing.Point(212, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(159, 59);
            this.groupBox3.TabIndex = 7;
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
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.screenSelector1);
            this.groupBox2.Location = new System.Drawing.Point(8, 142);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(771, 345);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Player Screen Selector";
            // 
            // screenSelector1
            // 
            this.screenSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screenSelector1.Location = new System.Drawing.Point(3, 16);
            this.screenSelector1.Name = "screenSelector1";
            this.screenSelector1.Size = new System.Drawing.Size(765, 326);
            this.screenSelector1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbDisplayGridForDM);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nudPlayerSize);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 59);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Grid Settings";
            // 
            // cbDisplayGridForDM
            // 
            this.cbDisplayGridForDM.AutoSize = true;
            this.cbDisplayGridForDM.Location = new System.Drawing.Point(9, 39);
            this.cbDisplayGridForDM.Name = "cbDisplayGridForDM";
            this.cbDisplayGridForDM.Size = new System.Drawing.Size(117, 17);
            this.cbDisplayGridForDM.TabIndex = 10;
            this.cbDisplayGridForDM.Text = "Display Grid for DM";
            this.cbDisplayGridForDM.UseVisualStyleBackColor = true;
            this.cbDisplayGridForDM.CheckedChanged += new System.EventHandler(this.cbDisplayGridForDM_CheckedChanged);
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
            this.nudPlayerSize.Location = new System.Drawing.Point(113, 19);
            this.nudPlayerSize.Name = "nudPlayerSize";
            this.nudPlayerSize.Size = new System.Drawing.Size(54, 20);
            this.nudPlayerSize.TabIndex = 1;
            this.nudPlayerSize.ValueChanged += new System.EventHandler(this.nudPlayerSize_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Player Window Size";
            // 
            // tpScripting
            // 
            this.tpScripting.Controls.Add(this.btnOpenDesigner);
            this.tpScripting.Controls.Add(this.flpScripts);
            this.tpScripting.Location = new System.Drawing.Point(4, 22);
            this.tpScripting.Name = "tpScripting";
            this.tpScripting.Padding = new System.Windows.Forms.Padding(3);
            this.tpScripting.Size = new System.Drawing.Size(835, 493);
            this.tpScripting.TabIndex = 1;
            this.tpScripting.Text = "Scripting";
            this.tpScripting.UseVisualStyleBackColor = true;
            // 
            // btnOpenDesigner
            // 
            this.btnOpenDesigner.Location = new System.Drawing.Point(8, 6);
            this.btnOpenDesigner.Name = "btnOpenDesigner";
            this.btnOpenDesigner.Size = new System.Drawing.Size(91, 23);
            this.btnOpenDesigner.TabIndex = 1;
            this.btnOpenDesigner.Text = "Open Designer";
            this.btnOpenDesigner.UseVisualStyleBackColor = true;
            this.btnOpenDesigner.Click += new System.EventHandler(this.btnOpenDesigner_Click);
            // 
            // flpScripts
            // 
            this.flpScripts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpScripts.AutoScroll = true;
            this.flpScripts.Location = new System.Drawing.Point(6, 35);
            this.flpScripts.Name = "flpScripts";
            this.flpScripts.Size = new System.Drawing.Size(821, 452);
            this.flpScripts.TabIndex = 0;
            // 
            // tbStreamDeck
            // 
            this.tbStreamDeck.Controls.Add(this.streamDeckConfig1);
            this.tbStreamDeck.Location = new System.Drawing.Point(4, 22);
            this.tbStreamDeck.Name = "tbStreamDeck";
            this.tbStreamDeck.Padding = new System.Windows.Forms.Padding(3);
            this.tbStreamDeck.Size = new System.Drawing.Size(835, 493);
            this.tbStreamDeck.TabIndex = 2;
            this.tbStreamDeck.Text = "StreamDeck";
            this.tbStreamDeck.UseVisualStyleBackColor = true;
            // 
            // streamDeckConfig1
            // 
            this.streamDeckConfig1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.streamDeckConfig1.Location = new System.Drawing.Point(3, 3);
            this.streamDeckConfig1.Name = "streamDeckConfig1";
            this.streamDeckConfig1.Size = new System.Drawing.Size(829, 487);
            this.streamDeckConfig1.TabIndex = 0;
            // 
            // btnTestAnimatedMap
            // 
            this.btnTestAnimatedMap.Location = new System.Drawing.Point(554, 71);
            this.btnTestAnimatedMap.Name = "btnTestAnimatedMap";
            this.btnTestAnimatedMap.Size = new System.Drawing.Size(218, 23);
            this.btnTestAnimatedMap.TabIndex = 10;
            this.btnTestAnimatedMap.Text = "Test Animated Map!";
            this.btnTestAnimatedMap.UseVisualStyleBackColor = true;
            this.btnTestAnimatedMap.Click += new System.EventHandler(this.btnTestAnimatedMap_Click);
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 519);
            this.Controls.Add(this.tabCtrlPages);
            this.Name = "Config";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Config";
            this.tabCtrlPages.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudServerPort)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayerSize)).EndInit();
            this.tpScripting.ResumeLayout(false);
            this.tbStreamDeck.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCtrlPages;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudServerPort;
        private System.Windows.Forms.TextBox tbServerIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel pnlTextColor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlGridColor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlDmColor;
        private System.Windows.Forms.Panel pnlPlayerColor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbDisplayGrid;
        private System.Windows.Forms.CheckBox cbDisplayChangesInstantly;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbAutoSaveAction;
        private System.Windows.Forms.GroupBox groupBox2;
        private ScreenSelector screenSelector1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbDisplayGridForDM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudPlayerSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpScripting;
        private System.Windows.Forms.FlowLayoutPanel flpScripts;
        private System.Windows.Forms.Button btnOpenDesigner;
        private System.Windows.Forms.TabPage tbStreamDeck;
        private OpenVTT.StreamDeck.StreamDeckConfig streamDeckConfig1;
        private System.Windows.Forms.Button btnTestAnimatedMap;
    }
}