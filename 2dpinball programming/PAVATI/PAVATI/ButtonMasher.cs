﻿using System;

namespace GXPEngine
{
	public class ButtonMasher : GameObject
	{
		public delegate void StopDelegate();
		public event StopDelegate StopEvent;

		int totalcounter = 0;
		LastPressed Player1Last;
		LastPressed Player2Last;

	

		public bool enabled {
			get;
			set;
		}
		MyGame _game;
		Level _level;

		Message message;
		Message message2;

		AnimSprite spriteAD = new AnimSprite("AandD.png",2,1);
		AnimSprite spriteArrows = new AnimSprite("Arrows2.png",2,1);

		Sprite tug = new Sprite("tug of war.png");
		Sprite bar = new Sprite("bar.png");

		float bar2startx;

		float frame = 0;
		float minframe = 0;
		float maxframe = 2;
		CountDown cd;

		public ButtonMasher (MyGame game, Level level)
		{
			enabled = false;
			_game = game;
			_level = level;
			StopEvent += Stop;
			cd = new CountDown (3,level);
			AddChild (cd);
		}


		public void Start(){
			Player1Last = LastPressed.none;
			Player2Last = LastPressed.none;
			totalcounter = 0;
			enabled = true;

			_game._stepstate = StepStates.None;

			message = new Message (_game.currentFps, "Press:", 0,20);
			message.SetXY (_level.GetWidth() / 4 - message.size.Width/2, 200);
			//spriteAD.SetScaleXY (2, 2);
			spriteAD.SetXY (_level.GetWidth() / 4 + 30,125);

			_level.AddChild (spriteAD);
			_level.AddChild (message);


			message2 = new Message (_game.currentFps, "Press:", 0,20);
			message2.SetXY (_level.GetWidth() / 4*3 - message.size.Width/2, 200);
			spriteArrows.SetXY (_level.GetWidth() / 4*3 - 50 + 100,147);
			spriteArrows.SetScaleXY (0.7f, 0.7f);
			_level.AddChild (spriteArrows);
			_level.AddChild (message2);


			tug.SetOrigin (tug.width / 2, tug.height / 2);
			tug.SetXY (game.width/2, 400);
			tug.SetScaleXY (1.6f, 1.0f);

			bar.SetOrigin (bar.width / 2, bar.height / 2);
			bar.SetXY (game.width/2, 400);

			//bar2startx = bar.x;

			//bar1.width = bar2.width * 20;

			//tug.SetScaleXY (3, 1);


			_level.AddChild (tug);
			_level.AddChild (bar);
		}
		public void Stop(){
			enabled = false;
			_game._stepstate = StepStates.All;
			spriteAD.Destroy ();
			spriteArrows.Destroy ();
			message.Step ();
			message2.Step ();
			bar.Destroy ();
			tug.Destroy ();
		}

		private void SetFrames(int min, int max){
			minframe = min;
			maxframe = max;
		}

		void Update(){
			if (enabled) {
				bar.x = _level.GetWidth()/2 + totalcounter * 10;
				if (Input.GetKeyDown (Key.A) && Player1Last != LastPressed.left) {
					Player1Last = LastPressed.left;
					totalcounter--;
				}
				if (Input.GetKeyDown (Key.D) && Player1Last != LastPressed.right) {
					Player1Last = LastPressed.right;
					totalcounter--;
				}
				if (Input.GetKeyDown (Key.LEFT) && Player2Last != LastPressed.left) {
					Player2Last = LastPressed.left;
					totalcounter++;
				}
				if (Input.GetKeyDown (Key.RIGHT) && Player2Last != LastPressed.right) {
					Player2Last = LastPressed.right;
					totalcounter++;
				}

				if (totalcounter <= -10) {
					StopEvent += _level.ButtonMashPlayer1Win;
					StopEvent ();
					StopEvent -= _level.ButtonMashPlayer1Win;
				
				}
				if (totalcounter >= 10) {
					StopEvent += _level.ButtonMashPlayer2Win;
					StopEvent ();
					StopEvent -= _level.ButtonMashPlayer2Win;
				
				}

				frame += 0.12f;

				SetFrames (0, 2);

				if (frame >= maxframe + 1) {
					frame = minframe;
				}
				if (frame < minframe) {
					frame = maxframe;
				}

				spriteArrows.SetFrame ((int)frame);
				spriteAD.SetFrame ((int)frame);
			
			
			
			}
		}

		public enum LastPressed{
			none,
			left,
			right
		}

	}
}

