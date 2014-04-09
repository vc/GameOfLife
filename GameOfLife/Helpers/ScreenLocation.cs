using System.Drawing;
using GameOfLife.GameOfLife;

namespace GameOfLife
{
	public class ScreenLocation
	{
		private Point _offsetView;
		private PointULong _absoluteView;

		public ScreenLocation(PointULong absoluteView)
		{
			_absoluteView = absoluteView;
			Scale = 1;
		}

		public Point OffsetView
		{
			get { return _offsetView; }
			set { _offsetView = value; }
		}

		public int ScreenWidth { get; set; }
		public int ScreenHeight { get; set; }

		public PointULong AbsoluteLocation
		{
			get { return _absoluteView + _offsetView; }
		}

		public void Reset(PointULong absoluteView)
		{
			_absoluteView = absoluteView;
			_offsetView = new Point();
		}

		public void IncOffsetX(int offsetX)
		{
			_offsetView = new Point((int)(_offsetView.X + offsetX / Scale), _offsetView.Y);
		}

		public void IncOffsetY(int offsetY)
		{
			_offsetView = new Point(_offsetView.X, (int)(_offsetView.Y + offsetY / Scale));
		}

		public float Scale { get; set; }

		public Point Dimension { get { return new Point((int)(ScreenWidth / Scale), (int)(ScreenHeight / Scale)); } }

		public PointULong RightBottom
		{
			get { return AbsoluteLocation + Dimension; }
		}


		public Point? GetVisiblePoint(PointULong point, bool nullIfUnvisible)
		{
			var windowPoint = point - AbsoluteLocation;
			var dimension = Dimension;

			var isInVisibleArea = windowPoint.X >= 0 && windowPoint.Y >= 0
				&& windowPoint.X <= dimension.X && windowPoint.Y <= dimension.Y;

			if (isInVisibleArea || !nullIfUnvisible)
				return windowPoint;

			return null;
		}
	}
}
