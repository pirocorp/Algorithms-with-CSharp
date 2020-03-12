using System;

namespace _06._Variations_With_Repetitions
{
    public static class VariationsWithRepetitionsProgram
    {
        private static int[] _elements;
        private static int[] _variation;

        public static void Main()
        {
            var variationSize = 2;
            _elements = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            _variation = new int[variationSize];

            Variations(0);
        }

        private static void Variations(int index)
        {
            if (index == _variation.Length)
            {
                Console.WriteLine(string.Join(" ", _variation));
            }
            else
            {
                for (var i = 0; i < _elements.Length; i++)
                {
                    _variation[index] = _elements[i];
                    Variations(index + 1);
                }
            }
        }
    }
}
