namespace _02._Longest_Increasing_Subsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class LongestIncreasingSubsequenceProgram
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var lengths = new int[numbers.Length];
            var previous = new int[numbers.Length];

            var maxIssLength = 0;
            var maxIssIndex = -1;

            for (var current = 0; current < numbers.Length; current++)
            {
                var maxLength = 1;
                var prevIndex = -1;
                var currentNumber = numbers[current];

                for (var prev = 0; prev < current; prev++)
                {
                    var prevNumber = numbers[prev];
                    var prevSolutionLength = lengths[prev];

                    if (currentNumber > prevNumber &&
                        maxLength <= prevSolutionLength)
                    {
                        maxLength = prevSolutionLength + 1;
                        prevIndex = prev;
                    }
                }

                lengths[current] = maxLength;
                previous[current] = prevIndex;

                if (maxLength > maxIssLength)
                {
                    maxIssLength = maxLength;
                    maxIssIndex = current;
                }
            }

            //Console.WriteLine(string.Join(" ", lengths));
            //Console.WriteLine(string.Join(" ", previous));
            //Console.WriteLine($"Max Increasing Subsequence Length Is: {maxIssLength}");

            var index = maxIssIndex;

            var result = new List<int>();

            while (index != -1)
            {
                var current = numbers[index];
                index = previous[index];

                result.Add(current);
            }

            result.Reverse();
            Console.WriteLine(string.Join(" ", result));
        }
    }
}
