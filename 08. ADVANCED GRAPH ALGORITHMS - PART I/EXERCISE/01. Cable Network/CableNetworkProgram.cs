namespace _01._Cable_Network
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CableNetworkProgram
    {
        public static void Main()
        {
            var budget = int.Parse(Console.ReadLine().Split(new []{' '})[1]);
            var nodesCount = int.Parse(Console.ReadLine().Split(new[] { ' ' })[1]);
            var edgesCount = int.Parse(Console.ReadLine().Split(new[] { ' ' })[1]);

            var edges = new List<Edge>();
            var network = new HashSet<int>();

            for (var i = 0; i < edgesCount; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var node1 = int.Parse(tokens[0]);
                var node2 = int.Parse(tokens[1]);

                if (tokens.Length == 4)
                {
                    network.Add(node1);
                    network.Add(node2);
                }
                else
                {
                    var weight = int.Parse(tokens[2]);
                    var newEdge = new Edge(node1, node2, weight);
                    edges.Add(newEdge);
                }
            }

            var total = 0;

            while (budget != 0)
            {
                var nextPossibleEdge = edges
                    .Where(x => network.Contains(x.First) ^ network.Contains(x.Second))
                    .OrderBy(x => x.Weight)
                    .FirstOrDefault();

                if (nextPossibleEdge == null)
                {
                    break;
                }

                if (budget < nextPossibleEdge.Weight)
                {
                    break;
                }

                total += nextPossibleEdge.Weight;
                budget -= nextPossibleEdge.Weight;

                network.Add(nextPossibleEdge.First);
                network.Add(nextPossibleEdge.Second);
            }

            Console.WriteLine($"Budget used: {total}");
        }
    }
}
