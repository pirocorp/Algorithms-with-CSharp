namespace _04._Rod_Cutting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class RodCuttingProgram
    {
        private static int _n;
        private static int[] _pricePerLength;

        private static int[] _bestPrices;
        private static int[] _bestPrev;

        public static void Main()
        {
            ReadInput();
            Initialize();
            CalculateBestRodCuts();

            var result = ReconstructSolution();
            Console.WriteLine(_bestPrices[_n]);
            Console.WriteLine(string.Join(" ", result));
        }

        private static List<int> ReconstructSolution()
        {
            var n = _n;
            var result = new List<int>();

            while (n != 0)
            {
                result.Add(_bestPrev[n]);
                n -= _bestPrev[n];
            }

            return result;
        }

        private static void CalculateBestRodCuts()
        {
            for (var currentLength = 2; currentLength <= _n; currentLength++)
            {
                var currentBestPrice = _pricePerLength[currentLength];
                var currentBestLength = currentLength;

                for (var cutLength = 1; cutLength <= currentLength / 2; cutLength++)
                {
                    var partOneSize = currentLength - cutLength;
                    var partTwoSize = cutLength;

                    var partOneBestPrice = _bestPrices[partOneSize];
                    var partTwoBestPrice = _bestPrices[partTwoSize];

                    var currentSum = partOneBestPrice + partTwoBestPrice;

                    if (currentSum > currentBestPrice)
                    {
                        currentBestPrice = currentSum;
                        currentBestLength = cutLength;
                    }
                }

                _bestPrices[currentLength] = currentBestPrice;
                _bestPrev[currentLength] = currentBestLength;
            }
        }

        private static void Initialize()
        {
            _bestPrices = new int[_n + 1];
            _bestPrev = new int[_n + 1];

            _bestPrices[1] = _pricePerLength[1];
            _bestPrev[1] = 1;
        }

        private static void ReadInput()
        {
            _pricePerLength = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            _n = int.Parse(Console.ReadLine());
        }
    }
}
