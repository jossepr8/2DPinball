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
		public List<LineSegment> _lines;
		public List<BasicBall> _balls;
		public List<BasicBall> _enemyballs;	//hitboxes
		public List<Enemy> enemylist = new List<Enemy> ();
		public Ball _ball;
		MyGame _game;

		CollisionManager2 colmanager;
		public Vec2 _previousPosition;
		Canvas _canvas;
		Canvas _layer1canvas;
		int width;
		int height;

		public HUD _hud;
	
		bool instadeath = false;	//hard mode --doesnt work atm



		public Flipper _player1;
		public Flipper _player2;

		public float Damage = 0;


		PointF pntmiddle;
		LineSegment DEBUGdistance;
		LineSegment DEBUGdistance2;


		Sprite background;
		Wave wave;

		public LineSegment matrixline1;
		public LineSegment matrixline2;
		Vec2 matrixvec1;
		Vec2 matrixvec2;
		public BasicBall linecap11;
		public BasicBall linecap12;
		public BasicBall linecap21;
		public BasicBall linecap22;

		public Players _touched = Players.none;

		public Level (MyGame game)
		{
			_lines = new List<LineSegment> ();
			_balls = new List<BasicBall> ();
			_enemyballs = new List<BasicBall> ();



			colmanager = new CollisionManager2(this);

			background = new Sprite ("nebula.png");
			background.SetScaleXY (0.75f,0.75f);
			AddChild (background);

			_game = game;
			width = _game.width;
			height = _game.height;
			_canvas = new Canvas (width, height);
			AddChild (_canvas);
			_layer1canvas = new Canvas (width, height);
			AddChild (_layer1canvas);

			pntmiddle = new PointF (width / 2, height / 2);



			wave = new Wave (this);	//spawns waves until wave.Step() isent called anymore

			_hud = new HUD();
			AddChild (_hud);


			//-----------------------outer walls----------------------
			AddLine (new Vec2 (0, 0), new Vec2 (width, 0));				//top
			AddLine (new Vec2 (width, 0), new Vec2 (width, height));	//right
			AddLine (new Vec2 (width, height), new Vec2 (0, height));	//bottom
			AddLine (new Vec2 (0, height), new Vec2 (0,0));				//left
			//--------------------------------------------------------


			//--------------------------------ball-------------------------------------------
			_ball = new Ball (this, 32, new Vec2 (width / 8, 3 * height / 4), null, Color.Green){
				position = new Vec2 (300, 400)};	//start position
			AddChild (_ball);
			_ball.velocity = new Vec2 (0,-200).Normalize().Scale(5);	//start velocity
			_previousPosition = _ball.position.Clone ();
			//--------------------------------------------------------------------------------
		

			//--------------------------------flippers/paddles/players------------------------
			_player1 = new Flipper ();
			_player1.SetXY (width/2-width/10, 150);
			_player1.rotation = _player1.StartAngle;
			_player1.SetColor (0, 0, 200);	//blue
			//_player1.SetScaleXY (2, 1);
			_player1.scaleX = 2;
			AddChild (_player1);

			_player2 = new Flipper ();
			_player2.SetXY (width/2+width/10, 150);
			_player2.rotation = -_player2.StartAngle;
			_player2.SetColor (200, 0, 0);	//red
			_player2.scaleX = 3;
			AddChild (_player2);
			//--------------------------------------------------------------------------------


			//------------ball distance to the paddles----------------------------------------
			//middle of the paddle-----------------------------------------------------------
			DEBUGdistance = new LineSegment (new Vec2 (0, 0), new Vec2 (0, 500));
			DEBUGdistance.end.RotateDegrees (_player1.rotation);
			DEBUGdistance.end.Add (new Vec2 (_player1.x, _player1.y));
			//AddChild (DEBUGdistance);	//add debug lines
			DEBUGdistance2 = new LineSegment (new Vec2 (0, 0), new Vec2 (0, 500));
			DEBUGdistance2.end.RotateDegrees (_player2.rotation);
			DEBUGdistance2.end.Add (new Vec2 (_player2.x, _player2.y));
			//AddChild (DEBUGdistance2);	//add debug lines
			//----------------------------------------------------------------------------------------

			//--------------------------------------------------------------------------------

			matrixline1 = new LineSegment (new Vec2 (0, 0), new Vec2 (0, 0));
			matrixline1.bounciness = 1.3f;
			AddChild (matrixline1);
			_lines.Add (matrixline1);

			matrixline2 = new LineSegment (new Vec2 (0, 0), new Vec2 (0, 0));
			matrixline2.bounciness = 1.3f;
			AddChild (matrixline2);
			_lines.Add (matrixline2);

			matrixvec1 = new Vec2 (0, 0);
			matrixvec2 = new Vec2 (0, 0);

			//BasicBall linecap1 = new BasicBall (50,new Vec2(500,400));
			//AddChild (linecap1);
			//_balls.Add (linecap1);

			linecap11 = new BasicBall (5, new Vec2 (0, 0));
			AddChild (linecap11);
			_balls.Add (linecap11);
			linecap12 = new BasicBall (5, new Vec2 (0, 0));
			AddChild (linecap12);
			_balls.Add (linecap12);
			linecap21 = new BasicBall (5, new Vec2 (0, 0));
			AddChild (linecap21);
			_balls.Add (linecap21);
			linecap22 = new BasicBall (5, new Vec2 (0, 0));
			AddChild (linecap22);
			_balls.Add (linecap22);

			BasicBall balltest = new BasicBall (10, new Vec2 (299, 500));
			AddChild (balltest);
			_balls.Add (balltest);

			_hud.UpdateHUD (Damage,_player1.score,_player2.score);
		}
		public MyGame GetGame(){
			return _game;
		}
		public Wave GetWave(){
			return wave;
		}
		public List<Enemy> GetEnemyList(){
			return enemylist;
		}
		public List<LineSegment> GetLineList(){
			return _lines;
		}
		void AddLine (Vec2 start, Vec2 end, float bounciness = 1) {
			LineSegment line = new LineSegment (start, end, 0xff00ff00, 4, false, bounciness);
			AddChild (line);
			_lines.Add (line);
			BasicBall linecap1 = new BasicBall (15,line.start);
			AddChild (linecap1);
			_balls.Add (linecap1);
			BasicBall linecap2 = new BasicBall (15,line.end);
			AddChild (linecap2);
			_balls.Add (linecap2);
		}
		public void Addenemy(Enemy enemy){
			AddChild (enemy);
			enemylist.Add (enemy);
			BasicBall ball = new BasicBall (25, new Vec2 (0, 0));
			ball.SetColor (0, 200, 0);
			AddChild (ball);
			_enemyballs.Add (ball);
		}
		public int GetWidth(){
			return width;
		}
		public int GetHeight(){
			return height;
		}

		void UpdateDistance(LineSegment line, Flipper player, bool plus = true){	//checks the distance between ball and _player
			line.end.Sub (new Vec2 (player.x, player.y));	//start from point 0,0 (needed to rotate)
			if (plus) {
				line.end.RotateDegrees (player.rotationspeed);	//rotate to desired rotation
			} else {
				line.end.RotateDegrees (-player.rotationspeed);
			}
			line.end.Add (new Vec2 (player.x, player.y));	//start from paddle location again
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
		public void CheckCollision(){
			for (int i = 0; i < _lines.Count; i++) {
				lineCollisionTest (_lines [i],false);	//collisiontest twice for both sides
				lineCollisionTest (_lines [i],true);
			}
		}

		public void Step(){
			Updatee ();
			colmanager.Step ();
		}
		void Updatee(){
			Console.WriteLine (enemylist.Count);

			DEBUGdistance.start = _ball.position;
			DEBUGdistance2.start = _ball.position;
			//------------line that represents paddle 1---------------------------
			//matrixvec represents the angle of the paddle
			matrixvec1.x = _player1.matrix[0];	
			matrixvec1.y = _player1.matrix [1];
			matrixline1.start = 
				DEBUGdistance.end.Clone()
					.Sub(matrixvec1.Clone()
						.Scale(_player1.width/(float)_player1.scaleX/2));
			matrixline1.end = 
				matrixvec1.Clone()
					.Scale (_player1.width/(float)_player1.scaleX)
				.Add (matrixline1.start);
			//--------------------------------------------------------------------
			//------------line that represents paddle 2---------------------------
			//matrixvec represents the angle of the paddle
			matrixvec2.x = _player2.matrix[0];
			matrixvec2.y = _player2.matrix [1];

			matrixline2.start = 
				DEBUGdistance2.end.Clone()
					.Sub(matrixvec2.Clone()
						.Scale(_player2.width/(float)_player2.scaleX/2));
			matrixline2.end = 
				matrixvec2.Clone()
					.Scale (_player2.width/(float)_player2.scaleX)
					.Add (matrixline2.start);
			//--------------------------------------------------------------------

			linecap11.position = matrixline1.start;
			linecap12.position = matrixline1.end;
			linecap21.position = matrixline2.start;
			linecap22.position = matrixline2.end;

			//linecap11.position = new Vec2 (500, 500);
			linecap11.Step ();
			linecap12.Step ();
			linecap21.Step ();
			linecap22.Step ();


//			if (GetChildren().Contains(_canvas)) {
//				_canvas.graphics.Clear (Color.Empty);
//				_canvas.graphics.DrawString (_player1.score.ToString (), _game.font, brushblue, pnt);
//				_canvas.graphics.DrawString (_player2.score.ToString (), _game.font, brushred, pnt2);
//			}




		
			//Console.WriteLine (enemylist.Count);
			Player1Control ();
			Player2Control ();

			if (_ball != null) {
				_ball.Step (false,false);
			}

			//CheckCollision ();

			/*	//old (bad) paddle collisions -- keep here as backup
			if (_ball.HitTest (_player1)) {
				if (DEBUGdistance.end.Clone().Sub(DEBUGdistance.start).Length() <= 100) {	//work in progress
					_ball.velocity.Reflect (new Vec2 (_player1.matrix [0], _player1.matrix [1]).Normal (), _player1.Bounce);	//bounce from top player1
				} else {
					_ball.velocity.Reflect (new Vec2 (_player1.matrix [4], _player1.matrix [5]).Normal (), _player1.Bounce);	//bounce from side player1
					_ball.position.Add (_ball.velocity);
				}
				_touched = Players._player1;
				_ball.overlay2.SetColor (0, 0, 200);	//color = blue
				_ball.overlay1.SetColor (0, 0, 200);	//color = blue

			}
			if (_ball.HitTest (_player2)) {
				if (DEBUGdistance2.end.Clone().Sub(DEBUGdistance2.start).Length() <= 100) {	//work in progress
					_ball.velocity.Reflect (new Vec2 (_player2.matrix [0], _player2.matrix [1]).Normal (), _player2.Bounce);	//bounce from top player2
				} else {
					_ball.velocity.Reflect (new Vec2 (_player2.matrix [4], _player2.matrix [5]).Normal (), _player2.Bounce);	//bounce from side player2
					_ball.position.Add (_ball.velocity);
				}
				_touched = Players._player2;
				_ball.overlay2.SetColor (200, 0, 0);	//color = red
				_ball.overlay1.SetColor (200, 0, 0);	//color = red
			}
			*/
			for (int i = 0; i < enemylist.Count; i++) {
				Enemy enemy = enemylist [i];
				_enemyballs [i].position.x = enemy.x;
				_enemyballs [i].position.y = enemy.y;
				_enemyballs [i].Step ();
				if (enemy.y >= height) {
					Damage++;
					_hud.UpdateHUD (Damage,_player1.score,_player2.score);
					enemylist.Remove (enemy);
					enemy.Destroy ();
					_enemyballs [i].Destroy ();
					_enemyballs.Remove (_enemyballs [i]);

				}
			}
			/*
			for (int i = 0; i < enemylist.Count; i++) {
				Enemy enemy = enemylist [i];
				_enemyballs [i].position.x = enemy.x;
				_enemyballs [i].position.y = enemy.y;
				_enemyballs [i].Step ();
				if (enemy.y >= height) {
					Damage++;
					_hud.UpdateHUD (Damage);
					enemylist.Remove (enemy);
					enemy.Destroy ();
					_enemyballs [i].Destroy ();
					_enemyballs.Remove (_enemyballs [i]);

					break;
				}
				if (_ball.HitTest (enemy)) {
					//_ball.velocity.Reflect (new Vec2 (_ball.x - enemy.x,  _ball.y - enemy.y).Normal());
					enemylist.Remove (enemy);
					enemy.Destroy ();
					_enemyballs [i].Destroy ();
					_enemyballs.Remove (_enemyballs [i]);
					if (_touched == Players._player1) {
						_player1.score++;
						SoundManager.Playsound (SoundEffect.enemyhit);
					} else if (_touched == Players._player2)
					{	
						SoundManager.Playsound (SoundEffect.enemyhit2);
						_player2.score++;
					}
					break;
				}
			}
			*/

			if (_ball != null) {
				/*
				_canvas.graphics.DrawLine (//white trail behind the ball
					Pens.White, _previousPosition.x, _previousPosition.y, _ball.position.x, _ball.position.y);
				*/
				//_previousPosition = _ball.position.Clone ();
				if (_ball.velocity.Length () > 25) {
					_ball.velocity.Scale (0.8f);
				}
			}
		}

		//not used atm
		void lineCollisionTest(LineSegment line, bool flipNormal) {	//general one... absolete atm
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
					if (line == _lines [2] && instadeath) {
						_game.SetState (States.MainMenu);
						this.Destroy ();
					}
				}
			} else if (overlap > 0) {
				_ball.velocity.Reflect (linecapnormal.Normal());
				if (line == _lines [2] && instadeath) {
					_game.SetState (States.MainMenu);
					this.Destroy ();
				}
			}
		}



	}
}