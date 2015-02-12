using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Resources;

namespace GXPEngine
{
	public class Menu : GameObject
	{

		MyGame _game;
		Sprite _playbutton;

		//sdsds


		public Menu (MyGame game)
		{
			_game = game;
			_playbutton = new Sprite ("thickmenu.png");
			AddChild (_playbutton);
			_playbutton.SetXY (460, 200);
			_playbutton.SetScaleXY (1.4f, 1.4f);

		}
	


		void Update()
		{
			if (Input.GetKeyDown (Key.SPACE)) 
			{
				Console.WriteLine("hi");
				_game.SetState (States.Level);
			}
		
		}



	}
}

