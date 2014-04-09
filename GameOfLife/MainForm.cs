using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using GameOfLife.Controls;
using GameOfLife.GameOfLife;
using GameOfLife.Helpers;

namespace GameOfLife
{
	public sealed partial class MainForm : Form
	{
		#region Fields
		private GameOfLifeClass _gol;
		private Thread _workerThread;

		private bool _vscrollProceed;
		private readonly GraphPaint _graph;

		private bool _hscrollProceed;
		private Point? _mouseDownPoint;
		private Point _offsetOnMouseDown;
		private readonly object _mouseMovementLockObj = new object();
		private bool _isStarted = false;
		private readonly ScreenLocation _currentView;

		private string _lastFileName;
		#endregion

		#region Constructor
		public MainForm()
		{
			_currentView = new ScreenLocation(new PointULong(ulong.MaxValue / 2, ulong.MaxValue / 2));

			_graph = new GraphPaint();
			_graph.MouseDown += Graph_MouseDown;
			_graph.MouseMove += Graph_MouseMove;
			_graph.MouseUp += Graph_MouseUp;
			_graph.MouseWheel += Graph_MouseWheel;

			base.Controls.Add(_graph);

			InitializeComponent();

			Initialize();
		}

		#endregion

		#region Private Methods

		private void Initialize()
		{
			_gol = new GameOfLifeClass();

			UpdateSize();
			UpdateScale(trbScale.Value);
			UpdateSpeed();

			_gol.FireUpdate += UpdateGraph;
		}

		private void Start()
		{
			AbortWorker();
			_workerThread = new Thread(() => _gol.Play(null))
			{
				Priority = ThreadPriority.AboveNormal
			};
			_workerThread.Start();
		}

		private void Step()
		{
			AbortWorker();
			_workerThread = new Thread(() => _gol.Play(1))
			{
				Priority = ThreadPriority.AboveNormal
			};
			_workerThread.Start();
		}

		private void Restart()
		{
			AbortWorker();
			Initialize();
			if (File.Exists(_lastFileName))
				LoadMap(_lastFileName);
		}

		private void AbortWorker()
		{
			if (_workerThread != null && _workerThread.IsAlive)
			{
				_workerThread.Abort();
				_workerThread.Join();
			}
		}

		private void UpdateSpeed()
		{
			_gol.Speed = (int)nudSpeed.Value;
		}

		private void UpdateScale(int scale)
		{
			_graph.Scale = _currentView.Scale = scale;
			_graph.Invalidate();
		}

		private void UpdateSize()
		{
			_graph.Left = hScrollBar1.Left;
			_graph.Top = vScrollBar1.Top;
			_currentView.ScreenHeight = _graph.Height = vScrollBar1.Height;
			_currentView.ScreenWidth = _graph.Width = hScrollBar1.Width;
		}

		private void UpdatePosition()
		{
			txtPosX.Text = _currentView.OffsetView.X.ToString();
			txtPosY.Text = _currentView.OffsetView.Y.ToString();
			_graph.Invalidate();
		}

		private void UpdateGraph(object sender, FireUpdateEventArgs e)
		{
			var visibleAlive= e.Alive.Select(p => _currentView.GetVisiblePoint(p, true)).Where(i => i.HasValue).Select(i => i.Value);
			_graph.UpdateList(visibleAlive);

			_graph.Invalidate();
		}

		private void LoadMap(string fileName)
		{
			AbortWorker();

			try
			{
				var listAlive = FileSerializer.LoadPoints(fileName);
				var dimension = FileSerializer.FindDimensions(listAlive);

				var centerMap = new Point((int)(dimension.Width / _currentView.Scale / 2), (int)(dimension.Height / _currentView.Scale / 2));
				var centerScreen = new Point((int)(_graph.Width / _currentView.Scale / 2), (int)(_graph.Height / _currentView.Scale / 2));

				_gol.Load(listAlive, _currentView.AbsoluteLocation + centerMap + centerScreen);
				Text = fileName;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: Could not load data from file. Original error: " + ex.Message);
			}
		}

		private void SaveMap(string fileName)
		{
			try
			{
				var alivePoints = _gol.GetAlivePoints();
				var matrix = new List<KeyValuePair<int, int>>();
				var dimension = FileSerializer.FindDimensions(alivePoints);

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
			catch (Exception ex)
			{
				MessageBox.Show("Error: Could not save data to file. Original error: " + ex.Message);
			}
		}

		#endregion

		#region Overloads

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			AbortWorker();
			base.OnFormClosing(e);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			UpdateSize();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			UpdateSize();
		}

		#endregion

		#region Control handlers
		private void BtnLoad_Click(object sender, EventArgs e)
		{
			var openFileDialog1 = new OpenFileDialog
			{
				Filter = "Game of life files (*.lfe)|*.lfe|All files (*.*)|*.*",
				FilterIndex = 1,
				RestoreDirectory = true
			};

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				_lastFileName = openFileDialog1.FileName;
				LoadMap(openFileDialog1.FileName);
			}
		}

		private void BtnSave_Click(object sender, EventArgs e)
		{
			var openFileDialog1 = new SaveFileDialog
			{
				Filter = "Game of life files (*.lfe)|*.lfe|All files (*.*)|*.*",
				FilterIndex = 1,
				RestoreDirectory = true
			};

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				_lastFileName = openFileDialog1.FileName;
				SaveMap(openFileDialog1.FileName);
			}
		}

		private void Graph_MouseWheel(object sender, MouseEventArgs e)
		{
			try
			{
				trbScale.Value += Math.Sign(e.Delta);
			}
			catch (ArgumentOutOfRangeException) { }
		}

		private void BtnStart_Click(object sender, EventArgs e)
		{
			if (!_isStarted)
			{
				Start();
				btnStart.Text = "Stop";
				btnStep.Enabled = false;
				btnLoad.Enabled = false;
				btnSave.Enabled = false;
				btnReset.Enabled = false;
			}
			else
			{
				AbortWorker();
				btnStart.Text = "Start";
				btnStep.Enabled = true;
				btnLoad.Enabled = true;
				btnSave.Enabled = true;
				btnReset.Enabled = true;
			}
			_isStarted = !_isStarted;
		}

		private void BtnReset_Click(object sender, EventArgs e)
		{
			Restart();
		}

		private void BtnStep_Click(object sender, EventArgs e)
		{
			Step();
		}

		private void HScrollBar1_ValueChanged(object sender, EventArgs e)
		{
			if (_hscrollProceed)
				return;
			_hscrollProceed = true;

			var changedValue = hScrollBar1.Value - 450;
			hScrollBar1.Value = 450;

			if (Math.Abs(changedValue) >= 100)
			{
				_currentView.IncOffsetX(Math.Sign(changedValue) * _graph.Width);
			}
			else
			{
				_currentView.IncOffsetX(Math.Sign(changedValue) * 100);
			}
			UpdatePosition();
			_hscrollProceed = false;
		}

		private void VScrollBar1_ValueChanged(object sender, EventArgs e)
		{
			if (_vscrollProceed)
				return;
			_vscrollProceed = true;

			var changedValue = vScrollBar1.Value - 450;
			vScrollBar1.Value = 450;

			if (Math.Abs(changedValue) >= 100)
			{
				_currentView.IncOffsetY(Math.Sign(changedValue) * _graph.Width);
			}
			else
			{
				_currentView.IncOffsetY(Math.Sign(changedValue) * 100);
			}
			UpdatePosition();
			_vscrollProceed = false;
		}

		private void TrbScale_ValueChanged(object sender, EventArgs e)
		{
			UpdateScale(trbScale.Value);
		}

		private void Graph_MouseDown(object sender, MouseEventArgs e)
		{
			lock (_mouseMovementLockObj)
			{
				_mouseDownPoint = new Point(e.X, e.Y);
				_offsetOnMouseDown = _currentView.OffsetView;
			}
		}

		private void Graph_MouseMove(object sender, MouseEventArgs e)
		{
			lock (_mouseMovementLockObj)
			{
				if (_mouseDownPoint.HasValue)
				{
					var sizeChanged = new Size((int)((e.X - _mouseDownPoint.Value.X) / _currentView.Scale), (int)((e.Y - _mouseDownPoint.Value.Y) / _currentView.Scale));
					_currentView.OffsetView = _offsetOnMouseDown - sizeChanged;
				}
				UpdatePosition();
			}
		}

		private void Graph_MouseUp(object sender, MouseEventArgs e)
		{
			lock (_mouseMovementLockObj)
				_mouseDownPoint = null;
		}

		private void NudSpeed_ValueChanged(object sender, EventArgs e)
		{
			UpdateSpeed();
		}

		#endregion


	}
}
