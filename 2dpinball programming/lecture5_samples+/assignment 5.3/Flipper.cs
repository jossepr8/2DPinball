using System;

namespace GXPEngine
{
	public class Flipper : Sprite
	{
		public float rotationspeed {
			get;
			set;
		}
		public float StartAngle {
			get;
			set;
		}
		public float MaxAngle {
			get;
			set;
		}
		public float Bounce {
			get;
			set;
		}

		public int score {
			get;
			set;
		}
			
			
		public Flipper () : base("paddletest.png")
		{
			SetOrigin (width / 2, height/2 - 500);
			rotationspeed = Properties.PaddleSpeed;
			StartAngle = Properties.PaddleStartAngle;
			MaxAngle = Properties.PaddleMaxAngle;
			Bounce = Properties.PaddleBounce;
			score = 1000;
			//SetScaleXY (1, 5);
			//scaleY = 5;
		}

		void Update(){
	
		}

	}
}

