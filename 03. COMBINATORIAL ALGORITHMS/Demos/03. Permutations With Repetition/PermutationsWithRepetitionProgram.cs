namespace _03._Permutations_With_Repetition
{
    using System;
    using System.Collections.Generic;

    public static class PermutationsWithRepetitionProgram
    {
        private static int[] _elements;

        public static void Main()
        {
            _elements = new[] { 1, 2, 2 };
            Permute(0);
        }

        private static void Permute(int index)
        {
            if (index == _elements.Length)
            {
                Console.WriteLine(string.Join(" ", _elements));
            }
            else
            {
                var swapped = new HashSet<int>();

                for (var i = index; i < _elements.Length; i++)
                {
                    if (!swapped.Contains(_elements[i]))
                    {
                        Swap(index, i);
                        Permute(index + 1);
                        Swap(index, i);
                        swapped.Add(_elements[i]);
                    }
                }
            }
        }

        private static void Swap(int from, int to)
        {
            var swap = _elements[from];
            _elements[from] = _elements[to];
            _elements[to] = swap;
        }
    }
}
