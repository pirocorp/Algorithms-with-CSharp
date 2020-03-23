namespace _05._Sum_with_Limited_Coins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SumLimitedCoinsProgram
    {
        private static int[] _coins;

        public static void Main()
        {
            _coins = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int targetSum = int.Parse(Console.ReadLine());
            var sums = CalcSums();
            Console.WriteLine(sums[targetSum].Count);
        }

        private static Dictionary<int, List<int>> CalcSums()
        {
            //sum, how we came here
            var result = new Dictionary<int, List<int>>
            {
                [0] = new List<int> {0}
            };
            
            foreach (int current in _coins)
            {
                foreach (int prevSum in result.Keys.ToArray())
                {
                    int newSum = prevSum + current;

                    if (!result.ContainsKey(newSum))
                    {
                        result[newSum] = new List<int>();
                    }

                    result[newSum].Add(current);
                }
            }

            return result;
        }
    }
}
