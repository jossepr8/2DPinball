using System;
using System.Collections.Generic;

namespace GXPEngine
{
	public class Menu : GameObject
	{

		List<LineSegment> _lines;
		MyGame _game;
		AnimSprite _start;
		AnimSprite _highscore;
		AnimSprite _manual;

		Sprite _background;
		Ball _ball;

		int selectednumber = 0;
		List<AnimSprite> buttonlist = new List<AnimSprite>();
		//CollisionManager2 colmanager;

		//NameMenu namemenu;

		public Menu (MyGame game)
		{	

			_lines = new List<LineSegment> ();
			_game = game;
			_background = new Sprite ("mainmenubackground.png");
			AddChild (_background);

			_ball = new Ball (30, new Vec2 (500, 300)){ velocity = new Vec2 (5, 5) };
			AddChild (_ball);
			SoundManager.Playmusic ("Testmusic.wav");

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
			//namemenu = new NameMenu(_game);
			//AddChild (namemenu);
			//MakeWalls ();
		}
		void MakeWalls(){
			//-----------------------outer walls----------------------
			AddLine (new Vec2 (0, 0), new Vec2 (_game.width, 0));				//top
			AddLine (new Vec2 (_game.width, 0), new Vec2 (_game.width, _game.height));		//right
			AddLine (new Vec2 (_game.width, _game.height), new Vec2 (0, _game.height));		//bottom
			AddLine (new Vec2 (0, _game.height), new Vec2 (0,0));				//left
			//AddLine (new Vec2 (width, 0), new Vec2 (width, height/2));		//right2
			//AddLine (new Vec2 (0, height/2), new Vec2 (0,0));				//left2
			//--------------------------------------------------------
		}
		void AddLine (Vec2 start, Vec2 end, float bounciness = 1) {
			LineSegment line = new LineSegment (start, end, 0xff00ff00, 4, false, bounciness);
			AddChild (line);
			_lines.Add (line);
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
			//namemenu.Step ();
			_ball.MenuStep ();
			//colmanager.Step ();

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

