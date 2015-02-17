using System;

namespace GXPEngine
{
	public class ButtonMasher : GameObject
	{
		public delegate void StopDelegate();
		public event StopDelegate StopEvent;

		int Player1Counter = 0;
		int Player2Counter = 0;
		LastPressed Player1Last;
		LastPressed Player2Last;

		public bool enabled {
			get;
			set;
		}
		MyGame _game;
		Level _level;

		public ButtonMasher (MyGame game, Level level)
		{
			enabled = false;
			_game = game;
			_level = level;
			StopEvent += Stop;
		}

		public void Start(){
			enabled = true;
			_game._stepstate = StepStates.None;
			Message message = new Message (_game.currentFps, "Press A and D", 3,20);
			message.SetXY (_level.GetWidth() / 4 - message.size.Width/2, 200);
			_level.AddChild (message);

			Message message2 = new Message (_game.currentFps, "Press LEFT and RIGHT", 3,20);
			message2.SetXY (_level.GetWidth() / 4*3 - message.size.Width/2, 200);
			_level.AddChild (message2);
		}
		public void Stop(){
			enabled = false;
			_game._stepstate = StepStates.All;
		}
		void Update(){
			if (enabled) {
				if (Input.GetKeyDown (Key.A) && Player1Last != LastPressed.left) {
					Player1Last = LastPressed.left;
					Player1Counter++;
				}
				if (Input.GetKeyDown (Key.D) && Player1Last != LastPressed.right) {
					Player1Last = LastPressed.right;
					Player1Counter++;
				}
				if (Input.GetKeyDown (Key.LEFT) && Player2Last != LastPressed.left) {
					Player2Last = LastPressed.left;
					Player2Counter++;
				}
				if (Input.GetKeyDown (Key.RIGHT) && Player2Last != LastPressed.right) {
					Player2Last = LastPressed.right;
					Player2Counter++;
				}
				if (Player1Counter >= 10) {
					StopEvent += _level.ButtonMashPlayer1Win;
					StopEvent ();
					StopEvent -= _level.ButtonMashPlayer1Win;
				}
				if (Player2Counter >= 10) {
					StopEvent += _level.ButtonMashPlayer2Win;
					StopEvent ();
					StopEvent -= _level.ButtonMashPlayer2Win;
				}
			}
		}

		public enum LastPressed{
			left,
			right
		}

	}
}

