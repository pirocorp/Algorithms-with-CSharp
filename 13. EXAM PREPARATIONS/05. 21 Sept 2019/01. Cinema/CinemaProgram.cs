namespace _01._Cinema
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CinemaProgram
    {
        private static HashSet<string> _names;
        private static Dictionary<int, string> _positionName;

        private static string[] _currentPermutationResult;
        private static List<List<string>> _allPermutationsOfNames;

        private static string[] _permutation;
        private static string[] _elements;
        private static bool[] _used;

        private static void ReadInput()
        {
            var names = Console.ReadLine()
                .Split(", ")
                .ToArray();

            _names = new HashSet<string>(names);
            _positionName = new Dictionary<int, string>();

            var inputLine = Console.ReadLine();

            while (inputLine != "generate")
            {
                var positionNameArgs = inputLine
                    .Split(" - ");

                var name = positionNameArgs[0];
                var position = int.Parse(positionNameArgs[1]);

                _positionName[position] = name;

                inputLine = Console.ReadLine();
            }
        }

        private static void PreprocessingResult()
        {
            _currentPermutationResult = new string[_names.Count];

            foreach (var position in _positionName.Keys)
            {
                var name = _positionName[position];

                _currentPermutationResult[position - 1] = name;
                _names.Remove(name);
            }
        }

        private static void Permute(int currentCellIndex)
        {
            if (currentCellIndex == _permutation.Length)
            {
                _allPermutationsOfNames.Add(_permutation.ToList());
            }
            else
            {
                for (var i = 0; i < _elements.Length; i++)
                {
                    if (!_used[i])
                    {
                        var currentElement = _elements[i];
                        _used[i] = true;

                        _permutation[currentCellIndex] = currentElement;
                        Permute(currentCellIndex + 1); 

                        _used[i] = false;
                    }
                }
            }
        }

        private static void GenerateAllPermutations()
        {
            _allPermutationsOfNames = new List<List<string>>();
            _permutation = new string[_names.Count];
            _elements = _names.ToArray();
            _used = new bool[_names.Count];

            Permute(0);
        }

        public static void Main()
        {
            ReadInput();

            PreprocessingResult();

            GenerateAllPermutations();

            var results = new List<string>();

            foreach (var currentPerm in _allPermutationsOfNames)
            {
                var count = 0;

                for (var i = 0; i < _currentPermutationResult.Length; i++)
                {
                    if (!_positionName.ContainsKey(i + 1))
                    {
                        _currentPermutationResult[i] = currentPerm[count++];
                    }
                }

                results.Add(string.Join(" ", _currentPermutationResult));
            }

            Console.WriteLine(string.Join(Environment.NewLine, results));
        }
    }
}
