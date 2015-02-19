﻿using System;

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
			
			
		public Flipper (string image = "paddletest.png") : base(image)
		{
			SetOrigin (width / 2, height/2 - 500);
			rotationspeed = Properties.PaddleSpeed;
			StartAngle = Properties.PaddleStartAngle;
			MaxAngle = Properties.PaddleMaxAngle;
			Bounce = Properties.PaddleBounce;
			score = 0;
		}

		void Update(){
	
		}

	}
}

