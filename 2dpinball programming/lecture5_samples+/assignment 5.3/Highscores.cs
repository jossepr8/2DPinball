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
			settings.Indent = true;

			Read ();
			//_game.SortScores ();

		}
		void ReadNames(){
			_game.namelist.Clear ();
			using (reader = XmlReader.Create ("names.xml")) {
				reader.ReadStartElement ("names");
				int amount = readInt ("amount", reader);
				for (int i = 0; i < amount; i++) {
					_game.namelist.Add (readString ("Name",reader));
				}
				reader.ReadEndElement ();
			}
			_game.SortNames ();
		}
		void SaveNames(){
			_game.SortNames ();
			using (writer = XmlWriter.Create ("names.xml",settings)) {
				writer.WriteStartElement ("names");
				WriteAmount (_game.namelist.Count,writer);
				for (int i = 0; i < _game.namelist.Count; i++) {
					WriteName (_game.namelist [i],writer);
				}
				writer.WriteEndElement ();
			}
		}

		public void Read(){
			ReadNames ();
			_game.scorelist.Clear ();
			_game.scorelistteam.Clear ();
			using (reader = XmlReader.Create ("highscores.xml")) {

				reader.ReadStartElement ("Highscores");
				reader.ReadStartElement ("Solo");

				for (int i = 0; i < 10; i++) {
					reader.ReadStartElement ("highscore");
					_game.scorelist.Add (new Score (readInt ("Score", reader), readString ("Name", reader)));
					reader.ReadEndElement ();
				}
					

				reader.ReadEndElement ();

				reader.ReadStartElement ("Team");

				for (int i = 0; i < 10; i++) {
					reader.ReadStartElement ("highscore");
					_game.scorelistteam.Add (new Score (readInt ("Score", reader), readString ("Name", reader)));
					reader.ReadEndElement ();
				}


				reader.ReadEndElement ();
				reader.ReadEndElement ();

				//reader.Dispose ();
			}
			_game.SortScores ();
		}

		public void Save(){
			SaveNames ();
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

				writer.WriteStartElement ("Team");

				for (int i = 0; i < 10; i++) {
					writer.WriteStartElement ("highscore");
					WriteScore (_game.scorelistteam [i].SCORE, writer);
					WriteName (_game.scorelistteam [i].NAME, writer);
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
		void WriteAmount (int value , XmlWriter writer)
		{
			writer.WriteStartElement ("amount");
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

