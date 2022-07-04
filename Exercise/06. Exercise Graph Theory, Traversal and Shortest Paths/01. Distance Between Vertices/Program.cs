namespace _01._Distance_Between_Vertices;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static readonly Dictionary<int, List<int>> graph;

    private static HashSet<int> visited;

    private static Dictionary<int, int> predecessors;

    private static int routesCount;

    static Program()
    {
        graph = new Dictionary<int, List<int>>();
        visited = new HashSet<int>();
        predecessors = new Dictionary<int, int>();
    }

    public static void Main()
    {
        ReadInput();

        ProcessRequests();
    }

    private static void ProcessRequests()
    {
        for (var i = 0; i < routesCount; i++)
        {
            var tokens = (Console.ReadLine() ?? string.Empty)
                .Split("-", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Select(int.Parse)
                .ToArray();

            var start = tokens[0];
            var end = tokens[1];

            predecessors = new Dictionary<int, int>();
            visited = new HashSet<int>();

            BFS(start);

            var pathLength = CalculatePathLength(start, end);

            Console.WriteLine($"{{{start}, {end}}} -> {pathLength}");
        }
    }

    private static void ReadInput()
    {
        var verticesCount = int.Parse(Console.ReadLine() ?? "0");
        routesCount = int.Parse(Console.ReadLine() ?? "0");

        for (var i = 0; i < verticesCount; i++)
        {
            var tokens = (Console.ReadLine() ?? string.Empty)
                .Split(":", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();

            var startVertex = int.Parse(tokens[0]);

            if (!graph.ContainsKey(startVertex))
            {
                graph[startVertex] = new List<int>();
            }

            if (tokens.Length <= 1)
            {
                continue;
            }

            var endVertices = tokens[1]
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Select(int.Parse)
                .ToArray();

            foreach (var endVertex in endVertices)
            {
                graph[startVertex].Add(endVertex);
            }
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
