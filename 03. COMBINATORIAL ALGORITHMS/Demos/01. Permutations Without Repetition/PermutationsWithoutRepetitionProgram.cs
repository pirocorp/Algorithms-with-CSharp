namespace _01._Permutations_Without_Repetition
{
    using System;

    public static class PermutationsWithoutRepetitionProgram
    {
        private static int[] _elements;
        private static bool[] _used;
        private static int[] _permutation;

        //Count of permutation is n!
        private static void Permute(int currentCellIndex)
        {
            if (currentCellIndex == _permutation.Length)
            {
                Console.WriteLine(string.Join(" ", _permutation));
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

        public static void Main()
        {
            _elements = new[] {1, 2, 3};
            _used = new bool[_elements.Length];
            _permutation = new int[_elements.Length];

            Permute(0);
        }
    }
}
