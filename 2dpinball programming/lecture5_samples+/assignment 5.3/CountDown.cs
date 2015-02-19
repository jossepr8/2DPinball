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

		public CountDown (int countdown,Level level)
		{
			_level = level;
			_level.GetGame ()._stepstate = StepStates.None;
			for (int i = countdown; i >= 0; i--) {
				Message message = new Message (60, i.ToString(), 1, 90);
				messagelist.Add (message);
			}
		}
			

		void Update(){
			if (!_level.GetChildren().Contains(currentmessage) && timer < messagelist.Count) {
				currentmessage = messagelist [timer];
				timer++;

				_level.AddChild (currentmessage);
				currentmessage.SetXY (_level.GetWidth()/2 - currentmessage.size.Width/2 , 150);
			}
			if (timer == messagelist.Count) {
				_level.buttonmasher.Start ();
				this.Destroy ();
				currentmessage.auto = true;

			}
			if (currentmessage != null) {
				currentmessage.Step ();
			}

		}


	}
}

