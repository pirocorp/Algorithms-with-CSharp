namespace _04._Cheap_Town_Tour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CheapTownTourProgram
    {
        private static List<Edge> _edges;
        private static Dictionary<int, List<Edge>> _graph;

        private static int _totalCost = 0;

        private static void ReadInput()
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            _graph = new Dictionary<int, List<Edge>>();
            _edges = new List<Edge>();

            for (int i = 0; i < edgesCount; i++)
            {
                var edgeArgs = Console.ReadLine()
                    .Split(" - ")
                    .Select(int.Parse)
                    .ToArray();

                var from = edgeArgs[0];
                var to = edgeArgs[1];
                var weight = edgeArgs[2];

                var edge = new Edge(@from, to, weight);
                _edges.Add(edge);
            }

            _edges = _edges.OrderBy(x => x.Weight).ToList();

            _parents = new int[nodesCount];

            for (int i = 0; i < nodesCount; i++)
            {
                _parents[i] = i;
            }
        }

        private static int[] _parents;

        private static int FindRootFast(int node)
        {
            if (_parents[node] == node)
            {
                return node;
            }

            return _parents[node] = FindRootFast(_parents[node]);
        }

        private static void BuildGraph()
        {
            foreach (var edge in _edges)
            {
                if (!_graph.ContainsKey(edge.From) && !_graph.ContainsKey(edge.To))
                {
                    _graph[edge.From] = new List<Edge>();
                    _graph[edge.To] = new List<Edge>();

                    var reverseEdge = new Edge(edge.To, edge.From, edge.Weight);

                    _graph[edge.From].Add(edge);
                    _graph[edge.To].Add(reverseEdge);

                    _parents[edge.From] = edge.To;
                }
                else if ((_graph.ContainsKey(edge.From) && _graph[edge.From].Count >= 2) ||
                         (_graph.ContainsKey(edge.To) && _graph[edge.To].Count >= 2))
                {
                    continue;
                }
                else if (FindRootFast(edge.From) != FindRootFast(edge.To))
                {
                    if (_graph.ContainsKey(edge.From) && _graph[edge.From].Count == 1)
                    {
                        if (!_graph.ContainsKey(edge.To))
                        {
                            _graph[edge.To] = new List<Edge>();
                        }

                        var reverseEdge = new Edge(edge.To, edge.From, edge.Weight);

                        _graph[edge.From].Add(edge);
                        _graph[edge.To].Add(reverseEdge);

                        _parents[edge.From] = edge.To;
                    }
                    else if (_graph.ContainsKey(edge.To) && _graph[edge.To].Count == 1)
                    {
                        if (!_graph.ContainsKey(edge.From))
                        {
                            _graph[edge.From] = new List<Edge>();
                        }

                        var reverseEdge = new Edge(edge.To, edge.From, edge.Weight);
                        
                        _graph[edge.From].Add(edge);
                        _graph[edge.To].Add(reverseEdge);

                        _parents[edge.From] = edge.To;
                    }
                }
            }
        }

        private static void Dfs(int node, HashSet<int> visited)
        {
            if (!visited.Contains(node))
            {
                visited.Add(node);
                var children = _graph[node];

                foreach (var child in children)
                {
                    if (!visited.Contains(child.To))
                    {
                        _totalCost += child.Weight;
                    }

                    Dfs(child.To, visited);
                }
            }
        }

        public static void Main()
        {
            ReadInput();

            BuildGraph();

            var visited = new HashSet<int>();

            var start = _graph.FirstOrDefault(x => x.Value.Count == 1).Key;
            Dfs(start, visited);

            Console.WriteLine($"Total cost: {_totalCost}");
        }
    }
}
