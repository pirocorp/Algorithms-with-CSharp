namespace _03._Symbol_Multiplication
{
    using System;
    using System.Linq;

    public static class SymbolMultiplicationProgram
    {
        //Converted From C Code

        private const int MAX_LENGTH = 100;
        private static int _alphabetLength;
        private static sbyte[,] _multiplicationTable;
        private static string _inputString;

        private static byte[,,] _table;
        private static readonly byte[,] Split = new byte[MAX_LENGTH, MAX_LENGTH];

        public static void Main()
        {
            ReadInput();

            if (CalculateSolution(0, (byte)(_inputString.Length - 1), 0))
            {
                PutBrackets(0, (byte)(_inputString.Length - 1));
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No solution");
            }
        }

        private static void ReadInput()
        {
            var alphabet =
                Console.ReadLine()
                    .Split(new[] {'=', ' ', '{', '}', ','}, StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1)
                    .Select(Char.Parse)
                    .ToArray();

            _alphabetLength = alphabet.Length;
            _table = new byte[MAX_LENGTH, MAX_LENGTH, _alphabetLength];

            Console.ReadLine();

            _multiplicationTable = new sbyte[_alphabetLength, _alphabetLength];

            for (var i = 0; i < _alphabetLength; i++)
            {
                var row = Console.ReadLine().Trim();
                for (var j = 0; j < _alphabetLength; j++)
                {
                    _multiplicationTable[i, j] = (sbyte) row[j];
                }
            }

            _inputString = Console.ReadLine().Split(new[] {'=', ' '}, StringSplitOptions.RemoveEmptyEntries).ToArray()[1];
        }

        private static bool CalculateSolution(byte i, byte j, byte ch)
        {
            if (_table[i, j, ch] != 0)
            {
                return true;
            }

            if (i == j)
            {
                return _inputString[i] == ch + 'a';
            }

            for (byte c1 = 0; c1 < _alphabetLength; c1++)
            {
                for (byte c2 = 0; c2 < _alphabetLength; c2++)
                {
                    if (_multiplicationTable[c1, c2] != ch + 'a')
                    {
                        continue;
                    }

                    for (var pos = i; pos <= j - 1; pos++)
                    {
                        if (!CalculateSolution(i, pos, c1))
                        {
                            continue;
                        }

                        if (!CalculateSolution((byte) (pos + 1), j, c2))
                        {
                            continue;
                        }

                        _table[i, j, ch] = 1;
                        Split[i, j] = pos;
                        return true;
                    }
                }
            }

            _table[i, j, ch] = 0;
            return false;
        }

        private static void PutBrackets(byte i, byte j)
        {
            if (i == j)
            {
                Console.Write($"{_inputString[i]}");
            }
            else
            {
                Console.Write("(");
                PutBrackets(i, Split[i, j]);
                Console.Write("*");
                PutBrackets((byte)(Split[i, j] + 1), j);
                Console.Write(")");
            }
        }
    }
}
