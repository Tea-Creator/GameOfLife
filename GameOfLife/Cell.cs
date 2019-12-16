using System;
using System.Linq;

namespace GameOfLife
{
    public class Cell
    {
        private Cell(bool isAlive)
        {
            IsAlive = isAlive;
        }

        public bool IsAlive { get; private set; }

        public void Reborn()
        {
            IsAlive = true;
        }

        public void Die()
        {
            IsAlive = false;
        }

        public bool WillBeDeadOnNextTurn(Board board)
        {
            if (board is null)
            {
                throw new ArgumentNullException(nameof(board));
            }

            int aliveNeighboursCount = board
                .GetNeighbourCells(this)
                .Count(cell => cell.IsAlive);

            if (IsAlive)
            {
                if (aliveNeighboursCount > 3 || aliveNeighboursCount < 2)
                {
                    return true;
                }

                return false;
            }

            return aliveNeighboursCount != 3;
        }

        public static Cell CreateAlive()
        {
            return new Cell(true);
        }

        public static Cell CreateDead()
        {
            return new Cell(false);
        }
    }
}