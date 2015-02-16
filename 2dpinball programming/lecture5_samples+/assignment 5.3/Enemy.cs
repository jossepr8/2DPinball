using System;

namespace GXPEngine
{
	public class Enemy : Sprite
	{
		public float speed {
			get;
			set;
		}


		public Enemy () : base("Enemy.png")
		{	
			SetScaleXY (0.5f, 0.5f);
			SetOrigin (width / 2, height / 2);
			speed = Properties.EnemyGravity;
		}

		void Update()
		{
			y += speed;
		}

	}

}



