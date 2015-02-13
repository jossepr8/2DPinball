using System;
using System.Collections.Generic;
using System.Xml;

namespace GXPEngine
{
	public class Highscores : GameObject
	{
		List<Score> scorelist;
		XmlReader reader;
		XmlWriter writer;
		XmlWriterSettings settings = new XmlWriterSettings();

		public Highscores ()
		{
			scorelist = new List<Score>();

			Score s1 = new Score (){ SCORE = 5, NAME = "adfdfdfdfaa" };
			Score s2 = new Score (){ SCORE = 1, NAME = "ddd" };
			Score s3 = new Score (){ SCORE = 7, NAME = "ccc" };
			scorelist.Add (s1);
			scorelist.Add (s2);
			scorelist.Add (s3);


			settings.Indent = true;
			//sort highscore list based on specified property
			scorelist.Sort((score2,score1) => score1.SCORE.CompareTo(score2.SCORE));
			Save ();
		}

		public void Read(){
			reader = XmlReader.Create ("highscores.xml");



			reader.Dispose ();
		}

		public void Save(){
			writer = XmlWriter.Create ("highscores.xml",settings);

			writer.WriteStartElement ("Highscores");
			writer.WriteStartElement ("Solo");

			foreach (Score score in scorelist) 
			{	

				writer.WriteStartElement ("highscore");
				WriteScore (score.SCORE, writer);
				WriteName (score.NAME, writer);
				writer.WriteEndElement ();
			}

			writer.WriteEndElement ();
			writer.WriteEndElement ();
			writer.Flush ();
			writer.Close ();
			writer.Dispose ();
		}



		public void WriteName (string value , XmlWriter writer)
		{
			writer.WriteStartElement ("Name");
			writer.WriteValue (value);
			writer.WriteEndElement	();
		}

		public void WriteScore (int value , XmlWriter writer)
		{
			writer.WriteStartElement ("Score");
			writer.WriteValue (value);
			writer.WriteEndElement	();
		}


	}
}

