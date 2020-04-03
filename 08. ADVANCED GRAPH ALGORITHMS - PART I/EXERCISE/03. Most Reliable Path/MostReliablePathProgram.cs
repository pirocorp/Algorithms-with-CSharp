namespace _03._Most_Reliable_Path
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MostReliablePathProgram
    {
        private static List<Edge> _edges;
        private static decimal[] _distances;
        private static int?[] _prev;
        private static bool[] _visited;

        private static int _start;
        private static int _end;

        private static void ReadInput()
        {
            var nodesCount = int.Parse(Console.ReadLine().Split(new[] { ' ' })[1]);
            var pathTokens = Console.ReadLine()
                .Split(":- ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Where(x => int.TryParse(x, out var y))
                .Select(int.Parse)
                .ToArray();

            _start = pathTokens[0];
            _end = pathTokens[1];

            var edgesCount = int.Parse(Console.ReadLine().Split(new[] { ' ' })[1]);

            _edges = new List<Edge>();

            for (var i = 0; i < edgesCount; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                var newEdge = new Edge(tokens[0], tokens[1], tokens[2]);
                _edges.Add(newEdge);
            }

            _distances = new decimal[nodesCount];

            for (var i = 0; i < _distances.Length; i++)
            {
                _distances[i] = decimal.MinValue;
            }

            _prev = new int?[nodesCount];
            _visited = new bool[nodesCount];
        }

        private static void CalculateDistancesDijkstra()
        {
            _distances[_start] = 1;

            while (true)
            {
                var maxDistance = decimal.MinValue;
                var maxNode = int.MinValue;

                for (var node = 0; node < _visited.Length; node++)
                {
                    if (!_visited[node] && _distances[node] > maxDistance)
                    {
                        maxDistance = _distances[node];
                        maxNode = node;
                    }
                }

                if (maxDistance == decimal.MinValue)
                {
                    //No min distance node found --> algo finished
                    break;
                }

                _visited[maxNode] = true;

                var connectedEdges = _edges
                    .Where(x => x.First == maxNode || x.Second == maxNode)
                    .ToArray();

                //Improve the distance[0, n-1] through minNode
                for (var i = 0; i < connectedEdges.Length; i++)
                {
                    var currentEdge = connectedEdges[i];

                    var newDistance = maxDistance * currentEdge.Weight;

                    var otherNode = currentEdge.Second == maxNode ? currentEdge.First : currentEdge.Second;

                    if (newDistance > _distances[otherNode])
                    {
                        _distances[otherNode] = newDistance;
                        _prev[otherNode] = maxNode;
                    }
                }
            }
        }

        private static List<int> ReconstructPath()
        {
            var path = new List<int>();
            int? currentNode = _end;

            while (currentNode != null)
            {
                path.Add(currentNode.Value);
                currentNode = _prev[currentNode.Value];
            }

            path.Reverse();
            return path;
        }

        public static void Main() 
        {
            ReadInput();
            CalculateDistancesDijkstra();

            if (_distances[_end] == decimal.MinValue)
            {
                //No path found from sourceNode to destinationNode
                Console.WriteLine($"No Path from {_start} to {_end}");
            }

            var path = ReconstructPath();

            Console.WriteLine($"Most reliable path reliability: {_distances[_end] * 100:F2}%");
            Console.WriteLine(string.Join(" -> ", path));
        }
    }
}
