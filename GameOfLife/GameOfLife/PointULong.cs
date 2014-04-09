
using System.Diagnostics;
using System.Drawing;

namespace GameOfLife.GameOfLife
{
	[DebuggerDisplay("X:{X} Y:{Y}")]
	public struct PointULong
	{
		public static PointULong operator -(PointULong p, Point p1)
		{
			return new PointULong(p.X - (ulong)p1.X, p.Y - (ulong)p1.Y);
		}

		public static Point operator -(PointULong p1, PointULong p2)
		{
			return new Point((int)(p1.X - p2.X), (int)(p1.Y - p2.Y));
		}

		public static PointULong operator +(PointULong p1, PointULong p2)
		{
			return new PointULong(p1.X + p2.X, p1.Y + p2.Y);
		}

		public static PointULong operator +(PointULong p1, Point p2)
		{
			return new PointULong(p1.X + (ulong)p2.X, p1.Y + (ulong)p2.Y);
		}

		public static bool operator !=(PointULong p1, PointULong p2)
		{
			return !(p1 == p2);
		}
		public static bool operator ==(PointULong p1, PointULong p2)
		{
			return p1.X == p2.X && p1.Y == p2.Y;
		}

		public ulong X;
		public ulong Y;

		public PointULong(ulong x, ulong y)
		{
			X = x;
			Y = y;
		}
	}
}
