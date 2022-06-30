namespace _02._Topological_Sorting;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static readonly Dictionary<string, List<string>> graph;

    private static readonly Dictionary<string, int> predecessorCount;

    private static readonly HashSet<string> visited;

    private static readonly HashSet<string> cycles;

    static Program()
    {
        graph = new Dictionary<string, List<string>>();
        predecessorCount = new Dictionary<string, int>();

        visited = new HashSet<string>();
        cycles = new HashSet<string>();
    }

    public static void Main()
    {
        ReadInput();

        GetPredecessorCounts();

        var sorted = TopologicalSort();

        PrintOutput(sorted);
    }

    private static void ReadInput()
    {
        var n = int.Parse(Console.ReadLine() ?? "0");

        for (var i = 0; i < n; i++)
        {
            var input = (Console.ReadLine() ?? string.Empty)
                .Split("->", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();

            var vertex = input[0];

            graph[vertex] = new List<string>();

            if (input.Length <= 1)
            {
                continue;
            }

            var children = input[1]
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            foreach (var child in children)
            {
                graph[vertex].Add(child);
            }
        }
    }

    private static void GetPredecessorCounts()
    {
        foreach (var (vertex, children) in graph)
        {
            if (!predecessorCount.ContainsKey(vertex))
            {
                predecessorCount[vertex] = 0;
            }

            foreach (var child in children)
            {
                if (!predecessorCount.ContainsKey(child))
                {
                    predecessorCount[child] = 0;
                }

                predecessorCount[child] += 1;
            }
        }
    }

    private static List<string> TopologicalSort()
    {
        var sorted = new List<string>();

        while (predecessorCount.Count > 0)
        {
            var node = predecessorCount
                .FirstOrDefault(s => s.Value == 0);

            var vertex = node.Key;

            if (vertex is null)
            {
                break;
            }

            var children = graph[vertex];

            foreach (var child in children)
            {
                predecessorCount[child]--;
            }

            sorted.Add(vertex);
            predecessorCount.Remove(vertex);
        }

        return sorted;
    }

    private static List<string> TopSortDfs()
    {
        var sorted = new LinkedList<string>();

        predecessorCount.Clear();

        foreach (var node in graph.Keys)
        {
            try
            {
                TopSortDfs(node, sorted);
            }
            catch (InvalidOperationException)
            {
                predecessorCount.Add("Error", 1);
                break;
            }
        }

        return sorted.ToList();
    }

    private static void TopSortDfs(string node, LinkedList<string> sorted)
    {
        if (cycles.Contains(node))
        {
            throw new InvalidOperationException();
        }

        if (visited.Contains(node))
        {
            return;
        }

        visited.Add(node);
        cycles.Add(node);

        var children = graph[node];

        foreach (var child in children)
        {
            TopSortDfs(child, sorted);
        }

        cycles.Remove(node);
        sorted.AddFirst(node);
    }

    private static void PrintOutput(List<string> sorted)
    {
        var response = predecessorCount.Count > 0
            ? "Invalid topological sorting"
            : $"Topological sorting: {string.Join(", ", sorted)}";

        Console.WriteLine(response);
    }
}
