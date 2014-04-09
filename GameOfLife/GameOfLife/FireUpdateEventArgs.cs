using System;
using System.Collections.Generic;

namespace GameOfLife.GameOfLife
{
	public class FireUpdateEventArgs : EventArgs
	{
		public ICollection<PointULong> Alive { get; set; }

		public FireUpdateEventArgs(ICollection<PointULong> alive)
		{
			Alive = alive;
		}
	}
}