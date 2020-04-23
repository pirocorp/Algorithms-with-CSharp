namespace _02._Mirror_String
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MirrorStringProgram
    {
        private static int[,] _lcs;

        private static void CalculateLcs(string first, string second)
        {
            _lcs = new int[first.Length + 1, second.Length + 1];

            for (var row = 1; row <= first.Length; row++)
            {
                for (var col = 1; col <= second.Length; col++)
                {
                    var up = _lcs[row - 1, col];
                    var left = _lcs[row, col - 1];

                    var result = Math.Max(up, left);

                    if (first[row - 1] == second[col - 1])
                    {
                        var diagonal = _lcs[row - 1, col - 1] + 1;
                        result = Math.Max(diagonal, result);
                    }

                    _lcs[row, col] = result;
                }
            }
        }

        private static List<char> ReconstructSolution(string first, string second)
        {
            var currentRow = first.Length;
            var currentCol = second.Length;

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

        public static void Main()
        {
            var inputString = Console.ReadLine();
            var reversedString = new string(inputString.ToCharArray().Reverse().ToArray());

            CalculateLcs(reversedString, inputString);
            var result = ReconstructSolution(reversedString, inputString);

            Console.WriteLine(string.Join("", result));
        }
    }
}
