public static class ScriptData
{
	public static void Save<T>(string path, T data)
	{
		SaveData<T>(path, data);
	}
	
	public static T Load<T>(string path)
	{
		return LoadData<T>(path);
	}
}