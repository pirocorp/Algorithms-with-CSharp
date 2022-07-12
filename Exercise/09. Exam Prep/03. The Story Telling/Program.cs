namespace _03._The_Story_Telling;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static Dictionary<string, List<string>> graph;

    private static Dictionary<string, int> predecessorCount;

    static Program()
    {
        graph = new Dictionary<string, List<string>>();
        predecessorCount = new Dictionary<string, int>();
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
        string input;

        while ((input = Console.ReadLine() ?? string.Empty) != "End")
        {
            var story = input
                .Split("->", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();

            var vertex = story[0];

            if (!graph.ContainsKey(vertex))
            {
                graph[vertex] = new List<string>();
            }

            if (story.Length <= 1)
            {
                continue;
            }

            var children = story[1]
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            foreach (var child in children)
            {
                if (!graph.ContainsKey(child))
                {
                    graph[child] = new List<string>();
                }

                graph[vertex].Add(child);
            }
        }

        graph = graph
            .Reverse()
            .ToDictionary(s => s.Key, s => s.Value);

        predecessorCount = graph
            .ToDictionary(s => s.Key, s => 0);
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

    private static IEnumerable<string> TopologicalSort()
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

    private static void PrintOutput(IEnumerable<string> sorted)
        => Console.WriteLine(string.Join(" ", sorted));
}
