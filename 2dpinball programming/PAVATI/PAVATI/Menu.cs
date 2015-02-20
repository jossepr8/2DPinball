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
		AnimSprite _names;
		Wave wave;
		Sprite logo;
		Sprite _esc;

		Sprite _background;


		int selectednumber = 0;
		List<AnimSprite> buttonlist = new List<AnimSprite>();
		public List<Enemy> enemylist = new List<Enemy> ();

		public Menu (MyGame game)
		{	
			_game = game;
			_background = new Sprite ("mainmenubackground.png");
			AddChild (_background);
			wave = new Wave (this);
			logo = new Sprite ("PAVATI.png");
			logo.SetXY (_game.width / 2 - logo.width / 2, -140);
			AddChild (logo);

			SoundManager.Playmusic ("mainmenu.mp3");



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

			_names = new AnimSprite ("Names2.png",2,1);
			_names.SetXY (460, 500);
			_names.SetScaleXY (1.4f, 1.4f);

			AddButtons ();

		}
		public MyGame GetGame(){
			return _game;
		}

		public void Addenemy(Enemy enemy){
			AddChild (enemy);
			enemylist.Add (enemy);
		}

		void AddButtons()
		{
			AddChild (_start);
			buttonlist.Add (_start);

			AddChild (_highscore);
			buttonlist.Add (_highscore);

			AddChild (_manual);
			buttonlist.Add (_manual);

			AddChild (_names);
			buttonlist.Add (_names);
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
			case 3:
				_game.SetState (States.NameMenu);
				this.Destroy ();
				return;
			}
		}

		public void UpdateButtons(){
			RemoveChild (_start);
			RemoveChild (_highscore);
			RemoveChild (_manual);
			RemoveChild (_names);
			RemoveChild (logo);

			AddChild (_start);
			AddChild (_highscore);
			AddChild (_manual);
			AddChild (_names);
			AddChild (logo);
		}
		void Update()
		{	
			wave.Step (true);
			foreach (Enemy enemy in enemylist) {
				if (enemy.y > _game.height + 50) {
					enemylist.Remove (enemy);
					enemy.Destroy ();
					break;
				}
			}

			if (Input.GetKeyDown (Key.ESCAPE)) {
				Environment.Exit (0);
			}
		

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
				_names.SetFrame (1);
			}


			if (selectednumber == 1) 
			{	
				_start.SetFrame (1);
				_highscore.SetFrame (0);
				_highscore.SetXY (459,300);
				_manual.SetFrame (1);
				_names.SetFrame (1);
			}

			if (selectednumber == 2) 
			{	
				_start.SetFrame (1);
				_highscore.SetFrame (1);
				_manual.SetFrame (0);
				_names.SetFrame (1);
			}

			if (selectednumber == 3) 
			{	
				_start.SetFrame (1);
				_highscore.SetFrame (1);
				_manual.SetFrame (1);
				_names.SetFrame (0);
			}


		}
	}
}

