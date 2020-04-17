namespace _02._Cable_Merchant
{
    using System;
    using System.Linq;

    public static class CableMerchantProgram
    {
        public static void Main()
        {
            var costs = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var connectorsPrice = int.Parse(Console.ReadLine());
            
            var bestPrice = new int[costs.Length + 1];

            bestPrice[1] = costs[0];

            for (var currentLength = 2; currentLength < bestPrice.Length; currentLength++)
            {
                var best = costs[currentLength - 1];

                for (var cutLength = 1; cutLength <= currentLength / 2; cutLength++)
                {
                    var current = bestPrice[cutLength] + bestPrice[currentLength - cutLength] - 2 * connectorsPrice;

                    if (best < current)
                    {
                        best = current;
                    }
                }

                bestPrice[currentLength] = best;
            }

            Console.WriteLine(string.Join(" ", bestPrice.Skip(1)));
        }
    }
}