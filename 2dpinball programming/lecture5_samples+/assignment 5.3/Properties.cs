using System;
using System.Xml;
using GXPEngine;
using System.Drawing;

namespace GXPEngine
{
    public class Properties
    {
		static XmlReader reader;

		public static Vec2 BallGravity {
			get;
			set;
		}
		public static float BallSpeed {
			get;
			set;
		}
		public static float BallMaxSpeed {
			get;
			set;
		}
		public static float PaddleSpeed {
			get;
			set;
		}
		public static float PaddleStartAngle {
			get;
			set;
		}
		public static float PaddleMaxAngle {
			get;
			set;
		}
		public static float PaddleBounce {
			get;
			set;
		}
		public static float EnemyGravity {
			get;
			set;
		}

        public Properties()
        {

        }

        public static void Read()
        {
			reader = XmlReader.Create ("properties.xml");
			readxml();
			reader.Dispose ();
        }

		static float readfloat(string name){
			reader.ReadStartElement (name);
			float defloat = reader.ReadContentAsFloat ();
			reader.ReadEndElement ();
			return defloat;
		}

        static void readxml()
        {
            reader.ReadStartElement("Properties");

           	reader.ReadStartElement("Ball");
				BallGravity = new Vec2(0, readfloat ("Gravity"));
				BallSpeed = readfloat ("Speed");
				BallMaxSpeed = readfloat ("MaxSpeed");
          	reader.ReadEndElement();	//ball

			reader.ReadStartElement("Paddle");
				PaddleSpeed = readfloat ("Speed");
				PaddleBounce = readfloat ("Bounce");
				PaddleStartAngle = readfloat ("StartAngle");
				PaddleMaxAngle = readfloat ("MaxAngle");
			reader.ReadEndElement ();	//paddle

			reader.ReadStartElement("Enemy");
				EnemyGravity = readfloat ("Gravity");
			reader.ReadEndElement ();	//enemy

            reader.ReadEndElement();	//properties
        }








    }
}

