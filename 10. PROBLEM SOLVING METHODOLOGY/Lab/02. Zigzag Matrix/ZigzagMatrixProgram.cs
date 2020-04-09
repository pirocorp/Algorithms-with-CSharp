namespace _02._Zigzag_Matrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ZigzagMatrixProgram
    {
        private static int[,] _matrix;
        private static int[,] _goUpFirst;
        private static int[,] _goDownFirst;

        private static int[,] _maxPaths;
        private static int[,] _previousRowIndex;

        private static void ReadInput()
        {
            var rowCount = int.Parse(Console.ReadLine());
            var colCount = int.Parse(Console.ReadLine());

            _matrix = new int[rowCount, colCount];
            _goUpFirst = new int[rowCount, colCount];
            _goDownFirst = new int[rowCount, colCount];

            for (var row = 0; row < rowCount; row++)
            {
                var inputTokens = Console.ReadLine()
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();

                for (var col = 0; col < colCount; col++)
                {
                    _matrix[row, col] = inputTokens[col];
                }
            }
        }

        private static int GetMax(int[,] calculatedMatrix, int row, int col, bool goUp)
        {
            var max = int.MinValue;

            if (goUp)
            {
                for (var rowIndex = row + 1; rowIndex < calculatedMatrix.GetLength(0); rowIndex++)
                {
                    if (calculatedMatrix[rowIndex, col] > max)
                    {
                        max = calculatedMatrix[rowIndex, col];
                    }
                }
            }
            else
            {
                for (var rowIndex = row - 1; rowIndex >= 0; rowIndex--)
                {
                    if (calculatedMatrix[rowIndex, col] > max)
                    {
                        max = calculatedMatrix[rowIndex, col];
                    }
                }
            }

            return max;
        }

        private static int GetIndex(int[,] matrix, int col, int value)
        {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                if (matrix[row, col] == value)
                {
                    return row;
                }
            }

            throw new KeyNotFoundException();
        }

        #region MySolution

        private static void CalculateMatrix(int[,] calculatedMatrix, bool goUp)
        {
            for (var row = 0; row < _matrix.GetLength(0); row++)
            {
                calculatedMatrix[row, 0] = _matrix[row, 0];
            }

            for (var col = 1; col < _matrix.GetLength(1); col++)
            {
                for (var row = 0; row < _matrix.GetLength(0); row++)
                {
                    if (goUp)
                    {
                        if (row == _matrix.GetLength(0) - 1)
                        {
                            calculatedMatrix[row, col] = -1;
                            continue;
                        }

                        calculatedMatrix[row, col] = _matrix[row, col] + GetMax(calculatedMatrix, row, col - 1, goUp);
                    }
                    else
                    {
                        if (row == 0)
                        {
                            calculatedMatrix[row, col] = -1;
                            continue;
                        }

                        calculatedMatrix[row, col] = _matrix[row, col] + GetMax(calculatedMatrix, row, col - 1, goUp);
                    }
                }

                goUp = !goUp;
            }
        }

        private static List<int> ReconstructSolution()
        {
            var result = new List<int>();

            var col = _matrix.GetLength(1) - 1;

            var goUpMax = GetMax(_goUpFirst, -1, col, true);
            var goDownMax = GetMax(_goDownFirst, -1, col, true);

            int[,] resultMatrix;
            int max;

            if (goUpMax >= goDownMax)
            {
                resultMatrix = _goUpFirst;
                max = goUpMax;
            }
            else
            {
                resultMatrix = _goDownFirst;
                max = goDownMax;
            }

            while (col >= 0)
            {
                var index = GetIndex(resultMatrix, col, max);
                result.Add(_matrix[index, col]);
                max -= _matrix[index, col];
                col--;
            }

            result.Reverse();
            return result;
        }

        private static void MySolution()
        {
            ReadInput();
            CalculateMatrix(_goUpFirst, true);
            CalculateMatrix(_goDownFirst, false);
            var result = ReconstructSolution();

            Console.WriteLine($"{result.Sum(x => x)} = {string.Join(" + ", result)}");
        }
        #endregion

        #region JudgeSolution
        private static void InitializeAuxiliaryStructures()
        {
            _maxPaths = new int[_matrix.GetLength(0), _matrix.GetLength(1)];
            _previousRowIndex = new int[_matrix.GetLength(0), _matrix.GetLength(1)];

            //Initialize first column
            for (var row = 0; row < _matrix.GetLength(0); row++)
            {
                _maxPaths[row, 0] = _matrix[row, 0];
            }
        }

        private static void CalculateMaxPath()
        {
            //Fill max paths
            var numberOfRows = _matrix.GetLength(0);
            var numberOfColumns = _matrix.GetLength(1);
            for (var col = 1; col < numberOfColumns; col++)
            {
                for (int row = 0; row < numberOfRows; row++)
                {
                    int previousMax = 0;

                    if (col % 2 != 0)
                    {
                        for (int i = row + 1; i < numberOfRows; i++)
                        {
                            if (_maxPaths[i, col - 1] > previousMax)
                            {
                                previousMax = _maxPaths[i, col - 1];
                                _previousRowIndex[row, col] = i;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < row; i++)
                        {
                            if (_maxPaths[i, col - 1] > previousMax)
                            {
                                previousMax = _maxPaths[i, col - 1];
                                _previousRowIndex[row, col] = i;
                            }
                        }
                    }

                    _maxPaths[row, col] = previousMax + _matrix[row, col];
                }
            }
        }

        private static List<int> ReconstructMaxPath()
        {
            var colIndex = _matrix.GetLength(1) - 1;
            var max = GetMax(_maxPaths, -1, colIndex, true);
            var rowIndex = GetIndex(_maxPaths, colIndex, max);

            var path = new List<int>();
            while (colIndex >= 0)
            {
                path.Add(_matrix[rowIndex, colIndex]);
                rowIndex = _previousRowIndex[rowIndex, colIndex];
                colIndex--;
            }

            path.Reverse();
            return path;
        }

        private static void JudgeSolution()
        {
            ReadInput();
            InitializeAuxiliaryStructures();
            CalculateMaxPath();
            var path = ReconstructMaxPath();
            Console.WriteLine($"{path.Sum(x => x)} = {string.Join(" + ", path)}");
        }
        #endregion

        public static void Main()
        {
            //MySolution();
            JudgeSolution();
        }
    }
}
