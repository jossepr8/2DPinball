using System;

namespace GXPEngine
{
	public class Enemie : Sprite
	{
		float _speed = 10f;

		public Enemie () : base("checkers.png")
		{
			SetScaleXY (0.5f, 0.5f);
			SetOrigin (width / 2, height / 2);
		}

		void Update(){
			y += _speed;
		}
	}
}

