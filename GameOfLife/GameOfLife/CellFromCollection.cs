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

				var containInFather = _father.Contains(aroundCoord);
				//around cell already in _father
				if (containInFather)
				{
					_aroundCells[i] = _father.GetCellByPoint(aroundCoord);
					var mePosInAroundCell = Math.Abs(i - 7);
					_aroundCells[i]._aroundCells[mePosInAroundCell] = this;
				}
				//around cell not found in _father
				else
				{
					//i'am is alive. Add all around cells to father. and store in me.
					if (isAlive)
					{
						_aroundCells[i] = _father.Add(aroundCoord, false);

						//store me in this around cell
						var mePosInAroundCell = Math.Abs(i - 7);
						_aroundCells[i]._aroundCells[mePosInAroundCell] = this;
					}
				}
			}
		}

		public bool IsAlive
		{
			get { return _isAlive; }
			set
			{
				if (value == false && _isAlive == false)
					throw new ArgumentException("I'am already dead!");

				_isAlive = value;

				if (_isAlive)
					FindAllAroundMe(true);
			}
		}

		/// <summary>
		/// Delete me from father and around me cells
		/// </summary>
		public void Delete()
		{
			_father.Remove(_location);
			for (int i = 0; i < 8; i++)
			{
				if (_aroundCells[i] != null)
				{
					var mePosInAroundCell = Math.Abs(i - 7);
					_aroundCells[i]._aroundCells[mePosInAroundCell] = null;
				}
			}
		}

		public int AliveCountAroundMe
		{
			get { return _aroundCells.Count(i => i != null && i._isAlive); }
		}
	}
}
