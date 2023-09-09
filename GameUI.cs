using SFML.Graphics;
using SFML.System;
using SFML.Audio;
public class GameUI : IFrameUpdatable
{


	private Button menu;
	private RenderWindow window;
	private Text timer;

	private ContentGenerator content;
	private Dumpling dumpling;

	private Rolls roll;
	private PopUp ripPOPUP;
	private PopUp pausepop;
	private Sprite recordtime;
	private Text timerecord;
	private BackgroundUI back;
	private string time;
	private float h;
	private float w;
	private Text instruction;
	private Sound lose1;
	private Sound lose2;
	private Sound lose3;
	private Sound backgroundaudio;


	public GameUI()
	{
		window = EventManager.win;
		backgroundaudio = new Sound(UIresourses.backgroundaudio);
		backgroundaudio.Play();
		content = new ContentGenerator();
		back = new BackgroundUI();
		dumpling = new Dumpling();
		lose1 = new Sound(UIresourses.losesound1);
		lose2 = new Sound(UIresourses.losesound2);
		lose3 = new Sound(UIresourses.losesound3);
		roll = new Rolls();

		time = GameData.currentTime;
		timer = content.MakeLetters(time, 120);

		recordtime = new Sprite(UIresourses.highscore);
		recordtime.Scale = new Vector2f(0.6f, 0.6f);

		w = recordtime.GetGlobalBounds().Width;
		h = recordtime.GetGlobalBounds().Height;
		timerecord = content.MakeLetters(GameData.recordTime.ToString("mm") + ":" + GameData.recordTime.ToString("ss"), 25);
		timerecord.Origin = timerecord.GetGlobalBounds().GetCenter();
		timerecord.Position = new Vector2f(recordtime.Position.X + w / 2.2f, recordtime.Position.Y + 1.1f * h);

		instruction = content.MakeLetters("                 Press SPACE to start!", 65);
		instruction.LineSpacing = 0.8f;
		instruction.Font = UIresourses.cookiefont;

		instruction.Position = new Vector2f(window.Size.X / 2f, window.Size.Y * 0.7f);


		EventManager.CollisionOccured += HandleCollision;

		EventManager.onENTERpressed += HandleENTERpressed;
		EventManager.onSPACEpressed += HandleSPACEpressed;


		TimeManager.AddUpdatable(this);
		DrawSystem.AddItemToDraw(timer);
		DrawSystem.AddItemToDraw(recordtime);
		DrawSystem.AddItemToDraw(timerecord);
		DrawSystem.AddItemToDraw(instruction);



	}

	private void HandleENTERpressed()
	{
		if (GameData.gameSTATE == GameData.Mode.Pause)
		{
			recordtime.Position = new Vector2f(0, window.Size.Y * 0.2f);
			timerecord.Position = new Vector2f(recordtime.Position.X + w / 2.2f, recordtime.Position.Y + 1.1f * h);

			pausepop = PopUp.MakeNoNinteractivePop(UIresourses.pausePOPUP, "Press ENTER to continue...");
			menu = Button.InstantiateSmallButton(UIresourses.menubutton, new Vector2f(window.Size.X * 0.06f, window.Size.Y * 0.08f));
			menu.onButtonClicked += HandleMenuButtonClicked;

		}
		if (GameData.gameSTATE == GameData.Mode.Play)
		{
			recordtime.Position = new Vector2f(0, 0);
			timerecord.Position = new Vector2f(recordtime.Position.X + w / 2.2f, recordtime.Position.Y + 1.1f * h);
			menu.onButtonClicked -= HandleMenuButtonClicked;
			pausepop.Dispose();
			pausepop = null;
			menu.Dispose();
			menu = null;
		}
	}

	public void Dispose()
	{
		if (this != null)
		{
			dumpling.Dispose();
			roll.Dispose();
			back.Dispose();
			backgroundaudio.Dispose();
			lose1.Dispose();
			lose2.Dispose();
			lose3.Dispose();

			EventManager.CollisionOccured -= HandleCollision;
			EventManager.onENTERpressed -= HandleENTERpressed;
			DrawSystem.RemoveItemToDraw(timer);

			DrawSystem.RemoveItemToDraw(recordtime);
			DrawSystem.RemoveItemToDraw(timerecord);
		}

	}
	private void HandleCollision()
	{
		TimeManager.RemoveUpdatable(dumpling);
		TimeManager.RemoveUpdatable(roll);
		TimeManager.RemoveUpdatable(back);
		ripPOPUP = PopUp.MakeInteractivePop(UIresourses.newgameSMALLbutton, UIresourses.menubutton, "R.I.P. dumpling ...", "Time without accidents: " + time + "\n Record time: " + GameData.recordTime.ToString("mm") + ":" + GameData.recordTime.ToString("ss"));
		backgroundaudio.Stop();
		lose1.Play();
		lose2.Play();
		lose3.Play();
		ripPOPUP.PositiveSelected += HandleNewGameConfirmed;
		ripPOPUP.NegativeSelected += HandleNewGameDenied;
	}
	private void HandleMenuButtonClicked()
	{
		EventManager.HandleMenuButtonClicked();
	}

	public void HandleNewGameConfirmed()
	{
		EventManager.HandleNewGameConfirmed();
		ripPOPUP.Dispose();
		ripPOPUP = null;
	}
	public void HandleNewGameDenied()
	{
		EventManager.HandleNewGameDenied();
		ripPOPUP.Dispose();
		ripPOPUP = null;
	}
	public void HandleSPACEpressed()
	{
		DrawSystem.RemoveItemToDraw(instruction);
		EventManager.onSPACEpressed -= HandleSPACEpressed;
	}

	public void Update(float deltaTime)
	{
		time = GameData.currentTime;
		timer.DisplayedString = time;
	}

}