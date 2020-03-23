namespace _04._Longest_Common_Subsequence
{
    using System;
    using System.Collections.Generic;

    public static class LongestCommonSubsequenceProgram
    {
        private static int[,] _lcs;

        public static void Main()
        {
            string first = Console.ReadLine(); //rows
            string second = Console.ReadLine(); //cols

            CalculateLcs(first, second);
            //PrintMatrix(_lcs);
            List<char> result = ReconstructSolution(first, second);

            Console.WriteLine(result.ToArray().Length);
        }

        private static List<char> ReconstructSolution(string first, string second)
        {
            int currentRow = first.Length;
            int currentCol = second.Length;

            var result = new List<char>();

            while (currentRow > 0 && currentCol > 0)
            {
                if (first[currentRow - 1] == second[currentCol - 1] &&
                    _lcs[currentRow, currentCol] - 1 == _lcs[currentRow - 1, currentCol - 1])
                {
                    result.Add(first[currentRow - 1]);
                    currentRow--;
                    currentCol--;
                }
                else if (_lcs[currentRow - 1, currentCol] == _lcs[currentRow, currentCol])
                {
                    currentRow--;
                }
                else
                {
                    currentCol--;
                }
            }

            result.Reverse();
            return result;
        }

        private static void CalculateLcs(string first, string second)
        {
            _lcs = new int[first.Length + 1, second.Length + 1];

            for (var row = 1; row <= first.Length; row++)
            {
                for (var col = 1; col <= second.Length; col++)
                {
                    int up = _lcs[row - 1, col];
                    int left = _lcs[row, col - 1];

                    int result = Math.Max(up, left);

                    if (first[row - 1] == second[col - 1])
                    {
                        int diagonal = _lcs[row - 1, col - 1] + 1;
                        result = Math.Max(diagonal, result);
                    }

                    _lcs[row, col] = result;
                }
            }
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    var element = _lcs[row, col].ToString().PadLeft(3);
                    Console.Write($"{element}");
                }

                Console.WriteLine();
            }
        }
    }
}
