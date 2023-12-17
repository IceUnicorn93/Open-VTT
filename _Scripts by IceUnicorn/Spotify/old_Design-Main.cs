public partial class Main : System.Windows.Forms.UserControl
{
    private System.Windows.Forms.Label lblClientID;
    private System.Windows.Forms.TextBox tbClientID;
    private System.Windows.Forms.TextBox tbClientSecret;
    private System.Windows.Forms.TextBox tbCallback;
    private System.Windows.Forms.TextBox tbPort;
    private System.Windows.Forms.Label lblClientSecret;
    private System.Windows.Forms.Label lblCallback;
    private System.Windows.Forms.Label lblPort;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.Button btnConnect;
    private System.Windows.Forms.Label lblDeviceId;
    private System.Windows.Forms.TextBox tbDeviceID;
    private System.Windows.Forms.TextBox tbShuffleMode;
    private System.Windows.Forms.TextBox tbRepeatMode;
    private System.Windows.Forms.Label lblShuffleMode;
    private System.Windows.Forms.Label lblRepeatMode;
    private System.Windows.Forms.Button btnShuffleOff;
    private System.Windows.Forms.Button btnShuffleOn;
    private System.Windows.Forms.Button btnRepeatOff;
    private System.Windows.Forms.Button btnRepeatContext;
    private System.Windows.Forms.Button btnRepeatSong;
    private System.Windows.Forms.Button btnPlay;
    private System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.Button btnPrevious;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Panel panel0;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.TextBox tbName;
    private System.Windows.Forms.Label lblURL;
    private System.Windows.Forms.TextBox tbURL;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.FlowLayoutPanel flp;
    private System.Windows.Forms.Button btnSaveDefault;
    private System.Windows.Forms.Button btnLoadDefault;
    private System.Windows.Forms.Panel panel2;
    public Main()
    {
        this.InitializeComponent();
    }
    private void InitializeComponent()
    {
        this.lblClientID = new System.Windows.Forms.Label();
        this.tbClientID = new System.Windows.Forms.TextBox();
        this.tbClientSecret = new System.Windows.Forms.TextBox();
        this.tbCallback = new System.Windows.Forms.TextBox();
        this.tbPort = new System.Windows.Forms.TextBox();
        this.lblClientSecret = new System.Windows.Forms.Label();
        this.lblCallback = new System.Windows.Forms.Label();
        this.lblPort = new System.Windows.Forms.Label();
        this.btnSave = new System.Windows.Forms.Button();
        this.btnLoad = new System.Windows.Forms.Button();
        this.btnConnect = new System.Windows.Forms.Button();
        this.lblDeviceId = new System.Windows.Forms.Label();
        this.tbDeviceID = new System.Windows.Forms.TextBox();
        this.tbShuffleMode = new System.Windows.Forms.TextBox();
        this.tbRepeatMode = new System.Windows.Forms.TextBox();
        this.lblShuffleMode = new System.Windows.Forms.Label();
        this.lblRepeatMode = new System.Windows.Forms.Label();
        this.btnShuffleOff = new System.Windows.Forms.Button();
        this.btnShuffleOn = new System.Windows.Forms.Button();
        this.btnRepeatOff = new System.Windows.Forms.Button();
        this.btnRepeatContext = new System.Windows.Forms.Button();
        this.btnRepeatSong = new System.Windows.Forms.Button();
        this.btnPlay = new System.Windows.Forms.Button();
        this.btnStop = new System.Windows.Forms.Button();
        this.btnPrevious = new System.Windows.Forms.Button();
        this.btnNext = new System.Windows.Forms.Button();
        this.panel0 = new System.Windows.Forms.Panel();
        this.panel1 = new System.Windows.Forms.Panel();
        this.lblName = new System.Windows.Forms.Label();
        this.tbName = new System.Windows.Forms.TextBox();
        this.lblURL = new System.Windows.Forms.Label();
        this.tbURL = new System.Windows.Forms.TextBox();
        this.btnAdd = new System.Windows.Forms.Button();
        this.flp = new System.Windows.Forms.FlowLayoutPanel();
        this.btnSaveDefault = new System.Windows.Forms.Button();
        this.btnLoadDefault = new System.Windows.Forms.Button();
        this.panel2 = new System.Windows.Forms.Panel();
        this.panel0.SuspendLayout();
        this.panel1.SuspendLayout();
        this.panel2.SuspendLayout();
        this.SuspendLayout();
        // 
        // lblClientID
        // 
        this.lblClientID.AutoSize = true;
        this.lblClientID.Location = new System.Drawing.Point(3, 6);
        this.lblClientID.Name = "lblClientID";
        this.lblClientID.Size = new System.Drawing.Size(50, 13);
        this.lblClientID.TabIndex = 0;
        this.lblClientID.Text = "Client ID:";
        // 
        // tbClientID
        // 
        this.tbClientID.Location = new System.Drawing.Point(84, 3);
        this.tbClientID.Name = "tbClientID";
        this.tbClientID.PasswordChar = '*';
        this.tbClientID.Size = new System.Drawing.Size(217, 20);
        this.tbClientID.TabIndex = 1;
        // 
        // tbClientSecret
        // 
        this.tbClientSecret.Location = new System.Drawing.Point(84, 29);
        this.tbClientSecret.Name = "tbClientSecret";
        this.tbClientSecret.PasswordChar = '*';
        this.tbClientSecret.Size = new System.Drawing.Size(217, 20);
        this.tbClientSecret.TabIndex = 2;
        // 
        // tbCallback
        // 
        this.tbCallback.Location = new System.Drawing.Point(84, 55);
        this.tbCallback.Name = "tbCallback";
        this.tbCallback.Size = new System.Drawing.Size(217, 20);
        this.tbCallback.TabIndex = 3;
        // 
        // tbPort
        // 
        this.tbPort.Location = new System.Drawing.Point(84, 81);
        this.tbPort.Name = "tbPort";
        this.tbPort.Size = new System.Drawing.Size(217, 20);
        this.tbPort.TabIndex = 4;
        // 
        // lblClientSecret
        // 
        this.lblClientSecret.AutoSize = true;
        this.lblClientSecret.Location = new System.Drawing.Point(3, 32);
        this.lblClientSecret.Name = "lblClientSecret";
        this.lblClientSecret.Size = new System.Drawing.Size(70, 13);
        this.lblClientSecret.TabIndex = 5;
        this.lblClientSecret.Text = "Client Secret:";
        // 
        // lblCallback
        // 
        this.lblCallback.AutoSize = true;
        this.lblCallback.Location = new System.Drawing.Point(3, 58);
        this.lblCallback.Name = "lblCallback";
        this.lblCallback.Size = new System.Drawing.Size(51, 13);
        this.lblCallback.TabIndex = 6;
        this.lblCallback.Text = "Callback:";
        // 
        // lblPort
        // 
        this.lblPort.AutoSize = true;
        this.lblPort.Location = new System.Drawing.Point(3, 84);
        this.lblPort.Name = "lblPort";
        this.lblPort.Size = new System.Drawing.Size(29, 13);
        this.lblPort.TabIndex = 7;
        this.lblPort.Text = "Port:";
        // 
        // btnSave
        // 
        this.btnSave.Location = new System.Drawing.Point(3, 133);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new System.Drawing.Size(75, 23);
        this.btnSave.TabIndex = 8;
        this.btnSave.Text = "Save Config";
        // 
        // btnLoad
        // 
        this.btnLoad.Location = new System.Drawing.Point(84, 133);
        this.btnLoad.Name = "btnLoad";
        this.btnLoad.Size = new System.Drawing.Size(75, 23);
        this.btnLoad.TabIndex = 9;
        this.btnLoad.Text = "Load Config";
        // 
        // btnConnect
        // 
        this.btnConnect.Location = new System.Drawing.Point(165, 133);
        this.btnConnect.Name = "btnConnect";
        this.btnConnect.Size = new System.Drawing.Size(75, 23);
        this.btnConnect.TabIndex = 10;
        this.btnConnect.Text = "Connect";
        // 
        // lblDeviceId
        // 
        this.lblDeviceId.AutoSize = true;
        this.lblDeviceId.Location = new System.Drawing.Point(3, 110);
        this.lblDeviceId.Name = "lblDeviceId";
        this.lblDeviceId.Size = new System.Drawing.Size(58, 13);
        this.lblDeviceId.TabIndex = 15;
        this.lblDeviceId.Text = "Device ID:";
        // 
        // tbDeviceID
        // 
        this.tbDeviceID.Location = new System.Drawing.Point(84, 107);
        this.tbDeviceID.Name = "tbDeviceID";
        this.tbDeviceID.ReadOnly = true;
        this.tbDeviceID.Size = new System.Drawing.Size(217, 20);
        this.tbDeviceID.TabIndex = 12;
        // 
        // tbShuffleMode
        // 
        this.tbShuffleMode.Location = new System.Drawing.Point(83, 5);
        this.tbShuffleMode.Name = "tbShuffleMode";
        this.tbShuffleMode.ReadOnly = true;
        this.tbShuffleMode.Size = new System.Drawing.Size(50, 20);
        this.tbShuffleMode.TabIndex = 13;
        // 
        // tbRepeatMode
        // 
        this.tbRepeatMode.Location = new System.Drawing.Point(83, 31);
        this.tbRepeatMode.Name = "tbRepeatMode";
        this.tbRepeatMode.ReadOnly = true;
        this.tbRepeatMode.Size = new System.Drawing.Size(50, 20);
        this.tbRepeatMode.TabIndex = 14;
        // 
        // lblShuffleMode
        // 
        this.lblShuffleMode.AutoSize = true;
        this.lblShuffleMode.Location = new System.Drawing.Point(4, 8);
        this.lblShuffleMode.Name = "lblShuffleMode";
        this.lblShuffleMode.Size = new System.Drawing.Size(73, 13);
        this.lblShuffleMode.TabIndex = 16;
        this.lblShuffleMode.Text = "Shuffle Mode:";
        // 
        // lblRepeatMode
        // 
        this.lblRepeatMode.AutoSize = true;
        this.lblRepeatMode.Location = new System.Drawing.Point(4, 34);
        this.lblRepeatMode.Name = "lblRepeatMode";
        this.lblRepeatMode.Size = new System.Drawing.Size(75, 13);
        this.lblRepeatMode.TabIndex = 17;
        this.lblRepeatMode.Text = "Repeat Mode:";
        // 
        // btnShuffleOff
        // 
        this.btnShuffleOff.Location = new System.Drawing.Point(139, 3);
        this.btnShuffleOff.Name = "btnShuffleOff";
        this.btnShuffleOff.Size = new System.Drawing.Size(50, 23);
        this.btnShuffleOff.TabIndex = 18;
        this.btnShuffleOff.Text = "Off";
        // 
        // btnShuffleOn
        // 
        this.btnShuffleOn.Location = new System.Drawing.Point(195, 3);
        this.btnShuffleOn.Name = "btnShuffleOn";
        this.btnShuffleOn.Size = new System.Drawing.Size(50, 23);
        this.btnShuffleOn.TabIndex = 19;
        this.btnShuffleOn.Text = "On";
        // 
        // btnRepeatOff
        // 
        this.btnRepeatOff.Location = new System.Drawing.Point(139, 29);
        this.btnRepeatOff.Name = "btnRepeatOff";
        this.btnRepeatOff.Size = new System.Drawing.Size(50, 23);
        this.btnRepeatOff.TabIndex = 20;
        this.btnRepeatOff.Text = "Off";
        // 
        // btnRepeatContext
        // 
        this.btnRepeatContext.Location = new System.Drawing.Point(195, 29);
        this.btnRepeatContext.Name = "btnRepeatContext";
        this.btnRepeatContext.Size = new System.Drawing.Size(50, 23);
        this.btnRepeatContext.TabIndex = 21;
        this.btnRepeatContext.Text = "Context";
        // 
        // btnRepeatSong
        // 
        this.btnRepeatSong.Location = new System.Drawing.Point(251, 28);
        this.btnRepeatSong.Name = "btnRepeatSong";
        this.btnRepeatSong.Size = new System.Drawing.Size(50, 23);
        this.btnRepeatSong.TabIndex = 22;
        this.btnRepeatSong.Text = "Song";
        // 
        // btnPlay
        // 
        this.btnPlay.Location = new System.Drawing.Point(29, 58);
        this.btnPlay.Name = "btnPlay";
        this.btnPlay.Size = new System.Drawing.Size(50, 23);
        this.btnPlay.TabIndex = 24;
        this.btnPlay.Text = "Play";
        // 
        // btnStop
        // 
        this.btnStop.Location = new System.Drawing.Point(85, 58);
        this.btnStop.Name = "btnStop";
        this.btnStop.Size = new System.Drawing.Size(50, 23);
        this.btnStop.TabIndex = 24;
        this.btnStop.Text = "Stop";
        // 
        // btnPrevious
        // 
        this.btnPrevious.Location = new System.Drawing.Point(195, 58);
        this.btnPrevious.Name = "btnPrevious";
        this.btnPrevious.Size = new System.Drawing.Size(50, 23);
        this.btnPrevious.TabIndex = 24;
        this.btnPrevious.Text = "Prev.";
        // 
        // btnNext
        // 
        this.btnNext.Location = new System.Drawing.Point(139, 58);
        this.btnNext.Name = "btnNext";
        this.btnNext.Size = new System.Drawing.Size(50, 23);
        this.btnNext.TabIndex = 24;
        this.btnNext.Text = "Next";
        // 
        // panel0
        // 
        this.panel0.Controls.Add(this.lblClientID);
        this.panel0.Controls.Add(this.lblDeviceId);
        this.panel0.Controls.Add(this.tbClientID);
        this.panel0.Controls.Add(this.tbDeviceID);
        this.panel0.Controls.Add(this.btnConnect);
        this.panel0.Controls.Add(this.tbClientSecret);
        this.panel0.Controls.Add(this.btnLoad);
        this.panel0.Controls.Add(this.tbCallback);
        this.panel0.Controls.Add(this.btnSave);
        this.panel0.Controls.Add(this.tbPort);
        this.panel0.Controls.Add(this.lblPort);
        this.panel0.Controls.Add(this.lblClientSecret);
        this.panel0.Controls.Add(this.lblCallback);
        this.panel0.Location = new System.Drawing.Point(3, 132);
        this.panel0.Name = "panel0";
        this.panel0.Size = new System.Drawing.Size(304, 160);
        this.panel0.TabIndex = 11;
        // 
        // panel1
        // 
        this.panel1.Controls.Add(this.btnRepeatSong);
        this.panel1.Controls.Add(this.tbShuffleMode);
        this.panel1.Controls.Add(this.btnPlay);
        this.panel1.Controls.Add(this.btnRepeatContext);
        this.panel1.Controls.Add(this.btnStop);
        this.panel1.Controls.Add(this.btnPrevious);
        this.panel1.Controls.Add(this.btnNext);
        this.panel1.Controls.Add(this.tbRepeatMode);
        this.panel1.Controls.Add(this.btnRepeatOff);
        this.panel1.Controls.Add(this.btnShuffleOn);
        this.panel1.Controls.Add(this.lblShuffleMode);
        this.panel1.Controls.Add(this.btnShuffleOff);
        this.panel1.Controls.Add(this.lblRepeatMode);
        this.panel1.Location = new System.Drawing.Point(3, 3);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(304, 88);
        this.panel1.TabIndex = 23;
        // 
        // lblName
        // 
        this.lblName.AutoSize = true;
        this.lblName.Location = new System.Drawing.Point(313, 11);
        this.lblName.Name = "lblName";
        this.lblName.Size = new System.Drawing.Size(38, 13);
        this.lblName.TabIndex = 24;
        this.lblName.Text = "Name:";
        // 
        // tbName
        // 
        this.tbName.Location = new System.Drawing.Point(357, 8);
        this.tbName.Name = "tbName";
        this.tbName.Size = new System.Drawing.Size(160, 20);
        this.tbName.TabIndex = 25;
        // 
        // lblURL
        // 
        this.lblURL.AutoSize = true;
        this.lblURL.Location = new System.Drawing.Point(523, 11);
        this.lblURL.Name = "lblURL";
        this.lblURL.Size = new System.Drawing.Size(29, 13);
        this.lblURL.TabIndex = 26;
        this.lblURL.Text = "URL";
        // 
        // tbURL
        // 
        this.tbURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.tbURL.Location = new System.Drawing.Point(558, 8);
        this.tbURL.Name = "tbURL";
        this.tbURL.Size = new System.Drawing.Size(280, 20);
        this.tbURL.TabIndex = 27;
        // 
        // btnAdd
        // 
        this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnAdd.Location = new System.Drawing.Point(844, 6);
        this.btnAdd.Name = "btnAdd";
        this.btnAdd.Size = new System.Drawing.Size(50, 23);
        this.btnAdd.TabIndex = 28;
        this.btnAdd.Text = "Add";
        // 
        // flp
        // 
        this.flp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                    | System.Windows.Forms.AnchorStyles.Left) 
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.flp.AutoScroll = true;
        this.flp.BackColor = System.Drawing.SystemColors.ControlDark;
        this.flp.Location = new System.Drawing.Point(313, 34);
        this.flp.Name = "flp";
        this.flp.Size = new System.Drawing.Size(581, 370);
        this.flp.TabIndex = 29;
        // 
        // btnSaveDefault
        // 
        this.btnSaveDefault.Location = new System.Drawing.Point(29, 3);
        this.btnSaveDefault.Name = "btnSaveDefault";
        this.btnSaveDefault.Size = new System.Drawing.Size(106, 23);
        this.btnSaveDefault.TabIndex = 30;
        this.btnSaveDefault.Text = "Save Default";
        // 
        // btnLoadDefault
        // 
        this.btnLoadDefault.Location = new System.Drawing.Point(141, 3);
        this.btnLoadDefault.Name = "btnLoadDefault";
        this.btnLoadDefault.Size = new System.Drawing.Size(106, 23);
        this.btnLoadDefault.TabIndex = 31;
        this.btnLoadDefault.Text = "Load Default";
        // 
        // panel2
        // 
        this.panel2.Controls.Add(this.btnSaveDefault);
        this.panel2.Controls.Add(this.btnLoadDefault);
        this.panel2.Location = new System.Drawing.Point(3, 97);
        this.panel2.Name = "panel2";
        this.panel2.Size = new System.Drawing.Size(304, 29);
        this.panel2.TabIndex = 32;
        // 
        // Main
        // 
        this.Controls.Add(this.panel0);
        this.Controls.Add(this.panel1);
        this.Controls.Add(this.lblName);
        this.Controls.Add(this.tbName);
        this.Controls.Add(this.lblURL);
        this.Controls.Add(this.tbURL);
        this.Controls.Add(this.btnAdd);
        this.Controls.Add(this.flp);
        this.Controls.Add(this.panel2);
        this.Name = "Main";
        this.Size = new System.Drawing.Size(897, 407);
        this.panel0.ResumeLayout(false);
        this.panel0.PerformLayout();
        this.panel1.ResumeLayout(false);
        this.panel1.PerformLayout();
        this.panel2.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
