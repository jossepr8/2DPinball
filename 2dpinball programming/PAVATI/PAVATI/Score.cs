﻿using System;

namespace GXPEngine
{
	public class Score
	{
		public int SCORE{
			get;
			set;
		}
		public string NAME {
			get;
			set;
		}
		public Score (int score, string name)
		{
			SCORE = score;
			NAME = name;
		}
	}
}

