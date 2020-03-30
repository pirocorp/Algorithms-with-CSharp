namespace _04._Salaries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SalariesProgram
    {
        private static List<int>[] _graph;
        private static List<int> _rootNodes;
        private static Dictionary<int, int> _salaries;
        private static HashSet<int> _visited;

        public static void Main()
        {
            FirstSolution();
        }

        private static void FirstSolution()
        {
            ReadInput();
            GetRootNodes();
            CalculateSalaries();
            Console.WriteLine(_salaries.Sum(x => x.Value));
        }

        private static void ReadInput()
        {
            var n = int.Parse(Console.ReadLine());
            _graph = new List<int>[n];

            for (var row = 0; row < n; row++)
            {
                var input = Console.ReadLine();
                _graph[row] = new List<int>();

                for (var col = 0; col < input.Length; col++)
                {
                    if (input[col] == 'Y')
                    {
                        _graph[row].Add(col);
                    }
                }
            }
        }

        private static void GetRootNodes()
        {
            var set = new HashSet<int>();

            for (var i = 0; i < _graph.Length; i++)
            {
                set.Add(i);
            }

            var remove = _graph
                .SelectMany(x => x)
                .ToList();

            for (var i = 0; i < _graph.Length; i++)
            {
                if ( _graph[i].Contains(i))
                {
                    remove.Remove(i);
                }
            }

            set.ExceptWith(remove);

            _rootNodes = set.ToList();
        }

        private static void CalculateSalaries()
        {
            _salaries = new Dictionary<int, int>();
            _visited = new HashSet<int>();

            foreach (var rootNode in _rootNodes)
            {
                _salaries[rootNode] = Dfs(rootNode);
            }
        }

        private static int Dfs(int node)
        {
            if (_graph[node].Count == 0)
            {
                _salaries[node] = 1;
                return _salaries[node];
            }

            if (_visited.Contains(node))
            {
                _salaries[node] = 1;
                return _salaries[node];
            }

            _visited.Add(node);
            var sum = 0;

            foreach (var child in _graph[node])
            {
                if (node == child)
                {
                    continue;
                }

                if (_salaries.ContainsKey(child))
                {
                    sum += _salaries[child];
                    continue;
                }
                
                sum += Dfs(child);
                
            }

            _salaries[node] = sum;
            return _salaries[node];
        }
    }
}
