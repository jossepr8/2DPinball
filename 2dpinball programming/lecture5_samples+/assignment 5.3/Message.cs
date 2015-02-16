using System;
using System.Drawing;

namespace GXPEngine
{
	public class Message : GameObject
	{
		Canvas _canvas;
		float _timer;
		Font font;
		SolidBrush brush;
		PointF point;
		public Message (int fps, string message, float timer)
		{
			font = new Font ("Broadway",50,FontStyle.Regular);
			brush = new SolidBrush (Color.Green);
			point = new PointF (0, 0);
			_timer = timer * fps;
			_canvas = new Canvas (1000, 400);	//random size that fits probably all messages
			AddChild (_canvas);
			_canvas.graphics.DrawString (message,font,brush,point);
		}

		void Update(){
			_timer--;
			if (_timer <= 0) {
				this.Destroy ();
			}
		}

	

	}
}

