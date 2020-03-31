namespace _02._Dijkstra_s_Shortest_Path
{
    using System.Collections.Generic;
    using System.Linq;

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

    public static class DijkstraShortestPathProgram
    {
        private static List<Edge> _graph;
        private static Dictionary<int, List<Edge>> _nodesToEdges;

        private static void Dijkstra()
        {
            var nodes = _graph.Select(x => x.First)
                .Union(_graph.Select(x => x.Second))
                .Distinct()
                .OrderBy(e => e)
                .ToArray();

            var distances = new int[nodes.Max() + 1];

            for (var i = 0; i < distances.Length; i++)
            {
                distances[i] = int.MaxValue;
            }

            distances[nodes.First()] = 0;

            var queue = new SortedSet<int>(
                Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));

            queue.Add(nodes.First());

            while (queue.Count != 0)
            {
                var min = queue.Min;
                queue.Remove(min);

                foreach (var edge in _nodesToEdges[min])
                {
                    var otherNode = edge.First == min
                        ? edge.Second
                        : edge.First;

                    if (distances[otherNode] == int.MaxValue)
                    {
                        queue.Add(otherNode);
                    }

                    var newDistance = distances[min] + edge.Weight;

                    if (newDistance < distances[otherNode])
                    {
                        distances[otherNode] = newDistance;
                        queue = new SortedSet<int>(queue,
                            Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));
                    }
                }
            }
        }

        private static void DijkstraAlgorithm()
        {
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

            Dijkstra();
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

            DijkstraAlgorithm();
        }
    }
}
