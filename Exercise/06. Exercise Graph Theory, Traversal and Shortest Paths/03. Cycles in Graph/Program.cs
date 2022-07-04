namespace _03._Cycles_in_Graph;

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

        // TopologicalSort();
        TopSortDfs();

        PrintOutput();
    }

    private static void ReadInput()
    {
        string input;

        while ((input = Console.ReadLine() ?? string.Empty) != "End")
        {
            var tokens = input
                .Split("-")
                .Select(x => x.Trim())
                .ToArray();

            var vertexSource = tokens[0];
            var vertexTarget = tokens[1];

            if (!graph.ContainsKey(vertexSource))
            {
                graph[vertexSource] = new List<string>();
            }

            if (!graph.ContainsKey(vertexTarget))
            {
                graph[vertexTarget] = new List<string>();
            }

            graph[vertexSource].Add(vertexTarget);
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

    private static void TopologicalSort()
    {
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

            predecessorCount.Remove(vertex);
        }
    }

    private static void PrintOutput()
    {
        var response = predecessorCount.Count > 0
            ? "Acyclic: No"
            : $"Acyclic: Yes";

        Console.WriteLine(response);
    }

    private static void TopSortDfs()
    {
        predecessorCount.Clear();

        foreach (var node in graph.Keys)
        {
            try
            {
                TopSortDfs(node);
            }
            catch (InvalidOperationException)
            {
                predecessorCount.Add("Error", 1);
                break;
            }
        }
    }

    private static void TopSortDfs(string node)
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
            TopSortDfs(child);
        }

        cycles.Remove(node);
    }
}
