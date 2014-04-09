using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameOfLife.GameOfLife
{
	public class FireUpdateEventArgs : EventArgs
	{
		public ICollection<PointULong> Born { get; set; }

		public ICollection<PointULong> Dead { get; set; }

		public FireUpdateEventArgs(ICollection<PointULong> born, ICollection<PointULong> dead)
		{
			Born = born;
			Dead = dead;
		}
	}
}