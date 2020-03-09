namespace _06._8_Queens_Puzzle
{
    using System;

    public static class QueensPuzzleProgram
    {
        private static int _counter = 0;
        public static void Main()
        {
            PlaceQueen(0);
            //Console.WriteLine(_counter);
        }

        private static void PlaceQueen(int row)
        {
            if (row == 8)
            {
                Print();
                _counter++;
                return;
            }
            else
            {
                for (var col = 0; col < 8; col++)
                {
                    if (CanPlaceQueen(row, col))
                    {
                        Mark(row, col);

                        PlaceQueen(row + 1);

                        UnMark(row, col);
                    }
                }
            }
        }

        private static void UnMark(int row, int col)
        {
            //EightQueens.AttackedRows.Remove(row);
            EightQueens.AttackedColumns.Remove(col);
            EightQueens.AttackedLeftDiagonals.Remove(row - col);
            EightQueens.AttackedRightDiagonals.Remove(row + col);

            EightQueens.Chessboard[row, col] = false;
        }

        private static void Mark(int row, int col)
        {
            //EightQueens.AttackedRows.Add(row);
            EightQueens.AttackedColumns.Add(col);
            EightQueens.AttackedLeftDiagonals.Add(row - col);
            EightQueens.AttackedRightDiagonals.Add(row + col);

            EightQueens.Chessboard[row, col] = true;
        }

        private static bool CanPlaceQueen(int row, int col)
        {
            return //!EightQueens.AttackedRows.Contains(row) &&
                   !EightQueens.AttackedColumns.Contains(col) &&
                   !EightQueens.AttackedLeftDiagonals.Contains(row - col) &&
                   !EightQueens.AttackedRightDiagonals.Contains(row + col);
        }

        private static void Print()
        {
            for (var row = 0; row < 8; row++)
            {
                for (var col = 0; col < 8; col++)
                {
                    if (EightQueens.Chessboard[row, col])
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
