namespace _07._Paths_in_Labyrinth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class PathsInLabyrinthProgram
    {
        private static readonly List<char> Path = new List<char>();

        private static char[,] Lab =
        {
            {'-', '-', '-', '*', '-', '-', '-'},
            {'*', '*', '-', '*', '-', '*', '-'},
            {'-', '-', '-', '-', '-', '-', '-'},
            {'-', '*', '*', '*', '*', '*', '-'},
            {'-', '-', '-', '-', '-', '-', 'e'},
        };

        public static void Main()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            Lab = new char[rows, cols];

            for (var rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                var rowInput = Console.ReadLine()
                    .ToCharArray();

                for (var colIndex = 0; colIndex < cols; colIndex++)
                {
                    Lab[rowIndex, colIndex] = rowInput[colIndex];
                }
            }

            FindPaths(0, 0, 'S');
        }

        private static void FindPaths(int row, int col, char direction)
        {
            if (!IsValidCell(row, col))
            {
                return;
            }

            if (Lab[row, col] == 'e')
            {
                Path.Add(direction);
                PrintPath();
                Path.RemoveAt(Path.Count - 1);
                return;
            }

            Lab[row, col] = 'v';
            Path.Add(direction);

            //Go Right
            FindPaths(row, col + 1, 'R');
            //Go Down
            FindPaths(row + 1, col, 'D');
            //Go Left
            FindPaths(row, col - 1, 'L');
            //Go Up
            FindPaths(row - 1, col, 'U');

            Lab[row, col] = '-';
            Path.RemoveAt(Path.Count - 1);
        }

        private static void PrintPath()
        {
            Console.WriteLine(string.Join(string.Empty, Path.Skip(1)));
        }

        private static bool IsValidCell(int row, int col)
        {
            return row >= 0 &&
                   col >= 0 &&
                   row < Lab.GetLength(0) &&
                   col < Lab.GetLength(1) &&
                   Lab[row, col] != '*' &&
                   Lab[row, col] != 'v';
        }
    }
}