namespace _01._Minimal_Spanning_Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    internal class Edge
    {
        public Edge(int first, int second, int weight)
        {
            this.First = first;
            this.Second = second;
            this.Weight = weight;
        }

        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }

        public override string ToString()
        {
            return $"{this.First} -> {this.Second}, {this.Weight}";
        }
    }

    public static class MinimalSpanningTreeProgram
    {
        private static List<Edge> _graph;

        private static int[] _parents;

        private static readonly HashSet<int> _visited = new HashSet<int>();
        private static readonly HashSet<int> _spanningTree = new HashSet<int>();
        private static Dictionary<int, List<Edge>> _nodesToEdges;

        private static int FindRoot(int node)
        {
            while (_parents[node] != node)
            {
                node = _parents[node];
            }

            return node;
        }

        private static void Kruskal()
        {
            var edges = _graph
                .OrderBy(x => x.Weight)
                .ToList();

            while (edges.Count != 0)
            {
                var edge = edges.First();
                edges.Remove(edge);

                var firstNode = edge.First;
                var secondNode = edge.Second;

                var firstRoot = FindRoot(firstNode);
                var secondRoot = FindRoot(secondNode);

                if (firstRoot != secondRoot)
                {
                    Console.WriteLine($"{firstNode} - {secondNode}");
                    _parents[firstRoot] = secondRoot;
                }
            }
        }

        private static void KruskalsAlgorithm()
        {
            var nodes = _graph.Select(x => x.First)
                .Union(_graph.Select(x => x.Second))
                .Distinct()
                .ToArray();

            _parents = new int[nodes.Max() + 1];

            foreach (var node in nodes)
            {
                _parents[node] = node;
            }

            Kruskal();
        }

        private static void Prim(int startingNode)
        {
            _spanningTree.Add(startingNode);
            var priorityQueue = new OrderedBag<Edge>(Comparer<Edge>.Create((f, s) => f.Weight.CompareTo(s.Weight)));

           priorityQueue.AddMany(_nodesToEdges[startingNode]);

           while (priorityQueue.Count != 0)
           {
               var minEdge = priorityQueue.GetFirst();
               priorityQueue.Remove(minEdge);

               var firstNode = minEdge.First;
               var secondNode = minEdge.Second;

               var nonTreeNode = -1;

               if (_spanningTree.Contains(firstNode)
                   && !_spanningTree.Contains(secondNode))
               {
                   nonTreeNode = secondNode;
               }

               if (_spanningTree.Contains(secondNode)
                   && !_spanningTree.Contains(firstNode))
               {
                   nonTreeNode = firstNode;
               }

               if (nonTreeNode == -1)
               {
                    continue;
               }

               _spanningTree.Add(nonTreeNode);
               Console.WriteLine($"{minEdge.First} - {minEdge.Second}");
               priorityQueue.AddMany(_nodesToEdges[nonTreeNode]);
            }
        }

        private static void PrimsAlgorithm()
        {
            var nodes = _graph.Select(x => x.First)
                .Union(_graph.Select(x => x.Second))
                .Distinct()
                .OrderBy(e => e)
                .ToArray();

            _nodesToEdges = new Dictionary<int, List<Edge>>();

            foreach (var edge in _graph)
            {
                if (!_nodesToEdges.ContainsKey(edge.First))
                {
                    _nodesToEdges[edge.First] = new List<Edge>();
                }

                if (!_nodesToEdges.ContainsKey(edge.Second))
                {
                    _nodesToEdges[edge.Second] = new List<Edge>();
                }

                _nodesToEdges[edge.First].Add(edge);
                _nodesToEdges[edge.Second].Add(edge);
            }

            foreach (var node in nodes)
            {
                if (!_visited.Contains(node))
                {
                    Prim(node);
                }
            }
        }

        public static void Main()
        {
            _graph = new List<Edge>
            {
                new Edge(2, 4, 2),
                new Edge(3, 4, 20),
                new Edge(1, 4, 9),
                new Edge(7, 9, 10),
                new Edge(1, 3, 5),
                new Edge(3, 5, 7),
                new Edge(1, 2, 4),
                new Edge(8, 9, 7),
                new Edge(1, 4, 9),
                new Edge(7, 9, 10),
                new Edge(4, 5, 8),
                new Edge(5, 6, 12),
                new Edge(7, 8, 8),
            };
            KruskalsAlgorithm();
            Console.WriteLine();
            PrimsAlgorithm();
        }
    }
}
