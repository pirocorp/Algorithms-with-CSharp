namespace _05._Break_Cycles;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static readonly Dictionary<string, List<string>> graph;

    private static List<Edge> edges;

    private static readonly List<Edge> removedEdges;

    private static readonly HashSet<string> visited;

    static Program()
    {
        graph = new Dictionary<string, List<string>>();
        edges = new List<Edge>();
        removedEdges = new List<Edge>();
        visited = new HashSet<string>();
    }

    public static void Main()
    {
        ReadInput();

        BreakCycles();

        PrintOutput();
    }

    private static void ReadInput()
    {
        var vertexCount = int.Parse(Console.ReadLine() ?? "0");

        for (var i = 0; i < vertexCount; i++)
        {
            var tokens = (Console.ReadLine() ?? string.Empty)
                .Split(" -> ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();

            var vertex = tokens[0];

            var children = tokens[1]
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();

            graph[vertex] = children;

            var newEdges = children
                .Where(c => c[0] > vertex[0])
                .Select(c => new Edge(vertex, c));

            edges.AddRange(newEdges);
        }

        OrderEdges();
    }

    private static void PrintOutput()
    {
        Console.WriteLine($"Edges to remove: {removedEdges.Count}");

        removedEdges.ForEach(e => Console.WriteLine($"{e.Source} - {e.Destination}"));
    }

    private static void OrderEdges()
    {
        edges = edges
            .OrderBy(e => e.Source)
            .ThenBy(e => e.Destination)
            .ToList();
    }

    private static void BreakCycles()
    {
        var enumerate = edges.ToArray();

        foreach (var edge in enumerate)
        {
            RemoveEdge(edge);

            visited.Clear();

            if (!PathExists(edge.Source, edge.Destination))
            {
                AddEdge(edge);
            }  
        }
    }

    private static void RemoveEdge(Edge edge)
    {
        edges.Remove(edge);

        removedEdges.Add(edge);

        graph[edge.Source].Remove(edge.Destination);
        graph[edge.Destination].Remove(edge.Source);
    }

    private static void AddEdge(Edge edge)
    {
        edges.Add(edge);
        OrderEdges();

        removedEdges.Remove(edge);

        graph[edge.Source].Add(edge.Destination);
        graph[edge.Destination].Add(edge.Source);
    }

    private static bool PathExists(string start, string end)
    {
        var queue = new Queue<string>();
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
    public Edge(string source, string destination)
    {
        this.Source = source;
        this.Destination = destination;
    }

    public string Source { get; }

    public string Destination { get; }
}
