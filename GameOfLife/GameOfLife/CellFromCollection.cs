using System;
using System.Diagnostics;
using System.Linq;

namespace GameOfLife.GameOfLife
{
	[DebuggerDisplay("IsAlive={_isAlive} AlivesAround={AliveCountAroundMe}")]
	public class CellFromCollection : Cell
	{
		private readonly CellCollection _father;
		private bool _isAlive;
		private readonly CellFromCollection[] _aroundCells;

		public CellFromCollection(CellCollection father, PointULong location, bool isAlive)
			: base(location)
		{
			_father = father;
			_isAlive = isAlive;

			_aroundCells = new CellFromCollection[8];

			FindAllAroundMe(isAlive);
		}

		private void FindAllAroundMe(bool isAlive)
		{
			var aroundCoords = base.AroundCellsCoords;
			for (int i = 0; i < 8; i++)
			{
				var aroundCoord = aroundCoords[i];

				var cell = _father.TryGet(aroundCoord);
				//around cell already in _father
				if (cell != null)
				{
					_aroundCells[i] = cell;
					var mePosInAroundCell = Math.Abs(i - 7);
					_aroundCells[i]._aroundCells[mePosInAroundCell] = this;
				}
				//around cell not found in _father
				//i'am is alive. Add all around cells to father. and store in me.
				else if (isAlive)
				{
					_aroundCells[i] = _father.Add(aroundCoord, false);

					//store me in this around cell
					var mePosInAroundCell = Math.Abs(i - 7);
					_aroundCells[i]._aroundCells[mePosInAroundCell] = this;
				}
			}
		}

		public bool IsAlive
		{
			get { return _isAlive; }
			set
			{
				_isAlive = value;

				if (_isAlive)
					FindAllAroundMe(true);
				else
					CheckIfINeed();
			}
		}

		/// <summary>
		/// check if i need
		/// </summary>
		private void CheckIfINeed()
		{
			//if isAlive or have lives around me, than i need
			if (_isAlive || AliveCountAroundMe != 0) return;

			//Delete me from father and around me cells
			_father.Remove(_location);
			for (int i = 0; i < 8; i++)
			{
				var cell = _aroundCells[i];
				_aroundCells[i] = null;

				if (cell != null)
				{
					var mePosInAroundCell = Math.Abs(i - 7);
					cell._aroundCells[mePosInAroundCell] = null;
					cell.CheckIfINeed();
				}
			}
		}

		public int AliveCountAroundMe
		{
			get { return _aroundCells.Count(i => i != null && i._isAlive); }
		}
	}
}
