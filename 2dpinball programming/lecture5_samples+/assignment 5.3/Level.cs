using System;
using System.Collections.Generic;
using System.Drawing;

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
		public List<LineSegment> _lines;	//all lines that have collisions
		public List<BasicBall> _balls;	//line caps
		public List<BasicBall> _enemyballs;	//hitboxes
		public List<Enemy> enemylist = new List<Enemy> ();
		public Ball _ball;	//the actual ball
		MyGame _game;

		CollisionManager2 colmanager;
		public Vec2 _previousPosition;
		Canvas _canvas;
		Canvas _layer1canvas;
		int width;
		int height;

		public HUD _hud;

		public Flipper _player1;
		public Flipper _player2;

		public float Damage = 0;


		LineSegment DEBUGdistance;
		LineSegment DEBUGdistance2;


		Sprite background;
		Wave wave;

		//public string currentmusic;

		public LineSegment matrixline1;
		public LineSegment matrixline2;

		public LineSegment matrixlineV1;
		public LineSegment matrixlineV2;
		public BasicBall linecap11;
		public BasicBall linecap12;
		public BasicBall linecap21;
		public BasicBall linecap22;
		Vec2 matrixvec1;
		Vec2 matrixvec2;

		CountDown cd;


		public ButtonMasher buttonmasher;

		public Players _touched = Players.none;

		Sprite blackhole;


		void GameOver(){
			_game._stepstate = StepStates.None;
			_game.scorelist.Add (new Score (_player1.score, _game.player1name));
			_game.scorelist.Add (new Score (_player2.score, _game.player2name));
			_game.scorelistteam.Add (new Score (_player2.score + _player1.score, _game.teamname));
			_game.SAVE ();
			_game.SetState (States.Highscores);
		}

		public void Step(){
			Player1Control ();
			Player2Control ();
			EnemyHeightControl ();
			BallControl ();
			UpdatePaddles ();
			colmanager.Step ();
			wave.Step ();
			blackholeStep ();
			ExitStep ();
			CheckBallHeight ();
			CheckBar ();

		}
		void CheckBallHeight(){
			if (_ball.y > height) {
				Damage += _hud.maxhealth / 4;
				_hud.UpdateHUD (Damage,_player1.score,_player2.score);
				if (!CheckBar()) {
					//buttonmasher.Start ();
					buttonmasher = new ButtonMasher (_game, this);
					//AddChild (cd);
				}

				_ball.y = 2000;
				_ball.velocity = new Vec2 (0, -10);
			}
		}
		bool CheckBar(){
			if (Damage >= _hud.maxhealth) {
				GameOver ();
				return true;
			} else {
				return false;
			}
		}
		void ExitStep(){
			if (Input.GetKeyDown (Key.ESCAPE)) {
				_game._stepstate = StepStates.None;
				_game.SetState (States.MainMenu);
			}
		}
		void blackholeStep(){
			blackhole.rotation -= 5;
		}

		public Level (MyGame game)
		{
			_lines = new List<LineSegment> ();
			_balls = new List<BasicBall> ();
			_enemyballs = new List<BasicBall> ();

			colmanager = new CollisionManager2(this);

			background = new Sprite ("nebula2.png");
			background.SetScaleXY (0.75f,0.75f);
			AddChild (background);

			blackhole = new Sprite ("blackhole.png");
			blackhole.SetOrigin (blackhole.width / 2, blackhole.height / 2);
			blackhole.SetXY (640, 100);
			AddChild (blackhole);

			_game = game;
			width = _game.width;
			height = _game.height;
			_canvas = new Canvas (width, height);
			AddChild (_canvas);
			_layer1canvas = new Canvas (width, height);
			AddChild (_layer1canvas);

			wave = new Wave (this);	//start the wave spawning ( 	with wave.step()	)

			_hud = new HUD();
			AddChild (_hud);


			MakeWalls ();
			MakeBall ();
			MakePaddles ();

			_hud.UpdateHUD (Damage, _player1.score, _player2.score);

			buttonmasher = new ButtonMasher (_game,this);
			//buttonmasher.Start ();



			//SoundManager.Playmusic ("Wave1.mp3");
		}

		void MakeWalls(){
			//-----------------------outer walls----------------------
			//AddLine (new Vec2 (0, 0), new Vec2 (width, 0));				//top
			//AddLine (new Vec2 (width, 0), new Vec2 (width, height));		//right
			//AddLine (new Vec2 (width, height), new Vec2 (0, height));		//bottom
			//AddLine (new Vec2 (0, height), new Vec2 (0,0));				//left
			AddLine (new Vec2 (width, 0), new Vec2 (width, height/2));		//right2
			AddLine (new Vec2 (0, height/2), new Vec2 (0,0));				//left2
			//--------------------------------------------------------
		}
		void MakeBall(){
			//--------------------------------ball-------------------------------------------
			_ball = new Ball ( 32, new Vec2 (width / 8, 3 * height / 4), null, Color.Green){
				position = new Vec2 (width/2, height/2)};	//start position
			AddChild (_ball);
			_ball.velocity = new Vec2 (0,-200).Normalize().Scale(5);	//start velocity
			_previousPosition = _ball.position.Clone ();
			_ball.Step ();
			//--------------------------------------------------------------------------------
		}
		void MakePaddles(){
			//--------------------------------flippers/paddles/players------------------------
			_player1 = new Flipper ();
			_player1.SetXY (width/2-width/10, 150);
			_player1.rotation = _player1.StartAngle;
			_player1.SetColor (0, 0, 200);	//blue
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

			//-------line that represents paddle of player 1--------
			matrixline1 = new LineSegment (new Vec2 (0, 0), new Vec2 (0, 0));
			matrixline1.bounciness = _player1.Bounce;
			//AddChild (matrixline1);
			_lines.Add (matrixline1);
			matrixvec1 = new Vec2 (0, 0);
			//---------paddle 1 line caps------------------------
			linecap11 = new BasicBall (3, new Vec2 (0, 0));
			//AddChild (linecap11);
			_balls.Add (linecap11);
			linecap12 = new BasicBall (3, new Vec2 (0, 0));
			//AddChild (linecap12);
			_balls.Add (linecap12);
			matrixlineV1 = new LineSegment (new Vec2 (0, 0), new Vec2 (0, 0));
			//AddChild (matrixlineV1);
			_lines.Add (matrixlineV1);

			//-------line that represents paddle of player 2--------
			matrixline2 = new LineSegment (new Vec2 (0, 0), new Vec2 (0, 0));
			matrixline2.bounciness = _player2.Bounce;
			//AddChild (matrixline2);
			_lines.Add (matrixline2);
			matrixvec2 = new Vec2 (0, 0);
			//---------paddle 2 line caps--------------------
			linecap21 = new BasicBall (3, new Vec2 (0, 0));
			//AddChild (linecap21);
			_balls.Add (linecap21);
			linecap22 = new BasicBall (3, new Vec2 (0, 0));
			//AddChild (linecap22);
			_balls.Add (linecap22);
			matrixlineV2 = new LineSegment (new Vec2 (0, 0), new Vec2 (0, 0));
			//AddChild (matrixlineV2);
			_lines.Add (matrixlineV2);

			_hud.UpdateHUD (Damage,_player1.score,_player2.score);

		}

		public void ButtonMashPlayer1Win(){
			_ball.position = new Vec2 (width/4, _player1.y);
			_touched = Players._player1;
		}
		public void ButtonMashPlayer2Win(){
			_ball.position = new Vec2 (width/4*3, _player2.y);
			_touched = Players._player2;
		}

		public MyGame GetGame(){
			return _game;
		}
		public Wave GetWave(){
			return wave;
		}
		void AddLine (Vec2 start, Vec2 end, float bounciness = 1) {
			LineSegment line = new LineSegment (start, end, 0xff00ff00, 4, false, bounciness);
			AddChild (line);
			_lines.Add (line);
			BasicBall linecap1 = new BasicBall (2,line.start);
			AddChild (linecap1);
			_balls.Add (linecap1);
			BasicBall linecap2 = new BasicBall (2,line.end);
			AddChild (linecap2);
			_balls.Add (linecap2);
		}
		public void Addenemy(Enemy enemy){
			AddChild (enemy);
			enemylist.Add (enemy);
			BasicBall ball = new BasicBall (25, new Vec2 (0, 0));
			ball.SetColor (0, 0, 0);
			AddChild (ball);
			_enemyballs.Add (ball);
		}
		public int GetWidth(){
			return width;
		}
		public int GetHeight(){
			return height;
		}
		void EnemyHeightControl(){
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
		void BallControl(){
			if (_ball != null) {
				_ball.Step (false,false);
			}
		}
		void UpdatePaddles(){
			DEBUGdistance.start = _ball.position;
			DEBUGdistance2.start = _ball.position;
			//------------line that represents paddle 1---------------------------
			//matrixvec represents the angle of the paddle
			matrixvec1.x = _player1.matrix[0];	
			matrixvec1.y = _player1.matrix [1];
			matrixline1.start = 
				DEBUGdistance.end.Clone ()
					.Sub (matrixvec1.Clone ()
						.Scale (_player1.width / (float)_player1.scaleX / 2))
							.Sub (new Vec2 (_player1.matrix [4], _player1.matrix [5]).Scale (_player1.height / 2));
			matrixline1.end = 
				matrixvec1.Clone()
					.Scale (_player1.width/(float)_player1.scaleX)
					.Add (matrixline1.start);

			matrixlineV1.start = matrixline1.start.Clone ()
				.Add (new Vec2 (_player1.matrix [4], _player1.matrix [5]).Scale (_player1.height))
				.Sub (matrixvec1.Clone().Scale (_player1.width / 12));
			matrixlineV1.end = matrixline1.end.Clone ()
				.Add (new Vec2 (_player1.matrix [4], _player1.matrix [5]).Scale (_player1.height))
				.Add (matrixvec1.Clone().Scale (_player1.width / 12));
			//--------------------------------------------------------------------
			//------------line that represents paddle 2---------------------------
			//matrixvec represents the angle of the paddle
			matrixvec2.x = _player2.matrix[0];
			matrixvec2.y = _player2.matrix [1];

			matrixline2.start = 
				DEBUGdistance2.end.Clone()
					.Sub(matrixvec2.Clone()
						.Scale(_player2.width/(float)_player2.scaleX/2))
							.Sub (new Vec2 (_player2.matrix [4], _player2.matrix [5]).Scale (_player2.height / 2));	
			matrixline2.end = 
				matrixvec2.Clone ()
					.Scale (_player2.width / (float)_player2.scaleX)
					.Add (matrixline2.start);

			matrixlineV2.start = matrixline2.start.Clone ()
				.Add (new Vec2 (_player2.matrix [4], _player2.matrix [5]).Scale (_player2.height))
					.Sub (matrixvec2.Clone().Scale (_player2.width / 12));
			matrixlineV2.end = matrixline2.end.Clone ()
				.Add (new Vec2 (_player2.matrix [4], _player2.matrix [5]).Scale (_player2.height))
					.Add (matrixvec2.Clone().Scale (_player2.width / 12));

					
			//--------------------------------------------------------------------


			linecap11.position = matrixline1.start;
			linecap12.position = matrixline1.end;
			linecap21.position = matrixline2.start;
			linecap22.position = matrixline2.end;

			linecap11.Step ();
			linecap12.Step ();
			linecap21.Step ();
			linecap22.Step ();
		}

		public void updateMusic(string music)
		{
			SoundManager.Playmusic (music);
		}
	}
}