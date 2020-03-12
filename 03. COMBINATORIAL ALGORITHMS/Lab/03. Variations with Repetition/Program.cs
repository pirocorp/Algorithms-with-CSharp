namespace _03._Variations_with_Repetition
{
    using System;
    using System.Linq;

    public static class Program
    {
        private static char[] _elements;
        private static char[] _currentVariation;

        public static void Main()
        {
            _elements = Console.ReadLine()
                .Split()
                .Select(Char.Parse)
                .ToArray();

            var n = int.Parse(Console.ReadLine());

            _currentVariation = new char[n];
            Gen(0);
        }

        public static void Gen(int index)
        {
            if (index >= _currentVariation.Length)
                Console.WriteLine(string.Join(" ", _currentVariation));
            else
            {
                for (int i = 0; i < _elements.Length; i++)
                {
                    _currentVariation[index] = _elements[i];
                    Gen(index + 1);
                }
            }

        }
    }
}
