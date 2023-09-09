
using SFML.Graphics;
using SFML.System;

public class Rolls : IFrameUpdatable
{

	Sprite roll;
	RenderWindow window;
	Queue<Sprite> upperRolls;
	Queue<Sprite> lowerRolls;
	Vector2f startingPositionUP;
	Vector2f velocity;
	float xGAP;
	Random rnd;
	float[] upperArray;
	float[] lowerArray;
	Queue<Sprite> one;
	Queue<Sprite> two;



	public Rolls()
	{

		window = EventManager.win;
		rnd = new Random();
		float winHeight = window.Size.Y;
		xGAP = 400;
		upperArray = new float[] { 0, -15f, -30f, -45f, -60f, -80, -5 };
		lowerArray = new float[] { winHeight, winHeight + 15, winHeight + 20, winHeight + 30f, winHeight + 60f, winHeight + 80f, winHeight + 90, winHeight + 100 };


		velocity = new Vector2f(-50f, 0);



		startingPositionUP = new Vector2f(window.Size.X * 0.7f, 0);
		upperRolls = MakeSpriteQueue(upperArray);
		lowerRolls = FliptheSpriteQueue(MakeSpriteQueue(lowerArray, 300));

		TimeManager.AddUpdatable(this);
		EventManager.onENTERpressed += HandlePause;
	}

	private void HandlePause()
	{

		if (GameData.gameSTATE == GameData.Mode.Pause)
		{
			RollsData.upRollsPos = new List<Vector2f>();
			RollsData.downRollsPos = new List<Vector2f>();
			foreach (Sprite i in upperRolls)
			{
				RollsData.upRollsPos.Add(i.Position);
			}
			foreach (Sprite i in lowerRolls)
			{
				RollsData.downRollsPos.Add(i.Position);
			}
		}

	}

	public void Dispose()
	{
		if (this != null)
		{

			EventManager.onENTERpressed -= HandlePause;
			foreach (Sprite i in upperRolls)
			{
				DrawSystem.RemoveItemToDraw(i);
			}
			foreach (Sprite i in lowerRolls)
			{
				DrawSystem.RemoveItemToDraw(i);
			}
			TimeManager.RemoveUpdatable(this);
		}
	}



	private float GetRandomY(float[] array)
	{

		int index = rnd.Next(0, array.Length);
		return array[index];
	}




	private Queue<Sprite> MakeSpriteQueue(float[] array, float optionalGAP = 0)
	{
		Queue<Sprite> queue = new Queue<Sprite>();
		for (int i = 0; i < 4; i++)
		{
			float y = GetRandomY(array);
			roll = new Sprite(UIresourses.rolltexture);
			startingPositionUP.X += xGAP;
			roll.Scale = new Vector2f(0.55f, 0.43f);
			if (RollsData.downRollsPos == null && RollsData.upRollsPos == null)
			{
				roll.Position = new Vector2f(startingPositionUP.X + optionalGAP, y);
				DrawSystem.AddItemToDraw(roll);
				queue.Enqueue(roll);
			}
			else
			{

				roll.Position = array == upperArray ? RollsData.upRollsPos[i] : RollsData.downRollsPos[i];
				DrawSystem.AddItemToDraw(roll);
				queue.Enqueue(roll);
			}
		}
		startingPositionUP = new Vector2f(window.Size.X * 0.7f, 0);
		return queue;
	}

	private Queue<Sprite> FliptheSpriteQueue(Queue<Sprite> queue)
	{

		foreach (Sprite i in queue)
		{
			i.Rotation = 180;

		}
		return queue;
	}


	private void MakeAgap(Queue<Sprite> up, Queue<Sprite> down, float characterHeight)
	{


		foreach (Sprite i in up)
		{
			float a = i.GetGlobalBounds().Top;
			Vector2f topCoordinate = new Vector2f(i.Position.X, i.GetGlobalBounds().Top + i.GetGlobalBounds().Height);
			foreach (Sprite j in down)
			{
				float b = j.GetGlobalBounds().Height;
				Vector2f downCoordinate = new Vector2f(j.Position.X, j.GetGlobalBounds().Top);
				float differenceHeight = Math.Abs(topCoordinate.Y - downCoordinate.Y);
				float differenceWidth = Math.Abs(downCoordinate.X - topCoordinate.X);
				if (differenceHeight < characterHeight && differenceWidth < 200)
				{
					i.Position = new Vector2f(topCoordinate.X, i.GetGlobalBounds().Top - (1.5f * DumplingData.size.Height));
				}

			}





		}

	}



	private void MoveRolls(Sprite image)
	{
		image.Position += velocity * GameData.deltaTime;
	}

	private Queue<Sprite> ProcesstheQueue(Queue<Sprite> que, float[] array)
	{

		if (que.First().Position.X < -que.First().GetGlobalBounds().Width)
		{
			float y = GetRandomY(array);
			Sprite copy = que.Dequeue();
			copy.Position = new Vector2f(que.Last().Position.X + xGAP, y);
			que.Enqueue(copy);
		}

		return que;

	}




	public void CheckifColided(Sprite image)
	{

		float dumplingHEIGHT = DumplingData.size.Height;
		float dumplingWIDTH = DumplingData.size.Width;
		float imagewidth = image.GetGlobalBounds().Width;
		float imageHeight = image.GetGlobalBounds().Height;
		CircleShape o = new CircleShape(dumplingWIDTH * 0.32f);
		// CircleShape k = new CircleShape(10);
		// k.Position = new Vector2f(image.Position.X, image.Position.Y - image.GetGlobalBounds().Height);
		// k.FillColor = new Color(Color.Red);
		// FloatRect imagebox = image.GetGlobalBounds();
		// Shape rect = new RectangleShape(new Vector2f(imagebox.Width, imagebox.Height));
		// rect.Rotation = image.Rotation;
		// rect.Position = image.Position;

		o.Origin = new Vector2f(o.GetGlobalBounds().Width * 0.5f, o.GetGlobalBounds().Height * 0.5f);
		o.Position = new Vector2f(DumplingData.currentposition.X + (0.5f * dumplingWIDTH), DumplingData.currentposition.Y + (0.5f * dumplingHEIGHT));
		// window.Draw(o);
		// window.Draw(rect);
		// window.Draw(k);
		Vector2f originA = image.Position;
		Vector2f secondpointB = new Vector2f(originA.X, originA.Y + imageHeight);
		Vector2f thirdpointD = new Vector2f(secondpointB.X + imagewidth, secondpointB.Y);

		if (image.Rotation != 0)
		{
			originA = new Vector2f(image.Position.X - imagewidth, image.Position.Y);
			secondpointB = new Vector2f(originA.X, originA.Y - imageHeight);
			thirdpointD = new Vector2f(secondpointB.X, image.Position.Y - imageHeight);

		}
		float distanceOC = GetDistPointToLine(originA, secondpointB, o.Position);
		float distanceOD = GetDistPointToLine(secondpointB, thirdpointD, o.Position);

		if (distanceOC <= o.Radius || distanceOD <= o.Radius)
		{
			EventManager.HandleCollision();
		}


	}

	static public float GetDistPointToLine(Vector2f A, Vector2f B, Vector2f o)
	{

		Vector2f ab = B - A;

		Vector2f oa = A - o;
		float dotProduct = GetDotProduct(ab, oa);
		float abMAG = (float)Math.Sqrt(ab.X * ab.X + ab.Y * ab.Y);
		Vector2f ac = (-dotProduct / abMAG) * (ab / abMAG);
		Vector2f C = A + ac;
		Vector2f x = new Vector2f(0, 0);
		float abACdotproduct = GetDotProduct(ab, ac);
		Vector2f ba = A - B;
		Vector2f bc = C - B;
		float babcdotproduct = GetDotProduct(ba, bc);
		if (abACdotproduct < 0)
		{
			x = A;

		}
		else if (babcdotproduct < 0)
		{

			x = B;
		}
		else
		{
			x = C;
		}



		Vector2f ox = x - o;
		float magnitude = (float)Math.Sqrt(ox.X * ox.X + ox.Y * ox.Y);

		return magnitude;
	}


	static float GetDotProduct(Vector2f vec1, Vector2f vec2)
	{
		float dotproduct = vec1.X * vec2.X + vec1.Y * vec2.Y;
		return dotproduct;
	}

	public void Update(float deltaTime)
	{
		MakeAgap(upperRolls, lowerRolls, DumplingData.size.Height);
		one = ProcesstheQueue(upperRolls, upperArray);
		two = ProcesstheQueue(lowerRolls, lowerArray);

		foreach (Sprite i in one)
		{

			MoveRolls(i);
			CheckifColided(i);

		}

		foreach (Sprite i in two)
		{


			MoveRolls(i);
			CheckifColided(i);



		}
	}
}