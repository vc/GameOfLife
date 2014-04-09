using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameOfLife.GameOfLife
{
	public class Cell
	{
		public PointULong Location
		{
			get { return _location; }
			set { _location = value; }
		}

		public Cell(PointULong location)
		{
			Location = location;
		}

		public bool IsNearMe(Cell otherCell)
		{
			var otherLoc = otherCell._location;
			return otherLoc.X >= _location.X - 1 && otherLoc.X <= _location.X + 1
				   && otherLoc.Y >= _location.Y - 1 && otherLoc.Y <= _location.Y + 1
				   && otherLoc != _location;
		}

		public List<PointULong> AroundCellsCoords
		{
			get
			{
				var ret = new List<PointULong>(8);
				for (int i = 0; i < 8; i++)
				{
					var posNeigh = GetNeighborsCoord((NeighborsPositions)i);
					if (posNeigh != null)
						ret.Add(posNeigh.Value);
				}

				return ret;
			}
		}

		private PointULong? GetNeighborsCoord(NeighborsPositions pos)
		{
			try
			{
				return Location - NeighborsPos[(int)pos];
			}
			catch (Exception)
			{
				return null;
			}
		}

		public enum NeighborsPositions
		{
			TopLeft = 0,
			Top = 1,
			TopRight = 2,
			Left = 3,
			Right = 4,
			BottomLeft = 5,
			Bottom = 6,
			BottomRight = 7
		}

		private static readonly Point[] NeighborsPos =
			{
				new Point(-1, -1),
				new Point(0, -1),
				new Point(1, -1),
				new Point(-1, 0),
				new Point(1, 0),
				new Point(-1, 1),
				new Point(1, 0),
				new Point(1, 1),
			};

		private PointULong _location;
	}
}
