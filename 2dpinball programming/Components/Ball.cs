using System;
using System.Drawing;
using System.Xml;
using GXPEngine.Core;

namespace GXPEngine
{
	public class Ball : Canvas
	{
		public Vec2 position;
		public Vec2 velocity;
		public readonly int radius;
		private Color _ballColor;
		Vec2 Gravity;
		float Speed;
		float MaxSpeed;
		float Frame = 1;
		public AnimSprite overlay2;
		public AnimSprite overlay1;
		Level _level;

		public Ball (Level level, int pRadius, Vec2 pPosition = null, Vec2 pVelocity = null, Color? pColor = null):base (pRadius*2, pRadius*2)
		{
			_level = level;
			overlay1 = new AnimSprite ("Electric Swirl.png",2,1);
			overlay1.SetOrigin (width / 2, height / 2);
			overlay1.SetFrame (0);
			//overlay1.SetScaleXY (1.2f, 1.2f);
			AddChild (overlay1);
			//overlay = new AnimSprite ("Ball Sprite Blue Player.png",4,1);
			overlay2 = new AnimSprite ("Ball Sprite.png",4,1);
			overlay2.SetOrigin (width / 2, height / 2);
			AddChild (overlay2);
			//overlay.initializeFromTexture(Texture2D.GetInstance("Ball Sprite Red Player.png"));


			Speed = Properties.BallSpeed;
			MaxSpeed = Properties.BallMaxSpeed;
			Gravity = Properties.BallGravity;
			radius = pRadius;
			SetOrigin (radius, radius);

			position = pPosition ?? Vec2.zero;
			velocity = pVelocity ?? Vec2.zero;
			_ballColor = pColor ?? Color.Blue;

			_ballColor = Color.Black;
			//draw ();
			Step ();
		}
		void Animation(){
			Frame += 0.5f;
			if (Frame > 4) {
				Frame = 0;
			}
			overlay2.SetFrame ((int)Frame);
		}

		void draw() {
			graphics.Clear (Color.Empty);
			graphics.FillEllipse (
				new SolidBrush (_ballColor),
				0, 0, 2 * radius, 2 * radius
			);
		}

		public void Step(bool skipVelocity = false, bool skipGravity = false, Vec2 pvelocity = null) {
			//Console.WriteLine (velocity.Length ());
			if (velocity.Length () > Properties.BallMaxSpeed) {
				velocity.Scale (20/velocity.Length());
			} 
			overlay1.rotation-=10;
			if (position == null || velocity == null)
				return;

			if (!skipVelocity) position.Add (pvelocity??velocity);
			if(!skipGravity) velocity.Add (Gravity);
			x = position.x;
			y = position.y;
			rotation += velocity.Length() + 1;
			//_level.CheckCollision ();
			//draw ();
		}

		public Color ballColor {
			get {
				return _ballColor;
			}

			set {
				_ballColor = value;
				draw ();
			}
		}


			

	}
}

