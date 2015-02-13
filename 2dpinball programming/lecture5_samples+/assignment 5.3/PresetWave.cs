using System;

namespace GXPEngine
{
	public class PresetWave
	{
		public int[,] wave {
			get;
			set;
		}
		public float paddlewidth {
			get;
			set;
		}
		public float ballsize{
			get;
			set;
		}
		public string startmessage {
			get;
			set;
		}
		public string endmessage {
			get;
			set;
		}

		public PresetWave ()
		{
		}
	}
}

