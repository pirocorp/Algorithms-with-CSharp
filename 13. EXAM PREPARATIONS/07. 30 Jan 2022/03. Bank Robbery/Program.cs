namespace _03._Bank_Robbery;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static int[] set;

    private static readonly Dictionary<int, int> possibleSums;

    static Program()
    {
        set = Array.Empty<int>();
        possibleSums = new Dictionary<int, int>();
    }

    public static void Main()
    {
        ReadInput();

        CalculatePossibleSums();

        var target = DivideSet();

        PrintOutput(target);
    }

    private static void ReadInput()
    {
        set = (Console.ReadLine() ?? string.Empty)
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToArray();

        possibleSums.Add(0, 0);
    }

    private static void CalculatePossibleSums()
    {
        foreach (var currentElement in set)
        {
            var currentSums = possibleSums.Keys.ToArray();

            foreach (var sum in currentSums)
            {
                var newSum = currentElement + sum;

                if (!possibleSums.ContainsKey(newSum))
                {
                    possibleSums.Add(newSum, currentElement);
                }
            }
        }
    }

    private static int DivideSet()
    {
        var maxPossibleSum = set.Sum();

        var target = maxPossibleSum / 2;

        var delta = maxPossibleSum;
        var best = 0;

        foreach (var possibleSum in possibleSums.Keys)
        {
            var newDelta = Math.Abs(target - possibleSum);

            if (newDelta < delta)
            {
                best = possibleSum;
                delta = newDelta;
            }
        }

        return best;
    }

    private static void PrintOutput(int target)
    {
        var subSet = FindSubset(target).ToHashSet();
        var subSet2 = set
            .Where(x => !subSet.Contains(x))
            .OrderBy(x => x)
            .ToArray();

        Console.WriteLine(string.Join(" ", subSet));
        Console.WriteLine(string.Join(" ", subSet2));
    }

    private static IEnumerable<int> FindSubset(int targetSum)
    {
        var subset = new List<int>();

        while (targetSum > 0)
        {
            var lastNum = possibleSums[targetSum];
            subset.Add(lastNum);
            targetSum -= lastNum;
        }

        // subset.Reverse();
        return subset.OrderBy(x => x);
    }
}
