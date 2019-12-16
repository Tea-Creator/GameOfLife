using System;
using System.Linq;
using System.Collections.Generic;

namespace GameOfLife
{
    public class Board
    {
        private readonly Cell[,] _cells;

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            _cells = new Cell[rows, columns];
        }

        public int Rows { get; }
        public int Columns { get; }

        public void ProcessNextTurn()
        {
            bool[,] statusMap = new bool[Rows, Columns];

            for(int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    statusMap[i, j] = this[i, j].WillBeDeadOnNextTurn(this);
                }
            }

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (statusMap[i, j])
                    {
                        this[i, j].Die();
                    }
                    else
                    {
                        this[i, j].Reborn();
                    }
                }
            }
        }

        public Cell this[int row, int column]
        { 
            get
            {
                if (row < 0 || row >= Rows || column < 0 || column >= Columns)
                {
                    return null;
                }                

                if (_cells[row, column] is null)
                {
                    _cells[row, column] = Cell.CreateDead();
                }

                return _cells[row, column];
            }
        }       

        public IEnumerable<Cell> GetNeighbourCells(Cell cell)
        {
            if (cell is null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            var (row, column) = GetCellPosition(cell);

            return GetNeighbourCells(row, column);
        }

        private (int row, int column) GetCellPosition(Cell cell)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (cell == this[i, j])
                    {
                        return (i, j);
                    }
                }
            }

            return (-1, -1);
        }

        private IEnumerable<Cell> GetNeighbourCells(int row, int column)
        {
            return GetTopNeighbours(row, column)
                .Union(GetSideNeighbours(row, column))
                .Union(GetBottomNeighbours(row, column))
                .Where(cell => cell is { });
        }

        private IEnumerable<Cell> GetTopNeighbours(int row, int column)
        {
            yield return this[row - 1, column - 1];
            yield return this[row - 1, column];
            yield return this[row - 1, column + 1];
        }

        private IEnumerable<Cell> GetSideNeighbours(int row, int column)
        {
            yield return this[row, column - 1];
            yield return this[row, column + 1];
        }

        private IEnumerable<Cell> GetBottomNeighbours(int row, int column)
        {
            yield return this[row + 1, column - 1];
            yield return this[row + 1, column];
            yield return this[row + 1, column + 1];
        }
    }
}