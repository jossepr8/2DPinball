﻿using System;
using System.Drawing;

namespace GXPEngine
{
	public class HighscoreMenu : GameObject
	{		
		MyGame _game;
		public Highscores highscores;
		Sprite _esc;

		float yvalue1 = 120;
		float yvalue2 = 120;

		string title;
		Sprite _background = new Sprite ("mainmenubackground.png");

		Canvas canvas = new Canvas (1280,720);
		static Color purpleish = ColorTranslator.FromHtml("#5a5492");
		Font font = new Font ("Broadway",20,FontStyle.Regular);

		Font fonttitle = new Font ("Broadway",30,FontStyle.Regular);

		readonly SolidBrush brushpurple = new SolidBrush (purpleish);



		public HighscoreMenu (MyGame game)
		{
			highscores = new Highscores (game);
			//highscores.Read ();
			AddChild (_background);

			_esc = new Sprite ("esc.png");
			_esc.SetOrigin (_esc.width / 2, _esc.height / 2);
			_esc.SetXY (75, 45);
			AddChild (_esc);

			title = "Highscores:";

			_game = game;
			AddChild (canvas);

			canvas.graphics.DrawString (title, fonttitle, brushpurple, 520, 100);

			foreach (Score scorez in _game.scorelist) 
			{
			
				canvas.graphics.DrawString (scorez.NAME,font,brushpurple,200 ,yvalue1 += 40);
				canvas.graphics.DrawString	(scorez.SCORE.ToString (), font, brushpurple, 400, yvalue1);
			}
			foreach (Score scorez in _game.scorelistteam) 
			{
				canvas.graphics.DrawString (scorez.NAME,font,brushpurple,600,yvalue2 += 40);
				canvas.graphics.DrawString	(scorez.SCORE.ToString (), font, brushpurple, 800, yvalue2);
			}

		}

		void Update()
		{

			if (Input.GetKeyDown(Key.ESCAPE))
			{
				_game.SetState (States.MainMenu);
			};
		}
	}
}

