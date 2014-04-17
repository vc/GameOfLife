using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GameOfLife.GameOfLife
{
	public class CellCollection
	{
		private readonly Dictionary<PointULong, CellFromCollection> _collection;

		public CellCollection()
		{
			_collection = new Dictionary<PointULong, CellFromCollection>();
		}

		public ReadOnlyDictionary<PointULong, CellFromCollection> Collection
		{
			get { return new ReadOnlyDictionary<PointULong, CellFromCollection>(_collection); }
		}

		public CellFromCollection Add(PointULong point, bool isAlive)
		{
			CellFromCollection cell;
			if (_collection.ContainsKey(point))
			{
				cell = _collection[point];
				if (cell.IsAlive)
					throw new ArgumentException("isAlive");
				cell.IsAlive = true;
			}
			else
			{
				cell = new CellFromCollection(this, point, isAlive);
				_collection.Add(point, cell);
			}

			return cell;
		}

		public void MarkAsDead(PointULong p)
		{
			if (_collection.ContainsKey(p))
			{
				var cell = _collection[p];
				if (cell.IsAlive)
					cell.IsAlive = false;
			}
			else
			{
				throw new ArgumentException();
			}
		}

		public void CleanAllDeads()
		{
			var listToRemove = new List<CellFromCollection>();
			foreach (var cell in _collection.Values)
			{
				if (cell.AliveCountAroundMe == 0 && !cell.IsAlive)
					listToRemove.Add(cell);
			}
		
			foreach (var cell in listToRemove)
				cell.Delete();
		}

		public bool Contains(PointULong p)
		{
			return _collection.ContainsKey(p);
		}

		public CellFromCollection GetCellByPoint(PointULong p)
		{
			return _collection[p];
		}

		public void Remove(PointULong location)
		{
			_collection.Remove(location);
		}

		public void Clear()
		{
			_collection.Clear();
		}

		public bool TryGet(PointULong point, out CellFromCollection cell)
		{
			return _collection.TryGetValue(point, out cell);
		}
	}
}
