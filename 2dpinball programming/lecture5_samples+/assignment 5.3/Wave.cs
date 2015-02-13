using System;
using System.Collections.Generic;
using System.Xml;

namespace GXPEngine
{
	public class Wave : GameObject
	{
		Level _level;

		static List<int[,]> wavelist = new List<int[,]>();
		static List<PresetWave> preset_wavelist = new List<PresetWave> ();
		static List<PresetWave> preset_wavelistCopy = new List<PresetWave> ();
		static List<int> enemycountlist = new List<int> ();
		static XmlReader reader;
		static Random rnd;

		public Wave (Level level)
		{
			preset_wavelistCopy = preset_wavelist;
			rnd = new Random ();
			_level = level;
			SpawnPresetWave (preset_wavelistCopy [0]);

		}

		void Update(){
			if (_level.GetEnemyList ().Count == 0) {
				if (preset_wavelistCopy.Count > 0) 
				{
					preset_wavelistCopy.Remove (preset_wavelistCopy [0]);
				}
					if (preset_wavelistCopy.Count > 0) 
				{
					SpawnPresetWave (preset_wavelistCopy [0]);
				} else {
					SpawnWave(wavelist[rnd.Next(0,wavelist.Count)]);
				}
			}
		}

		void SpawnWave(int[,] wave){
			for (int i = 0; i < wave.GetLength(0); i++) {
				for (int a = 0; a < wave.GetLength(1); a++) {
					if (wave [a, i] == 1) {
						Enemy enemy = new Enemy ();
						float WIDTH = _level.GetWidth () / 3 * 2 - _level.GetWidth () / 3;
						enemy.SetXY (WIDTH/10 * i + WIDTH , a * 64 - 10 * 64);
						_level.Addenemy (enemy);
					}
				}
			}
		}
		void SpawnPresetWave(PresetWave presetwave){
			_level.AddChild (new Message (presetwave.startmessage, 100));

			int[,] wave = presetwave.wave;
			for (int i = 0; i < wave.GetLength(0); i++) {
				for (int a = 0; a < wave.GetLength(1); a++) {
					if (wave [a, i] == 1) {
						Enemy enemy = new Enemy ();
						float WIDTH = _level.GetWidth () / 3 * 2 - _level.GetWidth () / 3;
						enemy.SetXY (WIDTH/10 * i + WIDTH , a * 64 - 10 * 64);
						_level.Addenemy (enemy);
					}
				}
			}
			//_level.DrawMessage (presetwave.endmessage);
		}

		static float readfloat(string name){
			reader.ReadStartElement (name);
			float defloat = reader.ReadContentAsFloat ();
			reader.ReadEndElement ();
			return defloat;
		}

		static string readstring(string name){
			reader.ReadStartElement (name);
			string destring = reader.ReadContentAsString ();
			reader.ReadEndElement ();
			return destring;
		}

		public static void Read(){	//dont touch this

			//------ first few waves-------------------------------------
			reader = XmlReader.Create ("preset_waves.xml");
			
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
					presetwave.endmessage = Wave.readstring ("end_message");
					presetwave.paddlewidth = Wave.readfloat ("paddlewidth");
					presetwave.ballsize = Wave.readfloat ("ballsize");
					for (int a = 0; a < 10; a++) {
						reader.ReadStartElement ("row");
						string[] cols = reader.ReadContentAsString ().Split (',');
						for (int b = 0; b < 10; b++) {
							presetwave.wave [a, b] = int.Parse(cols [b]);
						}
						reader.ReadEndElement ();	//row
					}
					reader.ReadEndElement ();	//wave
					preset_wavelist.Add (presetwave);
				}
				reader.ReadEndElement ();	//waves
				reader.ReadEndElement ();	//preset_waves
			reader.Dispose();

			//-------------------------------------------------------------


			//-----------all random waves----------------------------------
			reader = XmlReader.Create ("waves.xml");
			
				reader.ReadStartElement ("Waves");
				for (int i = 0; i < 19; i++) {
					int[,] wave = new int[10, 10];
					reader.ReadStartElement ("Wave");
					reader.ReadStartElement ("count");
					enemycountlist.Add(reader.ReadContentAsInt ());
					reader.ReadEndElement ();
					for (int a = 0; a < 10; a++) {
						reader.ReadStartElement ("row");
						string[] cols = reader.ReadContentAsString ().Split (',');
						for (int b = 0; b < 10; b++) {
							wave [a, b] = int.Parse(cols [b]);
						}
						reader.ReadEndElement ();
					}
					reader.ReadEndElement ();
					wavelist.Add (wave);
				}
				reader.ReadEndElement ();
			reader.Dispose();
			//---------------------------------------------------------------
		}
	}
}