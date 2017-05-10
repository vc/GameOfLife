using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GameOfLife.Controls;

namespace GameOfLife
{
	public partial class CreateMapForm : Form
	{
		private GraphPaint _graph;
		private List<Point> _points;

		public List<Point> AlivePoints
		{
			get { return _points; }
			set
			{
				_points = value;
				_graph.UpdateList(_points);
			}
		}

		public CreateMapForm()
		{
			InitializeComponent();

			_graph = new GraphPaint();
			base.Controls.Add(_graph);

			_points = new List<Point>();
			_graph.UpdateList(_points);

			_graph.Dock = DockStyle.Fill;

			_graph.Scale = 8;

			_graph.MouseClick += Graph_MouseClick;
			_graph.MouseWheel += Graph_MouseWheel;


			_graph.MouseDown += Graph_MouseDown;
			_graph.MouseUp += Graph_MouseUp;
			_graph.MouseMove += Graph_MouseMove;
		}

		private bool? _isPainting;
		private void Graph_MouseDown(object sender, MouseEventArgs e)
		{
			var scale = _graph.Scale;
			var p = new Point((int)(e.X / scale), (int)(e.Y / scale));

			_isPainting = _points.Contains(p);
			if (_isPainting.Value)
				_points.Remove(p);
			else
				_points.Add(p);

			_graph.Invalidate();
		}

		private void Graph_MouseUp(object sender, MouseEventArgs e)
		{
			_isPainting = null;
		}

		private void Graph_MouseMove(object sender, MouseEventArgs e)
		{
			if (_isPainting.HasValue)
			{
				var scale = _graph.Scale;
				var p = new Point((int)(e.X / scale), (int)(e.Y / scale));

				if (!_isPainting.Value)
				{
					if (!_points.Contains(p))
					{
						_points.Add(p);
						_graph.Invalidate();
					}
				}
				else
				{
					_points.Remove(p);
					_graph.Invalidate();
				}

			}
		}

		private void Graph_MouseWheel(object sender, MouseEventArgs e)
		{
			var newScale = _graph.Scale + Math.Sign(e.Delta);
			if (newScale > 1 && newScale < 10)
			{
				_graph.Scale = newScale;
				_graph.Invalidate();
			}
		}

		private void Graph_MouseClick(object sender, MouseEventArgs e)
		{
			if (_isPainting.HasValue)
				return;

			var scale = _graph.Scale;
			var p = new Point((int)(e.X / scale), (int)(e.Y / scale));
			if (_points.Contains(p))
				_points.Remove(p);
			else
				_points.Add(p);

			_graph.Invalidate();
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void BtnClear_Click(object sender, EventArgs e)
		{
			_points.Clear();
			_graph.Invalidate();
		}

	}
}
