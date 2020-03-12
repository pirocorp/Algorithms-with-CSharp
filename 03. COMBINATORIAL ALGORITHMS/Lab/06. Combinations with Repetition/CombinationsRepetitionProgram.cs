namespace _06._Combinations_with_Repetition
{
    using System;
    using System.Linq;

    public static class CombinationsRepetitionProgram
    {
        private static char[] _elements;
        private static char[] _currentCombination;

        public static void Main()
        {
            _elements = Console.ReadLine()
                .Split()
                .Select(Char.Parse)
                .ToArray();

            var n = int.Parse(Console.ReadLine());

            _currentCombination = new char[n];
            Combination(0, 0);
        }

        private static void Combination(int index, int start)
        {
            if (index == _currentCombination.Length)
            {
                Console.WriteLine(string.Join(" ", _currentCombination));
            }
            else
            {
                for (var i = start; i < _elements.Length; i++)
                {
                    _currentCombination[index] = _elements[i];
                    Combination(index + 1, i);
                }
            }
        }
    }
}
