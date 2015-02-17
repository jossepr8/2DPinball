using GXPEngine;
using System;
using System.Drawing.Text;
using System.IO;
using System.Drawing;

namespace GXPEngine
{

	public class MyGame : Game
	{	
		public Font font;
		Level level;
		Menu menu;
		Highscores highscores;
		public StepStates _stepstate;
		public States _state;



		static void Main() {
			new MyGame().Start();
		}
			
		public MyGame () : base(1280, 720, false, false)
		{
			font = new Font ("Broadway",50,FontStyle.Regular);

			Console.Beep ();

			Properties.Read ();
			Wave.Read ();	//read enemie wave patterns from xml
			//SetState(States.MainMenu);
			SetState (States.MainMenu);	// start at "Level"
			//targetFps = 5; //--test mode---

		}
		public override void Destroy(){

		}
	
	


			
		void Update () {

			if (level != null) 
			{
				PauseControl ();
			}
	
		}

		void PauseControl(){
			if (Input.GetKeyDown (Key.P)) {
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
			if (highscores != null) {
				highscores.Destroy ();
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
				highscores = new Highscores ();
				AddChild (highscores);
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

