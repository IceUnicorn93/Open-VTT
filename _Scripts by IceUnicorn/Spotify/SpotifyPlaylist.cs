class Playlist : UserControl
{
    public string plName;
    public string plURL;

    public Action RemoveAction;

    public Playlist(string PlaylistName, string URL, SpotifyAccess spotifyAccess)
    {
        plName = PlaylistName;
        plURL = URL;

        var lblName = new Label();
        lblName.Name = "lblName";
        lblName.Text = $"Name: {PlaylistName}";
        lblName.Location = new Point(5, 5);
        lblName.Size = new Size(200, 20);

        var lblURL = new Label();
        lblURL.Name = "lblURL";
        lblURL.Text = $"URL: {URL}";
        lblURL.Location = new Point(205, 5);
        lblURL.Size = new Size(1070, 20);

        var btnRemove = new Button();
        btnRemove.Name = "btnRemove";
        btnRemove.Text = $"X";
        btnRemove.Location = new Point(1330, 0);
        btnRemove.Size = new Size(30, 30);
        btnRemove.Padding = new Padding(-5);
        btnRemove.Click += (object s, EventArgs e) =>
        {
            RemoveAction();
        };

        var btnPlay = new Button();
        btnPlay.Name = "btnPlay";
        btnPlay.Text = $"Play";
        btnPlay.Location = new Point(1275, 0);
        btnPlay.Size = new Size(50, 30);
        btnPlay.Padding = new Padding(-5);
        btnPlay.Click += async (object s, EventArgs e) =>
        {
            await spotifyAccess.SetPlaylist(plURL);
        };

        Controls.Add(lblName);
        Controls.Add(lblURL);
        Controls.Add(btnRemove);
        Controls.Add(btnPlay);

        Size = new Size(1360, 30);
        BackColor = Color.DarkGray;
    }
}