using System;
using System.Collections.Generic;
using System.Xml;

namespace GXPEngine
{
	public class Highscores : GameObject
	{	
	
		public List<Score> scorelist;
		XmlReader reader;
		XmlWriter writer;
		XmlWriterSettings settings = new XmlWriterSettings();



		public Highscores ()
		{	


			scorelist = new List<Score>();

			Score s1 = new Score (){ SCORE = 0, NAME = "test" };
		
			for (int i = 0; i < 10; i++) {
				scorelist.Add (s1);
			}




			settings.Indent = true;
			//sort highscore list based on specified property
			scorelist.Sort((score2,score1) => score1.SCORE.CompareTo(score2.SCORE));
			Read ();

		}

		public void Read(){
			using (reader = XmlReader.Create ("highscores.xml")) {

				reader.ReadStartElement ("Highscores");
				reader.ReadStartElement ("Solo");

				for (int i = 0; i < 10; i++) {
					reader.ReadStartElement ("highscore");
					scorelist[i].SCORE = readInt ("Score", reader);
					scorelist[i].NAME = readString ("Name", reader);
					reader.ReadEndElement ();
				}
					

				reader.ReadEndElement ();
				reader.ReadEndElement ();

				//reader.Dispose ();
			}
		}

		public void Save(){
			using (writer = XmlWriter.Create ("highscores.xml", settings)) {
				//writer = XmlWriter.Create ("highscores.xml",settings);

				writer.WriteStartElement ("Highscores");
				writer.WriteStartElement ("Solo");

				for (int i = 0; i < 10; i++) {
					writer.WriteStartElement ("highscore");
					WriteScore (scorelist[i].SCORE, writer);
					WriteName (scorelist[i].NAME, writer);
					writer.WriteEndElement ();
				}

					
				writer.WriteEndElement ();
				writer.WriteEndElement ();

			}


			writer.WriteEndElement ();
			writer.WriteEndElement ();

			writer.Flush ();
			writer.Close ();

			writer.Dispose ();

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

