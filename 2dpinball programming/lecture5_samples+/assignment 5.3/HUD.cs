using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Resources;
using System.Drawing.Text;
using System.Collections.Generic;
using System.Globalization;


namespace GXPEngine
{	

	public class HUD : GameObject
	{	
		//PointF pnt2;

		Sprite HealthBar = new Sprite("HudTimer.png");
		Sprite overlayHealthbar = new Sprite ("InnerColor.png");

		Canvas _centcanvas = new Canvas (1000,1000);

		Sprite HudBlue = new Sprite ("HudBlue.png");
		Sprite HudRed = new Sprite ("HudRed.png");

		Font font = new Font ("Broadway BT",40,FontStyle.Regular);
		Font font1 = new Font ("Broadway BT",25,FontStyle.Regular);

		PointF pnt1 = new PointF (50, 10);
		PointF pnt2 = new PointF (1160, 10);

		SizeF size1;
		SizeF size2;

		readonly Pen redPen = new Pen(Color.Red, 3);
		readonly SolidBrush blueBrush= new SolidBrush(Color.Blue);
		readonly SolidBrush redBrush= new SolidBrush(Color.Red);

		Rectangle rec = new Rectangle (500,30,300,20);
		public int maxhealth = 100;
		Canvas _canvas;

		public HUD ()
		{	

			_canvas = new Canvas (1280, 720);
			AddChild (_canvas);


			AddChild (HudBlue);
			HudBlue.SetXY (10,10);

			AddChild (HudRed);
			HudRed.SetXY (1120, 10);

			AddChild (overlayHealthbar);
			overlayHealthbar.SetXY (650,55);
			overlayHealthbar.SetOrigin (overlayHealthbar.width / 2, overlayHealthbar.height / 2);
			overlayHealthbar.SetScaleXY (0.1f,0.75f);

			AddChild (HealthBar);
			HealthBar.SetXY (500,30);

		}

	
		public void UpdateHUD(float health, int score1, int score2)
		{	
			if (health >= maxhealth)
			{
				health = maxhealth;
			}

			overlayHealthbar.SetScaleXY ((health / maxhealth),0.9f);
			HealthBar.SetXY (500,30);

			if (GetChildren ().Contains (_canvas)) {
				_canvas.graphics.Clear (Color.Empty);

				if (score1 >= 1000) 
				{
					size1 = _centcanvas.graphics.MeasureString (score1.ToString (), font1);
					pnt1.Y = 23;
				} else 
				{
					size1 = _centcanvas.graphics.MeasureString (score1.ToString (), font);
				}

				if (score2 >= 1000) 
				{
					size2 = _centcanvas.graphics.MeasureString (score2.ToString (), font1);
					pnt2.Y = 23;
				} 
				else 
				{
					size2 = _centcanvas.graphics.MeasureString (score2.ToString (), font);
				}

				pnt1.X = _canvas.width - 1210 - size1.Width / 2;
				pnt2.X = _canvas.width  - 100 - size2.Width / 2;

				if (score1 >= 1000) 
				{
					_canvas.graphics.DrawString (score1.ToString (), font1, blueBrush, pnt1);
				} 
				else 
				{
					_canvas.graphics.DrawString (score1.ToString (), font, blueBrush, pnt1);
				}

				if (score2 >= 1000) 
				{
					_canvas.graphics.DrawString (score2.ToString (), font1, redBrush, pnt2);
				} 
				else 
				{
					_canvas.graphics.DrawString (score2.ToString (), font, redBrush, pnt2);
				}


			}
		}
	}
}

