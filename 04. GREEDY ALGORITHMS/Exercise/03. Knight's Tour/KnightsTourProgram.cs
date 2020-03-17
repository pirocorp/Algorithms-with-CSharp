namespace _03._Knight_s_Tour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class KnightsTourProgram
    {
        private static int[,] _board;
        private static int _size;

        public static void Main()
        {
            InitializeBoard();

            MoveKnight(0, 0, 1);

            PrintBoard();
        }

        //Warnsdorff's rule
        private static void MoveKnight(int currentRow, int currentCol, int step)
        {
            while (true)
            {
                _board[currentRow, currentCol] = step++;

                var possibleMoves = CreatePossibleCellMoves(currentRow, currentCol);

                var orderedMoves = possibleMoves
                    .OrderBy(CheckOnwardMoves)
                    .ToList();

                var nextMove = orderedMoves.FirstOrDefault();

                if (nextMove == null)
                {

                    break;
                }

                currentRow = nextMove.Row;
                currentCol = nextMove.Col;
            }
        }

        private static int CheckOnwardMoves(Cell cell)
        {
            var row = cell.Row;
            var col = cell.Col;

            var moves = CreatePossibleCellMoves(row, col).Count;

            return moves;
        }

        private static List<Cell> CreatePossibleCellMoves(int currentRow, int currentCol)
        {
            var cells = new List<Cell>();

            var dimensions = new List<int>()
            {
                currentRow + 1,
                currentCol + 2,

                currentRow - 1,
                currentCol + 2,

                currentRow + 1,
                currentCol - 2,

                currentRow - 1,
                currentCol - 2,

                currentRow + 2,
                currentCol + 1,

                currentRow - 2,
                currentCol + 1,

                currentRow - 2,
                currentCol - 1,

                currentRow + 2,
                currentCol - 1,
            };

            for (var i = 0; i < dimensions.Count; i += 2)
            {
                var row = dimensions[i];
                var col = dimensions[i + 1];

                if (row >= 0 &&
                    col >= 0 &&
                    row < _size &&
                    col < _size &&
                    _board[row, col] == 0)
                {
                    cells.Add(new Cell(row, col));
                }
            }

            return cells;
        }

        private static void InitializeBoard()
        {
            _size = int.Parse(Console.ReadLine());
            _board = new int[_size, _size];
        }

        private static void PrintBoard()
        {
            for (var row = 0; row < _board.GetLength(0); row++)
            {
                for (var col = 0; col < _board.GetLength(1); col++)
                {
                    var cell = _board[row, col].ToString().PadLeft(4, ' ');
                    Console.Write(cell);
                }

                Console.WriteLine();
            }
        }

    }
}
