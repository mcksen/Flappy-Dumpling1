using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class MenuUI : IFrameUpdatable
{
	Button newGAME;
	Button continueGame;
	Button exit;
	Sprite background;
	Text title;

	RenderWindow window;

	ContentGenerator content;
	String space;
	PopUp exitpop;
	PopUp newgamepop;
	bool popupisActive;
	Sprite recordtime;
	Text timerecord;
	Sound backgroundaudio;
	public MenuUI()
	{

		content = new ContentGenerator();
		backgroundaudio = new Sound(UIresourses.backgroundaudio);
		backgroundaudio.Play();
		background = new Sprite(UIresourses.menubackground);
		recordtime = new Sprite(UIresourses.highscore);
		float w = recordtime.GetGlobalBounds().Width;
		float h = recordtime.GetGlobalBounds().Height;
		timerecord = content.MakeLetters(GameData.recordTime.ToString("mm") + ":" + GameData.recordTime.ToString("ss"), 45);
		timerecord.Origin = timerecord.GetGlobalBounds().GetCenter();
		timerecord.Position = new Vector2f(recordtime.Position.X + w / 2.2f, recordtime.Position.Y + 1.1f * h);
		space = "    ";
		title = content.MakeLetters("Menu");
		DrawSystem.AddItemToDraw(background);
		DrawSystem.AddItemToDraw(title);
		DrawSystem.AddItemToDraw(recordtime);
		DrawSystem.AddItemToDraw(timerecord);
		window = EventManager.win;
		newGAME = Button.InstantiateBigButton(UIresourses.newgameBIGbutton, new Vector2f(window.Size.X * 0.47f, window.Size.Y * 0.4f), "NEW GAME");
		continueGame = Button.InstantiateBigButton(UIresourses.continuegameBIGbutton, new Vector2f(window.Size.X * 0.47f, window.Size.Y * 0.6f), "CONTINUE");
		exit = Button.InstantiateBigButton(UIresourses.exitgamebutton, new Vector2f(window.Size.X * 0.47f, window.Size.Y * 0.8f), "EXIT");
		newGAME.onButtonClicked += HandleNEWGAMEpressed;
		exit.onButtonClicked += HandleOnExitPressed;
		continueGame.onButtonClicked += HandleCONTINUEpressed;
		popupisActive = false;

		TimeManager.AddUpdatable(this);




	}

	public void Dispose()
	{
		if (this != null)
		{
			newGAME.Dispose();
			continueGame.Dispose();
			exit.Dispose();
			backgroundaudio.Dispose();
			newGAME.onButtonClicked -= HandleNEWGAMEpressed;
			continueGame.onButtonClicked -= HandleCONTINUEpressed;
			exit.onButtonClicked -= HandleOnExitPressed;
			if (exitpop != null)
			{
				exitpop.PositiveSelected -= HandleExitConfirmed;
				exitpop.NegativeSelected -= HandleExitDenied;
				exitpop.Dispose();
				exitpop = null;

			}
			if (newgamepop != null)
			{
				newgamepop.PositiveSelected -= HandleNewGameConfirmed;
				newgamepop.NegativeSelected -= HandleNewGameDenied;
				newgamepop.Dispose();
				newgamepop = null;

			}
			DrawSystem.RemoveItemToDraw(background);
			DrawSystem.RemoveItemToDraw(title);
			DrawSystem.RemoveItemToDraw(recordtime);
			DrawSystem.RemoveItemToDraw(timerecord);
		}
	}

	private void HandleNewGameDenied()
	{
		if (newgamepop != null)
		{
			newgamepop.PositiveSelected -= HandleNewGameConfirmed;
			newgamepop.NegativeSelected -= HandleNewGameDenied;
			newgamepop.Dispose();
			newgamepop = null;
		}
		EventManager.HandleNewGameDenied();
	}

	private void HandleNewGameConfirmed()
	{
		if (newgamepop != null)
		{
			newgamepop.PositiveSelected -= HandleNewGameConfirmed;
			newgamepop.NegativeSelected -= HandleNewGameDenied;
			newgamepop.Dispose();
			newgamepop = null;
		}
		EventManager.HandleNewGameConfirmed();
	}


	private void HandleCONTINUEpressed()
	{
		EventManager.HandleCONTINUEpressed();
	}

	private void HandleNEWGAMEpressed()
	{
		//add tool to check if a saved game exists;
		newgamepop = PopUp.MakeInteractivePop(UIresourses.yesbutton, UIresourses.nobutton, "Warning", "If you start a new game all information \n" + space + "about your saved game will be lost. \n\n" + space + space + space + "Do you wish to continue?");
		newgamepop.PositiveSelected += HandleNewGameConfirmed;
		newgamepop.NegativeSelected += HandleNewGameDenied;
		popupisActive = true;

	}

	private void HandleOnExitPressed()
	{
		exitpop = PopUp.MakeInteractivePop(UIresourses.yesbutton, UIresourses.nobutton, "It's sad to see you go ..", "\nAre you sure you want to EXIT the game?");
		exitpop.PositiveSelected += HandleExitConfirmed;
		exitpop.NegativeSelected += HandleExitDenied;
		popupisActive = true;

	}
	private void HandleExitDenied()
	{
		EventManager.HandleExitDenied();
		if (exitpop != null)
		{

			exitpop.PositiveSelected -= HandleExitConfirmed;
			exitpop.NegativeSelected -= HandleExitDenied;
			exitpop.Dispose();
			exitpop = null;

		}
	}

	private void HandleExitConfirmed()
	{
		EventManager.HandleExitConfirmed();
	}

	public void Update(float deltatime)
	{

		if (popupisActive)
		{
			exit.Unsubscribe();
			newGAME.Unsubscribe();
			continueGame.Unsubscribe();
		}
	}


}