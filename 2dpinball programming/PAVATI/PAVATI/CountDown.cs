using System;
using System.Collections.Generic;

namespace GXPEngine
{
	public class CountDown : GameObject
	{
		Message currentmessage;
		List<Message> messagelist = new List<Message>();
		int timer = 0;
		Level _level;
		bool _showmessage;

		public CountDown (int countdown,Level level,bool showmessage = true)
		{
			_showmessage = showmessage;
			_level = level;
			_level.GetGame ()._stepstate = StepStates.None;
			for (int i = countdown; i >= 0; i--) {
				Message message = new Message (60, i.ToString(), 1, 90,showmessage);
				if (i == 0) {
					message = new Message (60, "GO", 1, 90);
				}
				messagelist.Add (message);
			}
			if (!showmessage) {
				Message m = new Message (60, "GAME OVER", 3, 90u);
				_level.AddChild (m);
				m.SetXY (_level.GetWidth()/2 - m.size.Width/2 , 120);
			}
		}
			

		void Update(){
			if (!_level.GetChildren().Contains(currentmessage) && timer < messagelist.Count) {
				currentmessage = messagelist [timer];
				timer++;

				_level.AddChild (currentmessage);
				currentmessage.SetXY (_level.GetWidth()/2 - currentmessage.size.Width/2 , 220);
			}
			if (timer == messagelist.Count) {
				_level.buttonmasher.Start ();
				this.Destroy ();
				currentmessage.auto = true;
				if (!_showmessage) {
					_level.GetGame().SetState (States.Highscores);
				}

			}
			if (currentmessage != null) {
				currentmessage.Step ();
			}

		}


	}
}

