namespace _02._Rumors;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static readonly Dictionary<int, List<int>> graph;

    private static readonly HashSet<int> visited;

    private static readonly Dictionary<int, int> predecessors;

    static Program()
    {
        graph = new Dictionary<int, List<int>>();

        predecessors = new Dictionary<int, int>();
        visited = new HashSet<int>();
    }

    public static void Main()
    {
        ReadInput();

        var start = int.Parse(Console.ReadLine() ?? "0");

        CalculateDistances(start);
    }

    private static void ReadInput()
    {
        var verticesCount = int.Parse(Console.ReadLine() ?? "0");
        var connections = int.Parse(Console.ReadLine() ?? "0");

        var vertices = Enumerable.Range(1, verticesCount);

        foreach (var vertex in vertices)
        {
            graph[vertex] = new List<int>();
        }

        for (var i = 0; i < connections; i++)
        {
            var tokens = (Console.ReadLine() ?? string.Empty)
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Select(int.Parse)
                .ToArray();

            var startVertex = tokens[0];

            var endVertex = tokens[1];

            graph[startVertex].Add(endVertex);
            graph[endVertex].Add(startVertex);
        }
    }

    private static void CalculateDistances(int start)
    {
        BFS(start);

        var max = graph.Keys.Max();

        for (var end = 1; end <= max; end++)
        {
            var pathLength = CalculatePathLength(start, end);

            if (pathLength <= 0)
            {
                continue;
            }

            Console.WriteLine($"{start} -> {end} ({pathLength})");
        }
    }

    private static void BFS(int start)
    {
        predecessors[start] = 0;

        var queue = new Queue<int>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var children = graph[current];

            foreach (var child in children)
            {
                if (visited.Contains(child))
                {
                    continue;
                }

                predecessors[child] = current;
                visited.Add(child);
                queue.Enqueue(child);
            }

            visited.Add(current);
        }
    }

    private static int CalculatePathLength(int start, int end)
    {
        var path = new Stack();

        if (!predecessors.ContainsKey(end))
        {
            return -1;
        }

        var current = end;

        while (current != start)
        {
            path.Push(current);

            current = predecessors[current];
        }

        return path.Count;
    }
}
