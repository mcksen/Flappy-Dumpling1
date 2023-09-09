using SFML.Graphics;
using SFML.System;

public class PopUp
{
	public delegate void PopUpEvent();
	public event PopUpEvent PositiveSelected;
	public event PopUpEvent NegativeSelected;

	Sprite background;
	Text heading;
	Text mess;
	Button yes;
	Button no;
	float semimiddleX;
	float semimiddleY;


	public PopUp(Texture back, string message, Texture positive = null, Texture negative = null, string title = "")
	{
		Color color = new Color(68, 38, 17);
		background = new Sprite(back);
		FloatRect backSizes = background.GetGlobalBounds();
		background.Scale = new Vector2f(0.8f, 0.8f);

		background.Origin = backSizes.GetCenter();
		background.Position = EventManager.windowCentre;


		semimiddleX = backSizes.Width / 4f;
		semimiddleY = backSizes.Height / 4f;
		heading = new Text(title, UIresourses.fancyfont);
		heading.CharacterSize = 75;
		heading.FillColor = color;
		heading.Origin = new Vector2f(heading.GetGlobalBounds().Width / 2f, 0);
		heading.Position = new Vector2f(background.Position.X, background.Position.Y - 1.5f * semimiddleY);

		mess = new Text(message, UIresourses.cookiefont);
		mess.CharacterSize = 50;
		mess.LineSpacing = 0.8f;
		mess.FillColor = color;
		mess.Origin = new Vector2f(mess.GetGlobalBounds().Width / 2f, 0);
		mess.Position = new Vector2f(background.Position.X + 0.05f * semimiddleX, background.Position.Y - 0.6f * semimiddleY);
		// CircleShape o = new CircleShape(5);

		// o.Position = mess.Position;
		DrawSystem.AddItemToDraw(background);
		DrawSystem.AddItemToDraw(mess);
		DrawSystem.AddItemToDraw(heading);
		// DrawSystem.AddItemToDraw(o);

		if (positive != null)
		{
			Vector2f yesposition = new Vector2f(background.Position.X + semimiddleX, background.Position.Y + semimiddleY);
			yes = Button.InstantiateSmallButton(positive, yesposition);
			yes.onButtonClicked += HandlePositiveEvent;
		}
		if (negative != null)
		{
			Vector2f noposition = new Vector2f(background.Position.X - 1.1f * semimiddleX, background.Position.Y + semimiddleY);
			no = Button.InstantiateSmallButton(negative, noposition);
			no.onButtonClicked += HandleNegativeEvent;

		}



	}



	public static PopUp MakeInteractivePop(Texture positivetexture, Texture negativetexture, string title, string message)
	{
		PopUp pop = new PopUp(UIresourses.popUpbackground, message, positivetexture, negativetexture, title);


		return pop;
	}
	public static PopUp MakeNoNinteractivePop(Texture background, string message)
	{
		PopUp pop = new PopUp(background, message);
		pop.mess.Position = new Vector2f(pop.mess.Position.X, pop.mess.Position.Y * 1.49f);
		return pop;

	}



	public void Dispose()
	{
		if (this != null)
		{
			if (yes != null)
			{
				yes.onButtonClicked -= HandlePositiveEvent;
				yes.Dispose();
				yes = null;
			}
			if (no != null)
			{
				no.onButtonClicked -= HandleNegativeEvent;
				no.Dispose();
				no = null;
			}
			DrawSystem.RemoveItemToDraw(background);
			DrawSystem.RemoveItemToDraw(mess);
			DrawSystem.RemoveItemToDraw(heading);
		}
	}



	public void HandlePositiveEvent()
	{
		if (PositiveSelected != null)
		{
			PositiveSelected();
		}
	}

	public void HandleNegativeEvent()
	{
		if (NegativeSelected != null)
		{
			NegativeSelected();
		}
	}
}