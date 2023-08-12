class CreatureDisplay : UserControl
{
	public string CreatureName;
	
	private int _Initiative;
	public int Initiative
	{
		get => _Initiative;
		set
		{
			_Initiative = value;
			lblInitiative.Text = $"Initiative: {value}";
		}
	}
	
	private int _HP;
	public int HP
	{
		get => _HP;
		set
		{
			_HP = value;
			lblHp.Text = $"HP: {value}";
		}
	}
	
	private int _AC;
	public int AC
	{
		get => _AC;
		set
		{
			_AC = value;
			lblAc.Text = $"AC: {value}";
		}
	}
	
	Label lblName = new Label();
	Label lblInitiative = new Label();
	Label lblHp = new Label();
	Label lblAc = new Label();
	Label lblDamage = new Label();
	
	Button btnKill = new Button();
	Button btnAdd = new Button();
	Button btnRemove = new Button();
	
	TextBox tbDamage = new TextBox();
	
	public Action killAction;
	
	public CreatureDisplay(string name)
	{
		Size = new Size(500, 50);
		
		BackColor = Color.FromArgb(50,200,200,200);
		
		CreatureName = name;
		
		lblName.Name = "Name";
		lblName.Text = name;
		lblName.Location = new Point(5, 5);
		lblName.Font = new Font("Arial", 12);
		lblName.Size = new Size(490, lblName.Size.Height);
		lblName.Click += (s, e) => this.OnClick(null);
		
		lblInitiative.Name = "Initiative";
		lblInitiative.Text = "Initiative: 0";
		lblInitiative.Location = new Point(5, 30);
		lblInitiative.Click += (s, e) => this.OnClick(null);
		
		lblHp.Name = "HP";
		lblHp.Text = "HP: 0";
		lblHp.Location = new Point(110, 30);
		lblHp.Click += (s, e) => this.OnClick(null);
		
		lblAc.Name = "AC";
		lblAc.Text = "AC: 0";
		lblAc.Location = new Point(215, 30);
		lblAc.Click += (s, e) => this.OnClick(null);
		
		lblDamage.Name = "Damage";
		lblDamage.Text = "Damage";
		lblDamage.Size = new Size ( 50, lblDamage.Size.Height );
		lblDamage.Location = new Point(320, 30);
		lblDamage.Click += (s, e) => this.OnClick(null);
		
		tbDamage.Name = "tbDamage";
		tbDamage.Text = "0";
		tbDamage.Size = new Size ( 30, tbDamage.Size.Height );
		tbDamage.Location = new Point(375, 30);
		
		btnAdd.Name = "Add";
		btnAdd.Text = "+";
		btnAdd.Size = new Size ( 20, 20 );
		btnAdd.Location = new Point( 410, 30 );
		btnAdd.Click += (s, e) =>
		{
			
			HP += int.TryParse(tbDamage.Text , out var i) ? i : 0;
			lblHp.Text = $"HP: {HP}";
			tbDamage.Text = "0";
		};
		
		btnRemove.Name = "Remove";
		btnRemove.Text = "-";
		btnRemove.Size = new Size ( 20, 20 );
		btnRemove.Location = new Point( 435, 30 );
		btnRemove.Click += (s, e) =>
		{
			HP -= int.TryParse(tbDamage.Text , out var i) ? i : 0;
			lblHp.Text = $"HP: {HP}";
			tbDamage.Text = "0";
			//if(HP <= 0) killAction?.Invoke();
		};
		
		btnKill.Name = "Kill";
		btnKill.Text = "X";
		btnKill.Size = new Size ( 20, 20 );
		btnKill.Location = new Point( 475, 30 );
		btnKill.Click += (s, e) => { killAction?.Invoke(); };
		
		Controls.Add(lblName);
		Controls.Add(lblInitiative);
		Controls.Add(lblHp);
		Controls.Add(lblAc);
		Controls.Add(lblDamage);
		Controls.Add(tbDamage);
		Controls.Add(btnAdd);
		Controls.Add(btnRemove);
		Controls.Add(btnKill);
	}
}