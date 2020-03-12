namespace _02._Permutations_Swap_Algorithm
{
    using System;

    public static class PermutationsSwapAlgorithmProgram
    {
        private static int[] _elements;

        private static void Permute(int index)//index - current cell to fill
        {
            if (index >= _elements.Length)
            {
                Console.WriteLine(string.Join(" ", _elements));
            }
            else
            {
                Permute(index + 1);

                for (var i = index + 1; i < _elements.Length; i++)
                {
                    Swap(index, i);
                    Permute(index + 1);
                    Swap(index, i);
                }
            }
        }

        private static void Swap(int from, int to)
        {
            var swap = _elements[from];
            _elements[from] = _elements[to];
            _elements[to] = swap;
        }

        public static void Main()
        {
            _elements = new[] {1, 2, 3};
            Permute(0);
        }
    }
}
