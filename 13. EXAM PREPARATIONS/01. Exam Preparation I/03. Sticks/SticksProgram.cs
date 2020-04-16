namespace _03._Sticks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SticksProgram
    {
        private static SortedDictionary<int, List<int>> _graph;
        private static SortedDictionary<int, List<int>> _parents; //Reversed graph
        
        private static void ReadInput()
        {
            _graph = new SortedDictionary<int, List<int>>();
            _parents = new SortedDictionary<int, List<int>>();

            var vertexCount = int.Parse(Console.ReadLine());
            var edgeCount = int.Parse(Console.ReadLine());

            for (var i = 0; i < vertexCount; i++)
            {
                _graph[i] = new List<int>();
                _parents[i] = new List<int>();
            }

            for (var i = 0; i < edgeCount; i++)
            {
                var edgeArgs = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                var parent = edgeArgs[0]; //from
                var child = edgeArgs[1]; //to

                _graph[parent].Add(child);
                _parents[child].Add(parent);
            }
        }

        private static List<int> TryTopologicalSortGraph(out bool cycleDetected)
        {
            var result = new List<int>();
            cycleDetected = false;

            while (_graph.Count > 0)
            {
                var parents = _parents
                    .Where(x => x.Value.Count == 0)
                    .Select(x => x.Key)
                    .ToArray();

                if (parents.Length == 0)
                {
                    cycleDetected = true;
                    break;
                }

                var parent = parents.Last();

                var children = _graph[parent];

                _graph.Remove(parent);
                _parents.Remove(parent);
                result.Add(parent);

                foreach (var child in children)
                {
                    _parents[child].Remove(parent);
                }
            }

            return result;
        }

        public static void Main()
        {
            ReadInput();

            var result = TryTopologicalSortGraph(out var cycleDetected);

            if (cycleDetected)
            {
                Console.WriteLine($"Cannot lift all sticks");
            }

            Console.WriteLine(string.Join(" ", result));
        }
    }
}