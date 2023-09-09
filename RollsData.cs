using SFML.Graphics;
using SFML.System;

public static class RollsData
{
	public static List<Vector2f> upRollsPos;
	public static List<Vector2f> downRollsPos;

	public static void SetToDefault()
	{
		upRollsPos = null;
		downRollsPos = null;

	}
}
