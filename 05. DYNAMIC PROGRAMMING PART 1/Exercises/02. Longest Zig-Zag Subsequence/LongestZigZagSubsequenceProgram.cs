namespace _02._Longest_Zig_Zag_Subsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class LongestZigZagSubsequenceProgram
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var lenEven = new int[numbers.Length];
            var lenOdd = new int[numbers.Length];

            var prevEven = new int[numbers.Length];
            var prevOdd = new int[numbers.Length];

            FindZigZagSubsequences(numbers, lenEven, lenOdd, prevEven, prevOdd);
            PrintLongestZigZagSubsequence(lenOdd, lenEven, prevOdd, numbers, prevEven);
        }

        private static void FindZigZagSubsequences(int[] numbers, int[] lenEven, int[] lenOdd, int[] prevEven, int[] prevOdd)
        {
            for (var current = 0; current < numbers.Length; current++)
            {
                var currentMaxEvenLength = 1;
                var currentMaxOddLength = 1;
                var prevEvenIndex = -1;
                var prevOddIndex = -1;

                var currentNumber = numbers[current];

                for (var prev = 0; prev < current; prev++)
                {
                    var prevNumber = numbers[prev];
                    var prevEvenSolutionLength = lenEven[prev];
                    var prevOddSolutionLength = lenOdd[prev];

                    if (prevEvenSolutionLength % 2 == 0)
                    {
                        if (currentNumber > prevNumber &&
                            currentMaxEvenLength <= prevEvenSolutionLength)
                        {
                            currentMaxEvenLength = prevEvenSolutionLength + 1;
                            prevEvenIndex = prev;
                        }
                    }
                    else
                    {
                        if (currentNumber < prevNumber &&
                            currentMaxEvenLength <= prevEvenSolutionLength)
                        {
                            currentMaxEvenLength = prevEvenSolutionLength + 1;
                            prevEvenIndex = prev;
                        }
                    }

                    if (prevOddSolutionLength % 2 == 0)
                    {
                        if (currentNumber < prevNumber &&
                            currentMaxOddLength <= prevOddSolutionLength)
                        {
                            currentMaxOddLength = prevOddSolutionLength + 1;
                            prevOddIndex = prev;
                        }
                    }
                    else
                    {
                        if (currentNumber > prevNumber &&
                            currentMaxOddLength <= prevOddSolutionLength)
                        {
                            currentMaxOddLength = prevOddSolutionLength + 1;
                            prevOddIndex = prev;
                        }
                    }
                }

                lenEven[current] = currentMaxEvenLength;
                lenOdd[current] = currentMaxOddLength;
                prevEven[current] = prevEvenIndex;
                prevOdd[current] = prevOddIndex;
            }
        }

        private static void PrintLongestZigZagSubsequence(int[] lenOdd, int[] lenEven, int[] prevOdd, int[] numbers, int[] prevEven)
        {
            var lenOddMax = lenOdd.Max();
            var lenEvenMax = lenEven.Max();

            List<int> result;

            if (lenOddMax > lenEvenMax)
            {
                var index = GetMaxElementIndex(lenOdd);
                result = ReconstructSequence(index, prevOdd, numbers);
            }
            else
            {
                var index = GetMaxElementIndex(lenEven);
                result = ReconstructSequence(index, prevEven, numbers);
            }

            Console.WriteLine(string.Join(" ", result));
        }

        private static List<int> ReconstructSequence(int index, int[] prev, int[] numbers)
        {
            var result = new List<int>();

            while (index != -1)
            {
                var current = numbers[index];
                index = prev[index];

                result.Add(current);
            }

            result.Reverse();
            return result;
        }

        private static int GetMaxElementIndex(int[] arr)
        {
            var max = -1;
            var index = -1;

            if (arr.Length == 1)
            {
                return 0;
            }

            if (arr.Length == 0)
            {
                return -1;
            }

            for (var i = 0; i < arr.Length; i++)
            {
                var current = arr[i];

                if (current > max)
                {
                    index = i;
                    max = current;
                }
            }

            return index;
        }
    }
}
