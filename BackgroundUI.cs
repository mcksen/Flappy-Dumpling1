using SFML.Graphics;
using SFML.System;

public class BackgroundUI : IFrameUpdatable
{
	Vector2f velocity;
	Queue<Sprite> backimages;


	public BackgroundUI()
	{

		backimages = new Queue<Sprite>();
		for (int i = 0; i < 4; i++)
		{
			Sprite image = new Sprite(UIresourses.nonflourbackground);
			image.Scale = Methods.ScaleImage(image);

			image.Position = BackgroundData.backgroundPos[i];
			DrawSystem.AddItemToDraw(image);
			backimages.Enqueue(image);
		}



		velocity = new Vector2f(-50f, 0);
		TimeManager.AddUpdatable(this);
		EventManager.onENTERpressed += HandlePause;

	}
	public void HandlePause()
	{
		if (GameData.gameSTATE == GameData.Mode.Pause)
		{
			BackgroundData.backgroundPos.Clear();
			foreach (Sprite i in backimages)
			{
				BackgroundData.backgroundPos.Add(i.Position);
			}
		}
	}



	public void Dispose()
	{
		if (this != null)

		{

			foreach (Sprite i in backimages)
			{

				DrawSystem.RemoveItemToDraw(i);
			}

			TimeManager.RemoveUpdatable(this);
			EventManager.onMenuButtonClicked -= HandlePause;

		}
	}



	private void MoveScreen(Sprite image)
	{

		image.Position += velocity * GameData.deltaTime;
		float width = image.GetGlobalBounds().Width;
		if (image.Position.X <= -width)
		{
			image.Position = new Vector2f(EventManager.win.Size.X + width, 0);
		}

	}


	public void Update(float deltaTime)
	{
		if (this != null)
		{

			foreach (Sprite i in backimages)
			{
				MoveScreen(i);

			}
		}

	}
}