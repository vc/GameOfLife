using System.Drawing;
using GameOfLife.GameOfLife;

namespace GameOfLife.Helpers
{
    /// <summary>
    /// Adapter of view area in all ulong range
    /// </summary>
    public class ScreenLocation
    {
        private Point _offsetView;
        private PointULong _absoluteView;
        private float _scale;

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

        public float Scale
        {
            get { return _scale; }
            set
            {
                if (_scale != 0 && (MousePoint.X != 0 || MousePoint.Y != 0))
                {
                    var p = new Point(
                        (int)(MousePoint.X / value) - (int)(MousePoint.X / _scale),
                        (int)(MousePoint.Y / value) - (int)(MousePoint.Y / _scale)
                    );
                    
                    _offsetView = new Point(_offsetView.X - p.X, _offsetView.Y - p.Y);                    
                }
                _scale = value;
            }
        }

        public Point MousePoint { get; set; }

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
