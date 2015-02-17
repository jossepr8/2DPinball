using System;
using System.Collections.Generic;
using System.Xml;

namespace GXPEngine
{
	public class Highscores : GameObject
	{	
	

		XmlReader reader;
		XmlWriter writer;
		XmlWriterSettings settings = new XmlWriterSettings();
		MyGame _game;


		public Highscores (MyGame game)
		{	

			_game = game;


			//Score s1 = new Score (){ SCORE = 0, NAME = "test" };
		





			settings.Indent = true;
			//sort highscore list based on specified property

			Read ();
			_game.SortScores ();

		}

		public void Read(){
			_game.scorelist.Clear ();
			using (reader = XmlReader.Create ("highscores.xml")) {

				reader.ReadStartElement ("Highscores");
				reader.ReadStartElement ("Solo");

				for (int i = 0; i < 10; i++) {
					reader.ReadStartElement ("highscore");
					_game.scorelist.Add (new Score (readInt ("Score", reader), readString ("Name", reader)));
					reader.ReadEndElement ();
				}
					

				reader.ReadEndElement ();
				reader.ReadEndElement ();

				//reader.Dispose ();
			}
		}

		public void Save(){
			_game.SortScores ();
			using (writer = XmlWriter.Create ("highscores.xml", settings)) {
				//writer = XmlWriter.Create ("highscores.xml",settings);

				writer.WriteStartElement ("Highscores");
				writer.WriteStartElement ("Solo");

				for (int i = 0; i < 10; i++) {
					writer.WriteStartElement ("highscore");
					WriteScore (_game.scorelist [i].SCORE, writer);
					WriteName (_game.scorelist [i].NAME, writer);
					writer.WriteEndElement ();
				}
					
				writer.WriteEndElement ();
				writer.WriteEndElement ();

			}
		}



		void WriteName (string value , XmlWriter writer)
		{
			writer.WriteStartElement ("Name");
			writer.WriteValue (value);
			writer.WriteEndElement	();
		}

		void WriteScore (int value , XmlWriter writer)
		{
			writer.WriteStartElement ("Score");
			writer.WriteValue (value);
			writer.WriteEndElement	();
		}

		float readFloat(string name, XmlReader reader)
		{
			reader.ReadStartElement (name);
			float defloat = reader.ReadContentAsFloat ();
			reader.ReadEndElement ();
			return defloat;
		}

		string readString(string name, XmlReader reader)
		{
			reader.ReadStartElement (name);
			string destring = reader.ReadContentAsString ();
			reader.ReadEndElement ();
			return destring;
		}

		int readInt(string name, XmlReader reader)
		{
			reader.ReadStartElement (name);
			int value = reader.ReadContentAsInt ();
			reader.ReadEndElement ();
			return value;
		}




	}
}

