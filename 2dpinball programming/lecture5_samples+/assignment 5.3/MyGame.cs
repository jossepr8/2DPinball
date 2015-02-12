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




		static void Main() {
			new MyGame().Start();
		}
			
		public MyGame () : base(1280, 720, false, false)
		{
			Properties.Read ();
			Wave.Read ();	//read enemie wave patterns from xml
			SetState(States.MainMenu);
			SetState (States.Level);	// start at "Level"
			//SetState (States.Highscores);
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

