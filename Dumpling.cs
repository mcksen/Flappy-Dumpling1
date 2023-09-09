
using SFML.Graphics;
using SFML.System;

public class Dumpling : IFrameUpdatable
{
	Sprite dumpling;
	RenderWindow window;
	Vector2f gravity;
	Vector2f position;
	Vector2f velocity;
	Texture up;
	Texture down;
	float difference;


	public Dumpling()
	{
		window = EventManager.win;
		up = UIresourses.dumplingUP;
		down = UIresourses.dumplingDOWN;
		dumpling = new Sprite(down);

		dumpling.Scale = new Vector2f(0.12f, 0.12f);
		dumpling.Origin = dumpling.GetGlobalBounds().GetCenter();
		DumplingData.size = dumpling.GetGlobalBounds();
		position = DumplingData.startingPosition;
		dumpling.Position = position;
		velocity = new Vector2f(0, 0);
		EventManager.onSPACEpressed += HandleSPACEpressed;
		EventManager.onSPACEreleased += HandleSPACEReleased;
		EventManager.onENTERpressed += HandleENTERpressed;

		TimeManager.AddUpdatable(this);
		DrawSystem.AddItemToDraw(dumpling);
	}




	public void Dispose()
	{
		if (this != null)
		{
			EventManager.onSPACEpressed -= HandleSPACEpressed;
			EventManager.onSPACEreleased -= HandleSPACEReleased;
			EventManager.onENTERpressed -= HandleENTERpressed;

			DrawSystem.RemoveItemToDraw(dumpling);
			TimeManager.RemoveUpdatable(this);
		}
	}

	public void HandleENTERpressed()
	{
		if (GameData.gameSTATE == GameData.Mode.Pause)
		{
			DumplingData.currentposition = position;
		}
	}




	public void Update(float deltaTime)
	{

		velocity += gravity * deltaTime;
		position += velocity * deltaTime;
		dumpling.Position = position;
		if (dumpling.GetGlobalBounds().Top <= 0)
		{
			velocity = new Vector2f(0, 0);

		}
		SetBoundaries();
		DumplingData.currentposition = dumpling.Position;
	}








	public void SetBoundaries()
	{
		difference = window.Size.Y - dumpling.Position.Y;
		if (difference >= dumpling.GetGlobalBounds().Height)
		{
			gravity = new Vector2f(0, 150);

		}

		else
		{
			gravity = new Vector2f(0, 0);
			velocity = new Vector2f(0, -15);
		}
	}
	public void MoveUp()
	{
		if (dumpling.GetGlobalBounds().Top >= 0)
		{
			velocity = new Vector2f(0, -150);
		}


	}
	public void HandleSPACEpressed()
	{
		if (GameData.gameSTATE == GameData.Mode.Play)
		{
			MoveUp();
			dumpling.Texture = up;

		}


	}

	public void HandleSPACEReleased()
	{
		if (GameData.gameSTATE == GameData.Mode.Play)
		{
			dumpling.Texture = down;

		}


	}
}