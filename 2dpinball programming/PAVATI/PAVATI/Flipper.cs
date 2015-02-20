using System;

namespace GXPEngine
{
	public class Flipper : AnimSprite
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

		int frametimer = 0;
		int timer = 10;
			
		public Flipper (string image = "paddletest.png") : base(image,2,1)
		{
			SetOrigin (width / 2, height/2 - 500);
			rotationspeed = Properties.PaddleSpeed;
			StartAngle = Properties.PaddleStartAngle;
			MaxAngle = Properties.PaddleMaxAngle;
			Bounce = Properties.PaddleBounce;
			score = 0;
		}
		public void UpdateColor(){
			frametimer = timer;
			SetFrame (1);
		}

		void Update(){
			if (frametimer > 0) {
				frametimer--;
			}
			if (frametimer <= 0) {
				SetFrame (0);
			}
		

		}
			

	}
}

