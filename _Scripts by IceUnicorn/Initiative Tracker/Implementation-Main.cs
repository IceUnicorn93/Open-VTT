class RoundEvent : UserControl, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;
	
	private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
	{
		if (PropertyChanged == null) return;
		PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
	}
	
	public RoundEvent()
	{
		Size = new Size(424, 26);
		BackColor = System.Drawing.SystemColors.Control;
		
		var tbName = new TextBox();
		tbName.Name = "tbName";
		tbName.Location = new Point(100, 3);
		tbName.Size = new Size(324, 20);
		tbName.DataBindings.Add(nameof(tbName.Text), this, nameof(EventName));
		
		Controls.Add(tbName);
		
		var tbRounds = new TextBox();
		tbRounds.Name = "tbRounds";
		tbRounds.Location = new Point(3, 3);
		tbRounds.Size = new Size(94, 20);
		tbRounds.DataBindings.Add(nameof(tbName.Text), this, nameof(Rounds));
		
		Controls.Add(tbRounds);
	}
	
	private string _EventName;
	public string EventName { get => _EventName; set { _EventName = value; NotifyPropertyChanged(); } }
	
	private int _Rounds;
	public int Rounds { get => _Rounds; set { _Rounds = value; NotifyPropertyChanged();} }
	
	public override string ToString() => $"{Rounds} - {EventName}";
	public bool NewRound()
	{
		Rounds--;
		if(Rounds == 0)
		{
			MessageBox.Show(EventName);
			return true;
		}
		else
			return false;
	}
}

public partial class Main : System.Windows.Forms.UserControl
{
	public Main(object o)
	{
		this.InitializeComponent();
		
		//--------------------------------------
		//Implementation by: Dustin/IceUnicorn93
		//Feel free to change it how you like it
		//--------------------------------------
		
		var list = new List<CreatureDisplay>();
		var listPreset = new List<CreatureDisplay>();
		
		var pos = -1;
		CreatureDisplay current = null;
		
		Action<object, KeyEventArgs> keyDownAction = (s, e) =>
		{
			if(e.KeyCode == Keys.Enter)
				btnAdd.PerformClick();
		};
		
		Action<object, KeyEventArgs> keyDownActionPreset = (s, e) =>
		{
			if(e.KeyCode == Keys.Enter)
				btnAddPreset.PerformClick();
		};
		
		tbName.KeyDown += new KeyEventHandler(keyDownAction);
		tbInitiative.KeyDown += new KeyEventHandler(keyDownAction);
		tbHP.KeyDown += new KeyEventHandler(keyDownAction);
		tbAC.KeyDown += new KeyEventHandler(keyDownAction);
		
		tbNamePreset.KeyDown += new KeyEventHandler(keyDownActionPreset);
		tbInitiativePreset.KeyDown += new KeyEventHandler(keyDownActionPreset);
		tbHPPreset.KeyDown += new KeyEventHandler(keyDownActionPreset);
		tbACPreset.KeyDown += new KeyEventHandler(keyDownActionPreset);
		
		btnNext.Click += (s, e) =>
		{
			pos++;
			if(pos >= list.Count) pos = 0;
			
			if(pos == 0)
			{
				//Event Mechanic
				var events = flpEvent.Controls.Cast<RoundEvent>().ToList();
				for (int i = 0; i < events.Count; i++)
					if(events[i].NewRound()) flpEvent.Controls.Remove(events[i]);
			}
			
			if(current != null) current.Location = new Point(current.Location.X - 20, current.Location.Y);
			current = list[pos];
			current.Location = new Point(current.Location.X + 20, current.Location.Y);
			DisplayArtworkText(current.CreatureName);
			//DisplayArtworkImage(null);
		};
		btnAdd.Click += (s, e) =>
		{
			var cd = new CreatureDisplay(tbName.Text);
			cd.Initiative = int.TryParse(tbInitiative.Text , out var i) ? i : 0;
			cd.HP = int.TryParse(tbHP.Text , out var h) ? h : 0;
			cd.AC = int.TryParse(tbAC.Text , out var a) ? a : 0;
			cd.killAction = () =>
			{
				list.Remove(cd);
				this.Controls.Remove(cd);
				
				for(int p = 0; p < list.Count; p++)
					list[p].Location = new Point(20, p * 60 + 60);
			};
			
			tbName.Text = "";
			tbInitiative.Text = "";
			tbAC.Text = "";
			tbHP.Text = "";
			
			this.Controls.Add(cd);
			
			list.Add(cd);
			list = list.OrderByDescending(n => n.Initiative).ToList();
			
			for(int p = 0; p < list.Count; p++)
				list[p].Location = new Point(20, p * 60 + 60);
			
			pos = -1;
			current = null;
			
			tbName.Focus();
		};
		btnAddPreset.Click += (s, e) =>
		{
			var cd = new CreatureDisplay(tbNamePreset.Text);
			cd.Initiative = int.TryParse(tbInitiativePreset.Text , out var i) ? i : 0;
			cd.HP = int.TryParse(tbHPPreset.Text , out var h) ? h : 0;
			cd.AC = int.TryParse(tbACPreset.Text , out var a) ? a : 0;
			cd.killAction = () =>
			{
				listPreset.Remove(cd);
				this.Controls.Remove(cd);
				
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
			
			var customData = Session.Values.CustomData.SingleOrDefault(n => n.ScriptName == ConfigValues.Name);
			if(customData == null)Session.Values.CustomData.Add(new CustomSettings{ScriptName = ConfigValues.Name});
			customData = Session.Values.CustomData.SingleOrDefault(n => n.ScriptName == ConfigValues.Name);
			
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
			
			this.Controls.Add(cd);
			
			listPreset.Add(cd);
			listPreset = listPreset.OrderBy(n => n.CreatureName).ToList();
			
			for(int p = 0; p < listPreset.Count; p++)
				listPreset[p].Location = new Point(620, p * 60 + 60);
			
			tbNamePreset.Focus();
		};
		
		btnAddEvent.Click += (s, e) =>
		{
			var roundEvent = new RoundEvent
			{
				EventName = tbEvent.Text,
				Rounds = int.Parse(tbEventRounds.Text),
			};
			flpEvent.Controls.Add(roundEvent);
			tbEvent.Text = "";
			tbEventRounds.Text = "1";
		};
		
		var customData = Session.Values.CustomData.SingleOrDefault(n => n.ScriptName == ConfigValues.Name);
		if(customData != null)
		{
			foreach(var obj in customData.ScriptObjects)
			{
				var objName = obj.ObjectName;
				
				var cd = new CreatureDisplay(objName);
				cd.killAction = () =>
				{
					listPreset.Remove(cd);
					this.Controls.Remove(cd);
					
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
				
				this.Controls.Add(cd);
				listPreset.Add(cd);
			}
			
			for(int p = 0; p < listPreset.Count; p++)
				listPreset[p].Location = new Point(620, p * 60 + 60);
		}
	}
}
