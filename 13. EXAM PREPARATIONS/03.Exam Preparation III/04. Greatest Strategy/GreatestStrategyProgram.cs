namespace _04._Greatest_Strategy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class GreatestStrategyProgram
    {
        private static Dictionary<int, HashSet<int>> _graph;
        private static Dictionary<int, HashSet<int>> _disconnectedGraph;
        private static int _root;

        private static void ReadInput()
        {
            _graph = new Dictionary<int, HashSet<int>>();
            _disconnectedGraph = new Dictionary<int, HashSet<int>>();

            var inputArgs = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var nodes = inputArgs[0];
            var connections = inputArgs[1];
            _root = inputArgs[2];

            for (var i = 1; i <= nodes; i++)
            {
                _graph[i] = new HashSet<int>();
                _disconnectedGraph[i] = new HashSet<int>();
            }

            for (var i = 0; i < connections; i++)
            {
                inputArgs = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var from = inputArgs[0];
                var to = inputArgs[1];

                _graph[@from].Add(to);
                _graph[to].Add(@from);


                _disconnectedGraph[@from].Add(to);
                _disconnectedGraph[to].Add(@from);
            }
        }

        private static int Dfs(int node, int parent, HashSet<int> visited)
        {
            visited.Add(node);

            var totalNodes = 1;

            foreach (var child in _graph[node])
            {
                if (!visited.Contains(child)
                && child != parent)
                {
                    var subTreeNodes = Dfs(child, node, visited);

                    if (subTreeNodes % 2 == 0)
                    {
                        _disconnectedGraph[node].Remove(child);
                        _disconnectedGraph[child].Remove(node);
                    }

                    totalNodes += subTreeNodes;
                }
            }

            return totalNodes;
        }

        private static int GetValue(int node, HashSet<int> visited)
        {
            visited.Add(node);
            var value = node;

            foreach (var child in _disconnectedGraph[node])
            {
                if (!visited.Contains(child))
                {
                    value += GetValue(child, visited);
                }
            }

            return value;
        }

        public static void Main()
        {
            ReadInput();

            Dfs(_root, _root, new HashSet<int>());

            var visited = new HashSet<int>();
            var max = 0;

            foreach (var node in _disconnectedGraph.Keys)
            {
                if (!visited.Contains(node))
                {
                    var value = GetValue(node, visited);

                    if (value > max)
                    {
                        max = value;
                    }
                }
            }

            Console.WriteLine(max);
        }
    }
}
