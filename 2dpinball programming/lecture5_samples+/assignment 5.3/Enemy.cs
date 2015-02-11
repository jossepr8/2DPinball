using System;

namespace GXPEngine
{
	public class Enemy : Sprite
	{
		float _speed = 0.5f;

		public Enemy () : base("checkers.png")
		{
			SetScaleXY (0.5f, 0.5f);
			SetOrigin (width / 2, height / 2);
		}

		void Update(){
			y += _speed;
		}
	}
}

