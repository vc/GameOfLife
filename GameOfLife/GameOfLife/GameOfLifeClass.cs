using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace GameOfLife.GameOfLife
{
	public sealed class GameOfLifeClass : IDisposable
	{
		#region Fields

		internal Cellcollection _cells;

		private bool _stop = false;

		private delegate void MyEventHandler(List<PointULong> alive);
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

		private void OnFireUpdate(ICollection<PointULong> alive)
		{
			if (FireUpdate != null)
				FireUpdate(this, new FireUpdateEventArgs(alive));
		}
		#endregion

		#region Constructor
		public GameOfLifeClass()
		{
			_myFireUpdater = OnFireUpdate;

			_cells = new Cellcollection();
		}
		#endregion

		#region Private Methods

		private void SetAlive(IEnumerable<PointULong> alive, IEnumerable<PointULong> dead)
		{
			//set alive cells
			foreach (var item in alive)
				_cells.Add(item, true);

			foreach (var item in dead)
			{
				_cells.MarkAsDead(item);
			}
		}

		/// <summary>
		/// Make a step in the game
		/// </summary>
		private void GameStep(ref List<PointULong> alive)
		{
			alive = new List<PointULong>();
			var born = new List<PointULong>();
			var dead = new List<PointULong>();

			foreach (var i in _cells.Collection)
			{
				var willAlive = Algorithm.NextState(i.Value.IsAlive, i.Value.AliveCountAroundMe);

				if (willAlive)
				{
					alive.Add(i.Key);
					
					if (!i.Value.IsAlive)
						born.Add(i.Key);
				}
				else if(i.Value.IsAlive)
					dead.Add(i.Key);
			}

			SetAlive(born, dead);
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
			_cells.Clear();
			var aliveUPoints = alive.Select(i => offset + i);
			SetAlive(aliveUPoints, new List<PointULong>());
			_myFireUpdater.Invoke(aliveUPoints.ToList());
		}

		/// <summary>
		/// Save points
		/// </summary>
		/// <returns>Alive points</returns>
		public List<Point> GetAlivePoints()
		{
			var coll = _cells.Collection.Where(i => i.Value.IsAlive).ToDictionary(i => i.Key);
			if (coll.Count == 0)
				return new List<Point>();

			//Finding offset
			var minPoint = coll.First().Key;
			foreach (var p in coll.Keys)
			{
				if (p.X < minPoint.X)
					minPoint.X = p.X;
				if (p.Y < minPoint.Y)
					minPoint.Y = p.Y;
			}

			return coll.Keys.Select(p => p - minPoint).ToList();
		}

		/// <summary>
		/// Play game
		/// </summary>
		/// <param name="steps">Count of steps to play. If null then infinity</param>
		public void Play(int? steps)
		{
			_stop = false;
			List<PointULong> alive = null;
			List<PointULong> dead = null;
			while (!_stop && (!steps.HasValue || steps > 0))
			{
				GameStep(ref alive);
				_myFireUpdater.Invoke(alive);

				_speedChangedEvent.WaitOne(_speed);

				if (steps.HasValue)
					steps--;
			}
		}

		public void Dispose()
		{
			if (_speedChangedEvent != null)
				_speedChangedEvent.Dispose();
		}
		#endregion
	}
}