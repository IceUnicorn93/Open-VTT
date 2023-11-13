public partial class Main : System.Windows.Forms.UserControl
{
    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.Label lblInitiative;
    private System.Windows.Forms.Label lblHP;
    private System.Windows.Forms.Label lblAC;
    private System.Windows.Forms.TextBox tbName;
    private System.Windows.Forms.TextBox tbInitiative;
    private System.Windows.Forms.TextBox tbHP;
    private System.Windows.Forms.TextBox tbAC;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Label lblNamePreset;
    private System.Windows.Forms.Label lblInitiativePreset;
    private System.Windows.Forms.Label lblHPPreset;
    private System.Windows.Forms.Label lblACPreset;
    private System.Windows.Forms.TextBox tbNamePreset;
    private System.Windows.Forms.TextBox tbInitiativePreset;
    private System.Windows.Forms.TextBox tbHPPreset;
    private System.Windows.Forms.TextBox tbACPreset;
    private System.Windows.Forms.Button btnAddPreset;
    public Main()
    {
        this.InitializeComponent();
    }
    private void InitializeComponent()
    {
        this.lblName = new System.Windows.Forms.Label();
        this.lblInitiative = new System.Windows.Forms.Label();
        this.lblHP = new System.Windows.Forms.Label();
        this.lblAC = new System.Windows.Forms.Label();
        this.tbName = new System.Windows.Forms.TextBox();
        this.tbInitiative = new System.Windows.Forms.TextBox();
        this.tbHP = new System.Windows.Forms.TextBox();
        this.tbAC = new System.Windows.Forms.TextBox();
        this.btnNext = new System.Windows.Forms.Button();
        this.btnAdd = new System.Windows.Forms.Button();
        this.lblNamePreset = new System.Windows.Forms.Label();
        this.lblInitiativePreset = new System.Windows.Forms.Label();
        this.lblHPPreset = new System.Windows.Forms.Label();
        this.lblACPreset = new System.Windows.Forms.Label();
        this.tbNamePreset = new System.Windows.Forms.TextBox();
        this.tbInitiativePreset = new System.Windows.Forms.TextBox();
        this.tbHPPreset = new System.Windows.Forms.TextBox();
        this.tbACPreset = new System.Windows.Forms.TextBox();
        this.btnAddPreset = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // lblName
        // 
        this.lblName.AutoSize = true;
        this.lblName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblName.Location = new System.Drawing.Point(5, 5);
        this.lblName.Name = "lblName";
        this.lblName.Size = new System.Drawing.Size(50, 18);
        this.lblName.TabIndex = 0;
        this.lblName.Text = "Name";
        // 
        // lblInitiative
        // 
        this.lblInitiative.AutoSize = true;
        this.lblInitiative.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblInitiative.Location = new System.Drawing.Point(111, 5);
        this.lblInitiative.Name = "lblInitiative";
        this.lblInitiative.Size = new System.Drawing.Size(64, 18);
        this.lblInitiative.TabIndex = 1;
        this.lblInitiative.Text = "Initiative";
        // 
        // lblHP
        // 
        this.lblHP.AutoSize = true;
        this.lblHP.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblHP.Location = new System.Drawing.Point(217, 5);
        this.lblHP.Name = "lblHP";
        this.lblHP.Size = new System.Drawing.Size(30, 18);
        this.lblHP.TabIndex = 2;
        this.lblHP.Text = "HP";
        // 
        // lblAC
        // 
        this.lblAC.AutoSize = true;
        this.lblAC.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblAC.Location = new System.Drawing.Point(323, 5);
        this.lblAC.Name = "lblAC";
        this.lblAC.Size = new System.Drawing.Size(31, 18);
        this.lblAC.TabIndex = 3;
        this.lblAC.Text = "AC";
        // 
        // tbName
        // 
        this.tbName.Location = new System.Drawing.Point(5, 35);
        this.tbName.Name = "tbName";
        this.tbName.Size = new System.Drawing.Size(100, 20);
        this.tbName.TabIndex = 4;
        // 
        // tbInitiative
        // 
        this.tbInitiative.Location = new System.Drawing.Point(111, 35);
        this.tbInitiative.Name = "tbInitiative";
        this.tbInitiative.Size = new System.Drawing.Size(100, 20);
        this.tbInitiative.TabIndex = 5;
        // 
        // tbHP
        // 
        this.tbHP.Location = new System.Drawing.Point(217, 35);
        this.tbHP.Name = "tbHP";
        this.tbHP.Size = new System.Drawing.Size(100, 20);
        this.tbHP.TabIndex = 6;
        // 
        // tbAC
        // 
        this.tbAC.Location = new System.Drawing.Point(323, 35);
        this.tbAC.Name = "tbAC";
        this.tbAC.Size = new System.Drawing.Size(100, 20);
        this.tbAC.TabIndex = 7;
        // 
        // btnNext
        // 
        this.btnNext.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnNext.Location = new System.Drawing.Point(429, 32);
        this.btnNext.Name = "btnNext";
        this.btnNext.Size = new System.Drawing.Size(75, 23);
        this.btnNext.TabIndex = 8;
        this.btnNext.Text = "Next";
        // 
        // btnAdd
        // 
        this.btnAdd.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnAdd.Location = new System.Drawing.Point(429, 3);
        this.btnAdd.Name = "btnAdd";
        this.btnAdd.Size = new System.Drawing.Size(75, 23);
        this.btnAdd.TabIndex = 9;
        this.btnAdd.Text = "+";
        // 
        // lblNamePreset
        // 
        this.lblNamePreset.AutoSize = true;
        this.lblNamePreset.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblNamePreset.Location = new System.Drawing.Point(605, 5);
        this.lblNamePreset.Name = "lblNamePreset";
        this.lblNamePreset.Size = new System.Drawing.Size(100, 18);
        this.lblNamePreset.TabIndex = 10;
        this.lblNamePreset.Text = "Preset Name";
        // 
        // lblInitiativePreset
        // 
        this.lblInitiativePreset.AutoSize = true;
        this.lblInitiativePreset.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblInitiativePreset.Location = new System.Drawing.Point(711, 5);
        this.lblInitiativePreset.Name = "lblInitiativePreset";
        this.lblInitiativePreset.Size = new System.Drawing.Size(64, 18);
        this.lblInitiativePreset.TabIndex = 11;
        this.lblInitiativePreset.Text = "Initiative";
        // 
        // lblHPPreset
        // 
        this.lblHPPreset.AutoSize = true;
        this.lblHPPreset.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblHPPreset.Location = new System.Drawing.Point(815, 5);
        this.lblHPPreset.Name = "lblHPPreset";
        this.lblHPPreset.Size = new System.Drawing.Size(30, 18);
        this.lblHPPreset.TabIndex = 12;
        this.lblHPPreset.Text = "HP";
        // 
        // lblACPreset
        // 
        this.lblACPreset.AutoSize = true;
        this.lblACPreset.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblACPreset.Location = new System.Drawing.Point(921, 5);
        this.lblACPreset.Name = "lblACPreset";
        this.lblACPreset.Size = new System.Drawing.Size(31, 18);
        this.lblACPreset.TabIndex = 13;
        this.lblACPreset.Text = "AC";
        // 
        // tbNamePreset
        // 
        this.tbNamePreset.Location = new System.Drawing.Point(605, 35);
        this.tbNamePreset.Name = "tbNamePreset";
        this.tbNamePreset.Size = new System.Drawing.Size(100, 20);
        this.tbNamePreset.TabIndex = 14;
        // 
        // tbInitiativePreset
        // 
        this.tbInitiativePreset.Location = new System.Drawing.Point(711, 35);
        this.tbInitiativePreset.Name = "tbInitiativePreset";
        this.tbInitiativePreset.Size = new System.Drawing.Size(100, 20);
        this.tbInitiativePreset.TabIndex = 15;
        // 
        // tbHPPreset
        // 
        this.tbHPPreset.Location = new System.Drawing.Point(815, 35);
        this.tbHPPreset.Name = "tbHPPreset";
        this.tbHPPreset.Size = new System.Drawing.Size(100, 20);
        this.tbHPPreset.TabIndex = 16;
        // 
        // tbACPreset
        // 
        this.tbACPreset.Location = new System.Drawing.Point(921, 35);
        this.tbACPreset.Name = "tbACPreset";
        this.tbACPreset.Size = new System.Drawing.Size(100, 20);
        this.tbACPreset.TabIndex = 17;
        // 
        // btnAddPreset
        // 
        this.btnAddPreset.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnAddPreset.Location = new System.Drawing.Point(1036, 3);
        this.btnAddPreset.Name = "btnAddPreset";
        this.btnAddPreset.Size = new System.Drawing.Size(75, 23);
        this.btnAddPreset.TabIndex = 18;
        this.btnAddPreset.Text = "+";
        // 
        // Main
        // 
        this.Controls.Add(this.lblName);
        this.Controls.Add(this.lblInitiative);
        this.Controls.Add(this.lblHP);
        this.Controls.Add(this.lblAC);
        this.Controls.Add(this.tbName);
        this.Controls.Add(this.tbInitiative);
        this.Controls.Add(this.tbHP);
        this.Controls.Add(this.tbAC);
        this.Controls.Add(this.btnNext);
        this.Controls.Add(this.btnAdd);
        this.Controls.Add(this.lblNamePreset);
        this.Controls.Add(this.lblInitiativePreset);
        this.Controls.Add(this.lblHPPreset);
        this.Controls.Add(this.lblACPreset);
        this.Controls.Add(this.tbNamePreset);
        this.Controls.Add(this.tbInitiativePreset);
        this.Controls.Add(this.tbHPPreset);
        this.Controls.Add(this.tbACPreset);
        this.Controls.Add(this.btnAddPreset);
        this.Name = "Main";
        this.Size = new System.Drawing.Size(1143, 189);
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
