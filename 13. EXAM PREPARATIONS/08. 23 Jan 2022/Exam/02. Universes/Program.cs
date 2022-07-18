namespace _02._Universes;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static readonly Dictionary<string, List<string>> multiverse;

    private static readonly HashSet<string> visited;

    static Program()
    {
        multiverse = new Dictionary<string, List<string>>();

        visited = new HashSet<string>();
    }

    public static void Main()
    {
        ReadInput();

        var count = CountUniverses();

        Console.WriteLine(count);
    }

    private static void ReadInput()
    {
        var n = int.Parse(Console.ReadLine() ?? "0");

        for (var i = 0; i < n; i++)
        {
            var tokens = (Console.ReadLine() ?? string.Empty)
                .Split(" - ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();

            var source = tokens[0];
            var destination = tokens[1];

            if (!multiverse.ContainsKey(source))
            {
                multiverse[source] = new List<string>();
            }

            if (!multiverse.ContainsKey(destination))
            {
                multiverse[destination] = new List<string>();
            }

            multiverse[source].Add(destination);
            multiverse[destination].Add(source);
        }
    }

    private static int CountUniverses()
    {
        var count = 0;

        foreach (var system in multiverse.Keys)
        {
            if (visited.Contains(system))
            {
                continue;
            }

            count++;

            VisitUniverse(system);
        }

        return count;
    }

    private static void VisitUniverse(string system)
    {
        var queue = new Queue<string>();
        queue.Enqueue(system);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            visited.Add(current);

            var notVisitedSystems = multiverse[current]
                .Where(s => !visited.Contains(s));

            foreach (var newSystem in notVisitedSystems)
            {
                queue.Enqueue(newSystem);
            }
        }
    }
}
