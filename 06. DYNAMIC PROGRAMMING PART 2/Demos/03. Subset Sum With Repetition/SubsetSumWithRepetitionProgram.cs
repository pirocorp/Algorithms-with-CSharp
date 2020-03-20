namespace _03._Subset_Sum_With_Repetition
{
    using System;
    using System.Collections.Generic;

    public static class SubsetSumWithRepetitionProgram
    {
        private static int[] _numbers = { 3, 5, 2 };
        private static bool[] _sums;

        public static void Main()
        {
            var targetSum = 16;
            CalculateSums(targetSum);

            Console.WriteLine(_sums[targetSum]);

            List<int> result = ReconstructSolution(targetSum);
            Console.WriteLine($"{targetSum} = {string.Join(" + ", result)}");
        }

        private static List<int> ReconstructSolution(int targetSum)
        {
            var result = new List<int>();

            while (targetSum != 0)
            {
                for (var i = 0; i < _numbers.Length; i++)
                {
                    int currentNumber = _numbers[i];
                    int possibleSum = targetSum - currentNumber;

                    if (possibleSum >= 0 && _sums[possibleSum])
                    {
                        result.Add(currentNumber);
                        targetSum -= currentNumber;
                    }
                }
            }

            return result;
        }

        private static void CalculateSums(int targetSum)
        {
            _sums = new bool[targetSum + 1];
            _sums[0] = true; //Sentinel

            for (var sum = 0; sum < _sums.Length; sum++)
            {
                if (_sums[sum])
                {
                    for (var i = 0; i < _numbers.Length; i++)
                    {
                        int newSum = sum + _numbers[i];

                        if (newSum <= targetSum)
                        {
                            _sums[newSum] = true;
                        }
                    }
                }
            }
        }
    }
}
