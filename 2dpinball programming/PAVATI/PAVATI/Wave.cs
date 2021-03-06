﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace GXPEngine
{
	public class Wave : GameObject
	{
		Level _level;
		Menu _menu;
		MyGame _game;


		static List<int[,]> wavelist = new List<int[,]>();
		static List<PresetWave> preset_wavelist = new List<PresetWave> ();
		static List<PresetWave> random_wavelist = new List<PresetWave> ();
		static List<int> enemycountlist = new List<int> ();
		static XmlReader reader;
		static Random rnd;
		Message message;
		int currentwave = 0;
		PresetWave lastwave;

		public Wave (Level level)
		{
			rnd = new Random ();
			_level = level;
			_game = _level.GetGame ();
		}
		public Wave (Menu menu)
		{
			rnd = new Random ();
			_menu = menu;
		}


		public void Step(bool menu = false){
			if (!menu) {
				if (_level.enemylist.Count == 0 && _level.GetGame ()._state == States.Level) {
					if (currentwave < preset_wavelist.Count) {
						SpawnPresetWave (preset_wavelist [currentwave]);
						currentwave++;
					} else {
						int number = rnd.Next (0, random_wavelist.Count);
						if (random_wavelist [number] == lastwave) {
							return;
						}
						SpawnPresetWave (random_wavelist [number]);
						currentwave++;
					}
				}
				message.Step ();
				foreach (Enemy e in _level.enemylist) {
					e.Step ();
				}
			} else {
				if (_menu.enemylist.Count == 0 && _menu.GetGame ()._state == States.MainMenu) {
						int number = rnd.Next (0, random_wavelist.Count);
						if (random_wavelist [number] == lastwave) {
							return;
						}
						SpawnPresetWaveMenu (random_wavelist [number]);
						currentwave++;

				}
				message.Step ();
				foreach (Enemy e in _menu.enemylist) {
					e.Step ();
				}
			}
		}

		//not used atm
		void SpawnWave(int[,] wave){
			_level._player1.scaleX = 1;
			_level._player2.scaleX = 1;
			_level._ball.SetScaleXY (1, 1);
			for (int i = 0; i < wave.GetLength(0); i++) {
				for (int a = 0; a < wave.GetLength(1); a++) {
					if (wave [a, i] == 1) {
						Enemy enemy = new Enemy ();
						float WIDTH = _level.GetWidth () / 3 * 2 - _level.GetWidth () / 3;
						enemy.SetXY (WIDTH/10 * i + WIDTH , a * 64 - 10 * 64);
						_level.Addenemy (enemy);
						enemy.speed += currentwave;
					}
				}
			}
		}
		void SpawnPresetWave(PresetWave presetwave){
			lastwave = presetwave;
			SoundManager.StopMusic ();
			message = new Message (_game.currentFps, presetwave.startmessage, presetwave.messagetimer);
			message.SetXY (_level.GetWidth()/2 - message.size.Width/2 , 150);
			_level.AddChild (message);
			_level._player1.scaleX = presetwave.paddlewidth / 100;
			_level._player2.scaleX = presetwave.paddlewidth / 100;
			_level._ball.SetScaleXY (presetwave.ballsize/100, presetwave.ballsize/100);
			SoundManager.Playmusic (presetwave.wavemusic);
			int[,] wave = presetwave.wave;
			for (int i = 0; i < wave.GetLength(0); i++) {
				for (int a = 0; a < wave.GetLength(1); a++) {
					Enemy enemy;
					float WIDTH;
					switch (wave [a, i]) {
					case 1:
						enemy = new Enemy (){ speed = presetwave.enemygravity };
						WIDTH = _level.GetWidth () / 3 * 2 - _level.GetWidth () / 3;
						enemy.SetXY (WIDTH / 10 * i + WIDTH, a * 64 - 10 * 64);
						_level.Addenemy (enemy);
						break;
					case 2:
						enemy = new Enemy (Types.Green){ speed = presetwave.enemygravity };
						WIDTH = _level.GetWidth () / 3 * 2 - _level.GetWidth () / 3;
						enemy.SetXY (WIDTH / 10 * i + WIDTH, a * 64 - 10 * 64);
						_level.Addenemy (enemy);
						break;
					case 3:
						enemy = new Enemy (Types.Purple){ speed = presetwave.enemygravity };
						WIDTH = _level.GetWidth () / 3 * 2 - _level.GetWidth () / 3;
						enemy.SetXY (WIDTH / 10 * i + WIDTH, a * 64 - 10 * 64);
						_level.Addenemy (enemy);
						break;
					case 4:
						enemy = new Enemy (Types.Yellow){ speed = presetwave.enemygravity };
						WIDTH = _level.GetWidth () / 3 * 2 - _level.GetWidth () / 3;
						enemy.SetXY (WIDTH / 10 * i + WIDTH, a * 64 - 10 * 64);
						_level.Addenemy (enemy);
						break;
					}
				}
			}
			_level.FixHud ();
		}

		void SpawnPresetWaveMenu(PresetWave presetwave){
			lastwave = presetwave;
			message = new Message (60, presetwave.startmessage, presetwave.messagetimer);
			message.SetXY (_menu.GetGame().width/2 - message.size.Width/2 , 150);
			int[,] wave = presetwave.wave;
			for (int i = 0; i < wave.GetLength(0); i++) {
				for (int a = 0; a < wave.GetLength(1); a++) {
					Enemy enemy;
					float WIDTH;
					switch (wave [a, i]) {
					case 1:
						enemy = new Enemy (){ speed = presetwave.enemygravity };
						WIDTH = _menu.GetGame().width / 3 * 2 - _menu.GetGame().width / 3;
						enemy.SetXY (WIDTH / 10 * i + WIDTH, a * 64 - 10 * 64);
						_menu.Addenemy (enemy);
						break;
					case 2:
						enemy = new Enemy (Types.Green){ speed = presetwave.enemygravity };
						WIDTH = _menu.GetGame().width / 3 * 2 - _menu.GetGame().width / 3;
						enemy.SetXY (WIDTH / 10 * i + WIDTH, a * 64 - 10 * 64);
						_menu.Addenemy (enemy);
						break;
					case 3:
						enemy = new Enemy (Types.Purple){ speed = presetwave.enemygravity };
						WIDTH = _menu.GetGame().width / 3 * 2 - _menu.GetGame().width / 3;
						enemy.SetXY (WIDTH / 10 * i + WIDTH, a * 64 - 10 * 64);
						_menu.Addenemy (enemy);
						break;
					case 4:
						enemy = new Enemy (Types.Yellow){ speed = presetwave.enemygravity };
						WIDTH = _menu.GetGame().width / 3 * 2 - _menu.GetGame().width / 3;
						enemy.SetXY (WIDTH / 10 * i + WIDTH, a * 64 - 10 * 64);
						_menu.Addenemy (enemy);
						break;
					}
				}
			}
			_menu.UpdateButtons ();
		}


		static float readfloat(string xmlvariablename){
			reader.ReadStartElement (xmlvariablename);
			float defloat = reader.ReadContentAsFloat ();
			reader.ReadEndElement ();
			return defloat;
		}
		static string readstring(string xmlvariablename){
			reader.ReadStartElement (xmlvariablename);
			string destring = reader.ReadContentAsString ();
			reader.ReadEndElement ();
			return destring;
		}

		public static void Read(){

			//------ first few waves-------------------------------------
			using (reader = XmlReader.Create ("preset_waves.xml")) 
			{
				reader.ReadStartElement ("preset_waves");
				reader.ReadStartElement ("Config");
				reader.ReadStartElement ("number_of_waves");
				int numberofwaves = reader.ReadContentAsInt ();
				reader.ReadEndElement ();	//number of waves
				reader.ReadEndElement ();	//config
				reader.ReadStartElement ("Waves");
				for (int i = 0; i < numberofwaves; i++) {
					PresetWave presetwave = new PresetWave ();
					presetwave.wave = new int[10, 10];
					reader.ReadStartElement ("Wave");
					presetwave.startmessage = Wave.readstring ("start_message");
					presetwave.messagetimer = Wave.readfloat ("messagetimer");
					presetwave.paddlewidth = Wave.readfloat ("paddlewidth");
					presetwave.ballsize = Wave.readfloat ("ballsize");
					presetwave.enemygravity = Wave.readfloat ("enemygravity");
					presetwave.wavemusic = Wave.readstring ("wavemusic");
					for (int a = 0; a < 10; a++) {
						reader.ReadStartElement ("row");
						string[] cols = reader.ReadContentAsString ().Split (',');
						for (int b = 0; b < 10; b++) {
							presetwave.wave [a, b] = int.Parse (cols [b]);
						}
						reader.ReadEndElement ();	//row
					}
					reader.ReadEndElement ();	//wave
					preset_wavelist.Add (presetwave);
				}
				reader.ReadEndElement ();	//waves
				reader.ReadEndElement ();	//preset_waves
			}
				


			using (reader = XmlReader.Create ("random_waves.xml")) 
			{
				reader.ReadStartElement ("preset_waves");
				reader.ReadStartElement ("Config");
				reader.ReadStartElement ("number_of_waves");
				int numberofwaves = reader.ReadContentAsInt ();
				reader.ReadEndElement ();	//number of waves
				reader.ReadEndElement ();	//config
				reader.ReadStartElement ("Waves");
				for (int i = 0; i < numberofwaves; i++) {
					PresetWave presetwave = new PresetWave ();
					presetwave.wave = new int[10, 10];
					reader.ReadStartElement ("Wave");
					presetwave.startmessage = Wave.readstring ("start_message");
					presetwave.messagetimer = Wave.readfloat ("messagetimer");
					presetwave.paddlewidth = Wave.readfloat ("paddlewidth");
					presetwave.ballsize = Wave.readfloat ("ballsize");
					presetwave.enemygravity = Wave.readfloat ("enemygravity");
					presetwave.wavemusic = Wave.readstring ("wavemusic");
					for (int a = 0; a < 10; a++) {
						reader.ReadStartElement ("row");
						string[] cols = reader.ReadContentAsString ().Split (',');
						for (int b = 0; b < 10; b++) {
							presetwave.wave [a, b] = int.Parse (cols [b]);
						}
						reader.ReadEndElement ();	//row
					}
					reader.ReadEndElement ();	//wave
					random_wavelist.Add (presetwave);
				}
				reader.ReadEndElement ();	//waves
				reader.ReadEndElement ();	//preset_waves
			}
	
		}
	}
}