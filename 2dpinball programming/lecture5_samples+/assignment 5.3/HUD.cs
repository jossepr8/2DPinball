using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Resources;
using System.Drawing.Text;
using System.Collections.Generic;


namespace GXPEngine
{	

	public class HUD : GameObject
	{	
		//PointF pnt2;

		Sprite HealthBar = new Sprite("HudTimer.png");
		Sprite overlayHealthbar = new Sprite ("InnerColor.png");

		Sprite HudBlue = new Sprite ("HudBlue.png");
		Sprite HudRed = new Sprite ("HudRed.png");

		Font font = new Font ("Broadway",40,FontStyle.Regular);

		readonly SolidBrush brushred = new SolidBrush (Color.Red);
		readonly SolidBrush brushblue = new SolidBrush (Color.Blue);

		PointF pnt1 = new PointF (50, 10);
		PointF pnt2 = new PointF (1160, 10);


		readonly Pen redPen = new Pen(Color.Red, 3);
		readonly SolidBrush blueBrush= new SolidBrush(Color.Blue);
		Rectangle rec = new Rectangle (500,30,300,20);
		int maxhealth = 50;
		Canvas _canvas;

		public HUD ()
		{	

			_canvas = new Canvas (1280, 720);
			AddChild (_canvas);


			AddChild (HudBlue);
			HudBlue.SetXY (10,10);

			AddChild (HudRed);
			HudRed.SetXY (1120, 10);

			AddChild (HealthBar);
			HealthBar.SetXY (500,30);


			AddChild (overlayHealthbar);
			overlayHealthbar.SetXY (650,55);
			overlayHealthbar.SetOrigin (overlayHealthbar.width / 2, overlayHealthbar.height / 2);
			overlayHealthbar.alpha = 0.65f;
			overlayHealthbar.SetScaleXY (0.1f,0.75f);
		}

		public void UpdateHUD(float health, int score1, int score2)
		{	
			if (health >= maxhealth)
			{
				health = maxhealth;
			}

			overlayHealthbar.SetScaleXY ((health / maxhealth),0.9f);

			if (GetChildren ().Contains (_canvas)) {
				_canvas.graphics.Clear (Color.Empty);
				_canvas.graphics.DrawString (score1.ToString (), font, brushblue, pnt1);
				_canvas.graphics.DrawString (score2.ToString (), font, brushred, pnt2);


			}
		}
	}
}

