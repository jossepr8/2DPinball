using System;
using System.Xml;
using GXPEngine;
using System.Drawing;

namespace GXPEngine
{
    public class Properties
    {
		public static Vec2 Gravity {
			get;
			set;
		}

        public Properties()
        {

        }

        public void Read()
        {
            XmlReader reader = XmlReader.Create("properties.xml");
            ReadBall(reader);

			reader.Dispose ();
        }

        void ReadBall(XmlReader reader)
        {
            reader.ReadStartElement("Properties");
            reader.ReadStartElement("Ball");
            reader.ReadStartElement("Gravity");
            int g = reader.ReadContentAsInt();
            Gravity = new Vec2(0f, g);
            reader.ReadEndElement();	//gravity
            reader.ReadEndElement();	//ball
            reader.ReadEndElement();	//properties
        }








    }
}

