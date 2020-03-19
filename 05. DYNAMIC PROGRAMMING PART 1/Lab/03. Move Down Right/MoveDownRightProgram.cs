namespace _03._Move_Down_Right
{
    using System;
    using System.Collections.Generic;

    public static class MoveDownRightProgram
    {
        public static void Main()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            var numbers = ReadNumbers(rows, cols);
            var sums = new int[rows, cols];

            CalculateSums(sums, numbers, rows, cols);
            //PrintMatrix(sums);

            var result = GetPath(sums, numbers, rows, cols);
            Console.WriteLine(string.Join(" ", result));
        }

        private static List<string> GetPath(int[,] sums, int[,] numbers, int rows, int cols)
        {
            var result = new List<string>();

            var currentRow = rows - 1;
            var currentCol = cols - 1;

            result.Add($"[{currentRow}, {currentCol}]");

            while (currentRow != 0 && currentCol != 0)
            {
                var top = sums[currentRow - 1, currentCol];
                var left = sums[currentRow, currentCol - 1];

                if (top > left)
                {
                    result.Add($"[{currentRow - 1}, {currentCol}]");
                    currentRow--;
                }
                else
                {
                    result.Add($"[{currentRow}, {currentCol - 1}]");
                    currentCol--;
                }
            }

            while (currentRow != 0)
            {
                result.Add($"[{--currentRow}, 0]");
            }

            while (currentCol != 0)
            {
                result.Add($"[0, {--currentCol}]");
            }

            result.Reverse();
            return result;
        }

        private static void CalculateSums(int[,] sums, int[,] numbers, int rows, int cols)
        {
            sums[0, 0] = numbers[0, 0];

            CalculateFirstColumn(sums, numbers, rows);
            CalculateFirstRow(sums, numbers, cols);
            CalculateRest(sums, numbers, rows, cols);
        }

        private static void CalculateRest(int[,] sums, int[,] numbers, int rows, int cols)
        {
            for (var row = 1; row < rows; row++)
            {
                for (var col = 1; col < cols; col++)
                {
                    var result =
                        Math.Max(sums[row - 1, col], sums[row, col - 1]) +
                        numbers[row, col];

                    sums[row, col] = result;
                }
            }
        }

        private static void CalculateFirstRow(int[,] sums, int[,] numbers, int cols)
        {
            for (var col = 1; col < cols; col++)
            {
                var prevSum = sums[0, col - 1];
                var currentNumber = numbers[0, col];
                sums[0, col] = prevSum + currentNumber;
            }
        }

        private static void CalculateFirstColumn(int[,] sums, int[,] numbers, int rows)
        {
            for (var row = 1; row < rows; row++)
            {
                var prevSum = sums[row - 1, 0];
                var currentNumber = numbers[row, 0];
                sums[row, 0] = prevSum + currentNumber;
            }
        }

        private static int[,] ReadNumbers(int rows, int cols)
        {
            var numbers = new int[rows, cols];

            for (var row = 0; row < rows; row++)
            {
                var line = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (var col = 0; col < cols; col++)
                {
                    numbers[row, col] = int.Parse(line[col]);
                }
            }

            return numbers;
        }

        private static void PrintMatrix(int[,] sums)
        {
            for (var row = 0; row < sums.GetLength(0); row++)
            {
                for (var col = 0; col < sums.GetLength(1); col++)
                {
                    Console.Write($"{sums[row, col].ToString().PadLeft(3)} ");
                }

                Console.WriteLine();
            }
        }

    }
}
