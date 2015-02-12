using System;
using System.Collections.Generic;
using System.Xml;

namespace GXPEngine
{
	public class Highscores : GameObject
	{
		List<Score> scorelist;
		XmlReader reader;

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
			reader = XmlReader.Create ("highscores.xml");



			reader.Dispose ();
		}
	}
}

