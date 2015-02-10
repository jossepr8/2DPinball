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
			Spawn ();
		}
		void Spawn(){
			for (int i = 0; i < currentwave.GetLength(0); i++) {
				for (int a = 0; a < currentwave.GetLength(1); a++) {
					if (currentwave [a, i] == 1) {
						Enemie enemie = new Enemie ();
						enemie.SetXY (i * 64, a * 64);
						_level.AddEnemie (enemie);
					}
				}
			}
		}

		public static void Read(){	//dont touch this
			using (XmlReader reader = XmlReader.Create ("waves.xml")) {
				reader.ReadStartElement ("Waves");
				for (int i = 0; i < 3; i++) {
					int[,] wave = new int[20, 20];
					reader.ReadStartElement ("Wave");
					reader.ReadStartElement ("count");
					enemiecountlist.Add(reader.ReadContentAsInt ());
					reader.ReadEndElement ();
					for (int a = 0; a < 20; a++) {
						reader.ReadStartElement ("row");
						string[] cols = reader.ReadContentAsString ().Split (',');
						for (int b = 0; b < 20; b++) {
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

