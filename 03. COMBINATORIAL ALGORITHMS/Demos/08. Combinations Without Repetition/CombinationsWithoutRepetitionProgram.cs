namespace _08._Combinations_Without_Repetition
{
    using System;

    public static class CombinationsWithoutRepetitionProgram
    {
        private static int[] _elements;
        private static int[] _combination;

        public static void Main()
        {
            var combinationSize = 3;
            _elements = new[] { 1, 2, 3, 4, 5 };
            _combination = new int[combinationSize];
            Combination(0, 0);
        }

        //Combination Count = n! / k!(n-k)!
        private static void Combination(int index, int start)
        {
            if (index == _combination.Length)
            {
                Print();
            }
            else
            {
                for (var i = start; i < _elements.Length; i++)
                {
                    _combination[index] = _elements[i];
                    Combination(index + 1, i + 1);
                }
            }
        }

        private static void Print()
        {
            Console.WriteLine(string.Join(" ", _combination));
        }
    }
}
