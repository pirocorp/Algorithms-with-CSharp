namespace _06._Connected_Areas_in_Matrix
{
    using System;
    using System.Collections.Generic;

    public static class ConnectedAreasInMatrixProgram
    {
        private static SortedSet<Area> _areas;

        public static void Main()
        {
            _areas = new SortedSet<Area>();

            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            var matrix = new char[rows, cols];
            InitializeMatrix(rows, cols, matrix);

            TraverseMatrix(matrix);

            Console.WriteLine($"Total areas found: {_areas.Count}");
            var count = 1;

            foreach (var area in _areas)
            {
                Console.WriteLine($"Area #{count++} at ({area.PositionY}, {area.PositionX}), size: {area.Size}");
            }
        }

        private static void TraverseMatrix(char[,] matrix)
        {
            for (var rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
            {
                for (var colIndex = 0; colIndex < matrix.GetLength(1); colIndex++)
                {
                    var cell = matrix[rowIndex, colIndex];

                    if (cell != '*' && 
                        cell != 'v')
                    {
                        var size = MapArea(rowIndex, colIndex, matrix);
                        var area = new Area(colIndex, rowIndex, size);
                        _areas.Add(area);
                    }
                }
            }
        }

        private static int MapArea(int rowIndex, int colIndex, char[,] matrix)
        {
            var size = 0;
            var currentCol = colIndex;
            var currentRow = rowIndex;

            var minCol = colIndex;
            var maxCol = colIndex;

            while (currentRow < matrix.GetLength(0))
            {
                var x = true;

                for (var i = minCol; i <= maxCol; i++)
                {
                    if (matrix[currentRow, i] != '*' &&
                        matrix[currentRow, i] != 'v')
                    {
                        currentCol = i;
                        x = false;
                        break;
                    }
                }

                if (x)
                {
                    break;
                }

                maxCol = CheckRight(currentRow, matrix, currentCol, ref size);
                minCol = CheckLeft(currentRow, matrix, currentCol - 1, ref size);
                currentRow++;
            }

            return size;
        }

        private static int CheckLeft(int rowIndex, char[,] matrix, int currentCol, ref int size)
        {
            if (currentCol < 0)
            {
                return 0;
            }

            while (currentCol >= 0 &&
                   matrix[rowIndex, currentCol] != '*' &&
                   matrix[rowIndex, currentCol] != 'v')
            {
                size++;
                matrix[rowIndex, currentCol] = 'v';
                currentCol--;
            }

            return ++currentCol;
        }

        private static int CheckRight(int rowIndex, char[,] matrix, int currentCol, ref int size)
        {
            while (currentCol < matrix.GetLength(1) &&
                   matrix[rowIndex, currentCol] != '*' &&
                   matrix[rowIndex, currentCol] != 'v')
            {
                size++;
                matrix[rowIndex, currentCol] = 'v';
                currentCol++;
            }

            return --currentCol;
        }

        private static void InitializeMatrix(int rows, int cols, char[,] matrix)
        {
            for (var rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                var input = Console.ReadLine();

                for (var colIndex = 0; colIndex < cols; colIndex++)
                {
                    matrix[rowIndex, colIndex] = input[colIndex];
                }
            }
        }
    }
}
