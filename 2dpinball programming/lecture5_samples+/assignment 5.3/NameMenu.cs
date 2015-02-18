using System;
using System.Drawing;

namespace GXPEngine
{
	public class NameMenu : GameObject
	{
		Canvas _canvas;
		int currentletter;
		Sprite _esc;

		string[] Alphabet;
		new string name;
		MyGame _game;
		Font font = new Font ("Broadway",20,FontStyle.Regular);

		static Color purpleish = ColorTranslator.FromHtml("#5a5492");
		SolidBrush purpbrush = new SolidBrush (purpleish);

		Sprite background = new Sprite ("carbon.png");
		NameOwner state;
		int currentnumber = 0;

		Sprite arrow = new Sprite("arrow.png");


		public NameMenu (MyGame game)
		{ 
			AddChild (background);
			AddChild (arrow);
			arrow.SetScaleXY (0.5f,0.5f);
			arrow.y = 100;
			//SetState (NameOwner.player1);
			state = NameOwner.player1;
			arrow.x = 120;
			_game = game;
			_canvas = new Canvas (2000, 1000);
			AddChild (_canvas);
			Alphabet = new [] {
				"A", "B", "C", "D", "E", "F", "G", "H" , 
				"I", "J", "K", "L", "M", "N", "O", "P" ,
				"Q", "R", "S", "T", "U", "V", "W", "X" ,
				"Y", "Z"
			};

			_esc = new Sprite ("esc.png");
			_esc.SetOrigin (_esc.width / 2, _esc.height / 2);
			_esc.SetXY (75, 45);
			AddChild (_esc);


		}
		void ResetCounter(string namee){
			for (int i = 0; i < _game.namelist.Count; i++) {
				if (namee == _game.namelist [i]) {
					currentnumber = i;
				}
			}
		}
		void SetState(NameOwner namer){
			state = namer;
			switch (namer) {
			case NameOwner.player1:
				ResetCounter (_game.player1name);
				arrow.x = 120;
				break;
			case NameOwner.player2:
				ResetCounter (_game.player2name);
				arrow.x = 420;
				break;
			case NameOwner.team:
				ResetCounter (_game.teamname);
				arrow.x = 720;
				break;
			case NameOwner.writing:
				arrow.x = 1020;
				break;
			}
		}
		public void Step(){
			_canvas.graphics.Clear (Color.Empty);
			_canvas.graphics.DrawString ("Player1 name: ", font, purpbrush, 100, 200);
			_canvas.graphics.DrawString ("Player2 name: ", font, purpbrush, 400, 200);
			_canvas.graphics.DrawString ("Team name: ", font, purpbrush, 700, 200);
			_canvas.graphics.DrawString ("Add name: ", font, purpbrush, 1000, 200);

			_canvas.graphics.DrawString (_game.player1name, font, purpbrush, 100, 230);
			_canvas.graphics.DrawString (_game.player2name, font, purpbrush, 400, 230);
			_canvas.graphics.DrawString (_game.teamname, font, purpbrush, 700, 230);

			if (state == NameOwner.writing) {
				_canvas.graphics.DrawString (name, font, purpbrush, 1000, 230);
				if (name == null || name.Length < 8) {
					for (int i = 65; i < 90; i++) {
						if (Input.GetKeyDown (i)) {
							currentletter = i;
							name += Alphabet [i - 65];
						}
					}
				}
				if (Input.GetKeyDown (Key.SPACE)) {
					if (name != null) {
						_game.namelist.Add (name);
			
						_game.SAVE ();
						name = null;
					}

				}
				if (Input.GetKeyDown (Key.LEFT)) {
					SetState (NameOwner.team);
				}
			}
			if (state == NameOwner.player1) {
				_game.player1name = _game.namelist [currentnumber];
				if (Input.GetKeyDown (Key.RIGHT)) {
					SetState (NameOwner.player2);
				}
				if (Input.GetKeyDown (Key.DOWN)) {
					if (currentnumber < _game.namelist.Count - 1) {
						currentnumber++;
					}
				}
				if (Input.GetKeyDown (Key.UP)) {
					if (currentnumber > 0) {
						currentnumber--;
					}
				}
			}
			if (state == NameOwner.player2) {
				_game.player2name = _game.namelist [currentnumber];
				if (Input.GetKeyDown (Key.LEFT)) {
					SetState (NameOwner.player1);
				}
				if (Input.GetKeyDown (Key.RIGHT)) {
					SetState (NameOwner.team);
				}
				if (Input.GetKeyDown (Key.DOWN)) {
					if (currentnumber < _game.namelist.Count - 1) {
						currentnumber++;
					}
				}
				if (Input.GetKeyDown (Key.UP)) {
					if (currentnumber > 0) {
						currentnumber--;
					}
				}
			}
			if (state == NameOwner.team) {
				_game.teamname = _game.namelist [currentnumber];
				if (Input.GetKeyDown (Key.LEFT)) {
					SetState (NameOwner.player2);
				}
				if (Input.GetKeyDown (Key.RIGHT)) {
					SetState (NameOwner.writing);
				}
				if (Input.GetKeyDown (Key.DOWN)) {
					if (currentnumber < _game.namelist.Count - 1) {
						currentnumber++;
					}
				}
				if (Input.GetKeyDown (Key.UP)) {
					if (currentnumber > 0) {
						currentnumber--;
					}
				}
			}

				
		
			if (Input.GetKeyDown (Key.ESCAPE)) {
				_game.SetState (States.MainMenu);
			}


		}


	}
	public enum NameOwner{
		none,
		player1,
		player2,
		team,
		writing
	}

}

