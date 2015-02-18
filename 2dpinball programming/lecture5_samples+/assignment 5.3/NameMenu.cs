using System;
using System.Drawing;

namespace GXPEngine
{
	public class NameMenu : GameObject
	{
		Canvas _canvas;
		int currentletter;
		string[] Alphabet;
		new string name;
		MyGame _game;
		Font font = new Font ("Broadway",20,FontStyle.Regular);
		static Color purpleish = ColorTranslator.FromHtml("#5a5492");
		SolidBrush purpbrush = new SolidBrush (purpleish);
		NameOwner state;




		public NameMenu (MyGame game)
		{
			state = NameOwner.writing;
			_game = game;
			_canvas = new Canvas (2000, 1000);
			AddChild (_canvas);
			Alphabet = new [] {
				"A", "B", "C", "D", "E", "F", "G", "H" , 
				"I", "J", "K", "L", "M", "N", "O", "P" ,
				"Q", "R", "S", "T", "U", "V", "W", "X" ,
				"Y", "Z"
			};

		}

		public void Step(){
			_canvas.graphics.Clear (Color.Empty);

			if (state == NameOwner.writing) {
				_canvas.graphics.DrawString (name, font, purpbrush, _game.width / 2, _game.height / 2);
				for (int i = 65; i < 90; i++) {
					if (Input.GetKeyDown (i)) {
						currentletter = i;
						name += Alphabet [i - 65];
					}
				}
				if (Input.GetKeyDown (Key.SPACE)) {
					if (name != null) {
						_game.namelist.Add (name);
			
						_game.SAVE ();
						name = null;
					}

				}
			}

				
		
			for (int i = 0; i < _game.namelist.Count; i++) {
				_canvas.graphics.DrawString (_game.namelist[i], font, purpbrush, 100, i * 30);
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

