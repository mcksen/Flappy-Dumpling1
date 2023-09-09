using SFML.Audio;
using SFML.Graphics;
using SFML.System;

public class Button
{


	private Text title;
	private Sprite image;

	public delegate void ClickEvent();
	public event ClickEvent onButtonClicked;
	private ContentGenerator content;
	private Sound buttonclickaudio;

	public Button(Texture texture, Vector2f buttonSize, Vector2f position, string text, Vector2f textLocation)
	{
		content = new ContentGenerator();
		buttonclickaudio = new Sound(UIresourses.buttonclick);
		image = new Sprite(texture);
		image.Scale = buttonSize;
		float width = image.GetGlobalBounds().Width;
		float height = image.GetGlobalBounds().Height;
		image.Origin = new Vector2f(width * 0.5f, height * 0.5f);
		image.Position = position;
		title = content.MakeLetters(text, 85);
		float textWidth = title.GetGlobalBounds().Width;
		float textHeight = title.GetGlobalBounds().Height;
		title.Origin = new Vector2f(textWidth / 2f, 0);
		title.Position = new Vector2f(image.Position.X + 55, image.Position.Y - height * 0.35f);
		DrawSystem.AddItemToDraw(image);
		DrawSystem.AddItemToDraw(title);
		EventManager.onMousePressed += HandleMouseClick;
	}

	public static Button InstantiateBigButton(Texture texture, Vector2f position, string text = "")
	{
		Button result = new Button(texture, new Vector2f(0.8f, 0.8f), position, text, position);

		return result;
	}

	public static Button InstantiateSmallButton(Texture texture, Vector2f position)
	{
		Button result = new Button(texture, new Vector2f(0.8f, 0.8f), position, "", position);

		return result;
	}




	public void Dispose()
	{
		EventManager.onMousePressed -= HandleMouseClick;
		DrawSystem.RemoveItemToDraw(image);
		DrawSystem.RemoveItemToDraw(title);

	}

	public void Unsubscribe()
	{
		EventManager.onMousePressed -= HandleMouseClick;

	}


	public void HandleMouseClick()

	{

		if (image.GetGlobalBounds().Contains(EventManager.mouseClick.X, EventManager.mouseClick.Y) && onButtonClicked != null)
		{
			onButtonClicked();

			buttonclickaudio.Play();

		}
	}





}