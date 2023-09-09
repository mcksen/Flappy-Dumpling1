using SFML.Graphics;
using SFML.System;

public static class GameData
{
	public enum Mode { Play, Menu, Pause, Lose, Exit }
	public static Mode gameSTATE;
	public static string debugString = "";
	public static string[] debug = { "Play", "Menu", "Pause", "Lose", "Exit" };


	public static TimeSpan recordTime;

	public static string currentTime = "";

	public static double secondsSPAN;

	public static float deltaTime;







}