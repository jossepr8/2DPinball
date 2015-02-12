using GXPEngine;
using System;
using System.Drawing.Text;
using System.IO;
using System.Drawing;


namespace GXPEngine
{

	public class MyGame : Game
	{	
		States _state;
		public Font font;



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
			font = new Font ("Broadway",10,FontStyle.Regular);



			Properties.Read ();
			Wave.Read ();	//read enemie wave patterns from xml
			SetState(States.MainMenu);
			SetState (States.Level);	// start at "Level"
			//targetFps = 5; //--test mode---
		}
			
		void Update () {
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
		}

		public void SetState(States state){
			if (_state == state) {
				return;
			}
			_state = state;
			for (int i = 0; i < GetChildren().Count; i++) {	
				if (GetChildren () [i] is Level ||	//remove level
					GetChildren () [i] is Menu  ||	//remove menu
					GetChildren () [i] is Highscores//remove highscore menu
				){
					RemoveChild (GetChildren () [i]);
				}
			}
			StartState (_state);
		}
			
		void StartState(States state){
			GameObject statevar = new GameObject();
			switch (state) {
			case States.MainMenu:
				statevar = new Menu (this);
				break;
			case States.Level:
				statevar = new Level (this);
				break;
			case States.Highscores:
				statevar = new Highscores ();
				break;
			}
			AddChild (statevar);
		}
	}

	public enum States
	{
		MainMenu,
		Level,
		Highscores
	}

}

