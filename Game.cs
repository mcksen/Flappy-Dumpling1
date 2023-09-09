using SFML.Graphics;
using SFML.System;
using SFML.Audio;
public class Game
{


	private MenuUI menu;
	private GameUI ui;
	private Clock clock;
	private DateTime startTime;

	private TimeSpan span;
	private DateTime rightNOW;

	private TimeSpan current;
	private TimeSpan savedspan;


	public Game()
	{
		ScoreManager.Load();
		menu = new MenuUI();

		clock = new Clock();

		current = span;
		GameData.gameSTATE = GameData.Mode.Menu;



		EventManager.CollisionOccured += HandleCollision;
		EventManager.onMenuButtonClicked += BACKtomenuPressed;
		//new game
		EventManager.NewGameConfirmed += ConfirmNewGame;
		EventManager.NewGameDenied += BACKtomenuPressed;
		//exit
		EventManager.ExitConfirmed += ConfirmExit;
		EventManager.ExitDenied += BACKtomenuPressed;
		//continue
		EventManager.onCONTINUEpressed += HandleCONTINUEpressed;
		//pause
		EventManager.onENTERpressed += HandleENTERpressed;

	}

	private void HandleENTERpressed()
	{

		if (GameData.gameSTATE == GameData.Mode.Play)
		{
			GameData.secondsSPAN = span.TotalSeconds;
			current = span;
			GameData.gameSTATE = GameData.Mode.Pause;



		}
		else if (GameData.gameSTATE == GameData.Mode.Pause)
		{
			startTime = rightNOW - current;
			GameData.gameSTATE = GameData.Mode.Play;

		}
	}

	private void HandleCONTINUEpressed()
	{
		if (GameData.gameSTATE == GameData.Mode.Menu)
		{
			menu.Dispose();
			menu = null;
			GameSaver.Load();
			ui = new GameUI();
			savedspan = TimeSpan.FromSeconds((GameData.secondsSPAN));
			startTime = rightNOW - savedspan;
			EventManager.onSPACEpressed += HandleSPACEpressed;
			GameData.gameSTATE = GameData.Mode.Pause;

		}

	}

	private void BACKtomenuPressed()
	{
		if (GameData.gameSTATE == GameData.Mode.Pause)
		{
			GameSaver.Save();
			ScoreManager.Save();
			if (ui != null)
			{
				ui.Dispose();
				ui = null;
			}
		}
		else
		{
			if (menu != null)
			{
				menu.Dispose();
			}
		}
		GameData.gameSTATE = GameData.Mode.Menu;
		menu = new MenuUI();


	}



	~Game()
	{

		EventManager.CollisionOccured -= HandleCollision;
		EventManager.onMenuButtonClicked -= BACKtomenuPressed;

		//new game
		EventManager.NewGameConfirmed -= ConfirmNewGame;
		EventManager.NewGameDenied -= BACKtomenuPressed;
		//exit
		EventManager.ExitConfirmed -= ConfirmExit;
		EventManager.ExitDenied -= BACKtomenuPressed;
		//continue
		EventManager.onCONTINUEpressed -= HandleCONTINUEpressed;
		//pause
		EventManager.onENTERpressed -= HandleENTERpressed;

	}



	public void ConfirmExit()
	{

		EventManager.Unsubscribe();
		EventManager.win.Close();

	}
	public void ConfirmNewGame()
	{
		if (GameData.gameSTATE == GameData.Mode.Menu)
		{

			menu.Dispose();
			menu = null;
			DumplingData.SettoDefault();
			BackgroundData.SetToDefault();
			RollsData.SetToDefault();
			ui = new GameUI();
			startTime = DateTime.Now;
			EventManager.onSPACEpressed += HandleSPACEpressed;
			GameData.gameSTATE = GameData.Mode.Pause;

		}
		if (GameData.gameSTATE == GameData.Mode.Lose)
		{
			ui.Dispose();
			ui = null;
			ui = new GameUI();
			DumplingData.SettoDefault();
			BackgroundData.SetToDefault();
			RollsData.SetToDefault();
			startTime = DateTime.Now;
			EventManager.onSPACEpressed += HandleSPACEpressed;
			GameData.gameSTATE = GameData.Mode.Pause;

		}
	}

	public void HandleSPACEpressed()
	{
		GameData.gameSTATE = GameData.Mode.Play;
		EventManager.onSPACEpressed -= HandleSPACEpressed;
	}



	public void TrySetRecordTime()
	{
		if (GameData.gameSTATE == GameData.Mode.Play)
		{
			if (span > GameData.recordTime)
			{
				GameData.recordTime = span;

			}
		}
	}





	public void TimerHandler()
	{




		if (GameData.gameSTATE != GameData.Mode.Play)
		{
			startTime += span;
		}
		if (GameData.gameSTATE == GameData.Mode.Pause)
		{
			startTime = rightNOW - savedspan;
		}

		rightNOW = DateTime.Now;
		span = rightNOW - startTime;
		GameData.currentTime = span.ToString("mm") + ":" + span.ToString("ss");


	}

	public void HandleCollision()
	{
		ScoreManager.Save();

		GameData.gameSTATE = GameData.Mode.Lose;

	}












	public void Play()
	{

		GameData.deltaTime = clock.Restart().AsSeconds();
		EventManager.win.DispatchEvents();
		TimerHandler();
		TrySetRecordTime();


	}
}