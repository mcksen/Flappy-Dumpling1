using SFML.Graphics;
using SFML.System;
using SFML.Window;


VideoMode mode = new VideoMode(1366, 762);
RenderWindow window = new RenderWindow(mode, "FLAPPY DUMPLING.NET");

EventManager.Initialise(window);
Game mygame = new Game();

while (window.IsOpen)
{
	mygame.Play();
	TimeManager.UpdateFrame();
	DrawSystem.DrawFrame();
}

