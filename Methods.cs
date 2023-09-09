using SFML.Graphics;
using SFML.System;

public static class Methods
{
	public static Vector2f GetCenter(this FloatRect rect)
	{

		return new Vector2f(rect.Width * 0.5f, rect.Height * 0.5f);
	}

	public static Vector2f ScaleImage(Sprite image, float x = 1, float y = 1)
	{
		float width = image.GetLocalBounds().Width;
		float height = image.GetLocalBounds().Height;
		float WidthIndex = EventManager.win.Size.X / width;
		float HeightIndex = EventManager.win.Size.Y / height;
		Vector2f scaleAGAINSTthescreen = new Vector2f(WidthIndex * x, HeightIndex * y);
		return scaleAGAINSTthescreen;
	}
}