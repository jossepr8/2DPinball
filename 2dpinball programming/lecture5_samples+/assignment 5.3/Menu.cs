using System;
using System.Collections.Generic;

namespace GXPEngine
{
	public class Menu : GameObject
	{

		MyGame _game;
		AnimSprite _start;
		AnimSprite _highscore;
		AnimSprite _manual;

		Sprite _background;


		int selectednumber = 0;
		List<AnimSprite> buttonlist = new List<AnimSprite>();


		public Menu (MyGame game)
		{	
			_game = game;
			_background = new Sprite ("mainmenubackground.png");
			AddChild (_background);

			SoundManager.Playmusic ("Testmusic.wav");

			_start = new AnimSprite ("start.png",2,1);
			_start.SetXY (460, 200);
			_start.SetScaleXY (1.4f, 1.4f);

			_highscore = new AnimSprite ("highscore.png",2,1);
			_highscore.SetXY (460, 300);
			_highscore.SetScaleXY (1.4f, 1.4f);


			_manual = new AnimSprite("howtoplay.png",2,1);
			_manual.SetXY (460, 400);
			_manual.SetScaleXY (1.4f, 1.4f);

			AddButtons ();
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
			Console.WriteLine (selectednumber);

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

