using System;

namespace GXPEngine
{
	public class Flipper : LineSegment
	{
		Vec2 aaa;
		float alength;
		Vec2 OldStart;
		Vec2 OldEnd;
		movement moving;

		public Flipper (Vec2 pstart, Vec2 pend) : base(pstart,pend)
		{
			moving = movement.none;
			OldStart = pstart;
			OldEnd = pend;
			aaa = pend.Clone().Sub (this.start);
			alength = aaa.Length ();
		}

		void Update(){
			Console.WriteLine (aaa.GetAngleDegrees ());
			if (aaa.GetAngleDegrees() >= -45) {
				moving = movement.down;
			}
			if (aaa.GetAngleDegrees() < 190 && aaa.GetAngleDegrees() > 0) {
				moving = movement.none;
			}
			if (aaa.GetAngleDegrees () > 0 && aaa.GetAngleDegrees () < 180) {
				aaa.SetAngleDegrees (180);
			}
			this.end = this.start.Clone().Add (aaa);
			if (moving == movement.up) {
				aaa.RotateDegrees (15);
			}
			if (moving == movement.down) {
				aaa.RotateDegrees (-15);
			}

			if (Input.GetKey (Key.UP)) {
				moving = movement.up;
				aaa.RotateDegrees (1);
			}
			if (Input.GetKeyDown (Key.DOWN)) {
				moving = movement.down;
			}
		}

	}

	enum movement{
		none,
		up,
		down
	}
}

