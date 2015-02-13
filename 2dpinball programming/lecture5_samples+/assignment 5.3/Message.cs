using System;
using System.Drawing;

namespace GXPEngine
{
	public class Message : GameObject
	{
		Canvas _canvas;
		int _timer;
		Font font;
		SolidBrush brush;
		PointF point;
		public Message ( string message, int timer)
		{
			font = new Font ("Broadway",50,FontStyle.Regular);
			brush = new SolidBrush (Color.Green);
			point = new PointF (0, 0);
			_timer = timer;
			_canvas = new Canvas (1000, 400);
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

