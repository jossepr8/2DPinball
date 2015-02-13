using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Resources;
using System.Collections.Generic;

namespace GXPEngine
{
	public class Menu : GameObject
	{

		MyGame _game;
		Sprite _playbutton;
		Sprite _playbutton1;

		int selectednumber = 0;
		List<Sprite> buttonlist = new List<Sprite>();


		public Menu (MyGame game)
		{
			_game = game;
			_playbutton = new Sprite ("thickmenu.png");
			AddChild (_playbutton);
			_playbutton.SetXY (460, 200);
			_playbutton.SetScaleXY (1.4f, 1.4f);
			buttonlist.Add (_playbutton);

			_game = game;
			_playbutton1 = new Sprite ("thickmenu.png");
			AddChild (_playbutton1);
			_playbutton.SetXY (460, 400);
			_playbutton.SetScaleXY (1.4f, 1.4f);
			buttonlist.Add (_playbutton1);

		}


		private void SwitchMenu(int index)
		{
			switch (index) {

			case 0:
				_game.SetState (States.Level);
				this.Destroy ();
				return;

			case 1:
				_game.SetState (States.Highscores);
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
				} 
				else 
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
		}
	}
}

