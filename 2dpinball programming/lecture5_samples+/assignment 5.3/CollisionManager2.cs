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
		//aaa
		bool CheckCollision(BasicBall ball, bool checkonly = false){
			//if (_level._ball == null) {
			//	return false;
			//}
			Vec2 differenceVector = _level._ball.position.Clone ().Sub (ball.position);
			if (differenceVector.Length () <= ball.radius + _level._ball.radius * _level._ball.scaleX) {
				if (checkonly) 
				{
					HitBall (ball);
				} else {
					Reflect (differenceVector,ball);
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

			for (int i = 0; i < 10; i++) {
				if (CheckCollision (ball, true)) {
					//_level._ball.position.Sub (_level._ball.velocity.Clone ().Scale (-0.2f));
				} else {
					break;
				}
			}
			_level._ball.velocity.Reflect(normal,1.3f);
			//_level._ball.velocity.Add (_level._ball.position.Clone ().Sub (_middle).Scale (0.2f));
			if (ball.Equals (_level.linecap11) || ball.Equals (_level.linecap12)) {
				HitPlayer1 ();
			} else if (ball.Equals (_level.linecap21) || ball.Equals (_level.linecap22)) {
				HitPlayer2 ();
			}
		}
		void HitWall(){
			SoundManager.Playsound(SoundEffect.bounce);
		}
		void HitBall(BasicBall ball){
			//hit enemy
			for (int i = 0; i < _level._enemyballs.Count; i++) {
				if (ball == _level._enemyballs [i]) {
					_level._enemyballs [i].Destroy ();
					_level._enemyballs.Remove (_level._enemyballs [i]);
					_level.enemylist [i].Destroy ();
					_level.enemylist.Remove (_level.enemylist [i]);
					if (_level._touched == Players._player1) {
						_level._player1.score++;
						_level._hud.UpdateHUD (_level.Damage,_level._player1.score,_level._player2.score);
					}
					if (_level._touched == Players._player2) {
						_level._player2.score++;
						_level._hud.UpdateHUD (_level.Damage,_level._player1.score,_level._player2.score);
					}
				}
			}

		}
		void HitPlayer1(){
			_level._ball.overlay1.SetColor (0, 0, 200);
			_level._ball.overlay2.SetColor (0, 0, 200);
			SoundManager.Playsound (SoundEffect.bounce2,1,-1);
			_level._touched = Players._player1;

		}
		void HitPlayer2(){
			_level._ball.overlay1.SetColor (200, 0, 0);
			_level._ball.overlay2.SetColor (200, 0, 0);
			SoundManager.Playsound (SoundEffect.bounce3,1,1);
			_level._touched = Players._player2;
		
		}

		void Reflect(LineSegment line, bool flipNormal){
			Vec2 differenceVector = _level._ball.position.Clone ().Sub (line.start);
			Vec2 lineNormal = line.end.Clone ().Sub (line.start).Normal ().Scale(flipNormal? -1: 1);
			Vec2 lineNormalNormal = lineNormal.Clone().Normal ().Scale(flipNormal? -1: 1);
			float ballDistance = differenceVector.Dot (lineNormal);
			float ballDistanceNormal = differenceVector.Dot (lineNormalNormal);
			_level._ball.velocity.Reflect (lineNormal, line.bounciness);
	
			if (line.Equals (_level.matrixline1)) {
				HitPlayer1 ();
			} else if (line.Equals (_level.matrixline2)) {
				HitPlayer2 ();
			} else {
				HitWall ();
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
			foreach (BasicBall ball in _level._balls) {
				if (CheckCollision (ball)) {
					return;
				}
			}
			foreach (LineSegment line in _level._lines) {
				if (CheckCollision (line, false)) {
					return;
				}
				if (CheckCollision (line, true)) {
					return;
				}
			}

			foreach (BasicBall ball in _level._enemyballs) 
			{
				if (CheckCollision (ball,true)) 
				{
					return;
				}
			}

		}

	}
}

