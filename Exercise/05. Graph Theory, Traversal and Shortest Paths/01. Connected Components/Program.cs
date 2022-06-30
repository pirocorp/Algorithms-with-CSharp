namespace _01._Connected_Components;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static List<int>[] graph = Array.Empty<List<int>>();
    private static bool[] visited = Array.Empty<bool>();

    public static void Main()
    {
        ReadInput();

        FindGraphConnectedComponents();
    }

    private static void ReadInput()
    {
        var n = int.Parse(Console.ReadLine() ?? "0");

        graph = new List<int>[n];

        for (var i = 0; i < n; i++)
        {
            graph[i] = (Console.ReadLine() ?? string.Empty)
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Select(int.Parse)
                .ToList();
        }
    }

    private static void FindGraphConnectedComponents()
    {
        visited = new bool[graph.Length];

        for (var startNode = 0; startNode < graph.Length; startNode++)
        {
            if (visited[startNode])
            {
                continue;
            }

            Console.Write("Connected component:");

            Dfs(startNode);

            Console.WriteLine();
        }
    }

    private static void Dfs(int vertex)
    {
        if (visited[vertex])
        {
            return;
        }

        visited[vertex] = true;

        foreach (var child in graph[vertex])
        {
            Dfs(child);
        }

        Console.Write(" " + vertex);
    }

    private static void Bfs(int vertex)
    {
        var queue = new Queue<int>();
        queue.Enqueue(vertex);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            Console.Write(" " + node);
            visited[node] = true;

            var children = graph[node];

            foreach (var child in children)
            {
                if (visited[child])
                {
                    continue;
                }

                queue.Enqueue(child);
                visited[child] = true;
            }
        }
    }
}
