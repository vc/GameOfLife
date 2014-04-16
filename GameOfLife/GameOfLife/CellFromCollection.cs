using System;
using System.Diagnostics;
using System.Linq;

namespace GameOfLife.GameOfLife
{
	[DebuggerDisplay("IsAlive={_isAlive} AlivesAround={AliveCountAroundMe}")]
	public class CellFromCollection : Cell
	{
		private readonly Cellcollection _father;
		private bool _isAlive;
		private readonly CellFromCollection[] _aroundCells;

		public CellFromCollection(Cellcollection father, PointULong location, bool isAlive)
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

				//i'am dead. Check if all around is deads and remove me;
				if (!value)
				{
					RemoveMeInAroundCells();
					//CheckAroundCellsForRemove()
				}
				else
				{
					FindAllAroundMe(_isAlive);
				}
			}
		}

		private void RemoveMeInAroundCells()
		{
			if (AliveCountAroundMe != 0)
			{
				for (int i = 0; i < 8; i++)
				{
					if (_aroundCells[i] != null && _aroundCells[i].AliveCountAroundMe == 0)
					{
						_father.Remove(_aroundCells[i].Location);
						_aroundCells[i].RemoveMeInAroundCells();
					}
				}
			}
			else
			{
				for (int i = 0; i < 8; i++)
				{
					var aroundCell = _aroundCells[i];
					if (aroundCell != null)
					{
						_aroundCells[i] = null;

						var mePosInAroundCell = Math.Abs(i - 7);

						aroundCell.RemoveMeInAroundCells();
						aroundCell._aroundCells[mePosInAroundCell] = null;
					}
				}
				_father.Remove(Location);
			}
		}

		public int AliveCountAroundMe
		{
			get
			{
				return _aroundCells.Count(i => i != null && i.IsAlive);

				if (IsAlive)
					return _aroundCells.Count(i => i != null && i.IsAlive);
				else
				{
					var aliveAroundMe = 0;
					var aroundCoords = base.AroundCellsCoords;
					for (int i = 0; i < 8; i++)
					{
						var point = aroundCoords[i];
						CellFromCollection cell = null;
						if (_father.TryGet(point, out cell))
						{
							if (cell.IsAlive)
								aliveAroundMe++;
						}
					}
					return aliveAroundMe;
				}
			}
		}
	}
}
