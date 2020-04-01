namespace _02._Modified_Kruskal_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ModifiedKruskalAlgorithmProgram
    {
        private static List<Edge> _edges;
        private static int[] _parents;

        private static void ReadInput()
        {
            var nodesCount = int.Parse(Console.ReadLine().Split(new[] { ' ' })[1]);
            var edgesCount = int.Parse(Console.ReadLine().Split(new[] { ' ' })[1]);

            _edges = new List<Edge>();
            _parents = new int[nodesCount];

            for (var i = 0; i < _parents.Length; i++)
            {
                _parents[i] = i;
            }

            for (var i = 0; i < edgesCount; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var node1 = int.Parse(tokens[0]);
                var node2 = int.Parse(tokens[1]);

                var weight = int.Parse(tokens[2]);
                var newEdge = new Edge(node1, node2, weight);
                _edges.Add(newEdge);
            }

            _edges = _edges
                .OrderBy(x => x.Weight)
                .ToList();
        }

        private static List<Edge> ModifiedKruskalAlgorithm()
        {
            var result = new List<Edge>();

            for (var i = 0; i < _edges.Count; i++)
            {
                var currentEdge = _edges[i];

                var firstRoot = _parents[currentEdge.First];
                var secondRoot = _parents[currentEdge.Second];

                if (firstRoot != secondRoot)
                {
                    result.Add(_edges[i]);
                    _parents[currentEdge.Second] = firstRoot;

                    for (int j = 0; j < _parents.Length; j++)
                    {
                        if (_parents[j] == secondRoot)
                        {
                            _parents[j] = firstRoot;
                        }
                    }
                }
            }

            return result;
        }

        public static void Main()
        {
            ReadInput();

            var result = ModifiedKruskalAlgorithm();

            Console.WriteLine($"Minimum spanning forest weight: {result.Sum(x => x.Weight)}");

            foreach (var edge in result)
            {
                Console.WriteLine(edge);
            }
        }
    }
}
