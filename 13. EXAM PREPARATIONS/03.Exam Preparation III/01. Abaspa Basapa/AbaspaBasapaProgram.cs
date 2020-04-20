namespace _01._Abaspa_Basapa
{
    using System;
    using System.Collections.Generic;

    public static class AbaspaBasapaProgram
    {
        private static int[,] _lcs;

        public static void Main()
        {
            var firstString = Console.ReadLine();
            var secondString = Console.ReadLine();

            _lcs = new int[firstString.Length, secondString.Length];

            var max = 0;
            var maxRow = 0;
            var maxCol = 0;

            for (var row = 0; row < _lcs.GetLength(0); row++)
            {
                for (var col = 0; col < _lcs.GetLength(1); col++)
                {
                    if (firstString[col] == secondString[row])
                    {
                        var value = 1;

                        if (row - 1 >= 0 && col - 1 >= 0)
                        {
                            value = _lcs[row - 1, col - 1] + 1;
                        }

                        _lcs[row, col] = value;

                        if (_lcs[row, col] > max)
                        {
                            max = value;
                            maxRow = row;
                            maxCol = col;
                        }
                    }
                    else
                    {
                        _lcs[row, col] = 0;
                    }
                }
            }

            var result = new Stack<char>();

            while (maxRow >= 0 && maxCol >= 0 && max != 0)
            {
                result.Push(firstString[maxCol]);
                maxRow--;
                maxCol--;
                max--;
            }

            Console.WriteLine(new string(result.ToArray()));
        }
    }
}
