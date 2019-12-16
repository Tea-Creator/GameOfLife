using System;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Program
    {
        private static async Task Main()
        {
            Board board = new Board(20, 20);

            CreateGlider(board, 0, 0);
            CreateBox(board, 9, 9);

            while (true)
            {
                PrintBoard(board);
                board.ProcessNextTurn();
                await Task.Delay(100);
            }
        }

        private static void CreateGlider(Board board, int startRow, int startColumn)
        {
            board[startRow, startColumn + 1].Reborn();
            board[startRow + 1, startColumn + 2].Reborn();
            board[startRow + 2, startColumn].Reborn();
            board[startRow + 2, startColumn + 1].Reborn();
            board[startRow + 2, startColumn + 2].Reborn();
        }

        private static void CreateBox(Board board, int startRow, int startColumn)
        {
            board[startRow, startColumn].Reborn();
            board[startRow, startColumn + 1].Reborn();
            board[startRow + 1, startColumn].Reborn();
            board[startRow + 1, startColumn + 1].Reborn();
        }

        private static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                for (int j = 0; j < board.Columns; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write(board[i, j].IsAlive ? '+' : '-');
                }
            }
        }
    }
}
