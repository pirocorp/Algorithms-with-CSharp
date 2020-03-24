namespace _01._Connecting_Cables
{
    using System;
    using System.Linq;

    public static class ConnectingCablesProgram
    {
        private static int[,] _lcs;

        private static int[] _numbers;
        private static int[] _range;
        private static int[,] _maxConnected;

        public static void Main()
        {
            _numbers = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            _range = Enumerable.Range(1, _numbers.Length).ToArray();
            InitializeMaxConnected();

            CalculateLcs(_range, _numbers);
            var maxPairs = _lcs[_range.Length, _numbers.Length];
            Console.WriteLine($"Maximum pairs connected: {maxPairs}");
            Console.WriteLine($"Maximum pairs connected: {GetMaxConnectedMemoization(_numbers.Length, _range.Length)}");
        }

        private static void InitializeMaxConnected()
        {
            _maxConnected = new int[_numbers.Length + 1, _range.Length + 1];

            for (var i = 1; i <= _numbers.Length; i++)
            {
                for (var j = 1; j <= _range.Length; j++)
                {
                    _maxConnected[i, j] = -1;
                }
            }
        }

        private static int GetMaxConnectedMemoization(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                return 0;
            }

            if (_maxConnected[x, y] != -1)
            {
                return _maxConnected[x, y];
            }

            if (_numbers[x - 1] == _range[y - 1])
            {
                _maxConnected[x, y] = GetMaxConnectedMemoization(x - 1, y - 1) + 1;
            }
            else
            {
                var top = GetMaxConnectedMemoization(x - 1, y);
                var left = GetMaxConnectedMemoization(x, y - 1);

                _maxConnected[x, y] = Math.Max(top, left);
            }

            return _maxConnected[x, y];
        }

        private static void CalculateLcs(int[] first, int[] second)
        {
            _lcs = new int[first.Length + 1 , second.Length + 1];

            for (var row = 1; row <= first.Length; row++)
            {
                for (var col = 1; col <= second.Length; col++)
                {
                    var up = _lcs[row - 1, col];
                    var left = _lcs[row, col - 1];

                    var result = Math.Max(up, left);

                    if (first[row - 1] == second[col - 1])
                    {
                        var diagonal = _lcs[row - 1, col - 1] + 1;
                        result = Math.Max(diagonal, result);
                    }

                    _lcs[row, col] = result;
                }
            }
        }
    }
}
