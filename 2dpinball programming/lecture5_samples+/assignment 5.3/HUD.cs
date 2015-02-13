using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Resources;
namespace GXPEngine
{
	public class HUD : GameObject
	{	

		Sprite rightHealthbar = new Sprite ("RightHealthbar.png");
		Sprite leftHealthbar = new Sprite ("LeftHealthbar.png");

		readonly Pen redPen = new Pen(Color.Red, 3);
		readonly SolidBrush blueBrush= new SolidBrush(Color.Blue);
		Rectangle rec = new Rectangle (500,30,300,20);
		int maxhealth = 200;
		Canvas _canvas;

		public HUD ()
		{
			_canvas = new Canvas (1280, 720);
			AddChild (_canvas);

			AddChild (leftHealthbar);
			leftHealthbar.SetXY (650,30);
			leftHealthbar.SetScaleXY (-0.01f,1.7);

			AddChild (rightHealthbar);
			rightHealthbar.SetXY (650,30);
			rightHealthbar.SetScaleXY (0.01f,1.7);

			_canvas.graphics.DrawRectangle (redPen, rec);
		}

		public void UpdateHUD(float health)
		{	
			if (health >= maxhealth)
			{
				health = maxhealth;
			}

			rightHealthbar.SetScaleXY ((health / maxhealth),1.7);
			leftHealthbar.SetScaleXY ((-health / maxhealth), 1.7);
			if (GetChildren ().Contains (_canvas)) {
				_canvas.graphics.DrawRectangle (redPen, rec);
			}
		}
	
	
	}
}

