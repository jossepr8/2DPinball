using System;

namespace GXPEngine
{
	public class CollisionManager2
	{
		Level _level;

		public CollisionManager2 (Level level)
		{
			_level = level;
		}



		bool CheckCollision(BasicBall ball, bool checkonly = false){
			if (_level._ball == null) {
				return false;
			}
			Vec2 differenceVector = _level._ball.position.Clone ().Sub (ball.position);
			if (differenceVector.Length () <= ball.radius + _level._ball.radius * _level._ball.scaleX) {
				if (checkonly) {
				} else {
					Reflect (differenceVector.Normal(),ball);
				}
				return true;
			}
			return false;
		}

		bool CheckCollision(LineSegment line, bool flipNormal,bool checkonly = false){
			if (_level._ball == null) {
				return false;
			}
			Vec2 differenceVector = _level._ball.position.Clone ().Sub (line.start);
			Vec2 lineNormal = line.end.Clone ().Sub (line.start).Normal ().Scale(flipNormal? -1: 1);
			Vec2 lineNormalNormal = lineNormal.Clone().Normal ().Scale(flipNormal? -1: 1);
			Vec2 linevec = new Vec2(line.end.x - line.start.x,line.end.y - line.start.y);
			float ballDistance = differenceVector.Dot (lineNormal);
			float ballDistanceNormal = differenceVector.Dot (lineNormalNormal);
			if ((_level._previousPosition.Clone ().Sub (line.start)).Dot (lineNormal) >= _level._ball.radius * -_level._ball.scaleX && ballDistance < _level._ball.radius * _level._ball.scaleX) {
				if (ballDistanceNormal < 0 && ballDistanceNormal > -linevec.Length ()) {
					if (checkonly) {
						return true;
					} else {
						SoundManager.Playsound (SoundEffect.bounce);
						ResolveCollision (line, flipNormal);
						return true;
					}
				}
			}
			return false ;
		}
		void ResolveCollision(LineSegment line, bool flipnormal){
			if (line == null) {
				return;
			}
			Reflect (line, flipnormal);
		}
		void Reflect(Vec2 normal, BasicBall ball){
			_level._ball.velocity.Reflect(normal,1);
			for (int i = 0; i < 10; i++) {
				if (CheckCollision (ball, true)) {
					_level._ball.position.Sub (_level._ball.velocity.Clone ().Scale (-0.1f));
				} else {
					break;
				}
			}
		}

		void Reflect(LineSegment line, bool flipNormal){
			Vec2 differenceVector = _level._ball.position.Clone ().Sub (line.start);
			Vec2 lineNormal = line.end.Clone ().Sub (line.start).Normal ().Scale(flipNormal? -1: 1);
			Vec2 lineNormalNormal = lineNormal.Clone().Normal ().Scale(flipNormal? -1: 1);
			float ballDistance = differenceVector.Dot (lineNormal);
			float ballDistanceNormal = differenceVector.Dot (lineNormalNormal);
			_level._ball.velocity.Reflect (lineNormal, line.bounciness);
			if (line.Equals(_level.matrixline1)) {
				_level._ball.overlay1.SetColor (0, 0, 200);
				_level._ball.overlay2.SetColor (0, 0, 200);
				SoundManager.Playsound (SoundEffect.bounce2,1,-1);
				_level._touched = Players._player1;
			}
			if (line.Equals(_level.matrixline2)) {
				_level._ball.overlay1.SetColor (200, 0, 0);
				_level._ball.overlay2.SetColor (200, 0, 0);
				SoundManager.Playsound (SoundEffect.bounce3,1,1);
				_level._touched = Players._player2;
			}
			//_level._ball.position.Sub (lineNormal.Scale (ballDistance - _level._ball.radius));
			for (int i = 0; i < 10; i++) {	//put the ball back untill he doesnt hit the line anymore
				if(CheckCollision(line,flipNormal,true)){
				_level._ball.position.Sub (_level._ball.velocity.Clone ().Scale (-0.1f));
				} else{
					break;
				}
			}
				

		}

		public void Step(){
			foreach (LineSegment line in _level._lines) {
				CheckCollision (line,false);
				CheckCollision (line,true);
			}
			foreach (BasicBall ball in _level._balls) {
				CheckCollision (ball);
			}
		}

	}
}

