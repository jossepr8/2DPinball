using System;

namespace GXPEngine
{
	public class Flipper : Sprite
	{
		public float rotationspeed {
			get;
			set;
		}

		public Flipper () : base("paddle.png")
		{
			SetOrigin (width / 2, height/2 - 500);
			rotationspeed = Properties.PaddleSpeed;

		}

	}
}

