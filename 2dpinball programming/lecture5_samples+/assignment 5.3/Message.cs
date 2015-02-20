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
		public bool auto = false;

		public Message (int fps, string message, float timer,float fontsize = 50, bool showmessage = true)
		{
			_canvas = new Canvas (1000,1000);	//random size that fits probably all messages
			font = new Font ("Broadway BT",fontsize,FontStyle.Regular);
			size = _canvas.graphics.MeasureString (message, font);
			brush = new SolidBrush (Color.MediumPurple);
			point = new PointF (0, 0);
			_timer = timer * fps;
			_canvas = new Canvas ((int)size.Width, (int)size.Height);
			AddChild (_canvas);
			if (showmessage) {
				_canvas.graphics.DrawString (message, font, brush, point);
			}
		}

		public void AddPicture(string image,int x, int y, float scalex = 1, float scaley = 1)
		{
			Sprite Addedpicture = new Sprite(image);
			Addedpicture.SetXY (x,y);
			Addedpicture.SetOrigin (Addedpicture.x/2,Addedpicture.y/2);
			Addedpicture.SetScaleXY (scalex,scaley);
			AddChild (Addedpicture);
		}

		void Update(){
			if (auto) {
				Step ();
			}
		}

		public void Step()
		{
			_timer--;
			if (_timer <= 0) 
			{
				this.Destroy ();
			}
		}
	}
}