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
	
		public SizeF size;

		public Message (int fps, string message, float timer,float fontsize = 50, string musicfile = null)
		{
			_canvas = new Canvas (1000,1000);	//random size that fits probably all messages
			font = new Font ("Broadway",fontsize,FontStyle.Regular);
			size = _canvas.graphics.MeasureString (message, font);
			brush = new SolidBrush (Color.ForestGreen);
			point = new PointF (0, 0);
			_timer = timer * fps;
			_canvas = new Canvas ((int)size.Width, (int)size.Height);
			AddChild (_canvas);
			_canvas.graphics.DrawString (message,font,brush,point);
		}



		public void Step(){
			_timer--;
			if (_timer <= 0) {
				this.Destroy ();
			}
		}

	

	}
}

