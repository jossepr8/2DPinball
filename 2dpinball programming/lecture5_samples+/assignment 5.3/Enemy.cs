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
		public Types _type;

		public Enemy (Types type = Types.Normal) : base("sheet.png",8,1)
		{	
			_type = type;
			SetScaleXY (0.5f, 0.5f);
			SetOrigin (width / 2, height / 2);
			speed = Properties.EnemyGravity;
			switch (type) {
			case Types.Green:
				SetColor (0, 200, 0);
				break;
			case Types.Purple:
				SetColor (200, 0, 200);
				break;
			}

		}

		void SetFrames(int min, int max){
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
	public enum Types{
		Normal,
		Green,
		Purple
	}

}