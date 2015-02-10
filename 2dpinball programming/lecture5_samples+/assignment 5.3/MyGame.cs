using GXPEngine;
using System;
using System.Collections.Generic;


namespace GXPEngine
{

	public class MyGame : Game
	{	
		States _state;
		//Level _level;
		//Menu _menu;

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
			List<GameObject> children = GetChildren ();
			for (int i = 0; i < children.Count; i++) {	
				if (children [i] is Level ||	//remove level
					children [i] is Menu) {		//remove menu
					RemoveChild (children [i]);
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

