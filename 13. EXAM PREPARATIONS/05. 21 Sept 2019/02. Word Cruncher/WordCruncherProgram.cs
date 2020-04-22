namespace _02._Word_Cruncher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class WordCruncherProgram
    {
        private static string[] _elements;
        private static bool[] _visited;

        private static string _targetString;
        private static List<List<string>> _results;

        private static void Generate(string currentTarget, LinkedList<string> currentResult)
        {
            if (currentTarget.Length == 0)
            {
                _results.Add(currentResult.ToList());
                return;
            }

            for (var i = 0; i < _elements.Length; i++)
            {
                if (!_visited[i])
                {
                    _visited[i] = true;

                    if (currentTarget.StartsWith(_elements[i]))
                    {
                        currentResult.AddLast(_elements[i]);
                        Generate(currentTarget.Remove(0, _elements[i].Length), currentResult);
                        currentResult.RemoveLast();
                    }

                    _visited[i] = false;
                }
            }
        }

        public static void Main()
        {
            _elements = Console.ReadLine()
                .Split(", ")
                .OrderBy(x => x.Length)
                .ToArray();

            _visited = new bool[_elements.Length];

            _targetString = Console.ReadLine();

            _results = new List<List<string>>();

            Generate(_targetString, new LinkedList<string>());

            _results
                .Select(x => string.Join(" ", x))
                .Distinct()
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }
}
