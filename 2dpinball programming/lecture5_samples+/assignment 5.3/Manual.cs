using System;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Resources;

namespace GXPEngine
{
	public class Manual : GameObject
	{	
		static Color purpleish = ColorTranslator.FromHtml("#5a5492");
		Sprite _esc;

		MyGame _game;

		//string introduction;
		//string controls;
	//	Sprite _background = new Sprite ("mainmenubackground.png");
		Sprite _background = new Sprite ("carbon.png");
		Sprite _menu = new Sprite ("howtoplaytodo.png");
		//5a5492
		Font font = new Font ("Broadway BT",18,FontStyle.Regular);
		//readonly SolidBrush brushred = new SolidBrush (purpleish);
		readonly SolidBrush brushred = new SolidBrush (Color.DarkOrchid);


		Canvas canvas = new Canvas (1280,720);
		 
		public Manual (MyGame game)
		{	
			_game = game;
			AddChild (_background);
			AddChild (canvas);
			_menu.SetScaleXY (0.7f, 0.7f);
			//_menu.SetXY (100, 100);
			AddChild (_menu);
			SoundManager.Playmusic ("mainmenu.mp3");

			_esc = new Sprite ("esc.png");
			_esc.SetOrigin (_esc.width / 2, _esc.height / 2);
			_esc.SetXY (75, 45);
			AddChild (_esc);

			canvas.graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			/*
			introduction = "This game is a co-op game which is meant to be played with two players.\n" +
				"The purpose of the game is to destroy as many waves of enemies as\n" + "you can before the enemies do too much damage.\n"+
				"You receive damage when the enemies reach the bottom of the screen,\n" +
				"when that happens you can see your healthbar filling up,\n" + "once completly filled you lose the game.\n"+
				"Try to score points by hitting the enemies";
			controls = "Player one moves his paddle with the A & D keys,\n" + "Player two should make use of the arrow keys.";

			canvas.graphics.DrawString (introduction,font,brushred,320,160);
			canvas.graphics.DrawString (controls,font,brushred,320,380);
			*/
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

