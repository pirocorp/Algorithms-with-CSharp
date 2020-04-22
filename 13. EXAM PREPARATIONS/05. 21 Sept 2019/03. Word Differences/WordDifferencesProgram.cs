namespace _03._Word_Differences
{
    using System;

    public static class WordDifferencesProgram
    {
        private static int CalculateLcs(string first, string second)
        {
            var lcs = new int[first.Length + 1, second.Length + 1];
            var max = 0;

            for (var row = 1; row <= first.Length; row++)
            {
                for (var col = 1; col <= second.Length; col++)
                {
                    var up = lcs[row - 1, col];
                    var left = lcs[row, col - 1];

                    var result = Math.Max(up, left);

                    if (first[row - 1] == second[col - 1])
                    {
                        var diagonal = lcs[row - 1, col - 1] + 1;
                        result = Math.Max(diagonal, result);
                    }

                    lcs[row, col] = result;

                    if (result > max)
                    {
                        max = result;
                    }
                }
            }

            return max;
        }

        public static void Main()
        {
            var firstString = Console.ReadLine();
            var secondString = Console.ReadLine();

            var maxLcs = CalculateLcs(firstString, secondString);
            var result = (firstString.Length - maxLcs) * 2L;
            Console.WriteLine($"Deletions and Insertions: {result}");
        }
    }
}
