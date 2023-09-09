using SFML.Graphics;
using SFML.System;

public static class DumplingData
{

	public static Vector2f startingPosition;
	public static FloatRect size;
	public static Vector2f currentposition;

	public static void SettoDefault()
	{
		startingPosition = new Vector2f(EventManager.win.Size.X * 0.47f, EventManager.win.Size.Y * 0.44f);
	}
}