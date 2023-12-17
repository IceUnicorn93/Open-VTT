public class SpotifyConfig
{
	public string ClientID;
	public string ClientSecret;
	public string Callback;
	public int Port;
	
	public void Save(string path)
	{
		SaveData<SpotifyConfig>(path, this);
	}
	
	public static SpotifyConfig Load(string path)
	{
		return LoadData<SpotifyConfig>(path);
	}
}