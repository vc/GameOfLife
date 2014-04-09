using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GameOfLife.Controls
{
	public partial class GraphPaint : UserControl
	{
		#region Fields
		private Color _backColor = Color.White;
		private readonly Color _foreColor = Color.Red;
		private readonly Color _deadColor = Color.LightBlue;

		private IEnumerable<Point> _alivePoints;
		private IEnumerable<Point> _deadPoints;
		private readonly Pen _penAlive;
		private readonly Pen _penDead;

		#endregion

		#region Properties
		public float Scale { get; set; }
		#endregion

		#region Constructor
		public GraphPaint()
		{
			Scale = 1;
			Resize += (o, e) => Invalidate();
			Paint += Graph_Paint;

			InitializeComponent();
			_penAlive = new Pen(_foreColor);
			_penDead = new Pen(_deadColor);
		}
		#endregion

		#region Public Methods
		public void UpdateList(IEnumerable<Point> visibleBorn, IEnumerable<Point> visibleDead)
		{
			_alivePoints = visibleBorn;
			_deadPoints = visibleDead;
		}
		#endregion

		#region Private Methods
		private void Graph_Paint(object sender, PaintEventArgs e)
		{
			var g = e.Graphics;
			if (_alivePoints != null)
			{
				g.SmoothingMode = SmoothingMode.None;

				var scale = Scale;
				/*foreach (var p in _deadPoints)
					g.DrawRectangle(_penDead, p.X * scale, p.Y * scale, scale, scale); */

				foreach (var p in _alivePoints)
					g.DrawRectangle(_penAlive, p.X * scale, p.Y * scale, scale, scale);
			}
		}

		#endregion
	}
}
