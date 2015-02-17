using System;
using System.Drawing;

namespace GXPEngine
{
	public class HighscoreMenu : GameObject
	{		
		MyGame _game;
		Highscores highscores;

		new float x = 120;

		string title;

		Canvas canvas = new Canvas (1280,720);
		static Color purpleish = ColorTranslator.FromHtml("#5a5492");
		Font font = new Font ("Broadway",20,FontStyle.Regular);

		Font fonttitle = new Font ("Broadway",30,FontStyle.Regular);

		readonly SolidBrush brushpurple = new SolidBrush (purpleish);

		public HighscoreMenu (MyGame game)
		{
			highscores = new Highscores ();
			highscores.Read ();

			title = "Highscores:";

			_game = game;
			AddChild (canvas);

			canvas.graphics.DrawString (title, fonttitle, brushpurple, 520, 100);

			foreach (Score scorez in highscores.scorelist) 
			{
				canvas.graphics.DrawString (scorez.NAME,font,brushpurple,480 ,x += 80);
				canvas.graphics.DrawString	(scorez.SCORE.ToString (), font, brushpurple, 740, x);
			}

			Console.Beep ();

		}

		void Update()
		{
			if (Input.GetKeyDown(Key.DOWN))
			{
				_game.SetState (States.MainMenu);
			};
		}
	}
}

