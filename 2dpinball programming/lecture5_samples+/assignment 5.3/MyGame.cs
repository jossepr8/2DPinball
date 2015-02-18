using GXPEngine;
using System;
using System.Drawing.Text;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

namespace GXPEngine
{

	public class MyGame : Game
	{	
		public Font font;
		Level level;
		Menu menu;
		HighscoreMenu highscoremenu;
		Manual manual;
		public List<Score> scorelist;
		public List<Score> scorelistteam;
		public List<string> namelist;

		public StepStates _stepstate;
		public States _state;


		static void Main() {
			new MyGame().Start();
		}
		public void SAVE(){
			highscoremenu.highscores.Save ();
		}
			
		public MyGame () : base(1280, 720, false, false)
		{

			scorelist = new List<Score>();
			scorelistteam = new List<Score> ();
			namelist = new List<string> ();
			font = new Font ("Broadway",50,FontStyle.Regular);

			highscoremenu = new HighscoreMenu (this);
			//Console.Beep ();

			Properties.Read ();
			Wave.Read ();	//read enemie wave patterns from xml
			//SetState(States.MainMenu);
			SetState (States.MainMenu);	// start at "Level"
			//targetFps = 5; //--test mode---
		}
	
	
		public void SortScores(){
			scorelist.Sort((score2,score1) => score1.SCORE.CompareTo(score2.SCORE));
			scorelistteam.Sort((score2,score1) => score1.SCORE.CompareTo(score2.SCORE));
		}
		public void SortNames(){
		//	namelist.Sort ((name1, name2) => name1.CompareTo (name2));
			namelist.Sort ();
		}
		void Update () {
			Console.WriteLine (namelist.Count);
			//Console.WriteLine (GetChildren ().Count);
			if (level != null) 
			{
				PauseControl ();
			}
			if (Input.GetKeyDown (Key.G)) {
				SetState (States.Level);
			}
			if (Input.GetKeyDown (Key.M)) {
				SetState (States.MainMenu);
			}
			if (Input.GetKeyDown (Key.H)) {
				SetState (States.Highscores);
			}
	
		}

		void PauseControl(){
			if (Input.GetKeyDown (Key.ESCAPE)) {
				if (_stepstate == StepStates.None) {
					_stepstate = StepStates.All;
				} else {
					_stepstate = StepStates.None;
				}
			}
			if (_stepstate != StepStates.None) {
				level.Step ();
			}

		}

		public void SetState(States state){
			_state = state;
			SoundManager.StopMusic ();
			SoundManager.StopSound ();

			if (level != null) {
				level.Destroy ();
			}
			if (menu != null) {
				menu.Destroy ();
			}
			if (highscoremenu != null) {
				highscoremenu.Destroy ();
			}
			if (manual != null) {
				manual.Destroy ();
			}
			StartState (state);
		}
			
		void StartState(States state){
			switch (state) {
			case States.MainMenu:
				menu = new Menu (this);
				AddChild (menu);
				break;
			case States.Level:
				level = new Level (this);
				AddChild (level);
				break;
			case States.Highscores:
				highscoremenu = new HighscoreMenu (this);
				AddChild (highscoremenu);
				break;
			case States.Manual:
				manual = new Manual (this);
				AddChild (manual);
				break;
			}
		}
	}

	public enum States
	{
		None,
		MainMenu,
		Level,
		Highscores,
		Manual
	}
	public enum StepStates
	{
		All,
		None,
		MusicOnly
	}

}