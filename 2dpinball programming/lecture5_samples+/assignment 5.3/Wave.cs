using System;
using System.Collections.Generic;
using System.Xml;

namespace GXPEngine
{
	public class Wave : GameObject
	{
		int[,] currentwave;
		Level _level;

		static List<int[,]> wavelist = new List<int[,]>();
		static List<int> enemiecountlist = new List<int> ();

		public Wave (Level level)
		{
			_level = level;
			currentwave = wavelist[Utils.Random(0,wavelist.Count)];	//get a random wave
			//currentwave = wavelist [0];
			Spawn ();
		}
		void Spawn(){
			for (int i = 0; i < currentwave.GetLength(0); i++) {
				for (int a = 0; a < currentwave.GetLength(1); a++) {
					if (currentwave [a, i] == 1) {
						Enemie enemie = new Enemie ();
						float WIDTH = _level.GetWidth () / 3 * 2 - _level.GetWidth () / 3;
						enemie.SetXY (WIDTH/10 * i + WIDTH , a * 64 - 10 * 64);
						_level.AddEnemie (enemie);
					}
				}
			}
		}

		public static void Read(){	//dont touch this
			using (XmlReader reader = XmlReader.Create ("waves.xml")) {
				reader.ReadStartElement ("Waves");
				for (int i = 0; i < 19; i++) {
					int[,] wave = new int[10, 10];
					reader.ReadStartElement ("Wave");
					reader.ReadStartElement ("count");
					enemiecountlist.Add(reader.ReadContentAsInt ());
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

			}
		}



	}
}

