var lblNamePreset = new Label();
lblNamePreset.Name = "lblNamePreset";
lblNamePreset.Text = "Preset Name";
lblNamePreset.Location = new Point(605, 5);
lblNamePreset.Font = new Font("Arial", 12);
		
var lblInitiativePreset = new Label();
lblInitiativePreset.Name = "lblInitiativePreset";
lblInitiativePreset.Text = "Initiative";
lblInitiativePreset.Location = new Point(710, 5);
lblInitiativePreset.Font = new Font("Arial", 12);

var lblHPPreset = new Label();
lblHPPreset.Name = "lblHPPreset";
lblHPPreset.Text = "HP";
lblHPPreset.Location = new Point(815, 5);
lblHPPreset.Font = new Font("Arial", 12);

var lblACPreset = new Label();
lblACPreset.Name = "lblACPreset";
lblACPreset.Text = "AC";
lblACPreset.Location = new Point(920, 5);
lblACPreset.Font = new Font("Arial", 12);

Page.Controls.Add(lblNamePreset);
Page.Controls.Add(lblInitiativePreset);
Page.Controls.Add(lblHPPreset);
Page.Controls.Add(lblACPreset);
//-----------------------------------------------------------------------------
var tbNamePreset = new TextBox();
tbNamePreset.Name = "tbNamePreset";
tbNamePreset.Location = new Point(605,35);

var tbInitiativePreset = new TextBox();
tbInitiativePreset.Name = "tbInitiativePreset";
tbInitiativePreset.Location = new Point(710,35);

var tbHPPreset = new TextBox();
tbHPPreset.Name = "tbHPPreset";
tbHPPreset.Location = new Point(815,35);

var tbACPreset = new TextBox();
tbACPreset.Name = "tbACPreset";
tbACPreset.Location = new Point(920,35);	

Page.Controls.Add(tbNamePreset);
Page.Controls.Add(tbInitiativePreset);
Page.Controls.Add(tbHPPreset);
Page.Controls.Add(tbACPreset);
//-----------------------------------------------------------------------------
var listPreset = new List<CreatureDisplay>();

var btnAddPreset = new Button();
btnAddPreset.Name = "btnAddPreset";
btnAddPreset.Text = "+";
btnAddPreset.Font = new Font("Arial", 12);
btnAddPreset.Location = new Point(1025,5);
btnAddPreset.Click += (s, e) =>
{
	var cd = new CreatureDisplay(tbNamePreset.Text);
	cd.Initiative = int.TryParse(tbInitiativePreset.Text , out var i) ? i : 0;
	cd.HP = int.TryParse(tbHPPreset.Text , out var h) ? h : 0;
	cd.AC = int.TryParse(tbACPreset.Text , out var a) ? a : 0;
	cd.killAction = () =>
	{
		listPreset.Remove(cd);
		Page.Controls.Remove(cd);
		
		for(int p = 0; p < listPreset.Count; p++)
			listPreset[p].Location = new Point(620, p * 60 + 60);
	};
	
	cd.Click += (s, e) => 
	{
		tbName.Text = cd.CreatureName;
		tbAC.Text = cd.AC.ToString();
		tbHP.Text = cd.HP.ToString();
		tbInitiative.Text = cd.Initiative.ToString();
	};
	
	var customData = Session.Values.CustomData.SingleOrDefault(n => n.ScriptName == Config.Name);
	if(customData == null)Session.Values.CustomData.Add(new CustomSettings{ScriptName = Config.Name});
	customData = Session.Values.CustomData.SingleOrDefault(n => n.ScriptName == Config.Name);
	
	var co = new CustomObject();
	co.ObjectName = tbNamePreset.Text;
	co.ObjectData.Add(new CustomObjectData { Name = "AC", Value = tbACPreset.Text } );
	co.ObjectData.Add(new CustomObjectData { Name = "HP", Value = tbHPPreset.Text } );
	co.ObjectData.Add(new CustomObjectData { Name = "Initiative", Value = tbInitiativePreset.Text } );
	
	customData.ScriptObjects.Add(co);
	
	tbNamePreset.Text = "";
	tbInitiativePreset.Text = "";
	tbACPreset.Text = "";
	tbHPPreset.Text = "";
	
	Page.Controls.Add(cd);
	
	listPreset.Add(cd);
	listPreset = listPreset.OrderBy(n => n.CreatureName).ToList();
	
	for(int p = 0; p < listPreset.Count; p++)
		listPreset[p].Location = new Point(620, p * 60 + 60);
	
	tbNamePreset.Focus();
};

Page.Controls.Add(btnAddPreset);
//-----------------------------------------------------------------------------
Action<object, KeyEventArgs> keyDownActionPreset = (s, e) =>
{
	if(e.KeyCode == Keys.Enter)
		btnAddPreset.PerformClick();
};

tbNamePreset.KeyDown += new KeyEventHandler(keyDownActionPreset);
tbInitiativePreset.KeyDown += new KeyEventHandler(keyDownActionPreset);
tbHPPreset.KeyDown += new KeyEventHandler(keyDownActionPreset);
tbACPreset.KeyDown += new KeyEventHandler(keyDownActionPreset);
//-----------------------------------------------------------------------------
var customData = Session.Values.CustomData.SingleOrDefault(n => n.ScriptName == Config.Name);
if(customData != null)
{
	foreach(var obj in customData.ScriptObjects)
	{
		var objName = obj.ObjectName;
		
		var cd = new CreatureDisplay(objName);
		cd.killAction = () =>
		{
			listPreset.Remove(cd);
			Page.Controls.Remove(cd);
			
			customData.ScriptObjects.Remove(obj);
			
			for(int p = 0; p < listPreset.Count; p++)
				listPreset[p].Location = new Point(620, p * 60 + 60);
		};
		
		cd.Click += (s, e) => 
		{
			tbName.Text = cd.CreatureName;
			tbAC.Text = cd.AC.ToString();
			tbHP.Text = cd.HP.ToString();
			tbInitiative.Text = cd.Initiative.ToString();
		};
		
		foreach(var objData in obj.ObjectData)
		{
			if(objData.Name == "AC") cd.AC = objData.GetValue<int>();
			if(objData.Name == "HP") cd.HP = objData.GetValue<int>();
			if(objData.Name == "Initiative") cd.Initiative = objData.GetValue<int>();
		}
		
		Page.Controls.Add(cd);
		listPreset.Add(cd);
	}
	
	for(int p = 0; p < listPreset.Count; p++)
		listPreset[p].Location = new Point(620, p * 60 + 60);
}