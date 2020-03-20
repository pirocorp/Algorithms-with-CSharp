namespace _02._Subset_Sum
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    //No Repetition
    public static class SubsetSumProgram
    {
        private static int[] _numbers = { 3, 5, 1, 4, 2 };
        private static Dictionary<int, int> _sums;

        
        public static void Main()
        {
            _sums = CalcSums();
            //Console.WriteLine(string.Join(" ", _sums.Keys.ToArray().OrderBy(x => x)));
            
            var targetSum = 12;

            List<int> result = ReconstructSolution(targetSum);
            Console.WriteLine($"{targetSum} = {string.Join(" + ", result)}");
        }

        private static List<int> ReconstructSolution(int targetSum)
        {
            if (!_sums.ContainsKey(targetSum))
            {
                return new List<int>();
            }

            var result = new List<int>();

            while (targetSum > 0)
            {
                int number = _sums[targetSum];
                result.Add(number);
                targetSum -= number;
            }

            result.Reverse();
            return result;
        }

        public static Dictionary<int, int> CalcSums()
        {
            var result = new Dictionary<int, int>();

            result.Add(0, 0); //sum, how we came here

            for (var i = 0; i < _numbers.Length; i++)
            {
                int current = _numbers[i];

                foreach (int prevSum in result.Keys.ToArray())
                {
                    int newSum = prevSum + current;

                    if (!result.ContainsKey(newSum))
                    {
                        result.Add(newSum, current);
                    }
                }
            }

            return result;
        }
    }
}
