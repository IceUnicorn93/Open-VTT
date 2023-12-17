// Don't modify this code, please :>
static class StreamDeck
{
	public static (string State, string[,] ActionDescription, List<(string Name, Action action)> PageingActions) Description;
	
	public static Action btnRepeatContextAction;
	public static Action btnNextAction;
	public static Action btnRepeatOffAction;
	public static Action btnShuffleOffAction;
	public static Action btnShuffleOnAction;
	public static Action btnPlayAction;
	public static Action btnPreviousAction;
	public static Action btnRepeatSongAction;
	public static Action btnStopAction;
	
	public static void AddPageingAction((string Name, Action action) data) => Description.PageingActions.Add(data);
}

(string State, string[,] ActionDescription, List<(string Name, Action action)> PageingActions) description = StreamDeckStatics.CreateDescription("Spotify");
StreamDeck.Description = description;
