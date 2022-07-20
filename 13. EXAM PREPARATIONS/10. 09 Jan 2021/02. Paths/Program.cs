namespace _02._Paths;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static int n;

    private static readonly Dictionary<int, HashSet<int>> graph;

    static Program()
    {
        graph = new Dictionary<int, HashSet<int>>();
    }

    public static void Main()
    {
        ReadInput();

        GeneratePaths();
    }

    private static void ReadInput()
    {
        n = ReadNumberFromConsole();

        for (var i = 0; i < n; i++)
        {
            graph[i] = ReadSequenceFromConsole().ToHashSet();
        }
    }

    private static int ReadNumberFromConsole()
        => int.Parse(Console.ReadLine() ?? "0");

    private static IEnumerable<int> ReadSequenceFromConsole()
        => (Console.ReadLine() ?? string.Empty)
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToArray();

    private static void GeneratePaths()
    {
        for (var start = 0; start < n - 1; start++)
        {
            GenerateAllPathsBetweenNodes(start, n - 1);
        }
    }

    private static void GenerateAllPathsBetweenNodes(int start, int end)
    {
        var visited = new bool[n];
        var pathList = new List<int> { start };

        PathsGenerator(start, end, visited, pathList);
    }

    private static void PathsGenerator(
        int current, 
        int destination, 
        IList<bool> visited, 
        ICollection<int> pathList)
    {
        if (current == destination)
        {
            Console.WriteLine(string.Join(" ", pathList));
            return;
        }

        visited[current] = true;

        foreach (var child in graph[current])
        {
            if (visited[child])
            {
                continue;
            }

            pathList.Add(child);
            PathsGenerator(child, destination, visited, pathList);
            pathList.Remove(child);
        }

        visited[current] = false;
    }
}
