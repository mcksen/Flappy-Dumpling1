using SFML.Graphics;
using SFML.System;
using SFML.Window;
public class ContentGenerator
{


	RenderWindow window;

	public ContentGenerator()
	{
		window = EventManager.win;

	}

	public Text MakeLetters(string letter, uint characterSize = 150, string font = @"Amadeus Regular.ttf")
	{
		Font f = new Font(font);
		Text letters = new Text(letter, f);
		letters.FillColor = new Color(68, 38, 17);
		letters.Position = new Vector2f(window.Size.X * 0.51f, window.Size.Y * 0.04f);
		letters.CharacterSize = characterSize;
		float letterHeight = letters.GetLocalBounds().Height;
		float letterWidth = letters.GetLocalBounds().Width;
		letters.Origin = new Vector2f(letterWidth / 2f, 0);
		return letters;
	}





}