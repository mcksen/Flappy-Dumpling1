using SFML.Graphics;

public static class DrawSystem
{
	public static List<Drawable> itemsToDraw = new List<Drawable>();

	public static void AddItemToDraw(Drawable drawable)
	{
		itemsToDraw.Add(drawable);
	}

	public static void RemoveItemToDraw(Drawable drawable)
	{
		itemsToDraw.Remove(drawable);
	}

	public static void DrawFrame()
	{
		EventManager.win.Clear();
		for (int i = 0; i < itemsToDraw.Count; i++)
		{

			EventManager.win.Draw(itemsToDraw[i]);

		}
		EventManager.win.Display();
	}
}
