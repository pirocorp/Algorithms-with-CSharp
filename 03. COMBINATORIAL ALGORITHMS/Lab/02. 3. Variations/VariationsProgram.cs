namespace _02._Variations
{
    using System;
    using System.Linq;

    public static class VariationsProgram
    {
        private static char[] _elements;
        private static char[] _currentVariation;
        private static bool[] _used;

        static void Main(string[] args)
        {
            _elements = Console.ReadLine()
                .Split()
                .Select(Char.Parse)
                .ToArray();

            var n = int.Parse(Console.ReadLine());

            _currentVariation = new char[n];
            _used = new bool[_elements.Length];

            Variate(0);
        }

        private static void Variate(int index)
        {
            if (index >= _currentVariation.Length)
            {
                Console.WriteLine(string.Join(" ", _currentVariation));
            }
            else
            {
                for (var i = 0; i < _elements.Length; i++)
                {
                    if (!_used[i])
                    {
                        _used[i] = true;
                        _currentVariation[index] = _elements[i];
                        Variate(index + 1);
                        _used[i] = false;
                    }
                }
            }
        }
    }
}
