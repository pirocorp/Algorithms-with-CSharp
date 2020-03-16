using System;
using System.Collections.Generic;
using System.Linq;

public class SumOfCoins
{
    public static void Main(string[] args)
    {
        var availableCoins = new[] { 1, 2, 5, 10, 20, 50 };
        var targetSum = 923;

        var selectedCoins = ChooseCoins(availableCoins, targetSum);

        Console.WriteLine($"Number of coins to take: {selectedCoins.Values.Sum()}");
        foreach (var selectedCoin in selectedCoins)
        {
            Console.WriteLine($"{selectedCoin.Value} coin(s) with value {selectedCoin.Key}");
        }
    }

    public static Dictionary<int, int> ChooseCoins(IList<int> coins, int targetSum)
    {
        var result = new Dictionary<int, int>();
        var sortedCoins = coins.OrderByDescending(x => x).ToList();

        for (var i = 0; i < coins.Count; i++)
        {
            var currentCoin = sortedCoins[i];
            var count = targetSum / currentCoin;
            targetSum %= currentCoin;
            result.Add(currentCoin, count);
        }

        if (targetSum != 0)
        {
            throw new InvalidOperationException();
        }

        return result
            .Where(x => x.Value != 0)
            .ToDictionary(x => x.Key, x => x.Value);
    }
}