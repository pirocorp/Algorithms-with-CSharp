namespace _03._Path_Finder;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static readonly Dictionary<int, HashSet<int>> graph;

    static Program()
    {
        graph = new Dictionary<int, HashSet<int>>();
    }

    public static void Main()
    {
        ReadInput();

        CheckPaths();
    }

    private static void ReadInput()
    {
        var n = ReadNumberFromConsole();

        for (var i = 0; i < n; i++)
        {
            graph[i] = ReadSequenceFromConsole().ToHashSet();
        }
    }

    private static void CheckPaths()
    {
        var n = int.Parse(Console.ReadLine() ?? "0");

        for (var i = 0; i < n; i++)
        {
            CheckPath();
        }
    }

    private static int ReadNumberFromConsole()
    {
        return int.Parse(Console.ReadLine() ?? "0");
    }

    private static IEnumerable<int> ReadSequenceFromConsole()
        => (Console.ReadLine() ?? string.Empty)
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToArray();

    private static void CheckPath()
    {
        var path = ReadSequenceFromConsole();
        var queue = new Queue<int>();

        foreach (var node in path)
        {
            queue.Enqueue(node);
        }

        var children = graph[queue.Dequeue()];

        var result = "yes";

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (!children.Contains(current))
            {
                result = "no";
                break;
            }

            children = graph[current];
        }

        Console.WriteLine(result);
    }
}
