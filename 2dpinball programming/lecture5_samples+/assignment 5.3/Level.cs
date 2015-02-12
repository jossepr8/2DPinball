using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;

namespace GXPEngine
{

	public enum Players
	{
		_player1,
		_player2,
		none
	}

	public class Level : GameObject
	{	
		List<LineSegment> _lines;
		Ball _ball;
		MyGame _game;


		Vec2 _previousPosition;
		Canvas _canvas;
		int width;
		int height;

		HUD _hud;
	
		List<Enemy> enemylist = new List<Enemy> ();

		Flipper _player1;
		Flipper _player2;

		Players _touched = Players.none;
		float Damage = 0;
		SolidBrush brush = new SolidBrush (Color.Red);
		PointF pnt = new PointF (10, 10);
		LineSegment DEBUGdistance;
		LineSegment DEBUGdistance2;

		public Level (MyGame game)
		{
			_game = game;
			width = _game.width;
			height = _game.height;
			_canvas = new Canvas (width, height);
			AddChild (_canvas);
			_hud = new HUD();
			AddChild (_hud);
			_lines = new List<LineSegment> ();

			Wave wave = new Wave (this);	//spawns 1 wave



			//-----------------------outer walls----------------------
			AddLine (new Vec2 (0, 0), new Vec2 (width, 0));	
			AddLine (new Vec2 (width, 0), new Vec2 (width, height));	
			AddLine (new Vec2 (width, height), new Vec2 (0, height));	
			AddLine (new Vec2 (0, height), new Vec2 (0,0));	
			//--------------------------------------------------------



		

			//--------------------------------ball-------------------------------------------
			_ball = new Ball (32, new Vec2 (width / 8, 3 * height / 4), null, Color.Green){
				position = new Vec2 (300, 400)};	//start position
			AddChild (_ball);
			_ball.velocity = new Vec2 (0,-200).Normalize().Scale(3);	//start velocity
			_previousPosition = _ball.position.Clone ();
			//--------------------------------------------------------------------------------
		


			_player1 = new Flipper ();
			_player1.SetXY (width/2-150, 150);
			_player1.rotation = _player1.StartAngle;
			_player1.SetColor (0, 0, 200);	//blue
			AddChild (_player1);

			_player2 = new Flipper ();
			_player2.SetXY (width/2+150, 150);
			_player2.rotation = -_player2.StartAngle;
			_player2.SetColor (200, 0, 0);	//red
			AddChild (_player2);


			DEBUGdistance = new LineSegment (new Vec2 (0, 0), new Vec2 (0, 500));
			DEBUGdistance.end.RotateDegrees (_player1.rotation);
			DEBUGdistance.end.Add (new Vec2 (_player1.x, _player1.y));
			//AddChild (DEBUGdistance);	//add debug lines
			DEBUGdistance2 = new LineSegment (new Vec2 (0, 0), new Vec2 (0, 500));
			DEBUGdistance2.end.RotateDegrees (_player2.rotation);
			DEBUGdistance2.end.Add (new Vec2 (_player2.x, _player2.y));
			//AddChild (DEBUGdistance2);	//add debug lines

		}
		void AddLine (Vec2 start, Vec2 end, float bounciness = 1) {
			LineSegment line = new LineSegment (start, end, 0xff00ff00, 4, false, bounciness);
			AddChild (line);
			_lines.Add (line);
		}
		public void Addenemy(Enemy enemy){
			AddChild (enemy);
			enemylist.Add (enemy);
		}
		public int GetWidth(){
			return width;
		}
		public int GetHeight(){
			return height;
		}
		void UpdateDistance(LineSegment line, Flipper player, bool plus = true){	//checks the distance between ball and _player
			line.end.Sub (new Vec2 (player.x, player.y));
			if (plus) {
				line.end.RotateDegrees (player.rotationspeed);
			} else {
				line.end.RotateDegrees (-player.rotationspeed);
			}
			line.end.Add (new Vec2 (player.x, player.y));
		}
		void Player1Control(){
			if (Input.GetKey (Key.A)) {	//player 1 control LEFT
				if (_player1.rotation < _player1.MaxAngle) {
					_player1.rotation += _player1.rotationspeed;
					UpdateDistance (DEBUGdistance,_player1);
				}
			} 
			if (Input.GetKey (Key.D)) {	//player 1 control RIGHT
				if (_player1.rotation > 0) {
					_player1.rotation -= _player1.rotationspeed;
					UpdateDistance (DEBUGdistance,_player1, false);
				}
			} 
		}
		void Player2Control(){
			if (Input.GetKey (Key.LEFT)) {	//player 2 control LEFT
				if (_player2.rotation < 0) {
					_player2.rotation += _player2.rotationspeed;
					UpdateDistance (DEBUGdistance2,_player2);
				}
			} 
			if (Input.GetKey (Key.RIGHT)) {	//player 2 control RIGHT
				if (_player2.rotation > -_player2.MaxAngle) {
					_player2.rotation -= _player2.rotationspeed;
					UpdateDistance (DEBUGdistance2,_player2, false);
				}
			} 
		}


		void Update(){
			//Console.WriteLine (DEBUGdistance.start.DistanceTo (DEBUGdistance.end));
			//Console.WriteLine (_ball.position.DistanceTo (new Vec2 (_player1.matrix [0] + _player1.x, _player1.matrix [1] + _player1.y)));
			DEBUGdistance.start = _ball.position;
			DEBUGdistance2.start = _ball.position;
		
			//Console.WriteLine (enemylist.Count);
			Player1Control ();
			Player2Control ();
			if (Input.GetKeyDown (Key.SPACE)) {
				_canvas.graphics.DrawString ("abcdefghijklmnopqrstuvwxyz", _game.font, brush, pnt);	//test
			}
			if (_ball != null) {
				_ball.Step ();
			}

			for (int i = 0; i < _lines.Count; i++) {
				lineCollisionTest (_lines [i],false);	//collisiontest twice for both sides
				lineCollisionTest (_lines [i],true);
			}
			if (_ball.HitTest (_player1)) {
				if (DEBUGdistance.end.Clone().Sub(DEBUGdistance.start).Length() <= 100) {	//work in progress
					_ball.velocity.Reflect (new Vec2 (_player1.matrix [0], _player1.matrix [1]).Normal (), _player1.Bounce);	//bounce from top player1
				} else {
					_ball.velocity.Reflect (new Vec2 (_player1.matrix [4], _player1.matrix [5]).Normal (), _player1.Bounce);	//bounce from side player1
				}
				_touched = Players._player1;
				_ball.overlay2.SetColor (0, 0, 200);	//color = blue

			}
			if (_ball.HitTest (_player2)) {
				if (DEBUGdistance2.end.Clone().Sub(DEBUGdistance2.start).Length() <= 100) {	//work in progress
					_ball.velocity.Reflect (new Vec2 (_player2.matrix [0], _player2.matrix [1]).Normal (), _player2.Bounce);	//bounce from top player2
				} else {
					_ball.velocity.Reflect (new Vec2 (_player2.matrix [4], _player2.matrix [5]).Normal (), _player2.Bounce);	//bounce from side player2
				}
				_touched = Players._player2;
				_ball.overlay2.SetColor (200, 0, 0);	//color = red
			}
			foreach (Enemy enemy in enemylist) 
			{

				if (enemy.y >= height) {
					Damage++;
					_hud.UpdateHUD (Damage);
					enemylist.Remove (enemy);
					enemy.Destroy ();
					break;
				}


				if (_ball.HitTest (enemy)) {
					_ball.velocity.Reflect (new Vec2 (_ball.x - enemy.x,  _ball.y - enemy.y).Normal());
					enemylist.Remove (enemy);
					enemy.Destroy ();

					if (_touched == Players._player1) {
						_player1.score++;
					} else if (_touched == Players._player2)
					{
						_player2.score++;
					}
					break;
				}




			}

			if (_ball != null) {
				/*
				_canvas.graphics.DrawLine (		//white trail behind the ball
					Pens.White, _previousPosition.x, _previousPosition.y, _ball.position.x, _ball.position.y
				*/
				_previousPosition = _ball.position.Clone ();
				if (_ball.velocity.Length () > 25) {
					_ball.velocity.Scale (0.8f);
				}
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