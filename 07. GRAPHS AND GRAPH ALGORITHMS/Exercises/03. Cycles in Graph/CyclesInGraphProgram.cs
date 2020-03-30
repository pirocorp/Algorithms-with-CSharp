namespace _03._Cycles_in_Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CyclesInGraphProgram
    {
        private static Dictionary<string, HashSet<string>> _graph;

        private static Dictionary<string, List<string>> _otherGraph;
        private static Dictionary<string, bool> _visited;
        private static Dictionary<string, bool> _marked;
        private static bool IsCyclic;

        public static void Main()
        {
            Solution();
            //OtherSolution();
        }

        private static void Solution()
        {
            ReadInput();

            if (IsAcyclic())
            {
                Console.WriteLine($"Acyclic: Yes");
            }
            else
            {
                Console.WriteLine($"Acyclic: No");
            }
        }

        private static void ReadInput()
        {
            _graph = new Dictionary<string, HashSet<string>>();

            var line = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(line))
            {
                var tokens = line
                    .Split(new[] { '–' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var key = tokens[0];

                if (!_graph.ContainsKey(key))
                {
                    _graph[key] = new HashSet<string>();
                }

                for (var i = 1; i < tokens.Length; i++)
                {
                    var current = tokens[i];
                    _graph[key].Add(current);

                    if (!_graph.ContainsKey(current))
                    {
                        _graph[current] = new HashSet<string>();
                    }

                    _graph[current].Add(key);
                }

                line = Console.ReadLine();
            }
        }

        private static bool IsAcyclic()
        {
            var nodes = new HashSet<string>();

            var nodesWithIncomingEdges = _graph
                .Where(x => x.Value.Count > 1)
                .Select(x => x.Key)
                .ToHashSet();

            foreach (var node in _graph.Keys)
            {
                if (!nodesWithIncomingEdges.Contains(node))
                {
                    nodes.Add(node);
                }
            }

            while (nodes.Count != 0)
            {
                var current = nodes.First();
                nodes.Remove(current);

                var children = _graph[current]
                    .Where(child => !nodesWithIncomingEdges.Contains(child));
                _graph.Remove(current);

                foreach (var node in _graph)
                {
                    node.Value.Remove(current);
                }

                nodesWithIncomingEdges = _graph
                    .Where(x => x.Value.Count > 1)
                    .Select(x => x.Key)
                    .ToHashSet();

                foreach (var child in children)
                {
                    nodes.Add(child);
                }
            }

            if (_graph.Count != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = null)
        {
            return new HashSet<T>(source, comparer);
        }

        private static void OtherSolution()
        {
            Initialize();
            OtherReadInput();

            if (IsAcyclicOther())
            {
                Console.WriteLine($"Acyclic: Yes");
            }
            else
            {
                Console.WriteLine($"Acyclic: No");
            }
        }

        private static void Initialize()
        {
            _otherGraph = new Dictionary<string, List<string>>();
            _visited = new Dictionary<string, bool>();
            _marked = new Dictionary<string, bool>();
        }

        private static void OtherReadInput()
        {
            var line = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(line))
            {
                var tokens = line
                    .Split(new []{ '–' }, StringSplitOptions.RemoveEmptyEntries);

                if (!_otherGraph.ContainsKey(tokens[0]))
                {
                    _otherGraph[tokens[0]] = new List<string>();
                }

                if (!_otherGraph.ContainsKey(tokens[1]))
                {
                    _otherGraph[tokens[1]] = new List<string>();
                }

                if (!_visited.ContainsKey(tokens[0]))
                {
                    _visited.Add(tokens[0], false);
                    _marked.Add(tokens[0], false);
                }

                if (!_visited.ContainsKey(tokens[1]))
                {
                    _visited.Add(tokens[1], false);
                    _marked.Add(tokens[1], false);
                }

                _otherGraph[tokens[0]].Add(tokens[1]);
                _otherGraph[tokens[1]].Add(tokens[0]);

                line = Console.ReadLine();
            }
        }

        private static bool IsAcyclicOther()
        {
            foreach (var node in _otherGraph)
            {
                if (!_visited[node.Key])
                {
                    Dfs(node.Key, string.Empty);
                }
            }

            return !IsCyclic;
        }

        private static void Dfs(string current, string prev)
        {
            _visited[current] = true;
            _marked[current] = true;

            foreach (var adjacent in _otherGraph[current])
            {
                if (adjacent == prev)
                {
                    continue;
                }

                if (!_visited[adjacent])
                {
                    //Bitwise OR between current result and result returned from recursive call
                    Dfs(adjacent, current);
                }

                if (_marked[adjacent])
                {
                    IsCyclic = true;
                }
            }

            _marked[current] = false; //Backtracking
        }
    }
}
