namespace _05._Break_Cycles
{ 
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public static class BreakCycles
    {
        private static readonly SortedDictionary<string, List<string>> _graph = new SortedDictionary<string, List<string>>();
        private static readonly OrderedBag<Tuple<string, string>> _edges = new OrderedBag<Tuple<string, string>>();
        private static HashSet<string> _visited = new HashSet<string>();
        private static bool _stopRecursion = false;

        private static void ReadInput()
        {
            var line = Console.ReadLine();

            while (!string.IsNullOrEmpty(line))
            {
                var vertex =
                    line.Split(new char[] { ' ', ',', '-', '>' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                _graph.Add(vertex[0], new List<string>(vertex.Skip(1)));

                for (var i = 1; i < vertex.Length; i++)
                {
                    _edges.Add(new Tuple<string, string>(vertex[0], vertex[i]));
                }

                line = Console.ReadLine();
            }
        }

        private static bool CheckForCycle(string from, string to, string parent)
        {
            if (_visited.Contains(from))
            {
                return false;
            }

            if (from == to)
            {
                _stopRecursion = true;
                return _stopRecursion;
            }

            _visited.Add(from);
            foreach (var child in _graph[from])
            {
                if (child == parent)
                {
                    continue;
                }

                CheckForCycle(child, to, from);
                if (_stopRecursion)
                {
                    return true;
                }
            }

            return false;
        }

        private static List<Tuple<string, string>> BreakCyclesAlgorithm()
        {
            var result = new List<Tuple<string, string>>();
            foreach (var edge in _edges)
            {
                _visited.Clear();
                _stopRecursion = false;
                var parent = edge.Item1;
                var child = edge.Item2;

                _graph[parent].Remove(child);
                _graph[child].Remove(parent);

                var needToRemove = CheckForCycle(parent, child, null);

                if (needToRemove)
                {
                    if (!result.Contains(new Tuple<string, string>(child, parent)))
                    {
                        result.Add(edge);
                    }
                }
                else
                {
                    _graph[parent].Add(child);
                    _graph[child].Add(parent);
                }
            }

            return result;
        }

        private static void FirstSolution()
        {
            ReadInput();

            var result = BreakCyclesAlgorithm();

            Console.WriteLine("Edges to remove: " + result.Count);
            foreach (var tuple in result)
            {
                Console.WriteLine($"{tuple.Item1} - {tuple.Item2}");
            }
        }

        private static bool HasPath(string start, string end)
        {
            //BFS Graph Traversal
            var queue = new Queue<string>();
            _visited = new HashSet<string>();
            
            queue.Enqueue(start);
            _visited.Add(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                foreach (var child in _graph[node])
                {
                    if (!_visited.Contains(child))
                    {
                        _visited.Add(child);
                        queue.Enqueue(child);

                        if (child == end)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static List<string> RemoveCycles()
        {
            var result = new List<string>();

            foreach (var nodeKvp in _graph)
            {
                var start = nodeKvp.Key;

                foreach (var end in _graph[start].OrderBy(e => e))
                {
                    _graph[start].Remove(end);
                    _graph[end].Remove(start);

                    if (HasPath(start, end))
                    {
                        result.Add($"{start} - {end}");
                    }
                    else
                    {
                        _graph[start].Add(end);
                        _graph[end].Add(start);
                    }
                }
            }

            return result;
        }

        private static void SecondSolution()
        {
            ReadInput();
            var result = RemoveCycles();

            Console.WriteLine($"Edges to remove: {result.Count}");
            result.ForEach(Console.WriteLine);
        }

        public static void Main()
        {
            //FirstSolution();
            SecondSolution();
        }
    }
}
