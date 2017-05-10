using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace GameOfLife.Helpers
{
	public static class FileSerializer
	{
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
			using (var fs = File.OpenWrite(fileName))
				SavePoints(fs, alivePoints);

		}

		/// <summary>
		/// Save point to a stream
		/// </summary>
		/// <param name="stream">Stream to write</param>
		/// <param name="points">Points to write</param>
		public static void SavePoints(Stream stream, List<Point> points)
		{
			var dimension = Helper.FindDimensions(points);
			var matrix = points.Select(p => new KeyValuePair<int, int>(p.X, p.Y)).ToList();

			TextWriter tw = new StreamWriter(stream);
			for (int y = 0; y < dimension.Height; y++)
			{
				for (int x = 0; x < dimension.Width; x++)
					tw.Write(matrix.Contains(new KeyValuePair<int, int>(x, y)) ? '*' : '.');
				
				tw.Write(tw.NewLine);
			}
			tw.Flush();
		}
	}
}
