namespace _03._Guards;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static readonly Dictionary<int, List<int>> graph;

    private static readonly HashSet<int> visited;

    private static int startNode;

    static Program()
    {
        graph = new Dictionary<int, List<int>>();
        visited = new HashSet<int>();
    }

    public static void Main()
    {
        ReadInput();

        Bfs(startNode);

        PrintOutput();
    }

    private static void ReadInput()
    {
        var nodes = int.Parse(Console.ReadLine() ?? "0");

        for (var i = 1; i <= nodes; i++)
        {
            graph[i] = new List<int>();
        }

        var edges = int.Parse(Console.ReadLine() ?? "0");

        for (var i = 0; i < edges; i++)
        {
            var edge = (Console.ReadLine() ?? string.Empty)
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Select(int.Parse)
                .ToArray();

            var from = edge[0];
            var to = edge[1];

            graph[from].Add(to);
            //graph[to].Add(from);
        }

        startNode = int.Parse(Console.ReadLine() ?? "0");
    }

    private static void Dfs(int vertex)
    {
        if (visited.Contains(vertex))
        {
            return;
        }

        visited.Add(vertex);

        foreach (var child in graph[vertex])
        {
            Dfs(child);
        }
    }

    private static void Bfs(int vertex)
    {
        var queue = new Queue<int>();
        queue.Enqueue(vertex);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            visited.Add(vertex);

            var children = graph[node];

            foreach (var child in children)
            {
                if (visited.Contains(child))
                {
                    continue;
                }

                queue.Enqueue(child);
                visited.Add(child);
            }
        }
    }

    private static void PrintOutput()
    {
        var unreachable = graph
            .Select(n => n.Key)
            .Where(k => !visited.Contains(k));

        Console.WriteLine(string.Join(" ", unreachable));
    }
}
