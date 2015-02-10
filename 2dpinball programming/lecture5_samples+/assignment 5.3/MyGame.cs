using GXPEngine;
using System;


namespace GXPEngine
{

	public partial class MyGame : Game
	{	
		States _state;
		Level _level;
		Menu _menu;

		static void Main() {
			new MyGame().Start();
		}
			
		public MyGame () : base(800, 600, false, false)
		{
			SetState (States.Level);
		}
			
		void Update () {
			//---------test--------
			if (Input.GetKeyDown (Key.A)) {
				SetState (States.MainMenu);
			}
			if (Input.GetKeyDown (Key.S)) {
				SetState (States.Level);
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
					GetChildren () [i] is Menu) {	//remove menu
					RemoveChild (GetChildren () [i]);
				}
			}
			StartState (_state);
		}
			
		void StartState(States state){
			GameObject statevar = new GameObject();
			switch (state) {
			case States.MainMenu:
				statevar = new Menu ();
				break;
			case States.Level:
				statevar = new Level (this);
				break;
			}
			AddChild (statevar);
		}
	}

	public enum States
	{
		MainMenu,
		Level
	}

}

