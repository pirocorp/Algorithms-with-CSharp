namespace _09._Set_Cover;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static HashSet<int> universe = new HashSet<int>();

    private static List<int[]> sets = new List<int[]>();

    private static readonly List<int[]> tackedSets = new List<int[]>();

    public static void Main()
    {
        ReadInput();

        TakeSets();

        PrintOutput();
    }

    private static void ReadInput()
    {
        universe = (Console.ReadLine() ?? string.Empty)
            .Split(", ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToHashSet();

        var n = int.Parse(Console.ReadLine() ?? "0");

        for (var i = 0; i < n; i++)
        {
            var currentSet = (Console.ReadLine() ?? string.Empty)
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Select(int.Parse)
                .ToArray();

            sets.Add(currentSet);
        }
    }

    private static void TakeSets()
    {
        while (universe.Count > 0)
        {
            sets = sets
                .OrderByDescending(s => s.Intersect(universe).Count())
                .ToList();

            var currentSet = sets.First();

            tackedSets.Add(currentSet);
            sets.Remove(currentSet);

            foreach (var element in currentSet)
            {
                universe.Remove(element);
            }
        }
    }

    private static void PrintOutput()
    {
        Console.WriteLine($"Sets to take ({tackedSets.Count}):");

        foreach (var tackedSet in tackedSets)
        {
            Console.WriteLine(string.Join(", ", tackedSet));
        }
    }
}
