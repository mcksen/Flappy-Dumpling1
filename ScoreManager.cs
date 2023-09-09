public static class ScoreManager
{

	private static string tempName = "/tmp/dumplingrecordtim.tmp";




	public static void Save()
	{

		File.WriteAllText(tempName, GameData.recordTime.ToString());

	}


	public static TimeSpan Load()
	{
		TimeSpan recordTime;
		if (File.Exists(tempName))
		{
			recordTime = TimeSpan.Parse(File.ReadAllText(tempName));
			GameData.recordTime = recordTime;

		}
		else
		{
			recordTime = new TimeSpan(0, 0, 0);
		}

		return recordTime;
	}
}