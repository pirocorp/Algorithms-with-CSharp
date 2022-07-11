namespace _03._Sum_with_Unlimited_Coins;

using System;
using System.Linq;

public static class Program
{
    private static int[] set;

    private static int targetSum;

    private static int[] combinations;

    static Program()
    {
        set = Array.Empty<int>();
        combinations = Array.Empty<int>();
    }

    public static void Main()
    {
        ReadInput();

        CalculatePossibleSums();
        
        PrintOutput();
    }

    private static void ReadInput()
    {
        set = (Console.ReadLine() ?? string.Empty)
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToArray();

        targetSum = int.Parse(Console.ReadLine() ?? "0");
    }

    private static void PrintOutput() => Console.WriteLine(combinations[^1]);

    private static void CalculatePossibleSums()
    {
        combinations = new int[targetSum + 1];
        combinations[0] = 1;

        foreach (var item in set)
        {
            for (var i = 0; i < combinations.Length; i++)
            {
                if (i >= item)
                {
                    combinations[i] += combinations[i - item];
                }
            }
        }
    }
}
