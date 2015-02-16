using System;
using System.Collections.Generic;

namespace GXPEngine
{
	public class StuckManager
	{
		List<Vec2> positionlist = new List<Vec2>();
		Ball _ball;

		public StuckManager (Ball ball)
		{
			_ball = ball;
		}

		public void Step(){
			positionlist.Add (_ball.position);
			//Console.WriteLine (positionlist.Count);

		}

	}
}

