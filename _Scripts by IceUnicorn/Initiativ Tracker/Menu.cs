var lblName = new Label();
lblName.Name = "lblName";
lblName.Text = "Name";
lblName.Location = new Point(5, 5);
lblName.Font = new Font("Arial", 12);
		
var lblInitiative = new Label();
lblInitiative.Name = "lblInitiative";
lblInitiative.Text = "Initiative";
lblInitiative.Location = new Point(110, 5);
lblInitiative.Font = new Font("Arial", 12);

var lblHP = new Label();
lblHP.Name = "lblHP";
lblHP.Text = "HP";
lblHP.Location = new Point(215, 5);
lblHP.Font = new Font("Arial", 12);

var lblAC = new Label();
lblAC.Name = "lblAC";
lblAC.Text = "AC";
lblAC.Location = new Point(320, 5);
lblAC.Font = new Font("Arial", 12);

Page.Controls.Add(lblName);
Page.Controls.Add(lblInitiative);
Page.Controls.Add(lblHP);
Page.Controls.Add(lblAC);
//-----------------------------------------------------------------------------
var tbName = new TextBox();
tbName.Name = "tbName";
tbName.Location = new Point(5,35);

var tbInitiative = new TextBox();
tbInitiative.Name = "tbInitiative";
tbInitiative.Location = new Point(110,35);

var tbHP = new TextBox();
tbHP.Name = "tbHP";
tbHP.Location = new Point(215,35);

var tbAC = new TextBox();
tbAC.Name = "tbAC";
tbAC.Location = new Point(320,35);	

Page.Controls.Add(tbName);
Page.Controls.Add(tbInitiative);
Page.Controls.Add(tbHP);
Page.Controls.Add(tbAC);
//-----------------------------------------------------------------------------
var list = new List<CreatureDisplay>();
var pos = -1;
CreatureDisplay current = null;

var btnAdd = new Button();
btnAdd.Name = "btnAdd";
btnAdd.Text = "+";
btnAdd.Font = new Font("Arial", 12);
btnAdd.Location = new Point(425,5);
btnAdd.Click += (s, e) =>
{
	var cd = new CreatureDisplay(tbName.Text);
	cd.Initiative = int.TryParse(tbInitiative.Text , out var i) ? i : 0;
	cd.HP = int.TryParse(tbHP.Text , out var h) ? h : 0;
	cd.AC = int.TryParse(tbAC.Text , out var a) ? a : 0;
	cd.killAction = () =>
	{
		list.Remove(cd);
		Page.Controls.Remove(cd);
		
		for(int p = 0; p < list.Count; p++)
			list[p].Location = new Point(20, p * 60 + 60);
	};
	
	tbName.Text = "";
	tbInitiative.Text = "";
	tbAC.Text = "";
	tbHP.Text = "";
	
	Page.Controls.Add(cd);
	
	list.Add(cd);
	list = list.OrderByDescending(n => n.Initiative).ToList();
	
	for(int p = 0; p < list.Count; p++)
		list[p].Location = new Point(20, p * 60 + 60);
	
	pos = -1;
	
	tbName.Focus();
};

var btnNext = new Button();
btnNext.Name = "btnNext";
btnNext.Text = "Next";
btnNext.Font = new Font("Arial", 12);
btnNext.Location = new Point(425,35);
btnNext.Click += (s, e) =>
{
	pos++;
	if(pos >= list.Count) pos = 0;
	if(current != null) current.Location = new Point(current.Location.X - 20, current.Location.Y);
	current = list[pos];
	current.Location = new Point(current.Location.X + 20, current.Location.Y);
};

Page.Controls.Add(btnAdd);
Page.Controls.Add(btnNext);
//-----------------------------------------------------------------------------
Action<object, KeyEventArgs> keyDownAction = (s, e) =>
{
	if(e.KeyCode == Keys.Enter)
		btnAdd.PerformClick();
};

tbName.KeyDown += new KeyEventHandler(keyDownAction);
tbInitiative.KeyDown += new KeyEventHandler(keyDownAction);
tbHP.KeyDown += new KeyEventHandler(keyDownAction);
tbAC.KeyDown += new KeyEventHandler(keyDownAction);