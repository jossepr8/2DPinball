using System;

namespace GXPEngine
{
	public class ButtonMasher : GameObject
	{
		public delegate void StopDelegate();
		public event StopDelegate StopEvent;

		int totalcounter = 0;
		LastPressed Player1Last;
		LastPressed Player2Last;

		public bool enabled {
			get;
			set;
		}
		MyGame _game;
		Level _level;

		Message message;
		Message message2;
		Message message3;
		Message message4;

		Sprite bar1 = new Sprite("mashbar.png");
		Sprite bar2 = new Sprite("mashindicator.png");
		float bar2startx;

		public ButtonMasher (MyGame game, Level level)
		{
			enabled = false;
			_game = game;
			_level = level;
			StopEvent += Stop;
		}


		public void Start(){
			Player1Last = LastPressed.none;
			Player2Last = LastPressed.none;
			totalcounter = 0;
			enabled = true;

			_game._stepstate = StepStates.None;

			message = new Message (_game.currentFps, "Press:", 7,20);
			message.AddPicture ("ad.png",120,-225,1.2f,1.2f);
			message.SetXY (_level.GetWidth() / 4 - message.size.Width/2, 200);
			_level.AddChild (message);

			message3 = new Message (_game.currentFps, "Press:", 7,20);
			message3.AddPicture ("ad1.png",120,-225,1.2f,1.2f);
			message3.SetXY (_level.GetWidth() / 4 - message3.size.Width/2, 200);
			_level.AddChild (message3);

			message2 = new Message (_game.currentFps, "Press:", 0,20);
			message2.AddPicture ("arrows.png",(int)message2.x + 130,(int)message2.y - 78,0.7f,0.7f);
			message2.SetXY (_level.GetWidth() / 4*3 - message.size.Width/2, 200);
			_level.AddChild (message2);

			message4 = new Message (_game.currentFps, "Press:", 0,20);
			message4.AddPicture ("arrows1.png",(int)message4.x + 130,(int)message4.y - 78,0.7f,0.7f);
			message4.SetXY (_level.GetWidth() / 4*3 - message.size.Width/2, 200);
			_level.AddChild (message4);

			bar2.SetXY (game.width/2 - bar2.width/2, 300);
			bar2startx = bar2.x;

			bar1.width = bar2.width * 20;
			bar1.SetXY (game.width/2 - bar1.width/2, 300);

			_level.AddChild (bar1);
			_level.AddChild (bar2);
		}
		public void Stop(){
			enabled = false;
			_game._stepstate = StepStates.All;
			message.Step ();
			message2.Step ();
			bar1.Destroy ();
			bar2.Destroy ();
		}
		void Update(){
			if (enabled) {
				bar2.x = bar2startx + totalcounter * 10;
				if (Input.GetKeyDown (Key.A) && Player1Last != LastPressed.left) {
					Player1Last = LastPressed.left;
					totalcounter--;
				}
				if (Input.GetKeyDown (Key.D) && Player1Last != LastPressed.right) {
					Player1Last = LastPressed.right;
					totalcounter--;
				}
				if (Input.GetKeyDown (Key.LEFT) && Player2Last != LastPressed.left) {
					Player2Last = LastPressed.left;
					totalcounter++;
				}
				if (Input.GetKeyDown (Key.RIGHT) && Player2Last != LastPressed.right) {
					Player2Last = LastPressed.right;
					totalcounter++;
				}

				if (totalcounter <= -10) {
					StopEvent += _level.ButtonMashPlayer1Win;
					StopEvent ();
					StopEvent -= _level.ButtonMashPlayer1Win;
				
				}
				if (totalcounter >= 10) {
					StopEvent += _level.ButtonMashPlayer2Win;
					StopEvent ();
					StopEvent -= _level.ButtonMashPlayer2Win;
				
				}


			}
		}

		public enum LastPressed{
			none,
			left,
			right
		}

	}
}

