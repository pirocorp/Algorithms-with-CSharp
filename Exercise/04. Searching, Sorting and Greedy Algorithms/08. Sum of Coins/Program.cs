namespace _08._Sum_of_Coins;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Program
{
    private static int[] coinTypes = Array.Empty<int>();

    private static int target = 0;

    private static readonly Dictionary<int, int> coins = new Dictionary<int, int>();

    public static void Main(string[] args)
    {
        ReadInput();

        SumCoins();

        PrintOutput();
    }

    private static void ReadInput()
    {
        coinTypes = (Console.ReadLine() ?? string.Empty)
            .Split(", ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(int.Parse)
            .OrderByDescending(x => x)
            .ToArray();

        target = int.Parse(Console.ReadLine() ?? "0");
    }

    private static void SumCoins()
    {
        foreach (var coin in coinTypes)
        {
            var tackedCoins = target / coin;
            target %= coin;

            if (tackedCoins > 0)
            {
                coins[coin] = tackedCoins;
            }
        }
    }

    private static void PrintOutput()
    {
        if (target != 0)
        {
            Console.WriteLine("Error");
            return;
        }

        var totalCoins = coins.Sum(x => x.Value);

        Console.WriteLine($"Number of coins to take: {totalCoins}");

        var result = new StringBuilder();

        coins
            .OrderByDescending(x => x.Key)
            .ToList()
            .ForEach(x => result.AppendLine($"{x.Value} coin(s) with value {x.Key}"));

        Console.WriteLine(result);
    }
}
