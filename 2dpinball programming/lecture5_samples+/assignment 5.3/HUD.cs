using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Resources;
namespace GXPEngine
{
	public class HUD : Canvas
	{	

		readonly Pen redPen = new Pen(Color.Red, 3);
		readonly SolidBrush blueBrush= new SolidBrush(Color.Blue);
		Rectangle rec = new Rectangle (500,30,300,20);

		public HUD () : base(1280,720)
		{
		
			graphics.FillRectangle (blueBrush,500,30,30,20);
			graphics.DrawRectangle (redPen, rec);

		}


		public void UpdateHUD(int health)
		{
			graphics.FillRectangle (blueBrush,500,30,health * 3,20);
			graphics.DrawRectangle (redPen, rec);
		}

	//draw reactangle
	// rectangle width = damage * 3; assuming damge = 100 max total length of the bar would be 300px


	}
}

