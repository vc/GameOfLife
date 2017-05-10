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
			var cell = _collection[p];
			cell.IsAlive = false;
		}

		public CellFromCollection TryGet(PointULong p)
		{
			CellFromCollection ret;
			_collection.TryGetValue(p, out ret);
			return ret;
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
