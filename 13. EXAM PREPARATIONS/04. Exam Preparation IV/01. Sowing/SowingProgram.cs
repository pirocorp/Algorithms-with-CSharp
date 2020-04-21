namespace _01._Sowing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SowingProgram
    {
        private static int _n;
        private static char[] _numbers;

        private static List<string> _results;

        private static void Generate(int index, int count, char[] result)
        {
            if (index == _numbers.Length || count == _n)
            {
                if (count == _n)
                {
                    Console.WriteLine(new string(result));
                    //_results.Add(new string(result));
                }

                return;
            }

            for (var i = index; i < _numbers.Length; i++)
            {
                if (result[i] == '1'
                    && (i == 0 || (i - 1 >= 0 && result[i - 1] != '.')))
                {
                    var prev = result[i];
                    result[i] = '.';
                    count++;
                    Generate(i + 1, count, result);
                    result[i] = prev;
                    count--;
                }
            }
        }

        public static void Main()
        {
            _n = int.Parse(Console.ReadLine());

            _numbers = Console.ReadLine()
                .Split()
                .Select(x => x[0])
                .ToArray();

            _results = new List<string>();

            var result = new List<char>(_numbers).ToArray();

            Generate(0, 0, result);

            //Console.WriteLine(string.Join(Environment.NewLine, _results));
        }
    }
}
