using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace GameOfLife.GameOfLife
{
	public class GameOfLifeClass
	{
		#region Fields

		private readonly Dictionary<PointULong, Cell> _aliveCells;
		private readonly Dictionary<PointULong, Cell> _aroundAliveCells;

		private bool _stop = false;

		private delegate void MyEventHandler(List<PointULong> born, List<PointULong> dead);

		private readonly MyEventHandler _myFireUpdater;
		private int _speed;
		private readonly AutoResetEvent _speedChangedEvent = new AutoResetEvent(false);

		#endregion

		#region Events
		public event FireUpdateEventHandler FireUpdate;

		public delegate void FireUpdateEventHandler(object sender, FireUpdateEventArgs e);

		public int Speed
		{
			get { return _speed; }
			set
			{
				_speed = value;
				_speedChangedEvent.Set();
			}
		}

		private void OnFireUpdate(ICollection<PointULong> born, ICollection<PointULong> dead)
		{
			if (FireUpdate != null)
				FireUpdate(this, new FireUpdateEventArgs(born, dead));
		}
		#endregion

		#region Constructor
		public GameOfLifeClass()
		{
			_myFireUpdater = OnFireUpdate;

			_aliveCells = new Dictionary<PointULong, Cell>();
			_aroundAliveCells = new Dictionary<PointULong, Cell>();
		}
		#endregion
		
		#region Private Methods

		private void SetAlive(IEnumerable<PointULong> alive)
		{
			foreach (var item in alive)
			{
				_aliveCells[item] = new Cell(item);
			}

			foreach (var i in _aliveCells.Values)
			{
				foreach (var j in i.AroundCellsCoords)
				{
					if (!_aliveCells.ContainsKey(j))
						_aroundAliveCells[j] = new Cell(j);
				}
			}
		}

		/// <summary>
		/// Make a step in the game
		/// </summary>
		private void GameStep(ref List<PointULong> born, ref List<PointULong> dead)
		{
			var aliveNeighbors = new Dictionary<PointULong, int>();

			foreach (var i in _aliveCells.Concat(_aroundAliveCells).AsParallel())
			{
				var nearMeCells = _aliveCells.AsParallel().Count(j => i.Value.IsNearMe(j.Value));
				aliveNeighbors[i.Key] = nearMeCells;
			}

			born = new List<PointULong>();
			dead = new List<PointULong>();

			foreach (var i in aliveNeighbors.AsParallel())
			{
				var isAlive = _aliveCells.ContainsKey(i.Key);
				var willBorn = Algorithm.NextState(isAlive, i.Value);

				if (willBorn)
					born.Add(i.Key);
				/*else
					dead.Add(i.Key);*/
			}

			_aliveCells.Clear();
			_aroundAliveCells.Clear();
			SetAlive(born);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Load alive points
		/// </summary>
		/// <param name="alive">Alive points</param>
		/// <param name="offset">Global offset of all points</param>
		public void Load(List<Point> alive, PointULong offset)
		{
			_stop = true;
			_aliveCells.Clear();
			_aroundAliveCells.Clear();
			var aliveUPoints = alive.Select(i => offset + i);
			SetAlive(aliveUPoints);
			_myFireUpdater.Invoke(aliveUPoints.ToList(), new List<PointULong>());
		}

		/// <summary>
		/// Play game
		/// </summary>
		/// <param name="steps">Count of steps to play. If null then infinity</param>
		public void Play(int? steps)
		{
			_stop = false;
			List<PointULong> born = null;
			List<PointULong> dead = null;
			while (!_stop && (!steps.HasValue || steps > 0))
			{
				GameStep(ref born, ref dead);
				_myFireUpdater.Invoke(born, dead);

				_speedChangedEvent.WaitOne(_speed);

				if (steps.HasValue)
					steps--;
			}
		}
		#endregion
	}
}