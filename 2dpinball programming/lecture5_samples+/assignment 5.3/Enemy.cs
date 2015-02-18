using System;

namespace GXPEngine
{
	public class Enemy : AnimSprite
	{
		public float speed {
			get;
			set;
		}

		private float frame = 0;
		private float minframe = 0;
		private float maxframe = 8;

		public Enemy () : base("sheet.png",8,1)
		{	
			SetScaleXY (0.5f, 0.5f);
			SetOrigin (width / 2, height / 2);
			speed = Properties.EnemyGravity;
		}

		private void SetFrames(int min, int max){
			minframe = min;
			maxframe = max;
		}

		public void Step()
		{	
			frame += 0.12f;

			SetFrames (1, 8);

			if (frame >= maxframe + 1) {
				frame = minframe;
			}
			if (frame < minframe) {
				frame = maxframe;
			}
			SetFrame ((int)frame);

			y += speed;
		}

	}

}



