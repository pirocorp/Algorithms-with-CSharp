namespace _02._Stability_Check
{
    using System;
    using System.Linq;
    using System.Numerics;

    public static class StabilityCheckProgram
    {
        private static int _size;
        private static int[,] _matrix;

        private static BigInteger[,] _stripsMatrix;

        private static void ReadInput()
        {
            _size = int.Parse(Console.ReadLine());
            var matrixSize = int.Parse(Console.ReadLine());

            _matrix = new int[matrixSize, matrixSize];

            for (var row = 0; row < matrixSize; row++)
            {
                var rowInput = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                for (var col = 0; col < rowInput.Length; col++)
                {
                    _matrix[row, col] = rowInput[col];
                }
            }
        }

        private static BigInteger GetCurrentSubMatrixValue(int row, int col)
        {
            BigInteger result = 0;

            for (var rowIndex = row; rowIndex < row + _size; rowIndex++)
            {
                for (var colIndex = col; colIndex < col + _size; colIndex++)
                {
                    result += _matrix[rowIndex, colIndex];
                }
            }

            return result;
        }

        private static void SlowSolution()
        {
            BigInteger max = long.MinValue;

            for (var row = 0; row <= _matrix.GetLength(0) - _size; row++)
            {
                for (var col = 0; col <= _matrix.GetLength(1) - _size; col++)
                {
                    var current = GetCurrentSubMatrixValue(row, col);

                    if (current > max)
                    {
                        max = current;
                    }
                }
            }

            Console.WriteLine(max);
        }

        private static void CalculateStripMatrix()
        {
            var matrixSize = _matrix.GetLength(0);
            var stripMatrixRows = matrixSize - _size + 1;

            _stripsMatrix = new BigInteger[stripMatrixRows, matrixSize];

            for (var col = 0; col < matrixSize; col++)
            {
                // Calculate sum of first k x 1 rectangle  
                // in this column  
                BigInteger sum = 0;
                for (var row = 0; row < _size; row++)
                {
                    sum += _matrix[row, col];
                }

                _stripsMatrix[0, col] = sum;

                // Calculate sum of remaining rectangles  
                for (var row = 1; row < stripMatrixRows; row++)
                {
                    sum += (_matrix[row + _size - 1, col] - _matrix[row - 1, col]);
                    _stripsMatrix[row, col] = sum;
                }
            }
        }

        private static BigInteger FindMaxSubMatrixSum()
        {
            BigInteger maxSum = long.MinValue;

            // CALCULATE SUM of Sub-Squares using stripSum[,]
            var matrixSize = _matrix.GetLength(0);
            var stripMatrixRows = matrixSize - _size + 1;

            for (var row = 0; row < stripMatrixRows; row++)
            {
                // Calculate first sub matrix sum 
                BigInteger sum = 0;
                for (var col = 0; col < _size; col++)
                {
                    sum += _stripsMatrix[row, col];
                }

                if (sum > maxSum)
                {
                    maxSum = sum;
                }

                // Calculate sum of remaining squares in  
                // current row by removing the leftmost  
                // strip of previous sub-square and adding  
                // a new strip  
                for (var col = 1; col < stripMatrixRows; col++)
                {
                    sum += (_stripsMatrix[row, col + _size - 1] - _stripsMatrix[row, col - 1]);

                    if (sum > maxSum)
                    {
                        maxSum = sum;
                    }
                }
            }

            return maxSum;
        }

        public static void Main()
        {
            ReadInput();

            //SlowSolution();

            CalculateStripMatrix();

            var maxSum = FindMaxSubMatrixSum();
            Console.WriteLine(maxSum);
        }
    }
}
