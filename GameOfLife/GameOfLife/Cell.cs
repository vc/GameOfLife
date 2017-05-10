using System.Collections.Generic;
using System.Drawing;

namespace GameOfLife.GameOfLife
{
	public class Cell
	{
		protected PointULong _location;

		public PointULong Location
		{
			get { return _location; }
			set { _location = value; }
		}

		public Cell(PointULong location)
		{
			Location = location;
		}

		public List<PointULong> AroundCellsCoords
		{
			get
			{
				var ret = new List<PointULong>(8);
				for (int i = 0; i < 8; i++)
				{
					var posNeigh = GetNeighborsCoord(i);
					ret.Add(posNeigh);
				}

				return ret;
			}
		}

		private PointULong GetNeighborsCoord(int pos)
		{
			return Location + NeighborsPos[pos];
		}

		private static readonly Point[] NeighborsPos =
			{
				new Point(-1, -1),	//LT
				new Point(0, -1),	//T
				new Point(1, -1),	//RT
				new Point(-1, 0),	//L
				new Point(1, 0),	//R
				new Point(-1, 1),	//LB
				new Point(0, 1),	//B
				new Point(1, 1),	//RB
			};

	}
}
