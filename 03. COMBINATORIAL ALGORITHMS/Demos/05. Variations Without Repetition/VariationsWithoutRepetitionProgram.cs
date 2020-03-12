namespace _05._Variations_Without_Repetition
{
    using System;

    public static class VariationsWithoutRepetitionProgram
    {
        private static int[] _elements;
        private static bool[] _used;
        private static int[] _variation;

        //Count of variations is n! / (n-k)!
        private static void Variations(int currentCellIndex)
        {
            if (currentCellIndex == _variation.Length)
            {
                Console.WriteLine(string.Join(" ", _variation));
            }
            else
            {
                for (var i = 0; i < _elements.Length; i++)
                {
                    if (!_used[i])
                    {
                        var currentElement = _elements[i];
                        _used[i] = true;

                        _variation[currentCellIndex] = currentElement;
                        Variations(currentCellIndex + 1);

                        _used[i] = false;
                    }
                }
            }
        }

        public static void Main()
        {
            var variationSize = 2;
            _elements = new[] { 1, 2, 3, 4 };
            _used = new bool[_elements.Length];
            // The only difference from Permutation with no Repetition is here 
            _variation = new int[variationSize]; 

            Variations(0);
        }
    }
}
