using GXPEngine;
using System;
using System.Drawing.Text;
using System.IO;
using System.Drawing;


namespace GXPEngine
{

	public class MyGame : Game
	{	
		public States _state;
		public Font font;
		Level level;
		Menu menu;
		Highscores highscores;


		static void Main() {
			new MyGame().Start();
		}
			
		public MyGame () : base(1280, 720, false, false)
		{
			/* custom font doesnt work correctly, crashed after 3.5seconds
			PrivateFontCollection pfc = new PrivateFontCollection ();	
			pfc.AddFontFile ("Starjedi.ttf");
			//font = new Font(pfc.Families[0], 16, FontStyle.Regular);
			//pfc.Dispose ();
			*/
			font = new Font ("Broadway",50,FontStyle.Regular);



			Properties.Read ();
			Wave.Read ();	//read enemie wave patterns from xml
			//SetState(States.MainMenu);
			SetState (States.Level);	// start at "Level"
			//targetFps = 5; //--test mode---
		}
			
		void Update () {
			//Console.WriteLine (GetChildren ().Count);
			//---------test--------
			if (Input.GetKeyDown (Key.G)) {
				SetState (States.MainMenu);
			}
			if (Input.GetKeyDown (Key.H)) {
				SetState (States.Level);
			}
			if (Input.GetKeyDown (Key.J)) {
				SetState (States.Highscores);
			}
			//--------------------
			if (level != null) {
				level.Step ();
				level.GetWave ().Step ();
			}
	
		}

		public void SetState(States state){
			if (_state == state) {
				//return;
			}
			_state = state;
			/*
			for (int i = 0; i < GetChildren().Count; i++) {	
				if (GetChildren () [i] is Level ||	//remove level
					GetChildren () [i] is Menu  ||	//remove menu
					GetChildren () [i] is Highscores//remove highscore menu
				){
					GetChildren () [i].Destroy ();
					//RemoveChild (GetChildren () [i]);
				}
			}*/
			if (level != null) {
				level.Destroy ();
			}
			if (menu != null) {
				menu.Destroy ();
			}
			if (highscores != null) {
				highscores.Destroy ();
			}
			StartState (_state);
		}
			
		void StartState(States state){
			//GameObject statevar = new GameObject();
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
				highscores = new Highscores ();
				AddChild (highscores);
				break;
			}
			//AddChild (statevar);
		}
	}

	public enum States
	{
		None,
		MainMenu,
		Level,
		Highscores
	}

}

