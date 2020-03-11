namespace _05._Words
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class WordsProgram
    {
        private static int _count;
        private static char[] _symbols;

        public static void Main()
        {
            _symbols = Console.ReadLine()?.ToCharArray();

            #region Hack

            if (_symbols != null && 
                _symbols.Length == _symbols.Distinct().Count())
            {
                var result = 1;

                for (var i = 2; i <= _symbols.Length; i++)
                {
                    result *= i;
                }

                Console.WriteLine(result);
                return;
            }

            #endregion

            GeneratePermutations(0);

            Console.WriteLine(_count);
        }

        private static void GeneratePermutations(int index)
        {
            if (index == _symbols.Length)
            {
                var isValid = true;

                for (var i = 0; i < _symbols.Length - 1; i++)
                {
                    var current = _symbols[i];
                    var next = _symbols[i + 1];

                    if (current == next)
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                {
                    _count++;
                }

                return;
            }

            var swapped = new HashSet<char>();

            for (var i = index; i < _symbols.Length; i++)
            {
                if (!swapped.Contains(_symbols[i]))
                {
                    Swap(index, i);

                    GeneratePermutations(index + 1);

                    swapped.Add(_symbols[index]);
                    Swap(index, i);
                }
            }
        }

        private static void Swap(int from, int to)
        {
            var temp = _symbols[from];
            _symbols[from] = _symbols[to];
            _symbols[to] = temp;
        }
    }
}
