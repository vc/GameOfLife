using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace GameOfLife.Helpers
{
	public static class FileSerializer
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

			return new Size(w+1, h+1);
		}

		/// <summary>
		/// Load points from .lfe format
		/// </summary>
		/// <param name="fileName">File with point field, where '*' is alive point, and other character is empty point</param>
		/// <returns>List of points</returns>
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

		/// <summary>
		/// Save points to a file with .lfe format
		/// </summary>
		/// <param name="fileName">Full path to file</param>
		/// <param name="alivePoints">List of all points</param>
		public static void SavePoints(string fileName, List<Point> alivePoints)
		{
			var matrix = new List<KeyValuePair<int, int>>();
			var dimension = FindDimensions(alivePoints);

			foreach (var p in alivePoints)
			{
				matrix.Add(new KeyValuePair<int, int>(p.X, p.Y));
			}

			using (TextWriter tw = new StreamWriter(File.OpenWrite(fileName)))
			{
				for (int y = 0; y < dimension.Height; y++)
				{
					for (int x = 0; x < dimension.Width; x++)
					{
						tw.Write(matrix.Contains(new KeyValuePair<int, int>(x, y)) ? '*' : '.');
					}
					tw.Write(tw.NewLine);
				}
			}
		}
	}
}
