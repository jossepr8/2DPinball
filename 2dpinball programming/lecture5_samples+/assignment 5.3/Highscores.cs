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

		public Highscores ()
		{
			scorelist = new List<Score>();
			/*	test
			Score s1 = new Score (){ SCORE = 5, NAME = "aaa" };
			Score s2 = new Score (){ SCORE = 1, NAME = "ddd" };
			Score s3 = new Score (){ SCORE = 7, NAME = "ccc" };
			scorelist.Add (s1);
			scorelist.Add (s2);
			scorelist.Add (s3);
			*/	

			//sort highscore list based on specified property
			scorelist.Sort((score1,score2) => score1.SCORE.CompareTo(score2.SCORE));

		}

		public void Read(){
			reader = XmlReader.Create ("highscores.xml");



			reader.Dispose ();
		}

		public void Save(){
			scorelist.Sort((score1,score2) => score1.SCORE.CompareTo(score2.SCORE));

			writer = XmlWriter.Create ("highscxores.xml");
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
			reader.Dispose ();
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

