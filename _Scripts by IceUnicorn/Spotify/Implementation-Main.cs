static SpotifyAccess spotifyAccess = new SpotifyAccess();

public partial class Main : System.Windows.Forms.UserControl
{
	List<Playlist> plList = new List<Playlist>();

	public Main(object o)
	{
		this.InitializeComponent();
		
		btnSave.Click += (s, e) =>
		{
			var config = new SpotifyConfig
			{
				ClientID = tbClientID.Text,
				ClientSecret = tbClientSecret.Text,
				Callback = tbCallback.Text,
				Port = int.Parse(tbPort.Text),
			};
			config.Save(Path.Combine(Application.StartupPath, "SpotifyConfig.json"));
		};
		btnLoad.Click += (s, e) =>
		{
			if(!File.Exists(Path.Combine(Application.StartupPath, "SpotifyConfig.json"))) return;
			var config = SpotifyConfig.Load(Path.Combine(Application.StartupPath, "SpotifyConfig.json"));
			tbClientID.Text = config.ClientID;
			tbClientSecret.Text = config.ClientSecret;
			tbCallback.Text = config.Callback;
			tbPort.Text = config.Port.ToString();
		};
		btnConnect.Click += async (s, e) =>
		{
			if(tbClientID.Text == "" || tbClientSecret.Text == "" || tbCallback.Text == "" || tbPort.Text == "") return;
	
			int Port = int.Parse(tbPort.Text);
			
			await spotifyAccess.StartClient(tbClientID.Text, tbClientSecret.Text, tbCallback.Text, Port);
			SetSpotifyState(spotifyAccess);
		};
		btnShuffleOff.Click += async (s, e) =>
		{
			await spotifyAccess.ToggleShuffle(false);
			SetSpotifyState(spotifyAccess);
		};
		btnShuffleOn.Click += async (s, e) =>
		{
			await spotifyAccess.ToggleShuffle(true);
			SetSpotifyState(spotifyAccess);
		};
		btnRepeatOff.Click += async (s, e) =>
		{
			await spotifyAccess.Repeat(0);
			SetSpotifyState(spotifyAccess);
		};
		btnRepeatContext.Click += async (s, e) =>
		{
			await spotifyAccess.Repeat(1);
			SetSpotifyState(spotifyAccess);
		};
		btnRepeatSong.Click += async (s, e) =>
		{
			await spotifyAccess.Repeat(2);
			SetSpotifyState(spotifyAccess);
		};
		btnPlay.Click += async (s, e) =>
		{
			await spotifyAccess.Play();
		};
		btnStop.Click += async (s, e) =>
		{
			await spotifyAccess.Stop();
		};
		btnPrevious.Click += async (s, e) =>
		{
			await spotifyAccess.Previous();
		};
		btnNext.Click += async (s, e) =>
		{
			await spotifyAccess.Next();
		};
		btnAdd.Click += (s, e) =>
		{
			if(tbName.Text == "" || tbURL.Text == "") return;
			
			//Adding it to the Session Data
			var customData = Session.Values.CustomData.SingleOrDefault(n => n.ScriptName == ConfigValues.Name);
			if(customData == null)Session.Values.CustomData.Add(new CustomSettings{ScriptName = ConfigValues.Name});
			customData = Session.Values.CustomData.SingleOrDefault(n => n.ScriptName == ConfigValues.Name);

			var co = new CustomObject();
			co.ObjectName = tbName.Text;
			co.ObjectData.Add(new CustomObjectData { Name = "URL", Value = tbURL.Text } );

			customData.ScriptObjects.Add(co);
			
			//Creating Playlist-Object
			var pl = new Playlist(tbName.Text, tbURL.Text, spotifyAccess);
			pl.RemoveAction = () => 
			{
				plList.Remove(pl);
				flp.Controls.Remove(pl);
				customData.ScriptObjects.Remove(co);
			};
			
			//Adding it to the StreamDeck
			StreamDeck.AddPageingAction((pl.plName, async() => { await spotifyAccess.SetPlaylist(pl.plURL); }));

			plList.Add(pl);
			flp.Controls.Add(pl);
			
			tbName.Text = "";
			tbURL.Text = "";
		};
		btnSaveDefault.Click += (s, e) =>
		{
			var defaultPlaylist = plList.Select(n => new DefaultPlaylistData { Name = n.plName, URL = n.plURL }).ToList();
			ScriptData.Save<List<DefaultPlaylistData>>(Path.Combine(Application.StartupPath, "SpotifyDefault.json"), defaultPlaylist);
		};
		btnLoadDefault.Click += (object s, EventArgs e) =>
		{
			if(!File.Exists(Path.Combine(Application.StartupPath, "SpotifyDefault.json"))) return;
			var defaultPlaylist = ScriptData.Load<List<DefaultPlaylistData>>(Path.Combine(Application.StartupPath, "SpotifyDefault.json"));
			defaultPlaylist.ForEach(n =>
			{		
				var customData = Session.Values.CustomData.SingleOrDefault(m => m.ScriptName == ConfigValues.Name);
				if(customData == null)Session.Values.CustomData.Add(new CustomSettings{ScriptName = ConfigValues.Name});
				customData = Session.Values.CustomData.SingleOrDefault(m => m.ScriptName == ConfigValues.Name);

				var co = new CustomObject();
				
				//Check if Object already exists, if not, create it and Add it to the Session-Data
				if(customData.ScriptObjects.Any(m => m.ObjectName == n.Name))
				{
					co = customData.ScriptObjects.Single(m => m.ObjectName == n.Name);
				}	
				else
				{
					co.ObjectName = n.Name;
					co.ObjectData.Add(new CustomObjectData { Name = "URL", Value = n.URL } );

					customData.ScriptObjects.Add(co);
				}
				
				if(plList.Any(m => m.plName == n.Name)) return;
				
				//Creating Playlist-Object
				var pl = new Playlist(n.Name, n.URL, spotifyAccess);
				pl.RemoveAction = () => 
				{
					plList.Remove(pl);
					flp.Controls.Remove(pl);
					customData.ScriptObjects.Remove(co);
				};
				
				//Adding it to the StreamDeck
				StreamDeck.AddPageingAction((pl.plName, async() => { await spotifyAccess.SetPlaylist(pl.plURL); }));

				plList.Add(pl);
				flp.Controls.Add(pl);
			});
		};
		
		
		LoadPlaylists(spotifyAccess);
		btnLoadDefault.PerformClick();
		btnLoad.PerformClick();
		btnConnect.PerformClick();
		
		StreamDeck.btnRepeatContextAction = new Action(async () => { this.Invoke((MethodInvoker) delegate { btnRepeatContext.PerformClick(); }); });
		StreamDeck.btnNextAction = new Action(async () => { this.Invoke((MethodInvoker) delegate {btnNext.PerformClick();}); });
		StreamDeck.btnRepeatOffAction = new Action(async () => { this.Invoke((MethodInvoker) delegate {btnRepeatOff.PerformClick();}); });
		StreamDeck.btnShuffleOffAction = new Action(async () => { this.Invoke((MethodInvoker) delegate {btnShuffleOff.PerformClick();}); });
		StreamDeck.btnShuffleOnAction = new Action(async () => { this.Invoke((MethodInvoker) delegate {btnShuffleOn.PerformClick();}); });
		StreamDeck.btnPlayAction = new Action(async () => { this.Invoke((MethodInvoker) delegate {btnPlay.PerformClick();}); });
		StreamDeck.btnPreviousAction = new Action(async () => { this.Invoke((MethodInvoker) delegate {btnPrevious.PerformClick();}); });
		StreamDeck.btnRepeatSongAction = new Action(async () => { this.Invoke((MethodInvoker) delegate {btnRepeatSong.PerformClick();}); });
		StreamDeck.btnStopAction = new Action(async () => { this.Invoke((MethodInvoker) delegate {btnStop.PerformClick();}); });
	}
	
	void SetSpotifyState(SpotifyAccess spotifyAccess)
	{
		tbDeviceID.Text = spotifyAccess.DeviceID;
		tbShuffleMode.Text = spotifyAccess.ShuffleMode ? "On" : "Off";
		tbRepeatMode.Text = spotifyAccess.RepeatMode == 0 ? "Off" : spotifyAccess.RepeatMode == 1 ? "Context" : "Song";
	}
	
	void LoadPlaylists(SpotifyAccess spotifyAccess)
	{
		var customData = Session.Values.CustomData.SingleOrDefault(n => n.ScriptName == ConfigValues.Name);
		if(customData == null)Session.Values.CustomData.Add(new CustomSettings{ScriptName = ConfigValues.Name});
		customData = Session.Values.CustomData.SingleOrDefault(n => n.ScriptName == ConfigValues.Name);

		customData.ScriptObjects.ForEach(n =>
		{
			//Creating Playlist-Object
			var pl = new Playlist(n.ObjectName, n.ObjectData.First(m => m.Name == "URL").Value, spotifyAccess);
			pl.RemoveAction = () => 
			{
				plList.Remove(pl);
				flp.Controls.Remove(pl);
				customData.ScriptObjects.Remove(n);
			};
			
			//Adding it to the StreamDeck
			StreamDeck.AddPageingAction((pl.plName, async() => { await spotifyAccess.SetPlaylist(pl.plURL); }));

			plList.Add(pl);
			flp.Controls.Add(pl);
		});
	}
}
