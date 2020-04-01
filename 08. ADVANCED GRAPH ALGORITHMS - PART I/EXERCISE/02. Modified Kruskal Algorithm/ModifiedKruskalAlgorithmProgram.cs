namespace _02._Modified_Kruskal_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ModifiedKruskalAlgorithmProgram
    {
        public static void Main()
        {
            var nodesCount = int.Parse(Console.ReadLine().Split(new[] { ' ' })[1]);
            var edgesCount = int.Parse(Console.ReadLine().Split(new[] { ' ' })[1]);

            var edges = new List<Edge>();
            var parents = new int[nodesCount];

            for (var i = 0; i < parents.Length; i++)
            {
                parents[i] = i;
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
                edges.Add(newEdge);
            }

            edges = edges
                .OrderBy(x => x.Weight)
                .ToList();

            var result = new List<Edge>();

            for (var i = 0; i < edges.Count; i++)
            {
                var currentEdge = edges[i];

                var firstRoot = parents[currentEdge.First];
                var secondRoot = parents[currentEdge.Second];

                if (firstRoot != secondRoot)
                {
                    result.Add(edges[i]);
                    parents[currentEdge.Second] = firstRoot;

                    for (int j = 0; j < parents.Length; j++)
                    {
                        if (parents[j] == secondRoot)
                        {
                            parents[j] = firstRoot;
                        }
                    }
                }
            }

            Console.WriteLine($"Minimum spanning forest weight: {result.Sum(x => x.Weight)}");

            foreach (var edge in result)
            {
                Console.WriteLine(edge);
            }
        }
    }
}
