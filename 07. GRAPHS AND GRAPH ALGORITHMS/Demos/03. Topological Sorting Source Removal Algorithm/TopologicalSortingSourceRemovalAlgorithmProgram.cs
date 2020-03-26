namespace _03._Topological_Sorting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class TopologicalSortingSourceRemovalAlgorithmProgram
    {
        private static List<int>[] _graph;

        public static void Main()
        {
            _graph = new[]
            {
                new List<int> { 1, 2 }, 
                new List<int> { 3, 4 }, 
                new List<int> { 5 }, 
                new List<int> { 2, 5 }, 
                new List<int> { 3 }, 
                new List<int> { },
            };

            var result = new List<int>();
            var nodes = new HashSet<int>();

            var nodesWithIncomingEdges = _graph
                .SelectMany(s => s)
                .ToHashSet();

            for (var i = 0; i < _graph.Length; i++)
            {
                if (!nodesWithIncomingEdges.Contains(i))
                {
                    nodes.Add(i);
                }
            }

            while (nodes.Count != 0)
            {
                var current = nodes.First();
                nodes.Remove(current);

                result.Add(current);

                var children = _graph[current]
                    .Where(child => !nodesWithIncomingEdges.Contains(child));
                _graph[current] = new List<int>();

                nodesWithIncomingEdges = _graph
                    .SelectMany(s => s)
                    .ToHashSet();

                foreach (var child in children)
                {
                    nodes.Add(child);
                }
            }

            if (_graph.SelectMany(s => s).Any())
            {
                Console.WriteLine("Graph has at least one cycle");
            }
            else
            {
                Console.WriteLine(string.Join("->", result));
            }
        }
    }
}
