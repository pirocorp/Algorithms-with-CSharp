namespace _03._Message_Sharing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MessageSharingProgram
    {
        private static Dictionary<string, List<string>> _graph;
        private static string[] _start;

        private static HashSet<string> _notVisited;
        private static Dictionary<int, List<string>> _steps;

        private static int _maxStep;

        private static void ReadInput()
        {
            _graph = new Dictionary<string, List<string>>();
            _notVisited = new HashSet<string>();
            _steps = new Dictionary<int, List<string>>();

            var people = Console.ReadLine()
                .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .ToArray();

            foreach (var person in people)
            {
                _graph[person] = new List<string>();
                _notVisited.Add(person);
            }

            var connections = Console.ReadLine()
                .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Where(x => x != "-")
                .Skip(1)
                .ToArray();

            for (var i = 0; i < connections.Length; i += 2)
            {
                var first = connections[i];
                var second = connections[i + 1];

                _graph[first].Add(second);
                _graph[second].Add(first);
            }

            _start = Console.ReadLine()
                .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .ToArray();
        }

        private static void VisitVertexes(List<string> vertexes, int step)
        {
            if (vertexes.Count == 0)
            {
                return;
            }

            var nextVertexes = new List<string>();

            foreach (var vertex in vertexes)
            {
                if (_notVisited.Contains(vertex))
                {
                    _notVisited.Remove(vertex);

                    if (!_steps.ContainsKey(step))
                    {
                        _steps[step] = new List<string>();
                    }

                    _steps[step].Add(vertex);
                    nextVertexes.AddRange(_graph[vertex]);
                }
            }

            if (nextVertexes.Count > 0)
            {
                _maxStep = step;
            }

            VisitVertexes(nextVertexes, step + 1);
        }

        public static void Main()
        {
            ReadInput();

            VisitVertexes(_start.ToList(), 0);

            if (_notVisited.Count > 0)
            {
                Console.WriteLine($"Cannot reach: {string.Join(", ", _notVisited.OrderBy(x => x))}");
            }
            else
            {
                Console.WriteLine($"All people reached in {_maxStep} steps");
                Console.WriteLine($"People at last step: {string.Join(", ", _steps[_maxStep].OrderBy(x => x))}");
            }
        }
    }
}
