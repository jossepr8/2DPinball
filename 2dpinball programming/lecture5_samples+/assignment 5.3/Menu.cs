using System;
using System.Collections.Generic;
using System.Drawing;

namespace GXPEngine
{
	public class Menu : GameObject
	{

		MyGame _game;
		AnimSprite _start;
		AnimSprite _highscore;
		AnimSprite _manual;
		Sprite _esc;

		Ball _ball;
		Sprite _background;


		int selectednumber = 0;
		List<AnimSprite> buttonlist = new List<AnimSprite>();

		NameMenu namemenu;

		public Menu (MyGame game)
		{	
			_game = game;
			_background = new Sprite ("mainmenubackground.png");
			AddChild (_background);

			_ball = new Ball ( 32, new Vec2 (game.width / 8, 3 * game.height / 4), null, Color.Green){
				position = new Vec2 (game.width/2, game.height/2)};	//start position
			AddChild (_ball);
			_ball.velocity = new Vec2 (5,5);	//start velocity


			_esc = new Sprite ("esc.png");
			_esc.SetOrigin (_esc.width / 2, _esc.height / 2);
			_esc.SetXY (75, 45);
			AddChild (_esc);


			_start = new AnimSprite ("startgame.png",2,1);
			_start.SetXY (460, 200);
			_start.SetScaleXY (1.4f, 1.4f);

			_highscore = new AnimSprite ("highscore.png",2,1);
			_highscore.SetXY (460, 300);
			_highscore.SetScaleXY (1.4f, 1.4f);


			_manual = new AnimSprite("howtoplay.png",2,1);
			_manual.SetXY (460, 400);
			_manual.SetScaleXY (1.4f, 1.4f);

			AddButtons ();
		//	namemenu = new NameMenu(_game);
		//	AddChild (namemenu);
		}

		void AddButtons()
		{
			AddChild (_start);
			buttonlist.Add (_start);

			AddChild (_highscore);
			buttonlist.Add (_highscore);

			AddChild (_manual);
			buttonlist.Add (_manual);
		}
		private void SwitchMenu(int index)
		{
			switch (index) 
			{
			case 0:
				_game.SetState (States.Level);
				this.Destroy ();
				return;
			case 1:
				_game.SetState (States.Highscores);
				this.Destroy ();
				return;
			case 2:
				_game.SetState (States.Manual);
				this.Destroy ();
				return;
			}
		}


		void Update()
		{	
			if (Input.GetKeyDown (Key.ESCAPE)) {
				_game.SetState (States.MainMenu);
			}
			_ball.MenuStep ();
			//namemenu.Step ();

			if (Input.GetKeyDown (Key.SPACE)) 
			{
				SwitchMenu (selectednumber);
			}

			if (Input.GetKeyDown (Key.DOWN)) 
			{
				if (selectednumber + 1 < buttonlist.Count) 
				{
					selectednumber++;
				} else 
				{	
					selectednumber = 0;
				}
			}

			if (Input.GetKeyDown (Key.UP)) 
			{
				selectednumber--;

				if (selectednumber < 0) 
				{
					selectednumber = buttonlist.Count - 1;
				}
			}
			//Console.WriteLine (selectednumber);

			if (selectednumber == 0) 
			{	
				_start.SetFrame (0);
				_highscore.SetFrame (1);
				_manual.SetFrame (1);
			}


			if (selectednumber == 1) 
			{	
				_start.SetFrame (1);
				_highscore.SetFrame (0);
				_highscore.SetXY (459,300);
				_manual.SetFrame (1);
			}

			if (selectednumber == 2) 
			{	
				_start.SetFrame (1);
				_highscore.SetFrame (1);
				_manual.SetFrame (0);
			}


		}
	}
}

