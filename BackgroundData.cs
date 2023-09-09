using SFML.Graphics;
using SFML.System;

public static class BackgroundData
{

	public static List<Vector2f> backgroundPos;

	public static void SetToDefault()
	{

		Sprite imageback = new Sprite(UIresourses.nonflourbackground);
		imageback.Scale = Methods.ScaleImage(imageback);
		float width = imageback.GetGlobalBounds().Width;
		backgroundPos = new List<Vector2f> { new Vector2f(0, 0), new Vector2f(width, 0), new Vector2f(2 * width, 0), new Vector2f(3 * width, 0) };
	}
}