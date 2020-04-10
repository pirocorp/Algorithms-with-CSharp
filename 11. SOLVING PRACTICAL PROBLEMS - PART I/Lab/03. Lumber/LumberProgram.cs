namespace _03._Lumber
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class LumberProgram
    {
        private static List<Log> _logs;
        private static int _queriesCount;

        private static List<int>[] _graph;
        private static bool[] _visited;
        private static int[] _id;
        private static int _count = 0;

        private static void ReadInput()
        {
            var inputData = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var logsCount = inputData[0];
            _queriesCount = inputData[1];

            _logs = new List<Log>();
            _graph = new List<int>[logsCount + 1];
            _visited = new bool[logsCount + 1];
            _id = new int[logsCount + 1];

            for (var i = 1; i <= logsCount; i++)
            {
                var coordinates = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var newLog = new Log(i, coordinates);

                _graph[i] = new List<int>();

                foreach (var element in _logs)
                {
                    if (element.Intersect(newLog))
                    {
                        _graph[element.Id].Add(i);
                        _graph[i].Add(element.Id);
                    }
                }

                _logs.Add(newLog);
            }
        }

        private static bool CheckForPath(Log from, Log to)
        {
            var result = false;
            var visited = new HashSet<Log>();

            var queue = new Queue<Log>();
            queue.Enqueue(from);
            visited.Add(from);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current == to)
                {
                    return true;
                }

                foreach (var rectAngle in _logs)
                {
                    if (!visited.Contains(rectAngle) && rectAngle.IntersectsWith(current))
                    {
                        queue.Enqueue(rectAngle);
                        visited.Add(rectAngle);
                    }
                }
            }

            return result;
        }

        private static void SlowSolution()
        {
            for (var i = 0; i < _queriesCount; i++)
            {
                var inputData = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var from = _logs[inputData[0] - 1];
                var to = _logs[inputData[1] - 1];

                if (CheckForPath(@from, to))
                {
                    Console.WriteLine("YES");
                }
                else
                {
                    Console.WriteLine("NO");
                }
            }
        }

        private static void Dfs(int vertex)
        {
            _visited[vertex] = true;
            _id[vertex] = _count;

            foreach (var child in _graph[vertex])
            {
                if (!_visited[child])
                {
                    Dfs(child);
                }
            }
        }

        private static void MapConnectedComponents()
        {
            for (var vertex = 1; vertex < _graph.Length; vertex++)
            {
                if (!_visited[vertex])
                {
                    Dfs(vertex);
                    _count++;
                }
            }
        }

        private static void ProcessQueries()
        {
            for (var i = 0; i < _queriesCount; i++)
            {
                var args = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var from = args[0];
                var to = args[1];

                if (_id[from] == _id[to])
                {
                    Console.WriteLine("YES");
                }
                else
                {
                    Console.WriteLine("NO");
                }
            }
        }

        public static void Main()
        {
            ReadInput();
            //SlowSolution();

            MapConnectedComponents();
            ProcessQueries();
        }
    }
}
