namespace _03._Evacuation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EvacuationProgram
    {
        private static List<Edge>[] _graph;
        private static int[] _exitNodes;
        private static TimeSpan _targetTime;

        private static TimeSpan[][] _timesToExit;
        private static bool[] _visited;
        private static PriorityQueue<int> _priorityQueue;

        private static readonly TimeSpan _defaultTimeSpan = new TimeSpan(30, 23, 59, 59);

        private static void ReadInput()
        {
            var nodesCount = int.Parse(Console.ReadLine());
            _graph = new List<Edge>[nodesCount];

            for (var i = 0; i < nodesCount; i++)
            {
                _graph[i] = new List<Edge>();
            }

            _exitNodes = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var edgesCount = int.Parse(Console.ReadLine());

            for (var i = 0; i < edgesCount; i++)
            {
                var edgeArgs = Console.ReadLine()
                    .Split();

                var from = int.Parse(edgeArgs[0]);
                var to = int.Parse(edgeArgs[1]);

                var timeArgs = edgeArgs[2]
                    .Split(':')
                    .Select(int.Parse)
                    .ToArray();

                var minutes = timeArgs[0];
                var seconds = timeArgs[1];

                var timeSpan = new TimeSpan(0, minutes, seconds);
                var edge = new Edge(from, to, timeSpan);
                var reverseEdge = new Edge(to, from, timeSpan);

                _graph[from].Add(edge);
                _graph[to].Add(reverseEdge);
            }

            var targetTimeArgs = Console.ReadLine()
                .Split(":")
                .Select(int.Parse)
                .ToArray();

            var targetMinutes = targetTimeArgs[0];
            var targetSeconds = targetTimeArgs[1];

            _targetTime = new TimeSpan(0, targetMinutes, targetSeconds);

            _timesToExit = new TimeSpan[_exitNodes.Length][];

            for (var exitIndex = 0; exitIndex < _timesToExit.Length; exitIndex++)
            {
                _timesToExit[exitIndex] = new TimeSpan[nodesCount];

                for (var nodeIndex = 0; nodeIndex < _timesToExit[exitIndex].Length; nodeIndex++)
                {
                    _timesToExit[exitIndex][nodeIndex] = _defaultTimeSpan;
                }
            }
        }

        private static void VisitVertex(int vertex, TimeSpan[] timesToExit)
        {
            _visited[vertex] = true;

            foreach (var edge in _graph[vertex])
            {
                if (timesToExit[edge.To].CompareTo(_defaultTimeSpan) == 0)
                {
                    _priorityQueue.Enqueue(edge.To);
                }

                var timeSpan = timesToExit[vertex] + edge.Weight;

                if (timeSpan < timesToExit[edge.To])
                {
                    timesToExit[edge.To] = timeSpan;
                    _priorityQueue.DecreaseKey(edge.To);
                }
            }
        }

        private static void Dijkstra(int exitNode, TimeSpan[] timesToExit)
        {
            _visited = new bool[_graph.Length];
            _priorityQueue = new PriorityQueue<int>(Comparer<int>
                .Create((f, s) => timesToExit[f].CompareTo(timesToExit[s])));

            timesToExit[exitNode] = new TimeSpan(0,0,0);
            _priorityQueue.Enqueue(exitNode);

            while (_priorityQueue.Count > 0)
            {
                var vertex = _priorityQueue.ExtractMin();

                VisitVertex(vertex, timesToExit);
            }
        }

        private static TimeSpan[] GetBestTimesToAnyExit()
        {
            for (var nodeIndex = 0; nodeIndex < _exitNodes.Length; nodeIndex++)
            {
                Dijkstra(_exitNodes[nodeIndex], _timesToExit[nodeIndex]);
            }

            var bestTimesToAnyExit = new TimeSpan[_graph.Length];

            for (var nodeIndex = 0; nodeIndex < bestTimesToAnyExit.Length; nodeIndex++)
            {
                var min = _defaultTimeSpan;

                for (var exitIndex = 0; exitIndex < _timesToExit.Length; exitIndex++)
                {
                    var current = _timesToExit[exitIndex][nodeIndex];

                    if (current.CompareTo(min) < 0)
                    {
                        min = current;
                    }
                }

                bestTimesToAnyExit[nodeIndex] = min;
            }

            return bestTimesToAnyExit;
        }

        public static void Main()
        {
            ReadInput();

            var bestTimesToAnyExit = GetBestTimesToAnyExit();

            var isSafe = true;
            var result = new List<KeyValuePair<int, TimeSpan>>();
            var max = new KeyValuePair<int, TimeSpan>(-1, new TimeSpan(0, 0, 0));

            for (var index = 0; index < bestTimesToAnyExit.Length; index++)
            {
                var timeSpan = bestTimesToAnyExit[index];

                if (timeSpan.CompareTo(_targetTime) > 0)
                {
                    isSafe = false;
                    result.Add(new KeyValuePair<int, TimeSpan>(index, timeSpan));
                }

                if (max.Value.CompareTo(timeSpan) < 0)
                {
                    max = new KeyValuePair<int, TimeSpan>(index, timeSpan);
                }
            }

            if (isSafe)
            {
                Console.WriteLine("Safe");
                Console.WriteLine($"{max.Key} ({max.Value})");
            }
            else
            {
                Console.WriteLine("Unsafe");
                Console.WriteLine(string.Join(", ", result.Select(x => $"{x.Key} ({(x.Value == _defaultTimeSpan ? "unreachable" : x.Value.ToString())})")));
            }
        }
    }
}
