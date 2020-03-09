namespace _06._8_Queens_Puzzle
{
    using System.Collections.Generic;

    public static class EightQueens
    {
        private const int SIZE = 8;

        public static readonly bool[,] Chessboard = new bool[SIZE, SIZE];

        //public static readonly HashSet<int> AttackedRows = new HashSet<int>();
        public static readonly HashSet<int> AttackedColumns = new HashSet<int>();
        public static readonly HashSet<int> AttackedLeftDiagonals = new HashSet<int>();
        public static readonly HashSet<int> AttackedRightDiagonals = new HashSet<int>();
    }
}
