using SFML.System;

public static class TimeManager
{

	private static float deltaTime;

	private static Clock clock;
	private static List<IFrameUpdatable> frameUpdatables = new List<IFrameUpdatable>();

	public static float DeltaTime => deltaTime;

	static TimeManager()
	{
		clock = new Clock();
	}

	public static void AddUpdatable(IFrameUpdatable updatable)
	{
		frameUpdatables.Add(updatable);
	}
	public static void RemoveUpdatable(IFrameUpdatable updatable)
	{
		frameUpdatables.Remove(updatable);
	}

	public static void UpdateFrame()
	{
		deltaTime = clock.Restart().AsSeconds();
		if (GameData.gameSTATE != GameData.Mode.Pause)
		{
			for (int i = 0; i < frameUpdatables.Count; i++)
			{
				frameUpdatables[i].Update(deltaTime);
			}
		}
	}


}