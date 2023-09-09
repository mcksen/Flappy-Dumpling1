using SFML.Graphics;
using SFML.System;

public static class GameSaver
{

	private static string up = "/tmp/uppossave.tmp";
	private static string down = "/tmp/downpossave.tmp";
	private static string dump = "/tmp/dumppossave.tmp";
	public static string timecurrent = "/tmp/timesave.tmp";
	public static string backPos = "/tmp/backPos.tmp";






	public static void Save()
	{
		SaveMultiplePosition(RollsData.upRollsPos, up);
		SaveMultiplePosition(RollsData.downRollsPos, down);
		SaveMultiplePosition(BackgroundData.backgroundPos, backPos);
		File.WriteAllText(timecurrent, GameData.secondsSPAN.ToString());
		File.WriteAllText(dump, DumplingData.currentposition.X + " " + DumplingData.currentposition.Y);
	}

	public static void Load()
	{

		if (File.Exists(up) && File.Exists(down) && File.Exists(dump) && File.Exists(timecurrent))
		{

			string str = File.ReadAllText(dump);
			string[] array = str.Split(' ');
			DumplingData.startingPosition = new Vector2f(float.Parse(array[0]), float.Parse(array[1]));
			GameData.secondsSPAN = float.Parse(File.ReadAllText(timecurrent));
			RollsData.upRollsPos = LoadPositions(up);
			RollsData.downRollsPos = LoadPositions(down);
			BackgroundData.backgroundPos = LoadPositions(backPos);
		}


	}

	public static void SaveMultiplePosition(List<Vector2f> list, string filename)
	{
		List<string> save = new List<string>();
		foreach (Vector2f i in list)
		{
			save.Add(i.X + " " + i.Y);
		}
		File.WriteAllLines(filename, save);

	}

	public static List<Vector2f> LoadPositions(string filename)
	{
		string[] saved = File.ReadAllLines(filename);
		List<Vector2f> list = new List<Vector2f>();
		foreach (string i in saved)
		{
			string[] xy = i.Split(' ');
			Vector2f position = new Vector2f(float.Parse(xy[0]), float.Parse(xy[1]));
			list.Add(position);

		}
		return list;
	}
}

