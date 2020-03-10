namespace _06._Connected_Areas_in_Matrix
{
    using System;
    using System.Collections.Generic;

    public static class ConnectedAreasInMatrixProgram
    {
        private static SortedSet<Area> _areas;
        private static char[,] _matrix;

        private const char WALL = '*';
        private const char VISITED = 'v';

        public static void Main()
        {
            _areas = new SortedSet<Area>();

            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            _matrix = new char[rows, cols];
            InitializeMatrix(rows, cols);

            TraverseMatrix();

            PrintAreas();
        }

        private static void TraverseMatrix()
        {
            for (var row = 0; row < _matrix.GetLength(0); row++)
            {
                for (var col = 0; col < _matrix.GetLength(1); col++)
                {
                    MapArea(row, col);
                }
            }
        }

        private static void MapArea(int row, int col)
        {
            if (_matrix[row, col] == WALL ||
                _matrix[row, col] == VISITED)
            {
                return;
            }

            var size = 0;
            CalculateAreaSize(row, col, ref size);
            var area = new Area(row, col, size);
            _areas.Add(area);
        }

        private static void CalculateAreaSize(int row, int col, ref int size)
        {
            if (!IsInBoundaries(row, col) ||
                _matrix[row, col] == VISITED ||
                _matrix[row, col] == WALL)
            {
                return;
            }

            size++;
            _matrix[row, col] = VISITED;

            //Go Left
            CalculateAreaSize(row, col - 1, ref size);
            //Go Right
            CalculateAreaSize(row, col + 1, ref size);
            //Go Up
            CalculateAreaSize(row + 1, col, ref size);
            //Go Down
            CalculateAreaSize(row - 1, col, ref size);
        }

        private static bool IsInBoundaries(int row, int col)
        {
            return row >= 0 &&
                   row < _matrix.GetLength(0) &&
                   col >= 0 &&
                   col < _matrix.GetLength(1);

        }

        private static void PrintAreas()
        {
            Console.WriteLine($"Total areas found: {_areas.Count}");
            var count = 1;

            foreach (var area in _areas)
            {
                Console.WriteLine($"Area #{count++} at ({area.Row}, {area.Col}), size: {area.Size}");
            }
        }

        private static void InitializeMatrix(int rows, int cols)
        {
            for (var rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                var input = Console.ReadLine();

                for (var colIndex = 0; colIndex < cols; colIndex++)
                {
                    _matrix[rowIndex, colIndex] = input[colIndex];
                }
            }
        }
    }
}
