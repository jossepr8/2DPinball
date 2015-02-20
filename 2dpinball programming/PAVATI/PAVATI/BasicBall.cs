using System;
using System.Drawing;

namespace GXPEngine
{
	public class BasicBall : Canvas
	{
		public Vec2 position;
		public float radius;

		public BasicBall (int pRadius, Vec2 pPosition = null):base (pRadius*2, pRadius*2)
		{
			position = pPosition;
			radius = pRadius;

			Step ();
			//draw ();
			SetOrigin (width / 2, height / 2);
		}


		void draw() {
			graphics.Clear (Color.Empty);
			graphics.FillEllipse (
				new SolidBrush (Color.Blue),
				0, 0, 2 * radius, 2 * radius
			);
		}

		public void Step(){
			if (position != null) {
				x = position.x;
				y = position.y;
			}
		}


	}
}

