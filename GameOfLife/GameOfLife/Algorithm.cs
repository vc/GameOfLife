
namespace GameOfLife.GameOfLife
{
	public static class Algorithm
	{
		public static bool NextState(bool isAlive, int aliveNeighbors)
		{
			// Is dead, check if it born
			if (!isAlive && aliveNeighbors == 3)
				return true;

			// Is alive, check if it keeps living or dies
			if (isAlive && (aliveNeighbors == 2 || aliveNeighbors == 3))
				return true;

			return false;
		}
	}
}