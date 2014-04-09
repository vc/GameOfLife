using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameOfLife.GameOfLife
{
	public class Cell
	{
		public int Age { get; set; }
		public PointULong Location { get; set; }

		public Cell(PointULong location)
		{
			Location = location;
		}

		public bool IsNearMe(Cell otherCell)
		{
			return otherCell.Location.X >= Location.X - 1 && otherCell.Location.X <= Location.X + 1
				   && otherCell.Location.Y >= Location.Y - 1 && otherCell.Location.Y <= Location.Y + 1
				   && otherCell.Location != Location;
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

		protected PointULong? GetNeighborsCoord(NeighborsPositions pos)
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
	}
}
