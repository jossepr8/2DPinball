using System;
using System.Collections.Generic;
using System.Drawing;

namespace GXPEngine
{
	public class Level : GameObject
	{	
		List<LineSegment> _lines;
		Ball _ball;
		MyGame _game;

		Vec2 _previousPosition;
		Canvas _canvas;
		int width;
		int height;

		LineSegment flip1;
		LineSegment flip2;
		List<Enemie> enemielist = new List<Enemie> ();



		public Level (MyGame game)
		{
			_game = game;
			width = _game.width;
			height = _game.height;
			_canvas = new Canvas (width, height);
			AddChild (_canvas);

			_lines = new List<LineSegment> ();

			Wave wav = new Wave (this);


			//-----------------------------walls----------------------
			AddLine (new Vec2 (0, 0), new Vec2 (width, 0));	
			AddLine (new Vec2 (width, 0), new Vec2 (width, height));	
			AddLine (new Vec2 (width, height), new Vec2 (0, height));	
			AddLine (new Vec2 (0, height), new Vec2 (0,0));	
			//--------------------------------------------------------

			//------------------------------enemies-------------------
			//AddLine (new Vec2 (400, 300), new Vec2 (500,400));	
			//--------------------------------------------------------
			flip1 = new LineSegment (new Vec2 (200, 600), new Vec2 (0,400), 0xff00ff00, 4, false, 1);
			flip2 = new LineSegment (new Vec2 (600, 600), new Vec2 (800,400), 0xff00ff00, 4, false, 1);
			AddChild (flip1);
			_lines.Add (flip1);
			AddChild (flip2);
			_lines.Add (flip2);
		


			_ball = new Ball (30, new Vec2 (width / 8, 3 * height / 4), null, Color.Green);
			AddChild (_ball);
			_ball.velocity = new Vec2 (500,200).Sub(_ball.position).Normalize().Scale(25);	//start velocity
			_previousPosition = _ball.position.Clone ();
		}
		void AddLine (Vec2 start, Vec2 end, float bounciness = 0.95f) {
			LineSegment line = new LineSegment (start, end, 0xff00ff00, 4, false, bounciness);
			AddChild (line);
			_lines.Add (line);
		}
		public void AddEnemie(Enemie enemie){
			AddChild (enemie);
			enemielist.Add (enemie);
		}
		public int GetWidth(){
			return width;
		}
		public int GetHeight(){
			return height;
		}

		void Update(){

			if (Input.GetKey (Key.LEFT)) {
				flip1.bounciness = 1.5f;
			} else {
				flip1.bounciness = 1;
			}
			if (Input.GetKey (Key.RIGHT)) {
				flip2.bounciness = 1.5f;
			} else {
				flip2.bounciness = 1;
			}
			if (_ball != null) {
				_ball.Step ();
			}

			for (int i = 0; i < _lines.Count; i++) {
				lineCollisionTest (_lines [i],false);	//collisiontest twice for both sides
				lineCollisionTest (_lines [i],true);
			}
			if (_ball != null) {
				_canvas.graphics.DrawLine (//white trail behind the ball
					Pens.White, _previousPosition.x, _previousPosition.y, _ball.position.x, _ball.position.y
				);
			}


			if (_ball != null) {
				_previousPosition = _ball.position.Clone ();
				_ball.velocity.Add (new Vec2 (0, 1.5f));	//gravity
			}
		}

		void lineCollisionTest(LineSegment line, bool flipNormal) {
			if (_ball == null) {
				return;
			}
			Vec2 differenceVector = _ball.position.Clone ().Sub (line.start);
			Vec2 lineNormal = line.end.Clone ().Sub (line.start).Normal ().Scale(flipNormal? -1: 1);
			Vec2 lineNormalNormal = lineNormal.Clone().Normal ().Scale(flipNormal? -1: 1);
			Vec2 linevec = new Vec2(line.end.x - line.start.x,line.end.y - line.start.y);
			float ballDistance = differenceVector.Dot (lineNormal);
			float ballDistanceNormal = differenceVector.Dot (lineNormalNormal);
			Vec2 distancevec = _ball.position.Clone ().Sub (line.start);
			float distance = Math.Abs(distancevec.Length());
			Vec2 linecapnormal = distancevec.Clone ().Normal ();
			float overlap = _ball.radius + 25 - distance;
			if ((_previousPosition.Clone ().Sub (line.start)).Dot (lineNormal) >= _ball.radius && ballDistance < _ball.radius) {
				if (ballDistanceNormal < 0 && ballDistanceNormal > -linevec.Length ()) {
					_ball.velocity.Reflect (lineNormal,line.bounciness);
					_ball.position.Sub (lineNormal.Scale (ballDistance - _ball.radius));
				}
			} else if (overlap > 0) {
				_ball.velocity.Reflect (linecapnormal.Normal());
			}



		}

	}
}

