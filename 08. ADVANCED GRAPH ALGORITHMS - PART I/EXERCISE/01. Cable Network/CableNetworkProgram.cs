namespace _01._Cable_Network
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CableNetworkProgram
    {
        private static int _budget;
        private static List<Edge> _edges;
        private static HashSet<int> _network;

        private static void ReadInput()
        {
            _budget = int.Parse(Console.ReadLine().Split(new[] { ' ' })[1]);
            var nodesCount = int.Parse(Console.ReadLine().Split(new[] { ' ' })[1]);
            var edgesCount = int.Parse(Console.ReadLine().Split(new[] { ' ' })[1]);

            _edges = new List<Edge>();
            _network = new HashSet<int>();

            for (var i = 0; i < edgesCount; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var node1 = int.Parse(tokens[0]);
                var node2 = int.Parse(tokens[1]);

                if (tokens.Length == 4)
                {
                    _network.Add(node1);
                    _network.Add(node2);
                }
                else
                {
                    var weight = int.Parse(tokens[2]);
                    var newEdge = new Edge(node1, node2, weight);
                    _edges.Add(newEdge);
                }
            }
        }

        private static int CalculateBudget()
        {
            var total = 0;

            while (_budget != 0)
            {
                var nextPossibleEdge = _edges
                    .Where(x => _network.Contains(x.First) ^ _network.Contains(x.Second))
                    .OrderBy(x => x.Weight)
                    .FirstOrDefault();

                if (nextPossibleEdge == null 
                    || _budget < nextPossibleEdge.Weight)
                {
                    break;
                }

                total += nextPossibleEdge.Weight;
                _budget -= nextPossibleEdge.Weight;

                _network.Add(nextPossibleEdge.First);
                _network.Add(nextPossibleEdge.Second);
            }

            return total;
        }

        public static void Main()
        {
            ReadInput();
            var total = CalculateBudget();
            Console.WriteLine($"Budget used: {total}");
        }
    }
}
