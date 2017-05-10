using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using GameOfLife.GameOfLife;

namespace GameOfLife.Helpers
{
	public static class Helper
	{
		/// <summary>
		/// Find dimension on all points in array
		/// </summary>
		/// <param name="points">Points</param>
		/// <returns>Dimension of points region</returns>
		public static Size FindDimensions(List<Point> points)
		{
			var w = 0;
			var h = 0;
			foreach (var point in points)
			{
				if (point.X > w)
					w = point.X;
				if (point.Y > h)
					h = point.Y;
			}

			return new Size(w + 1, h + 1);
		}

		public static PointULong FindOffset(IEnumerable<PointULong> coll)
		{
			if (!coll.Any())
				return new PointULong();

			var minPoint = coll.First();
			foreach (var p in coll)
			{
				if (p.X < minPoint.X)
					minPoint.X = p.X;
				if (p.Y < minPoint.Y)
					minPoint.Y = p.Y;
			}

			return minPoint;
		}

		public static List<Point> GetMap(IEnumerable<PointULong> points)
		{
			var offset = FindOffset(points);

			return points.Select(p => p - offset).ToList();
		}

		public static void SavePointsToClip(IDictionary<PointULong, CellFromCollection> collection)
		{
			var points = GetMap(collection.Keys);
			var offset = FindOffset(collection.Keys);
			var dimension = FindDimensions(points);
			var pointsColl = collection.ToDictionary(i => i.Key - offset, i => i.Value);

			using (var ms = new MemoryStream())
			{
				var tw = new StreamWriter(ms);
				for (int y = 0; y < dimension.Height; y++)
				{
					for (int x = 0; x < dimension.Width; x++)
					{
						var p = new Point(x, y);
						var chr = points.Contains(p)
							//? pointsColl[p].IsAlive ? '*' : '.'
							? pointsColl[p].AliveCountAroundMe.ToString()[0]
							: ' ';

						tw.Write(chr);
					}
					tw.Write(tw.NewLine);
				}
				tw.Flush();
				ms.Seek(0, SeekOrigin.Begin);
				var r = new StreamReader(ms);

				var t = new Thread(() => Clipboard.SetData(DataFormats.Text, r.ReadToEnd()));
				t.SetApartmentState(ApartmentState.STA);
				t.Start();
				t.Join();

			}
		}

		public static Point GetMyCoords(IDictionary<PointULong, CellFromCollection> collection, CellFromCollection cell)
		{
			var offset = FindOffset(collection.Keys);
			return cell.Location - offset;
		}
	}
}
