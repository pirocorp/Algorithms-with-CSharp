namespace _01._Distance_Between_Vertices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DistanceBetweenVerticesProgram
    {
        private static Dictionary<string, List<string>> _adjacencyList;
        private static List<Tuple<string, string>> _pairs;

        public static void Main()
        {
            ReadInput();

            foreach (var pair in _pairs)
            {
                var distances = CalculatePairDistance(pair);
                Console.WriteLine($"{{{pair.Item1}, {pair.Item2}}} -> {distances[pair.Item2]}");
            }
        }

        private static Dictionary<string, int> CalculatePairDistance(Tuple<string, string> pair)
        {
            var queue = new Queue<string>();
            var distances = InitializeDistances();

            queue.Enqueue(pair.Item1);
            distances[pair.Item1] = 0;

            var found = false;

            while (queue.Count > 0 && !found)
            {
                var current = queue.Dequeue();

                foreach (var adjacent in _adjacencyList[current])
                {
                    if (distances[adjacent] == -1)
                    {
                        distances[adjacent] = distances[current] + 1;

                        if (adjacent == pair.Item2)
                        {
                            found = true;
                            break;
                        }

                        queue.Enqueue(adjacent);
                    }
                }
            }

            return distances;
        }

        private static Dictionary<string, int> InitializeDistances()
        {
            var distances = new Dictionary<string, int>();

            foreach (var element in _adjacencyList)
            {
                distances.Add(element.Key, -1);
            }

            return distances;
        }

        private static void ReadInput()
        {
            _adjacencyList = new Dictionary<string, List<string>>();
            _pairs = new List<Tuple<string, string>>();

            var verticesCount = int.Parse(Console.ReadLine());
            var pairsCount = int.Parse(Console.ReadLine());

            for (var i = 0; i < verticesCount; i++)
            {
                var input = Console.ReadLine()
                    .Split(new[] {' ', ':'}, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                _adjacencyList.Add(input[0], input.Skip(1).ToList());
            }

            for (var i = 0; i < pairsCount; i++)
            {
                var input = Console.ReadLine()
                    .Split(new[] {'-'}, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var pair = new Tuple<string, string>(input[0], input[1]);
                _pairs.Add(pair);
            }
        }
    }
}
