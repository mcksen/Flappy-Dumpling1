using SFML.Graphics;
using SFML.System;
using SFML.Window;
public static class EventManager
{
	public delegate void MouseEvent();
	public static event MouseEvent onMousePressed;
	public delegate void KeyEvent();
	public static event KeyEvent onSPACEpressed;
	public static event KeyEvent onSPACEreleased;
	public static event KeyEvent onENTERpressed;
	public delegate void CollisionEvent();
	public static event CollisionEvent CollisionOccured;
	public static Vector2f mouseClick;
	public static RenderWindow win;
	public static Vector2f windowCentre;

	public delegate void ButtonClicked();
	public static event ButtonClicked onMenuButtonClicked;

	public static event ButtonClicked onCONTINUEpressed;
	public delegate void PopUpEvent();
	public static event PopUpEvent ExitConfirmed;
	public static event PopUpEvent ExitDenied;
	public static event PopUpEvent NewGameConfirmed;
	public static event PopUpEvent NewGameDenied;





	public static void Initialise(RenderWindow window)

	{

		win = window;
		windowCentre = new Vector2f(win.Size.X / 2f, win.Size.Y / 2f);
		win.MouseButtonPressed += HandleClick;
		win.KeyPressed += HandleSPACE;
		win.KeyReleased += HandleSPACEReleased;



	}

	public static void Unsubscribe()
	{
		win.MouseButtonPressed -= HandleClick;
		win.KeyPressed -= HandleSPACE;
		win.KeyReleased -= HandleSPACEReleased;
	}


	public static void HandleClick(object sender, MouseButtonEventArgs e)
	{
		if (e.Button == Mouse.Button.Left && onMousePressed != null)
		{
			mouseClick = new Vector2f(e.X, e.Y);
			onMousePressed();

		}
	}

	public static void HandleSPACE(object sender, KeyEventArgs e)
	{
		if (e.Code == Keyboard.Key.Space && onSPACEpressed != null)
		{
			onSPACEpressed();
		}
		if (e.Code == Keyboard.Key.Enter && onENTERpressed != null)
		{
			onENTERpressed();
		}

	}

	public static void HandleSPACEReleased(object sender, KeyEventArgs e)
	{
		if (e.Code == Keyboard.Key.Space && onSPACEreleased != null)
		{
			onSPACEreleased();
		}

	}

	public static void HandleCollision()
	{
		if (CollisionOccured != null && GameData.gameSTATE == GameData.Mode.Play)
		{
			CollisionOccured();
		}
	}

	public static void HandleMenuButtonClicked()
	{
		if (onMenuButtonClicked != null && GameData.gameSTATE == GameData.Mode.Pause)
		{
			onMenuButtonClicked();
		}
	}






	public static void HandleCONTINUEpressed()
	{

		if (onCONTINUEpressed != null && GameData.gameSTATE == GameData.Mode.Menu)
		{
			onCONTINUEpressed();
		}
	}

	public static void HandleNewGameDenied()
	{
		if (NewGameDenied != null)
		{
			NewGameDenied();
		}
	}

	public static void HandleNewGameConfirmed()
	{
		if (NewGameConfirmed != null)
		{
			NewGameConfirmed();
		}
	}

	public static void HandleExitDenied()
	{
		if (ExitDenied != null)
		{
			ExitDenied();
		}
	}

	public static void HandleExitConfirmed()
	{
		if (ExitConfirmed != null)
		{
			ExitConfirmed();
		}
	}
}