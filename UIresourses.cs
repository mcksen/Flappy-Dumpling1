using SFML.Audio;

using SFML.Graphics;

public static class UIresourses
{
	public static Texture popUpbackground = new Texture(@"bin/Sprites/popUPbackground.png");
	public static Texture yesbutton = new Texture(@"bin/Sprites/yesbutton.png");
	public static Texture nobutton = new Texture(@"bin/Sprites/nobutton.png");
	public static Texture newgameBIGbutton = new Texture(@"bin/Sprites/rollerBACKG.png");
	public static Texture continuegameBIGbutton = new Texture(@"bin/Sprites/ROller2.png");
	public static Texture exitgamebutton = new Texture(@"bin/Sprites/rollerTRY3.png");
	public static Texture menubutton = new Texture(@"bin/Sprites/Menu button.png");
	public static Texture newgameSMALLbutton = new Texture(@"bin/Sprites/NEW GAME BUTTON.png");


	public static Font fancyfont = new Font(@"Amadeus Regular.ttf");
	public static Font cookiefont = new Font(@"cookiefont/Games Studios.otf");


	public static Texture menubackground = new Texture(@"bin\Sprites\pf-s96-pm-0032-01.jpg");
	public static Texture highscore = new Texture(@"bin/Sprites/highscoresprite.png");
	public static Texture nonflourbackground = new Texture(@"bin/Sprites/image2.png");
	public static Texture dumplingUP = new Texture(@"bin/Sprites/flyup.png");
	public static Texture dumplingDOWN = new Texture(@"bin/Sprites/flydown.png");
	public static Texture rolltexture = new Texture(@"bin/Sprites/try.png");
	public static Texture pausePOPUP = new Texture(@"bin/Sprites/pausednote.png");
	public static SoundBuffer backgroundMusic = new SoundBuffer(@"bin\Sprites\Sounds\game-music-loop-6-144641.wav");
	public static SoundBuffer buttonclick = new SoundBuffer(@"bin\Sprites\Sounds\buttonclick.wav");
	public static SoundBuffer losesound1 = new SoundBuffer(@"bin\Sprites\Sounds\smash.wav");
	public static SoundBuffer losesound2 = new SoundBuffer(@"bin\Sprites\Sounds\crash.wav");
	public static SoundBuffer losesound3 = new SoundBuffer(@"bin\Sprites\Sounds\mixkit-arcade-retro-game-over-213 (1).wav");
	public static Sound backgroundaudio = new Sound(backgroundMusic);
}