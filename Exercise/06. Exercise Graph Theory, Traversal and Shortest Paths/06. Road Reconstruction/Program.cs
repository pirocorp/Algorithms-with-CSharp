namespace _06._Road_Reconstruction;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static readonly Dictionary<int, List<int>> graph;

    private static readonly List<Edge> edges;

    private static readonly List<Edge> importantEdges;

    private static readonly HashSet<int> visited;

    static Program()
    {
        graph = new Dictionary<int, List<int>>();
        edges = new List<Edge>();
        importantEdges = new List<Edge>();
        visited = new HashSet<int>();
    }

    public static void Main()
    {
        ReadInput();

        FindImportantStreets();

        PrintOutput();
    }

    private static void ReadInput()
    {
        var vertices = int.Parse(Console.ReadLine() ?? "0");

        var relations = int.Parse(Console.ReadLine() ?? "0");

        for (var i = 0; i < relations; i++)
        {
            var tokens = (Console.ReadLine() ?? string.Empty)
                .Split(" - ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Select(int.Parse)
                .ToArray();

            var source = tokens[0];
            var destination = tokens[1];

            if (!graph.ContainsKey(source))
            {
                graph[source] = new List<int>();
            }

            if (!graph.ContainsKey(destination))
            {
                graph[destination] = new List<int>();
            }

            graph[source].Add(destination);
            graph[destination].Add(source);

            var edge = new Edge(source, destination);
            edges.Add(edge);
        }
    }

    private static void PrintOutput()
    {
        Console.WriteLine("Important streets:");

        foreach (var edge in importantEdges)
        {
            var min = Math.Min(edge.Source, edge.Destination);
            var max = Math.Max(edge.Source, edge.Destination);

            Console.WriteLine($"{min} {max}");
        }
    }

    private static void FindImportantStreets()
    {
        var enumerate = edges.ToArray();

        foreach (var edge in enumerate)
        {
            SelectEdge(edge);

            visited.Clear();

            if (PathExists(edge.Source, edge.Destination))
            {
                UnSelectEdge(edge);
            }
        }
    }

    private static void SelectEdge(Edge edge)
    {
        edges.Remove(edge);
        importantEdges.Add(edge);

        graph[edge.Source].Remove(edge.Destination);
        graph[edge.Destination].Remove(edge.Source);
    }

    private static void UnSelectEdge(Edge edge)
    {
        edges.Add(edge);
        importantEdges.Remove(edge);

        graph[edge.Source].Add(edge.Destination);
        graph[edge.Destination].Add(edge.Source);
    }

    private static bool PathExists(int start, int end)
    {
        var queue = new Queue<int>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current == end)
            {
                return true;
            }

            var children = graph[current];

            foreach (var child in children)
            {
                if (visited.Contains(child))
                {
                    continue;
                }


                visited.Add(child);
                queue.Enqueue(child);
            }

            visited.Add(current);
        }

        return false;
    }
}

internal class Edge
{
    public Edge(int source, int destination)
    {
        this.Source = source;
        this.Destination = destination;
    }

    public int Source { get; }

    public int Destination { get; }
}
