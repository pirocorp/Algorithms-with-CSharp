namespace _04._Robbery
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class RobberyProgram
    {
        private static List<Edge>[] _graph;
        private static bool[] _colors;
        private static int _energy;
        private static int _weightCost;
        private static int _start;
        private static int _end;

        private static int[] _distanceTo;
        private static bool[] _visited;
        private static int[] _stepsTo;

        private static void ReadInput()
        {
            _colors = Console.ReadLine()
                .Split()
                .Select(x => x[x.Length - 1] == 'w')
                .ToArray();

            _graph = new List<Edge>[_colors.Length];

            for (var i = 0; i < _graph.Length; i++)
            {
                _graph[i] = new List<Edge>();
            }

            _energy = int.Parse(Console.ReadLine());
            _weightCost = int.Parse(Console.ReadLine());
            _start = int.Parse(Console.ReadLine());
            _end = int.Parse(Console.ReadLine());

            var edgesCount = int.Parse(Console.ReadLine());

            for (var i = 0; i < edgesCount; i++)
            {
                var edgeArgs = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var from = edgeArgs[0];
                var to = edgeArgs[1];
                var weight = edgeArgs[2];

                var edge = new Edge(@from, to, weight);
                _graph[@from].Add(edge);
            }
        }

        private static int GetCurrentVertex()
        {
            var vertex = -1;
            var lowestDistance = int.MaxValue;

            for (var index = 0; index < _distanceTo.Length; index++)
            {
                if (!_visited[index] && _distanceTo[index] < lowestDistance)
                {
                    vertex = index;
                    lowestDistance = _distanceTo[index];
                }
            }

            return vertex;
        }

        private static void VisitVertex(int vertex)
        {
            _visited[vertex] = true;

            foreach (var edge in _graph[vertex])
            {
                var steps = _stepsTo[vertex];
                var color = steps % 2 == 0 ? _colors[edge.To] : !_colors[edge.To];
                var weightCost = color ? 0 : _weightCost;
                var distance = _distanceTo[vertex] + edge.Weight + weightCost;

                if (distance < _distanceTo[edge.To])
                {
                    var additionalStep = color ? 0 : 1;
                    _distanceTo[edge.To] = distance;
                    _stepsTo[edge.To] = steps + 1 + additionalStep;
                }
            }
        }

        private static void Dijkstra()
        {
            _distanceTo = Enumerable.Repeat(int.MaxValue, _graph.Length).ToArray();
            _visited = new bool[_graph.Length];
            _stepsTo = new int[_graph.Length];
            
            _distanceTo[_start] = 0;

            var vertex = GetCurrentVertex();

            while (vertex >= 0)
            {
                if (vertex == _end)
                {
                    break;
                }

                VisitVertex(vertex);

                vertex = GetCurrentVertex();
            }
        }

        public static void Main()
        {
            ReadInput();
            Dijkstra();

            var result = _energy - _distanceTo[_end];

            if (result < 0)
            {
                Console.WriteLine($"Busted - need {-result} more energy");
            }
            else
            {
                Console.WriteLine(result);
            }
        }
    }
}