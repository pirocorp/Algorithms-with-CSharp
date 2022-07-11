namespace _04._Sum_with_Limited_Coins;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static int[] set;

    private static int targetSum;

    private static readonly Dictionary<int, List<int>> possibleSums;

    static Program()
    {
        set = Array.Empty<int>();
        possibleSums = new Dictionary<int, List<int>>();
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

    private static void CalculatePossibleSums()
    {
        possibleSums.Add(0, new List<int>());

        foreach (var currentElement in set)
        {
            var currentSums = possibleSums.Keys.ToArray();

            foreach (var sum in currentSums)
            {
                var newSum = currentElement + sum;

                if (!possibleSums.ContainsKey(newSum))
                {
                    possibleSums.Add(newSum, new List<int>());
                }

                possibleSums[newSum].Add(currentElement);
            }
        }
    }

    private static void PrintOutput() 
        => Console.WriteLine(
            possibleSums.ContainsKey(targetSum) 
                ? possibleSums[targetSum].Count 
                : 0);
}
