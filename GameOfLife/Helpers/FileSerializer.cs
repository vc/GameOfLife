using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace GameOfLife.Helpers
{
	public static class FileSerializer
	{

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

			return new Size(w+1, h+1);
		}

		public static List<Point> LoadPoints(string fileName)
		{
			var listAlive = new List<Point>();

			using (var tr = File.OpenText(fileName))
			{
				var y = 0;
				while (!tr.EndOfStream)
				{
					var line = tr.ReadLine();
					var chars = line.ToCharArray();
					var x = 0;
					foreach (var c in chars)
					{
						if (c == '*')
							listAlive.Add(new Point(x, y));
						x++;

					}
					y++;
				}
			}

			return listAlive;
		}
	}
}
