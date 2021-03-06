﻿namespace _02._Fast_and_Furious
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public static class FastAndFuriousProgram
    {
        private static List<string> _nodes;
        private static List<Edge> _graph;
        private static Dictionary<string, List<Edge>> _nodesToEdges;
        private static Dictionary<string, Dictionary<string, decimal>> _timeDistances;
        private static Dictionary<string, SortedSet<Record>> _records;

        private static void ReadInputRoads()
        {
            _nodes = new List<string>();
            _graph = new List<Edge>();
            _nodesToEdges = new Dictionary<string, List<Edge>>();
            _timeDistances = new Dictionary<string, Dictionary<string, decimal>>();

            var inputRoad = Console.ReadLine();
            inputRoad = Console.ReadLine();

            while (inputRoad != "Records:")
            {
                var tokenArgs = inputRoad
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var firstTown = tokenArgs[0];
                var secondTown = tokenArgs[1];
                var distance = decimal.Parse(tokenArgs[2], CultureInfo.InvariantCulture);
                var speedLimit = decimal.Parse(tokenArgs[3], CultureInfo.InvariantCulture);
                var timeWeight = distance / speedLimit;

                var newEdge = new Edge(firstTown, secondTown, timeWeight);

                _graph.Add(newEdge);

                if (!_nodesToEdges.ContainsKey(newEdge.First))
                {
                    _nodesToEdges[newEdge.First] = new List<Edge>();
                    _nodes.Add(newEdge.First);
                }

                if (!_nodesToEdges.ContainsKey(newEdge.Second))
                {
                    _nodesToEdges[newEdge.Second] = new List<Edge>();
                    _nodes.Add(newEdge.Second);
                }

                _nodesToEdges[newEdge.First].Add(newEdge);
                _nodesToEdges[newEdge.Second].Add(newEdge);

                inputRoad = Console.ReadLine();
            }
        }

        private static void ReadInputRecords()
        {
            _records = new Dictionary<string, SortedSet<Record>>();

            var inputString = Console.ReadLine();

            while (inputString != "End")
            {
                var tokensArgs = inputString
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var town = tokensArgs[0];
                var plate = tokensArgs[1];
                var timeString = tokensArgs[2];

                var timeTokens = timeString
                    .Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var hours = timeTokens[0];
                var minutes = timeTokens[1];
                var seconds = timeTokens[2];

                var newTime = new Time(hours, minutes, seconds);
                var record = new Record(town, plate, newTime);

                if (!_records.ContainsKey(plate))
                {
                    _records[plate] = new SortedSet<Record>();
                }

                _records[plate].Add(record);

                inputString = Console.ReadLine();
            }
        }

        private static void Dijkstra(string startNode)
        {
            var distances = new Dictionary<string, decimal>();

            for (var i = 0; i < _nodes.Count; i++)
            {
                distances[_nodes[i]] = decimal.MaxValue;
            }

            distances[startNode] = 0;

            var priorityQueue = new SortedSet<string>(
                Comparer<string>.Create((f, s) => distances[f].CompareTo(distances[s])));

            priorityQueue.Add(startNode);

            while (priorityQueue.Count != 0)
            {
                var min = priorityQueue.Min;
                priorityQueue.Remove(min);

                foreach (var edge in _nodesToEdges[min])
                {
                    var otherNode = edge.First == min
                        ? edge.Second
                        : edge.First;

                    if (distances[otherNode] == decimal.MaxValue)
                    {
                        priorityQueue.Add(otherNode);
                    }

                    var newDistance = distances[min] + edge.Weight;

                    if (newDistance < distances[otherNode])
                    {
                        distances[otherNode] = newDistance;

                        priorityQueue = new SortedSet<string>(priorityQueue,
                            Comparer<string>.Create((f, s) => distances[f].CompareTo(distances[s])));
                    }
                }
            }

            _timeDistances[startNode] = distances;
        }

        private static void FindTimeDistances()
        {
            foreach (var node in _nodes)
            {
                Dijkstra(node);
            }
        }

        private static List<string> FindSpeedingPlates()
        {
            var result = new List<string>();

            var currentRecords = new Dictionary<string, Record>();

            foreach (var record in _records)
            {
                var plate = record.Key;
                var cameras = record.Value.ToArray();

                for (var origin = 0; origin < cameras.Length; origin++)
                {
                    var originRecord = cameras[origin];
                    var isFound = false;

                    for (var destination = origin + 1; destination < cameras.Length; destination++)
                    {
                        var destinationRecord = cameras[destination];

                        if (!_timeDistances[originRecord.Town].ContainsKey(destinationRecord.Town))
                        {
                            continue;
                        }

                        var minAllowedTime = _timeDistances[originRecord.Town][destinationRecord.Town];
                        var actualTime = originRecord.Time.GetHoursInterval(destinationRecord.Time);

                        if (minAllowedTime == decimal.MaxValue)
                        {
                            continue;
                        }

                        if (actualTime < minAllowedTime)
                        {
                            result.Add(originRecord.Plate);
                            isFound = true;
                            break;
                        }
                    }

                    if (isFound)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        public static void Main()
        {
            ReadInputRoads();
            ReadInputRecords();

            FindTimeDistances();

            var result = FindSpeedingPlates();

            result = result.ToList();
            Console.WriteLine(string.Join(Environment.NewLine, result.OrderBy(x => x)));
        }
    }
}
