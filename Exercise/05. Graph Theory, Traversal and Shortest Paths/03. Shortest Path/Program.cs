namespace _03._Shortest_Path;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static readonly Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();

    private static readonly HashSet<int> visited = new HashSet<int>();

    private static readonly Dictionary<int, int> predecessors = new Dictionary<int, int>();

    private static int start;

    private static int end;

    public static void Main()
    {
        ReadInput();

        BFS();

        ReconstructPath();
    }

    private static void ReadInput()
    {
        var nodesCount = int.Parse(Console.ReadLine() ?? "0");

        var edgesCount = int.Parse(Console.ReadLine() ?? "0");

        for (var i = 0; i < edgesCount; i++)
        {
            var nodes = (Console.ReadLine() ?? "0")
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Select(int.Parse)
                .ToArray();

            var node1 = nodes[0];
            var node2 = nodes[1];

            if (!graph.ContainsKey(node1))
            {
                graph[node1] = new List<int>();
            }

            if (!graph.ContainsKey(node2))
            {
                graph[node2] = new List<int>();
            }

            graph[node1].Add(node2);
            graph[node2].Add(node1);
        }

        start = int.Parse(Console.ReadLine() ?? "0");

        end = int.Parse(Console.ReadLine() ?? "0");
    }

    private static void BFS()
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

    private static void ReconstructPath()
    {
        var path = new Stack();

        if (!predecessors.ContainsKey(end))
        {
            Console.WriteLine("No path is found");
            return;
        }

        var current = end;

        while (current != start)
        {
            path.Push(current);

            current = predecessors[current];
        }

        Console.WriteLine($"Shortest path length is: {path.Count}");

        path.Push(start);

        Console.WriteLine(string.Join(" ", path.ToArray()));
    }
}
