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

		public NameMenu (MyGame game)
		{
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
			for (int i = 65; i < 90; i++) {
				if (Input.GetKeyDown (i)) {
					currentletter = i;
					name += Alphabet [i - 65];
				}
			}
			if (Input.GetKeyDown (Key.SPACE)) {
				_game.namelist.Add (name);
			
				_game.SAVE ();
				_canvas.graphics.DrawString (name, font, purpbrush, _game.width / 2, _game.height / 2);
				name = null;

			}

		}

	}
}

