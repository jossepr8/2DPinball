using System;

namespace GXPEngine
{
	public class Vec2 
	{
		public static Vec2 zero { get { return new Vec2(0,0); }}
		public static Vec2 temp = new Vec2 ();

		public float x = 0;
		public float y = 0;

		public Vec2 (float pX = 0, float pY = 0)
		{
			x = pX;
			y = pY;
		}

		public override string ToString ()
		{
			return String.Format ("({0}, {1})", x, y);
		}
			
		public Vec2 Add (Vec2 other) {
			x += other.x;
			y += other.y;
			return this;
		}

		public Vec2 Sub (Vec2 other) {
			x -= other.x;
			y -= other.y;
			return this;
		}

		public float Length() {
			return (float)Math.Sqrt (x * x + y * y);
		}

		public Vec2 Normalize () {
			if (x == 0 && y == 0) {
				return this;
			} else {
				return Scale (1/Length ());
			}
		}

		public Vec2 Clone() {
			return new Vec2 (x, y);
		}
	
		public Vec2 Scale (float scalar) {
			x *= scalar;
			y *= scalar;
			return this;
		}

		public Vec2 Set (float pX, float pY) {
			x = pX;
			y = pY;
			return this;
		}

		public Vec2 Normal() {
			//temp = this.Normalize();
			return new Vec2 (-this.y,this.x).Normalize();
		}
	
		public float Dot (Vec2 other) {
			return this.x * other.x + this.y * other.y;
		}

		public Vec2 Reflect(Vec2 normal, float bounciness = 1){
			//this.Sub(normal.Clone().Scale(-1 * bounciness));
			this.Sub (normal.Clone().Scale((1 + bounciness)*this.Dot(normal)));
			//this  -(1+ bounciness) * (this * normal) * normal
			//this.Dot (normal);
			return this;
		}

		public void SetAngleRadians(float radians){
			float length = Length ();
			x = (float)Math.Cos (radians) * length;
			y = (float)Math.Sin (radians) * length;
		}
		public void SetAngleDegrees(float degrees){
			SetAngleRadians (degrees * ((float)Math.PI / 180));
		}
		public float GetAngleRadians(){
			return (float)Math.Atan2 (y,x);
		}
		public float GetAngleDegrees(){
			float angle = GetAngleRadians() * (180 / (float)Math.PI);
			return angle;
		}
		public void RotateDegrees(float degrees){
			//RotateRadians (degrees * ((float)Math.PI / 180));
			SetAngleDegrees (GetAngleDegrees () + degrees);
		}
		public void RotateRadians(float radians){
			SetAngleRadians (GetAngleRadians() + radians);
		}



	}
}

