namespace _04._Sum_with_Unlimited_Coins
{
    using System;
    using System.Linq;

    public static class SumUnlimitedCoinsProgram
    {
        private static int[] _coins;

        public static void Main()
        {
            _coins = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int targetSum = int.Parse(Console.ReadLine());

            int result = CalculateSums(_coins.Length, targetSum);
            Console.WriteLine(result);
        }

        private static int CalculateSums(int coinsCount, int targetSum)
        {
            // If targetSum is 0 then there is 1 solution  
            // (do not include any coin) 
            if (targetSum == 0)
            {
                return 1;
            }

            // If targetSum is less than 0 then no  
            // solution exists 
            if (targetSum < 0)
            {
                return 0;
            }

            // If there are no coins and targetSum  
            // is greater than 0, then no 
            // solution exist 
            if (coinsCount <= 0 && targetSum >= 1)
            {
                return 0;
            }

            // count is sum of solutions (i)  
            // including _coins[coinsCount-1] (ii) excluding _coins[coinsCount-1] 
            return CalculateSums(coinsCount - 1, targetSum) +
                   CalculateSums(coinsCount, targetSum - _coins[coinsCount - 1]);
        }
    }
}
