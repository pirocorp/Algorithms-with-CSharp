namespace _09._Combinations_With_Repetition
{
    using System;

    public static class CombinationsWithRepetitionProgram
    {
        private static int[] _elements;
        private static int[] _combination;

        public static void Main()
        {
            var combinationSize = 2;
            _elements = new[] { 1, 2, 3 };
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
                    //The only difference between with or without repetition i or i+1
                    Combination(index + 1, i); 
                }
            }
        }

        private static void Print()
        {
            Console.WriteLine(string.Join(" ", _combination));
        }
    }
}
